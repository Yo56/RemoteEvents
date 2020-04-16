using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RemotingEvents.Common;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting;
using System.Collections;
using System.Runtime.Remoting.Channels;

namespace RemotingEvents.Server
{
    public class RemotingServer : MarshalByRefObject, IServerObject
    {

        #region Fields

        private TcpServerChannel serverChannel;
        private int tcpPort;
        private ObjRef internalRef;
        private Boolean serverActive = false;
        private static string serverURI = "serverExample.Rem";


        /////////////////////////////////      Data    /////////////////////////////////////////////
        
        Dictionary<String, User> registeredUsers;
        //Dictionary of Online users
        Dictionary<String, String> onlineUsers;

        //Dictionary of used ports in each address
        Dictionary<String, List<int>> usedPortsByAddress;


        ////////////////////////////////////////////////////////////////////////////////////////////
        #endregion

        #region IServerObject Members

        public event MessageArrivedEvent MessageArrived;

        public void PublishMessage(string Message)
        {
            SafeInvokeMessageArrived(Message);
        }

        ///////////////////////// METHODS  ////////////////////////////////////////////
        
        public string HelloWorld()
        {
            Console.WriteLine("[Client connected to the server]");
            return "Connection established to the server";
        }

        public RemotingServer()
        {
            Console.WriteLine("Initialization...");
            

            //Storage.DeleteAllUsers();   // line commented => users are loaded from file
                                          // line uncommented => users are deleted at server loading

            registeredUsers = Storage.LoadRegisteredUsersFromFile();

            onlineUsers = new Dictionary<String, String>();
            usedPortsByAddress = new Dictionary<string, List<int>>();

            StartServer(1234);

            Console.ReadKey(); //Avoid stop
        }

        //Method checking if there is already an existing account with this nickname
        public Boolean DoesThisUserExist(String nickname)
        {
            User existingUser = null;
            try
            {
                existingUser = registeredUsers[nickname];
                return true;
            }
            catch (KeyNotFoundException)
            {
                Console.WriteLine("User " + nickname + " not found.");
                return false;
            }
            return false;
        }

        //method that returns an user from a nickname
        public User GetUser(String nickname)
        {
            User user = null;
            try
            {
                user = registeredUsers[nickname];
                return user;
            }
            catch (KeyNotFoundException)
            {
                Console.WriteLine("User " + nickname + " not found. Can't get it");
                return null;
            }
            return null;
        }

        public string GetRealNameFromUser(String username)
        {
            User user = GetUser(username);
            if (user == null)
            {
                Console.WriteLine("Could not find real name of " + username);
                return null;
            }
            return user.Name;
        }

        // method which allows a connection or not on the client
        public Boolean Authentication(String nickname, String hashedPasswordInput, String ipAddress)
        {
            User user = null;
            try
            {
                user = registeredUsers[nickname];

                if (user.Password.Equals(hashedPasswordInput))
                {   // user is found and password is ok

                    // add the user to the dictionary of online users
                    addUserToOnlineUsers(user.Nickname, ipAddress);

                    return true;
                }
                else return false;
            }
            catch (KeyNotFoundException)
            {
                Console.WriteLine("User " + nickname + " not found. Can't authenticate it");
                return false;
            }
            return false;
        }

        //Add an account to registeredUsers
        //before calling the method, you need to check whether or not the nickname is already taken
        public void Registration(User user, String ipAddress)
        {
            try
            {
                registeredUsers.Add(user.Nickname, user);
            }
            catch (ArgumentException)
            {
                Console.WriteLine("A User with nickname = \"txt\" already exists. Will not be added again");
                return;
            }

            Console.WriteLine("New account created on client : " + user.Nickname + " " + user.Password);
            Storage.SaveRegisteredUsersToFile(registeredUsers);
            addUserToOnlineUsers(user.Nickname, ipAddress);
        }

        public void LogOut(string username)
        {
            User user = null;
            if (onlineUsers.Remove(username))
            {
                Console.WriteLine("user " + username + " disconnected!");
            }
            else
            {
                Console.WriteLine("Unable to logout user " + username);
            }

        }

        public void addUserToOnlineUsers(String username, String ipAddress)
        {
            try
            {
                onlineUsers.Add(username, ipAddress);
            }
            catch (ArgumentException)
            {
                onlineUsers[username] = ipAddress;
                Console.WriteLine("An user with Key = " + username + " is already online. Updated it's IP address to " + ipAddress);
                return;
            }


            try
            {
                usedPortsByAddress.Add(ipAddress, new List<int>());
            }
            catch (ArgumentException)
            {
                Console.WriteLine("A user with the same IP address: " + ipAddress);
                Console.WriteLine("Is already online. Not creating new list of used ports");
            }


            Console.WriteLine("User " + username + " added to online users at address: " + ipAddress);
        }

        public string GetAddressFromOnlineUser(String username)
        {
            String address = "localhost";
            try
            {
                address = onlineUsers[username];
            }
            catch (KeyNotFoundException)
            {
                Console.WriteLine("Could not find user " + username + " online. Returning address <localhost>");
            }
            return address;
        }

        public int AllocatePort(String nickname)
        {
            int port = GetFreePort(nickname);

            String userAddress = onlineUsers[nickname];

            List<int> usedPorts = usedPortsByAddress[userAddress];

            usedPorts.Add(port);

            return port;
        }

        public int GetFreePort(String nickname)
        {
            //TO DO: check first if other users on the same IP Adress are using these ports

            //This will need to be stored in the server for every address (dictionary<String address, List<int Ports>>)
            //nickname as argument

            //Go to list of online users
            //If their address is the same as ours, add their usedPorts list to our usedPorts list

            int port = 2828;
            String userAddress = onlineUsers[nickname];

            while (port < 9999)
            {
                if (IsPortFree(userAddress, port))
                {
                    return port;
                }
                port++;
            }

            Console.WriteLine("Colud not get a free port, assigning to port 1000");
            return 1000;
        }

        public bool IsPortFree(String userAddress, int port)
        {

            List<int> usedPorts = usedPortsByAddress[userAddress];

            foreach (int usedPort in usedPorts)
            {
                if (port == usedPort)
                {
                    return false;
                }
            }
            return true;
        }


        public Dictionary<string, string> getOnlineUsers()
        {
            Console.WriteLine("onlineUsers dictionnary returned");
            return onlineUsers;
        }

        //////////////////////////////////////////////////////////////////////////////
        #endregion

        public void StartServer(int port)
        {
            Console.WriteLine("Server starting on port " + port + " ...");
            if (serverActive)
                return;

            Hashtable props = new Hashtable();
            props["port"] = port;
            props["name"] = serverURI;

            //Set up for remoting events properly
            BinaryServerFormatterSinkProvider serverProv = new BinaryServerFormatterSinkProvider();
            serverProv.TypeFilterLevel = System.Runtime.Serialization.Formatters.TypeFilterLevel.Full;

            serverChannel = new TcpServerChannel(props, serverProv);

            try
            {
                ChannelServices.RegisterChannel(serverChannel, false);
                internalRef = RemotingServices.Marshal(this, props["name"].ToString());
                serverActive = true;
                Console.WriteLine("[Server active]\n");
                //Console.ReadLine();
            }
            catch (RemotingException re)
            {
                //Could not start the server because of a remoting exception
            }
            catch (Exception ex)
            {
                //Could not start the server because of some other exception
            }
        }

        public void StopServer()
        {
            if (!serverActive)
                return;

            RemotingServices.Unmarshal(internalRef);

            try
            {
                ChannelServices.UnregisterChannel(serverChannel);
            }
            catch (Exception ex)
            {

            }
        }

        private void SafeInvokeMessageArrived(string Message)
        {
            if (!serverActive)
                return;

            if (MessageArrived == null)
                return;         //No Listeners

            MessageArrivedEvent listener = null;
            Delegate[] dels = MessageArrived.GetInvocationList();

            foreach (Delegate del in dels)
            {
                try
                {
                    listener = (MessageArrivedEvent)del;
                    listener.Invoke(Message);
                }
                catch (Exception ex)
                {
                    //Could not reach the destination, so remove it
                    //from the list
                    MessageArrived -= listener;
                }
            }
        }
    }
}

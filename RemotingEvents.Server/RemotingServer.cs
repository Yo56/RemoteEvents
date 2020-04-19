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
        private ObjRef internalRef;
        private Boolean serverActive = false;
        private static string serverURI = "serverExample.Rem";

        ////////////////////////////   Data    /////////////////////////////////////////////
        
        Dictionary<String, User> registeredUsers;
        //Dictionary of Online users
        Dictionary<String, String> onlineUsers;

        //Dictionary of used ports in each address
        Dictionary<String, List<int>> usedPortsByAddress;
        Dictionary<String, int> lastAllocatedPort;

        #endregion

        #region IServerObject Members
        ///////////////////////// ACCESSIBLE REMOTELY ////////////////////////////////////
        
        //event to handle when a user log in/out
        public event OnlineUsersChangedEvent OnlineUsersChanged;
        public event NewChatRequestEvent NewChatRequest;
        public event OpenAcceptedChatRequestEvent OpenAcceptedChatRequest;
        public event CloseOtherUserChatPageEvent CloseOtherUserChatPage;

        public string HelloWorld()
        {
            Console.WriteLine("[Client connected to the server]");
            return "Connection established to the server";
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
            if (onlineUsers.Remove(username))
            {
                Console.WriteLine("user " + username + " disconnected!");
                SafeInvokeOnlineUsersChanged(onlineUsers);
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
                SafeInvokeOnlineUsersChanged(onlineUsers);
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

            lastAllocatedPort[nickname] = port;

            return port;
        }

        public int GetLastAllocatedPort(String nickname)
        {
            return lastAllocatedPort[nickname];
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
            return onlineUsers;
        }

        public Boolean SendChatRequest(string senderNickname, string receiverUsername)
        {
            if (onlineUsers.ContainsKey(receiverUsername))
            {
                safeInvokeNewChatRequest(senderNickname, receiverUsername);
                return true;
            }
            else
            {
                Console.WriteLine("User " + receiverUsername + " not found in online users");
                return false;
            }
            
        }

        public void makeOtherUserOpenChatPage(string senderNickname, string receiverUsername)
        {
            safeInvokeOpenAcceptedChatRequest(senderNickname, receiverUsername);
        }

        public void makeOtherUserCloseChatPage(string senderNickname, string receiverUsername)
        {
            safeInvokeCloseOtherUserChatPage(senderNickname, receiverUsername);
        }

        #endregion

        #region server configuration
        ////////////////////  SERVER CONFIGURATION  //////////////////////////////

        public RemotingServer()
        {
            Console.WriteLine("Initialization...");

            Storage.DeleteAllUsers();   // line commented => users are loaded from file
                                        // line uncommented => users are deleted at server loading

            registeredUsers = Storage.LoadRegisteredUsersFromFile();

            onlineUsers = new Dictionary<String, String>();
            usedPortsByAddress = new Dictionary<string, List<int>>();
            lastAllocatedPort = new Dictionary<String, int>();

            StartServer(1234);

            Console.ReadKey(); //Avoid stop
        }

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
            }
            catch (RemotingException re)
            {
                //Could not start the server because of a remoting exception
                Console.WriteLine(re.ToString());
            }
            catch (Exception ex)
            {
                //Could not start the server because of some other exception
                Console.WriteLine(ex.ToString());
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
                Console.WriteLine(ex.ToString());
            }
        }
        #endregion

        #region Safe invoke
        // Call event listeners when ONLINE USERS change
        private void SafeInvokeOnlineUsersChanged(Dictionary<string,string> listOfOnlineUsers )
        {
            if (!serverActive)
                return;

            if (OnlineUsersChanged == null)
                return;         //No Listeners

            OnlineUsersChangedEvent listener = null;
            Delegate[] dels = OnlineUsersChanged.GetInvocationList();

            foreach (Delegate del in dels)
            {
                try
                {
                    listener = (OnlineUsersChangedEvent) del;
                    listener.Invoke(listOfOnlineUsers);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    //Could not reach the destination, so remove it
                    //from the list
                    OnlineUsersChanged -= listener;
                }
            }
        }

        //call event listeners when A new chat request is created
        private void safeInvokeNewChatRequest(string senderNickname, string receiverNickname)
        {
            if (!serverActive)
                return;

            if (NewChatRequest == null)
                return;         //No Listeners

            NewChatRequestEvent listener = null;
            Delegate[] dels = NewChatRequest.GetInvocationList();

            foreach (Delegate del in dels)
            {
                try
                {
                    listener = (NewChatRequestEvent)del;
                    listener.Invoke(senderNickname, receiverNickname);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    //Could not reach the destination, so remove it
                    //from the list
                    NewChatRequest -= listener;
                }
            }
        }

        private void safeInvokeOpenAcceptedChatRequest(string senderNickname, string receiverNickname)
        {
            if (!serverActive)
                return;

            if (OpenAcceptedChatRequest == null)
                return;         //No Listeners

            OpenAcceptedChatRequestEvent listener = null;
            Delegate[] dels = OpenAcceptedChatRequest.GetInvocationList();

            foreach (Delegate del in dels)
            {
                try
                {
                    listener = (OpenAcceptedChatRequestEvent)del;
                    listener.Invoke(senderNickname, receiverNickname);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    //Could not reach the destination, so remove it
                    //from the list
                    OpenAcceptedChatRequest -= listener;
                }
            }
        }

        private void safeInvokeCloseOtherUserChatPage(string senderNickname, string receiverNickname)
        {
            if (!serverActive)
                return;

            if (CloseOtherUserChatPage == null)
                return;         //No Listeners

            CloseOtherUserChatPageEvent listener = null;
            Delegate[] dels = CloseOtherUserChatPage.GetInvocationList();

            foreach (Delegate del in dels)
            {
                try
                {
                    listener = (CloseOtherUserChatPageEvent)del;
                    listener.Invoke(senderNickname, receiverNickname);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    //Could not reach the destination, so remove it
                    //from the list
                    CloseOtherUserChatPage -= listener;
                }
            }
        }
        #endregion
    }
}

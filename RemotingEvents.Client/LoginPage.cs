using System;
using System.Collections.Generic;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using RemotingEvents.Common;
using System.Collections;

namespace RemotingEvents.Client
{
    public partial class LoginPage : Form
    {
        //Server component
        String ipAddress = GetLocalIPAddress();
        TcpClientChannel channel;

        IServerObject remoteServer;
        
        TcpChannel tcpChan;
        BinaryClientFormatterSinkProvider clientProv;
        BinaryServerFormatterSinkProvider serverProv;
        private string serverURI = "tcp://localhost:1234/serverExample.Rem";    
        private bool connected = false;

        public LoginPage()
        {
            Console.WriteLine("Client starting ...");
            InitializeComponent();

            clientProv = new BinaryClientFormatterSinkProvider();
            serverProv = new BinaryServerFormatterSinkProvider();
            serverProv.TypeFilterLevel = System.Runtime.Serialization.Formatters.TypeFilterLevel.Full;
          

            Hashtable props = new Hashtable();
            props["name"] = "remotingClient";
            props["port"] = 0;      //First available port

            tcpChan = new TcpChannel(props, clientProv, serverProv);
            ChannelServices.RegisterChannel(tcpChan);

            RemotingConfiguration.RegisterWellKnownClientType(new WellKnownClientTypeEntry(typeof(IServerObject), serverURI));

            try
            {
                remoteServer = (IServerObject)Activator.GetObject(typeof(IServerObject), serverURI);
                connected = true;
                Console.WriteLine(remoteServer.HelloWorld());
            }
            catch (Exception ex)
            {
                connected = false;
                Console.WriteLine("Could not connect: " + ex.Message);
            }
            /* OLD
            //init server connection
            channel = new TcpClientChannel();
            ChannelServices.RegisterChannel(channel, false);
            RemotingConfiguration.RegisterWellKnownClientType(
            typeof(ServerData), "tcp://localhost:1235/ServerData");
            
            sd = new ServerData();
            Console.WriteLine(sd.Hello());
            */
        }


        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }

        //action methods LOGIN
        private void buttonLogin_Click(object sender, EventArgs e)
        {
            //get values of the fields nickname & password
            String nickname = inputLoginNick.Text;
            String HashPassword = Password.HashSHA256(inputLoginPass.Text); //hash password => clear password isn't visible on network

            Console.WriteLine("login attempt from " + nickname + " " + HashPassword);

            if(remoteServer.Authentication(nickname,HashPassword, ipAddress)== true)
            {
                //get user from nickname
                User user = remoteServer.GetUser(nickname);

                if(user != null) //user == null should not occured !
                {
                    logUser(user);
                }
            }
            else
            {
                labelLoginError.Visible = true;
            }
        }

        //REGISTER
        private void buttonRegister_Click(object sender, EventArgs e)
        {
            //clear all labelErrors
            labelRegisterPassError.Visible = false;
            labelRegisterNickError.Visible = false;
            
            Boolean allowRegistration = true;

            //get values of the fields nickname & password
            String name = inputRegisterName.Text;
            String nickname = inputRegisterNick.Text;
            String password = inputRegisterPass.Text;
            String passwordConfirmation = inputRegisterPassConf.Text;

            //check nickname unicity
            if (remoteServer.DoesThisUserExist(nickname) == true)
            {   //nickname unavailable
                //display error label
                labelRegisterNickError.Visible = true;

                //disable registration
                allowRegistration = false;
            }

            //check password inputs are equals
            if (!password.Equals(passwordConfirmation))
            {   //wrong password

                //display error label
                labelRegisterPassError.Visible = true;
                //clear password inputs
                inputRegisterPass.Clear();
                inputRegisterPassConf.Clear();
                //disable registration
                allowRegistration = false;
            }

            //if passwords are matching and nickname is available => registration
            if(allowRegistration)
            {
                User user = new User(name, nickname,Password.HashSHA256(password));
                remoteServer.Registration(user, ipAddress);
                Console.WriteLine("Registration sent to the server \n" + user);

                //log user into the app with the account he has just created
                logUser(user);
            }
            
        }

        //private method that opens the mainPage with user's account
        //used in Login and register button methods
        private void logUser(User user)
        {
            if (user != null) //user == null should not occured !
            {
                //Close login page and open next one
                MainPage mainPage = new MainPage(user, remoteServer);
                this.Visible = false;
                mainPage.ShowDialog();
                this.Close();
            }
        }
    }
}

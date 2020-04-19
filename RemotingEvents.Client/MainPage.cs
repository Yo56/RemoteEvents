using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Text;
using System.Windows.Forms;
using RemotingEvents.Common;

namespace RemotingEvents.Client
{
    public partial class MainPage : Form
    {
        //DATA
        User userLogged;
        Dictionary<String, List<ChatPage>> activeChatPages;

        //Server component
        EventProxy eventProxy;
        IServerObject remoteServer;

        //delegate functions
        private delegate void InvokeDelegateOnlineUsersUpdate(Dictionary<string,string> users);
        private delegate void InvokeDelegateChatRequestUpdate(string senderNickname);
        private delegate void InvokeDelegateOpenAcceptedChatRequest(string senderNickname, Boolean isAcceptingInvite);
        private delegate void InvokeDelegateCloseOtherUserChatPageUpdate(string senderNickname);

        public MainPage(User userLogged, IServerObject server)
        {
            Text = "Chat Service";
            //get user from loginPage
            this.userLogged = userLogged;
            this.activeChatPages = new Dictionary<string, List<ChatPage>>();
            InitializeComponent();
            labelUserFullName.Text = userLogged.Name;
            labelUserNickname.Text = userLogged.Nickname;
            
            //Server components
            this.eventProxy = new EventProxy();
            this.remoteServer = server;

            //get activeUsers from server
            GenerateActiveUsersList(remoteServer.getOnlineUsers());

            //Handle proxy
            eventProxy.OnlineUsersChanged += new OnlineUsersChangedEvent(EventProxy_OnlineUsersChanged);
            eventProxy.NewChatRequest += new NewChatRequestEvent(EventProxy_NewChatRequest);
            eventProxy.OpenAcceptedChatRequest += new OpenAcceptedChatRequestEvent(EventProxy_OpenAcceptedChatRequest);
            eventProxy.CloseOtherUserChatPage += new CloseOtherUserChatPageEvent(EventProxy_CloseOtherUserChatPage);

            //attach the proxy handle functions to Server events 
            remoteServer.OnlineUsersChanged += new OnlineUsersChangedEvent(eventProxy.LocallyHandleOnlineUsersChanged);
            remoteServer.NewChatRequest += new NewChatRequestEvent(eventProxy.LocallyHandleNewChatRequest);
            remoteServer.OpenAcceptedChatRequest += new OpenAcceptedChatRequestEvent(eventProxy.LocallyHandleOpenAcceptedChatRequest);
            remoteServer.CloseOtherUserChatPage += new CloseOtherUserChatPageEvent(eventProxy.LocallyHandleCloseOtherUserChatPageRequest);

        }

        private void buttonLogOut_Click(object sender, EventArgs e) //MAYBE divide into 2 functions 
        {
            //disconnect user from server
            Console.WriteLine("Disconnecting from server...");
            remoteServer.LogOut(userLogged.Nickname);

            Process.Start("RemotingEvents.Client.exe");
            Environment.Exit(0);
        }

        #region Chat Request
        /////////////////// CHAT REQUEST MANAGEMENT //////////////////////////


        private void GenerateNewChatRequestRow(String username)
        {
            Console.WriteLine("Creating new chatRequestPanel: " + chatRequestsFlowLayoutPanel.Controls.Count);

            Panel chatRequestPanel = new Panel();
            chatRequestPanel.Name = username;
            chatRequestPanel.BackColor = Color.FromArgb(0, 112, 204);
            chatRequestPanel.Size = new Size(325, 50);

            GenerateNewChatRequestRowButtons(chatRequestPanel);

            chatRequestsFlowLayoutPanel.Controls.Add(chatRequestPanel);
            chatRequestsFlowLayoutPanel.Controls.SetChildIndex(chatRequestPanel, 0);  // this moves the new one to the top!
                                                                       
            chatRequestPanel.Paint += (ss, ee) => { ee.Graphics.DrawString(chatRequestPanel.Name, Font, Brushes.White, 22, 11); };
            chatRequestsFlowLayoutPanel.Invalidate();
        }

        private void GenerateNewChatRequestRowButtons(Panel chatRequestPanel)
        {
            GenerateAcceptButton(chatRequestPanel);
            GenerateDeclineButton(chatRequestPanel);
        }

        private void GenerateAcceptButton(Panel chatRequestPanel)
        {
            Button acceptButton = new Button();
            acceptButton.Text = "Accept";
            acceptButton.Name = "Accept";
            acceptButton.Size = new Size(75, 25);
            acceptButton.BackColor = Color.FromArgb(255, 255, 255);
            acceptButton.ForeColor = Color.FromArgb(0, 0, 0);
            acceptButton.Location = new Point(150, 12);
            acceptButton.Click += new EventHandler(this.Accept_Click);

            chatRequestPanel.Controls.Add(acceptButton);
        }

        private void GenerateDeclineButton(Panel chatRequestPanel)
        {
            Button declineButton = new Button();
            declineButton.Text = "Decline";
            declineButton.Name = "Decline";
            declineButton.Size = new Size(75, 25);
            declineButton.BackColor = Color.FromArgb(255, 255, 255);
            declineButton.ForeColor = Color.FromArgb(0, 0, 0);
            declineButton.Location = new Point(235, 12);
            declineButton.Click += new EventHandler(this.Decline_Click);

            chatRequestPanel.Controls.Add(declineButton);
        }

        private void Accept_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            String username = clickedButton.Parent.Name;
            OpenChatPage(username, true);
            remoteServer.makeOtherUserOpenChatPage(username, userLogged.Nickname);

            clickedButton.Click -= new EventHandler(this.Decline_Click);
            clickedButton.Parent.Dispose();
        }

        private void Decline_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            String username = clickedButton.Parent.Name;
            clickedButton.Click -= new EventHandler(this.Decline_Click);
            clickedButton.Parent.Dispose();

        }

        //method in relation with EventProxy
        private void EventProxy_NewChatRequest(string senderNickname, string receiverNickname)
        {            
            if(userLogged.Nickname.Equals(receiverNickname))
            {
                this.BeginInvoke(new InvokeDelegateChatRequestUpdate(GenerateNewChatRequestRow), new object[] { senderNickname });
                return;
            }
            
        }

        private void EventProxy_OpenAcceptedChatRequest(string senderNickname, string receiverNickname)
        {

            if (userLogged.Nickname.Equals(senderNickname))
            {
                this.BeginInvoke(new InvokeDelegateOpenAcceptedChatRequest(OpenChatPage), new object[] { receiverNickname , false });
                return;
            }

            

        }

        private void EventProxy_CloseOtherUserChatPage(string senderNickname, string receiverNickname)
        {
            if (userLogged.Nickname.Equals(receiverNickname))
            {
                this.BeginInvoke(new InvokeDelegateCloseOtherUserChatPageUpdate(CloseChatPage), new object[] { senderNickname });
                return;
            }

        }

        private void OpenChatPage(string otheruserNickname, Boolean isAcceptingInvitation)
        {
            int otherPort;

            String name = remoteServer.GetRealNameFromUser(otheruserNickname);
            String otherAddress = remoteServer.GetAddressFromOnlineUser(otheruserNickname);
            int port = remoteServer.AllocatePort(userLogged.Nickname);


            if (isAcceptingInvitation)
            {
                otherPort = remoteServer.GetFreePort(otheruserNickname);
            }
            else
            {
                otherPort = remoteServer.GetLastAllocatedPort(otheruserNickname);
            }

            ChatPage chatPage = new ChatPage(this, userLogged, otheruserNickname, name, port, otherAddress, otherPort);
            if(!activeChatPages.ContainsKey(otheruserNickname))
            {
                activeChatPages[otheruserNickname] = new List<ChatPage>();
            }
           

            activeChatPages[otheruserNickname].Add(chatPage);
            chatPage.Show();
        }

        public void CloseChatPage(string otheruserNickname)
        {
            foreach(ChatPage cp in activeChatPages[otheruserNickname])
            {
                if(cp.otherUsername == otheruserNickname)
                {
                    cp.Close();
                }
            }

        }

        public void CloseOtherUserChatPage(string otheruserNickname)
        {            
            remoteServer.makeOtherUserCloseChatPage(userLogged.Nickname, otheruserNickname);
        }

        #endregion



        #region Online users management

        /////////////////// ACTIVE/ONLINE USERS MANAGEMENT //////////////////////////

        // method in relation with EventProxy
        private void EventProxy_OnlineUsersChanged(Dictionary<string,string> listOfOnlineUsers)
        {
            Console.WriteLine("Client received an update of OnlineUsers!");
            RefreshOnlineUsersDisplay(listOfOnlineUsers);
        }

        private void RefreshOnlineUsersDisplay(Dictionary<string, string> listOfOnlineUsers)
        {
            this.BeginInvoke(new InvokeDelegateOnlineUsersUpdate(GenerateActiveUsersList), new object[] { listOfOnlineUsers });
            return;
        }

        //method wich refreshes the Active/online users
        private void GenerateActiveUsersList(Dictionary<string, string> listOfOnlineUsers)
        {
            //clear all rows
            activeUsersFlowLayoutPanel.Controls.Clear();

            //fill the rows with fresh data
            foreach (KeyValuePair<string, string> entry in listOfOnlineUsers)
            {
                // remove the user logged into the list of online available users 
                if (!userLogged.Nickname.Equals(entry.Key))
                {
                    GenerateActiveUserRow(entry.Key);
                }
                
            }
        }

        //method that creates a online user row
        private void GenerateActiveUserRow(String username)
        {
            Panel onlineUserPanel = new Panel();
            onlineUserPanel.Name = username;
            onlineUserPanel.BackColor = Color.FromArgb(204, 233, 255);
            onlineUserPanel.Size = new Size(325, 50);

            GenerateSendInvitationButton(onlineUserPanel);

            activeUsersFlowLayoutPanel.Controls.Add(onlineUserPanel);
            //activeUsersFlowLayoutPanel.Controls.SetChildIndex(onlineUserPanel, 0);  // this moves the new one to the top!
            onlineUserPanel.Paint += (ss, ee) => { ee.Graphics.DrawString(username, Font, Brushes.Black, 22, 18); };

        }

        private void GenerateSendInvitationButton(Panel onlineUserPanel)
        {
            Button sendRequestButton = new Button();
            sendRequestButton.Text = "Send Invitation";
            sendRequestButton.Name = "sendInvitation";
            sendRequestButton.Size = new Size(100, 25);
            sendRequestButton.Location = new Point(200, 12);
            sendRequestButton.BackColor = Color.FromArgb(255, 255, 255);
            sendRequestButton.Click += new EventHandler(this.SendChatInvitation_Click);

            onlineUserPanel.Controls.Add(sendRequestButton);
        }

        private void SendChatInvitation_Click(object sender, EventArgs e)
        {
            //get the name of the parent container which is the name of the user
            Button button = (Button) sender;
            string parentName = button.Parent.Name; 
            Console.WriteLine("Sending an invitation to user "+parentName+"...");

            Boolean invitationSent = remoteServer.SendChatRequest(userLogged.Nickname, parentName);
            if (invitationSent)
            {
                Console.WriteLine("invitation sent !");
                //button.Text = "Sent";
                //button.Enabled = false;
            }
            else
            {
                Console.WriteLine("Problem : invitation not sent");
            }
        }

        #endregion


        
    }
}

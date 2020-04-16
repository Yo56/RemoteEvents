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

        //Server component
        EventProxy eventProxy;
        IServerObject remoteServer;

        //delegate function
        private delegate void InvokeDelegateOnlineUsersUpdate(Dictionary<string,string> users);

        public MainPage(User userLogged, IServerObject server)
        {
            //get user from loginPage
            this.userLogged = userLogged;
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

            //attach the onlineUsersChanged event to receive updates when the list changes
            remoteServer.OnlineUsersChanged += new OnlineUsersChangedEvent(eventProxy.LocallyHandleOnlineUsersChanged);
            
            
        }

        private void buttonLogOut_Click(object sender, EventArgs e) //MAYBE divide into 2 functions 
        {
            //disconnect user from server
            Console.WriteLine("Disconnecting from server...");
            remoteServer.LogOut(userLogged.Nickname);

            //RemotingServices.Disconnect(sd);
            //ChannelServices.UnregisterChannel(TCPChannel);

            Process.Start("RemotingEvents.Client.exe");
            Environment.Exit(0);
            
            
            //manage windows 
            /*LoginPage loginPage = new LoginPage(sd);
            this.Visible = false;
            loginPage.ShowDialog();
            this.Close();*/

        }

        #region Chat Request
        /////////////////// CHAT REQUEST MANAGEMENT //////////////////////////

        private void TempSimulateNewChatRequest_Click(object sender, EventArgs e)
        {
            GenerateNewChatRequestRow("user2");
        }

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
            declineButton.Location = new Point(235, 12);
            declineButton.Click += new EventHandler(this.Decline_Click);

            chatRequestPanel.Controls.Add(declineButton);
        }

        private void Accept_Click(object sender, EventArgs e)
        {/*
            Button clickedButton = (Button)sender;
            String username = clickedButton.Parent.Name;
            String name = remoteServer.GetRealNameFromUser(username);
            String otherAddress = remoteServer.GetAddressFromOnlineUser(username);
            int port = remoteServer.AllocatePort(userLogged.Nickname);
            ChatPage chatPage = new ChatPage(userLogged, username, name, port, otherAddress, true);
            chatPage.Show();

            //TODO : Make other user open his chatPage

            clickedButton.Click -= new EventHandler(this.Decline_Click);
            clickedButton.Parent.Dispose();
            //Local only for now
            //Might need to be comunicated and handled by the server later
            Console.WriteLine("Accepted invitation from " + username);*/
        }

        private void Decline_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            String username = clickedButton.Parent.Name;
            clickedButton.Click -= new EventHandler(this.Decline_Click);
            clickedButton.Parent.Dispose();
            //Local only for now
            //Might need to be comunicated and handled by the server later
            Console.WriteLine("Declined invitation from " + username);

        }

        #endregion

        #region Online users management
        /////////////////// ACTIVE/ONLINE USERS MANAGEMENT //////////////////////////

        // method in relation with EventProxy
        private void EventProxy_OnlineUsersChanged(Dictionary<string,string> listOfOnlineUsers)
        {
            Console.WriteLine("Client received an update of OnlineUsers !");
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
            Console.WriteLine("Creating new activeUsersFlowLayoutPanel : " + activeUsersFlowLayoutPanel.Controls.Count);

            Panel onlineUserPanel = new Panel();
            onlineUserPanel.Name = "panelOnlineUser" + username;
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
            //sendRequestButton.Click += new EventHandler(this.Accept_Click);

            onlineUserPanel.Controls.Add(sendRequestButton);
        }

        #endregion
    }
}

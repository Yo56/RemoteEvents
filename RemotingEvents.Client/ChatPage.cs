using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;

using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Windows.Forms;
using RemotingEvents.Common;

namespace RemotingEvents.Client
{
    public partial class ChatPage : Form
    {
        User userLogged;

        public ChatPage(User userLogged,  String otherUsername, String otherName, int port, String otherAddress)//will need other user's details
        {
            this.userLogged = userLogged;
            InitializeComponent();
            RealName.Text = otherName;
            Nickname.Text = otherUsername;

            Console.WriteLine("Start chat: I, " + userLogged.Nickname + ", am starting a chat with " + otherUsername + " (real name: " + otherName + "), his address is: " + otherAddress + ", on port " + port);

        }
    }
}

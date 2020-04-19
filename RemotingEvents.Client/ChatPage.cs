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

using System.Net;
using System.Net.Sockets;

namespace RemotingEvents.Client
{
    public partial class ChatPage : Form
    {
        private User userLogged;
        private MainPage mp;
        public string otherUsername;

        Socket sck;
        EndPoint epLocal, epRemote;



        public ChatPage(MainPage mp, User userLogged,  String otherUsername, String otherName, int port, String otherAddress, int otherPort)
        {
            this.userLogged = userLogged;
            this.mp = mp;
            this.otherUsername = otherUsername;
            InitializeComponent();
            RealName.Text = otherName;
            Nickname.Text = otherUsername;

            Console.WriteLine("Start chat: I, " + userLogged.Nickname + ", am starting a chat with " + otherUsername + " (real name: " + otherName + "), his address is: " + otherAddress + " , and his port:" + otherPort + ", my listening port: " + port);

            sck = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            sck.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);

            start(port, otherAddress, otherPort);
        }

        private void start(int port, String otherAddress, int otherPort)
        {
            try
            {
                epLocal = new IPEndPoint(IPAddress.Parse(GetLocalIPAddress()), port);
                sck.Bind(epLocal);

                epRemote = new IPEndPoint(IPAddress.Parse(otherAddress), otherPort);
                sck.Connect(epRemote);

                byte[] buffer = new byte[262200];
                sck.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref epRemote, new AsyncCallback(MessageCallBack), buffer);

                MessageToSend.Focus();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Could not start:");
                Console.WriteLine(ex.ToString());
            }
        } 

        private void MessageCallBack(IAsyncResult aResult)
        {
            try
            {
                int size = sck.EndReceiveFrom(aResult, ref epRemote);
                if (size > 0)
                {
                    byte[] receivedData = new byte[262144];

                    receivedData = (byte[])aResult.AsyncState;

                    ASCIIEncoding eEncoding = new ASCIIEncoding();
                    string receivedMessage = eEncoding.GetString(receivedData);

                    showNewMessage(receivedMessage, true);
                }

                byte[] buffer = new byte[262200];
                sck.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref epRemote, new AsyncCallback(MessageCallBack), buffer);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Could not run MessageCallBack:");
                Console.WriteLine(ex.ToString());
            }
        }

        private void Send_Click(object sender, EventArgs e)
        {
            try
            {
                System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
                byte[] msg = new byte[262200];
                msg = enc.GetBytes(MessageToSend.Text);

                sck.Send(msg);

                showNewMessage(MessageToSend.Text, false);
                MessageToSend.Clear();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Could not send message:");
                Console.WriteLine(ex.ToString());
            }
        }

        public delegate void showNewMessageDelegate(String message, Boolean isReceiving);
        public void showNewMessage(String message, Boolean isReceiving)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new showNewMessageDelegate(showNewMessage), message, isReceiving);
            }
            else
            {
                if (message.Length > 0)
                {
                    Console.WriteLine("Sowing new message: #" + Messages.Controls.Count);

                    Panel messagePanel = new Panel();
                    messagePanel.Name = message;

                    messagePanel.Size = new Size(700, 50);//Y needs to be dynamic to fit length of the text

                    TextBox text = new TextBox();
                    text.Text = message;
                    text.Size = new Size(450, 50);//Y needs to be dynamic to fit length of the text


                    if (isReceiving)
                    {
                        messagePanel.BackColor = Color.FromArgb(204, 233, 255);
                        text.Location = new Point(100, 0);
                    }
                    else
                    {
                        messagePanel.BackColor = Color.FromArgb(33, 149, 237);
                        text.Location = new Point(150, 0);
                    }


                    messagePanel.Controls.Add(text);

                    Messages.Controls.Add(messagePanel);
                    Messages.ScrollControlIntoView(messagePanel);
                }
            }
        }

        private void ChatPage_FormClosed(object sender, FormClosedEventArgs e)
        {
            Console.WriteLine("Chat page: Closing " + userLogged.Nickname + "'s chat page with " + otherUsername);
            Console.WriteLine("Chat page: Telling Main Page to close " + otherUsername + "'s chat page");
            mp.CloseOtherUserChatPage(otherUsername);
        }

        private static string GetLocalIPAddress()
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
    }
}

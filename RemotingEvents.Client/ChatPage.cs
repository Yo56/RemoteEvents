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
        User userLogged;

        Socket sck;
        EndPoint epLocal, epRemote;

        

        public ChatPage(User userLogged,  String otherUsername, String otherName, int port, String otherAddress, int otherPort)
        {
            this.userLogged = userLogged;
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

                    showNewMessage(receivedMessage);
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

                showNewMessage(MessageToSend.Text);
                MessageToSend.Clear();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Could not send message:");
                Console.WriteLine(ex.ToString());
            }
        }

        public delegate void showNewMessageDelegate(String message);
        public void showNewMessage(String message)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new showNewMessageDelegate(showNewMessage), message);
            }
            else
            {
                Console.WriteLine("Sowing new message: #" + Messages.Controls.Count);

                Panel messagePanel = new Panel();
                messagePanel.Name = message;
                messagePanel.BackColor = Color.FromArgb(204, 233, 255);
                messagePanel.Size = new Size(500, 50);//Y needs to be dynamic to fit length of the text

                TextBox text = new TextBox();
                text.Text = message;


                messagePanel.Controls.Add(text);

                Messages.Controls.Add(messagePanel);
            }
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

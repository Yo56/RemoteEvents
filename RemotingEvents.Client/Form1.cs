using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RemotingEvents.Common;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Channels;
using System.Collections;
using System.Runtime.Remoting;

namespace RemotingEvents.Client
{
    public partial class Form1 : Form
    {

        IServerObject remoteServer;
        EventProxy eventProxy;
        TcpChannel tcpChan;
        BinaryClientFormatterSinkProvider clientProv;
        BinaryServerFormatterSinkProvider serverProv;
        private string serverURI = "tcp://localhost:1234/serverExample.Rem";        //Replace with your IP
        private bool connected = false;

        private delegate void SetBoxText(string Message);

        public Form1()
        {
            InitializeComponent();

            clientProv = new BinaryClientFormatterSinkProvider();
            serverProv = new BinaryServerFormatterSinkProvider();
            serverProv.TypeFilterLevel = System.Runtime.Serialization.Formatters.TypeFilterLevel.Full;

            eventProxy = new EventProxy();
            eventProxy.MessageArrived += new MessageArrivedEvent(eventProxy_MessageArrived);

            Hashtable props = new Hashtable();
            props["name"] = "remotingClient";
            props["port"] = 0;      //First available port

            tcpChan = new TcpChannel(props, clientProv, serverProv);
            ChannelServices.RegisterChannel(tcpChan);

            RemotingConfiguration.RegisterWellKnownClientType(new WellKnownClientTypeEntry(typeof(IServerObject), serverURI));

        }

        void eventProxy_MessageArrived(string Message)
        {
            SetTextBox(Message);
        }

        private void bttn_Connect_Click(object sender, EventArgs e)
        {
            if (connected)
                return;

            try
            {
                remoteServer = (IServerObject)Activator.GetObject(typeof(IServerObject), serverURI);
                remoteServer.PublishMessage("Client Connected");        //This is where it will break if we didn't connect
            
                //Now we have to attach the events...
                remoteServer.MessageArrived += new MessageArrivedEvent(eventProxy.LocallyHandleMessageArrived);
                connected = true;
                Console.WriteLine(remoteServer.HelloWorld());
            }
            catch (Exception ex)
            {
                connected = false;
                SetTextBox("Could not connect: " + ex.Message);
            }
        }

        private void bttn_Disconnect_Click(object sender, EventArgs e)
        {
            if (!connected)
                return;

            //First remove the event
            remoteServer.MessageArrived -= eventProxy.LocallyHandleMessageArrived;

            //Now we can close it out
            ChannelServices.UnregisterChannel(tcpChan);
        }

        private void bttn_Send_Click(object sender, EventArgs e)
        {
            if (!connected)
                return;

            remoteServer.PublishMessage(tbx_Input.Text);
            tbx_Input.Text = "";
        }

        private void SetTextBox(string Message)
        {
            if (tbx_Messages.InvokeRequired)
            {
                this.BeginInvoke(new SetBoxText(SetTextBox), new object[] { Message });
                return;
            }
            else
                tbx_Messages.AppendText(Message + "\r\n");
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RemotingEvents.Server
{
    public partial class frm_Main : Form
    {
        RemotingServer server;
        private delegate void SetBoxText(string Message);

        public frm_Main()
        {
            InitializeComponent();
        }

        private void bttn_StartServer_Click(object sender, EventArgs e)
        {
            server = new RemotingServer();
            server.StartServer(1234);
            server.MessageArrived += new RemotingEvents.Common.MessageArrivedEvent(server_MessageArrived);
            SetTextBox("Server Started");
        }

        void server_MessageArrived(string Message)
        {
            SetTextBox(Message);
        }

        private void bttn_StopServer_Click(object sender, EventArgs e)
        {
            server.StopServer();
            server = null;
            SetTextBox("Server Stopped");
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

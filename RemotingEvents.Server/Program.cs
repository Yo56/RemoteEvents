using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace TDIN_PROJ1.Server
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            RemotingServer server = new RemotingServer();
        }
    }
}

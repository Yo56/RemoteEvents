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
    class Server : MarshalByRefObject
    {
        #region Fields

        private TcpServerChannel serverChannel;
        private int tcpPort;
        private ObjRef internalRef;
        private Boolean serverActive = false;
        private static string serverURI = "serverExample.Rem";

        #endregion

        public void Start(int port)
        {
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
                Console.WriteLine("Server starting...");
                Console.ReadLine();
            }
            catch (RemotingException re)
            {
                //Could not start the server because of a remoting exception
            }
            catch (Exception ex)
            {
                //Could not start the server because of some other exception
            }
        }
    }
}

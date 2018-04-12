using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace SocketClient_Example
{
    public class SocketClass
    {
        private Socket m_client;
        private IPEndPoint m_remoteEp;
        
        public void ConnectServer(string ip, int port)
        {
            m_remoteEp = new IPEndPoint(IPAddress.Parse(ip), port);
            m_client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);  
        }

        public Socket Client
        {
            get { return m_client; }
        }

        public IPEndPoint RemoteEp
        {
            get { return m_remoteEp; }
        }
    }
}

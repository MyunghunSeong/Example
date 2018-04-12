using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace SocketServer_Example_Default_
{
    public class SocketServer
    {
       /***********************************
       * 
       *      Member Variable
       * 
       * *********************************/
        private Socket m_server;

        /***********************************
        * 
        *      Property Area
        * 
        * *********************************/
        public Socket Server { get { return m_server; } }

        /***********************************
        * 
        *           Create Socket
        * 
        * *********************************/
        public void CreateSocket()
        {
            m_server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp); //Tcp Socket Create
            IPEndPoint ip = new IPEndPoint(IPAddress.Any, 9000); //Ip and Port Information
            m_server.Bind(ip); //Address Binding
            m_server.Listen(5); 
        }

    }

    
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace SocketServer_Example_Default_
{
    public class AsyncObjectClass
    {
        /*********************************
        * 
        *           Member Variable
        * 
        **********************************/
        private byte[] m_buffer; //Data Buffer
        private Socket m_workSocket; //비동기 소켓
        private readonly int m_size; //Buffer Size

        /*********************************
        * 
        *           Constructor
        * 
        **********************************/
        public AsyncObjectClass(int size)
        {
            m_size = size;
            m_buffer = new Byte[m_size];
        }

        /*********************************
        * 
        *      Buffer Clear Function
        * 
        **********************************/
        public void ClearBuffer()
        {
            Array.Clear(m_buffer, 0, m_size);
        }

        /*********************************
        * 
        *          Property Area
        * 
        **********************************/
        public Socket WorkingSocket
        {
            get { return m_workSocket; }
            set { m_workSocket = value; }
        }

        public Byte[] Buffer
        {
            get { return m_buffer; }
        }

        public int Size
        {
            get { return m_size; }
        }
    }
}

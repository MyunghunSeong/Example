using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace SocketClient_Example
{
    public class AsyncObjectClass
    {
        private byte[] m_buffer;
        private Socket m_workSocket;
        private readonly int m_size;

        public AsyncObjectClass(int size)
        {
            m_size = size;
            m_buffer = new Byte[m_size];
        }

        public void ClearBuffer()
        {
            Array.Clear(m_buffer, 0, m_size);
        }

        /* Property */
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

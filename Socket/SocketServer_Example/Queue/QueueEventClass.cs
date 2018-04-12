using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SocketServer_Example
{
    public enum MSG_STATUS
    {
        MSG_READ = 1,
        MSG_WRITE = 2,
    }

    public class QueueEventClass : IDisposable
    {
        /*********************************
        * 
        *      Member Variable
        * 
        **********************************/
        private Queue<MSG_STATUS> m_qArray; //메세지 상태 저장 큐
        private AutoResetEvent m_newEvent; //이벤트 발생 
        private ManualResetEvent m_exitEvent; //이벤트 종료
        private WaitHandle[] m_handle; //이벤트 핸들러 배열

        public QueueEventClass()
        {
            m_qArray = new Queue<MSG_STATUS>();
            m_newEvent = new AutoResetEvent(false);
            m_exitEvent = new ManualResetEvent(false);
            m_handle = new WaitHandle[2];
            m_handle[0] = m_newEvent; //배열 인덱스 0 => 이벤트 발생
            m_handle[1] = m_exitEvent; //배열 인덱스 1 => 이벤트 종료
        }

        /*********************************
        * 
        *          Property Area
        * 
        **********************************/
        public Queue<MSG_STATUS> Queue
        {
            get { return m_qArray; }
        }

        public AutoResetEvent NewEvent
        {
            get { return m_newEvent; }
        }

        public ManualResetEvent ExitEvent
        {
            get { return m_exitEvent; }
        }

        public WaitHandle[] Handler
        {
            get { return m_handle; }
        }

        /*********************************
        * 
        *     IDisposable Implements
        * 
        **********************************/
        public void Dispose()
        {
            m_qArray.Clear();
            m_qArray = null;

            m_exitEvent.Dispose();
            m_exitEvent = null;

            m_newEvent.Dispose();
            m_newEvent = null;
        }
    }
}

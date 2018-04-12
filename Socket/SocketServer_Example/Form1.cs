using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using SimpleInspect_template.Err;
using System.Threading.Tasks;
using System.Threading;

namespace SocketServer_Example
{
    public partial class Form1 : Form
    {
        /***********************************
         * 
         *        Delegate Function
         * 
         * *********************************/
        public delegate void del_thread(int index, StringBuilder message); //Form Control을 사용하기 위한 delegate 함수
        public delegate void SYSTEMERROR(String messge); //Error Process를 위한 delegate 함수

        /***********************************
        * 
        *           Global Variable
        * 
        * *********************************/
        public SocketServer m_SocketClass; //Server Socket Create Class Object
        public AsyncObjectClass m_asyncObject; //Class for Async Procee
        public Socket m_server; //Server Socket
        public List<Socket> m_clientList; //Connected Client List
        public ErrProcess m_err; //Class for Error Process
        public Task m_task; //Thread Object
        public CancellationTokenSource m_token; 
        public QueueEventClass m_queue; //Class for Queue Event
        public string m_recvData; //Receive Date

        /***********************************
        * 
        *           Constructor 
        * 
        * *********************************/
        public Form1()
        {
            InitializeComponent();
            m_SocketClass = new SocketServer();
            m_asyncObject = new AsyncObjectClass(1024);
            m_clientList = new List<Socket>();
            m_err = new ErrProcess();
            m_token = new CancellationTokenSource();
            m_queue = new QueueEventClass();
            m_recvData = string.Empty;
        }

        /***********************************
        * 
        *        Form Load Event
        * 
        * *********************************/
        private void Form1_Load(object sender, EventArgs e)
        {
            ERR_RESULT result = new ERR_RESULT();

            try
            {
                m_SocketClass.CreateSocket(); //서버 소켓 생성
                m_server = m_SocketClass.Server; //생성된 소켓
                m_server.BeginAccept(new AsyncCallback(AcceptCallBack), m_server); //접속할 클라이언트를 비동기적으로 받을 준비
                pictureBox1.BackColor = Color.Red;
                m_task = Task.Factory.StartNew(() => CheckFunc(), m_token.Token); //Event Check Thread 시작
            }
            catch (_MainException err)
            {
                result = ErrProcess.SetErrResult(err);
                m_err.SetErrCall(result);
            }
            catch (Exception err)
            {
                result = ErrProcess.SetErrResult(err);
                m_err.SetErrCall(result);
            }
        }

        /***********************************
        * 
        *     Accept CallBack Function
        * 
        * *********************************/
        void AcceptCallBack(IAsyncResult ar)
        {
            ERR_RESULT result = new ERR_RESULT();

            try
            {
                Socket client = m_server.EndAccept(ar); //클라이언트의 접속을 완료

                this.Invoke(new del_thread(InputText), new object[] { 1, null }); //접속 상태 변경 

                m_server.BeginAccept(AcceptCallBack, null); //다른 클라이언트 접속 대기

                m_clientList.Add(client); //클라이언트 List에 추가
                 
                m_asyncObject.WorkingSocket = client; //비동기 처리를 위한 클래스의 소켓에 클라이언트 저장

                m_queue.Queue.Enqueue(MSG_STATUS.MSG_READ); //Read Event를 Queue에 저장
                m_queue.NewEvent.Set(); //Read Event 발생
            }
            catch (_MainException err)
            {
                result = ErrProcess.SetErrResult(err);
                m_err.SetErrCall(result);
            }
            catch (Exception err)
            {
                result = ErrProcess.SetErrResult(err);
                m_err.SetErrCall(result);
            }
        }

        /***********************************
       * 
       *      Connect Status Invoke
       * 
       * *********************************/
        void InputText(int idx, StringBuilder msg)
        {
            ERR_RESULT result = new ERR_RESULT();

            try
            {
                if (idx == 1)
                {
                    pictureBox1.BackColor = Color.Green; //접속시 색상 Green으로 변경
                    lb_Status.Text = "Connected"; //클라이언트 접속 상태 변경
                }
                else
                    tx_log.Text += msg;
            }
            catch (_MainException err)
            {
                result = ErrProcess.SetErrResult(err);
                m_err.SetErrCall(result);
            }
            catch (Exception err)
            {
                result = ErrProcess.SetErrResult(err);
                m_err.SetErrCall(result);
            }
        }

        /***********************************
        * 
        *        Check Event Queue
        * 
        * *********************************/
        void CheckFunc()
        {
            ERR_RESULT result = new ERR_RESULT();

            try
            {
                int EventIdx = -1;

                while (!(EventIdx == 1)) //Event 종료 인덱스 1로 설정
                {
                    EventIdx = WaitHandle.WaitAny(m_queue.Handler, 1000); //1초 간격으로 Event 발생 체크
                    if (EventIdx == 0) //Event 시작 인덱스 0으로 설정
                    {
                        Loop_Start(); //Event 처리 Loop
                        m_queue.NewEvent.Reset();
                    }
                }
            }
            catch (_MainException err)
            {
                result = ErrProcess.SetErrResult(err);
                m_err.SetErrCall(result);
            }
            catch (Exception err)
            {
                result = ErrProcess.SetErrResult(err);
                m_err.SetErrCall(result);
            }
        }

        /***********************************
        * 
        *       Queue Event Loop Sart
        * 
        * *********************************/
        void Loop_Start()
        {
            ERR_RESULT result = new ERR_RESULT();
            MSG_STATUS msg;

            try
            {
                msg = m_queue.Queue.Dequeue(); //Queue에 저장된 Event 목록을 가져옴
                switch (msg)
                {
                    case MSG_STATUS.MSG_READ: //Read Event
                        m_task = Task.Factory.StartNew(() => DataReceived()); //Read Thread 실행
                        break;
                    case MSG_STATUS.MSG_WRITE: //Write Event
                        m_task = Task.Factory.StartNew(() => ReturnClientSignal()); //Write Thread 실행
                        break;
                }
            }
            catch (_MainException err)
            {
                result = ErrProcess.SetErrResult(err);
                m_err.SetErrCall(result);
            }
            catch (Exception err)
            {
                result = ErrProcess.SetErrResult(err);
                m_err.SetErrCall(result);
            }
        }

       
        /*********************************
        * 
        *     MSG_READ Event Thread
        * 
        **********************************/
        void DataReceived()
        {
            ERR_RESULT result = new ERR_RESULT();

            try
            {
                while(true) //반복해서 Read 실행
                {
                    m_asyncObject.WorkingSocket.Receive(m_asyncObject.Buffer); //Data Read

                    string recvDataFull = string.Empty;;

                    recvDataFull = Encoding.ASCII.GetString(m_asyncObject.Buffer); //Byte Data -> String Data

                    for (int i = 0; recvDataFull[i] != '\0'; i++)
                        m_recvData += recvDataFull[i]; //Null 값 제거

                    WriteLogMsg(m_recvData); //받은 메시지 출력

                    m_asyncObject.ClearBuffer(); //버퍼 비우기

                    m_queue.Queue.Enqueue(MSG_STATUS.MSG_WRITE); //Write Event Queue에 저장
                    m_queue.NewEvent.Set(); //Event 발생

                    Thread.Sleep(100); //0.01초 간격으로 반복
                }
            }
            catch (_MainException err)
            {
                result = ErrProcess.SetErrResult(err);
                m_err.SetErrCall(result);
            }
            catch (Exception err)
            {
                result = ErrProcess.SetErrResult(err);
                m_err.SetErrCall(result);
            }
        }

        /*********************************
        * 
        *      MSG_WRITE Event Thread
        * 
        **********************************/
        void ReturnClientSignal()
        {
            ERR_RESULT result = new ERR_RESULT();

            try
            {
                for (int i = m_clientList.Count - 1; i >= 0; i--)
                {
                    Socket socket = m_clientList[i];

                    socket.Send(Encoding.ASCII.GetBytes("[" + DateTime.Now + "] " + 
                        "Received Data : " + m_recvData + Environment.NewLine)); //받은 데이터 다시 리턴
                }
            }
            catch (_MainException err)
            {
                result = ErrProcess.SetErrResult(err);
                m_err.SetErrCall(result);
            }
            catch (Exception err)
            {
                result = ErrProcess.SetErrResult(err);
                m_err.SetErrCall(result);
            }

            Thread.Sleep(100);
        }

        /*********************************
        * 
        *   Receive Message Input Invoke
        * 
        **********************************/
        void WriteLogMsg(string msg)
        {
            ERR_RESULT result = new ERR_RESULT();

            try
            {
                StringBuilder msgLog = new StringBuilder();

                msgLog.Append("[" + DateTime.Now + "]");
                msgLog.Append(msg + Environment.NewLine);     

                this.Invoke(new del_thread(InputText), new object[] { 2, msgLog }); //메시지 출력 함수 Invoke
            }
            catch (_MainException err)
            {
                result = ErrProcess.SetErrResult(err);
                m_err.SetErrCall(result);
            }
            catch (Exception err)
            {
                result = ErrProcess.SetErrResult(err);
                m_err.SetErrCall(result);
            }
        }

        /*********************************
        * 
        *    Error Infomation Setting
        * 
        **********************************/
        private void OnErrOut(ERR_RESULT err)
        {
            m_err.ResetErr();//에러메세지 초기화

            //에러 정보 포맷
            StringBuilder makeStr = new StringBuilder();
            makeStr.Append("ErrorCode:");
            makeStr.Append(err.errCode);
            makeStr.Append(", Message:");
            makeStr.Append(err.message);
            makeStr.Append(", InnerErrCode:");
            makeStr.AppendLine(err.Inner_errCode.ToString());
            makeStr.Append("FuncName:");
            makeStr.AppendLine(err.funcName);
            //윈폼 Invoke로 변경
            if (this.InvokeRequired)
                this.Invoke(new SYSTEMERROR(OnMessageBox), new object[] { makeStr.ToString() });
            else
                OnMessageBox(makeStr.ToString());
        }

        /*********************************
        * 
        *    Error Message Output
        * 
        **********************************/
        private void OnMessageBox(String message)
        {
            MessageBox.Show(this, message);
        }

        /*********************************
        * 
        *          Form Close Event
        * 
        **********************************/
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            ERR_RESULT result = new ERR_RESULT();

            try
            {
                m_server.Shutdown(SocketShutdown.Both); //Socket ShutDown(Server and Client)
                m_server.Close(); //Server Close
            }
            catch (_MainException err)
            {
                result = ErrProcess.SetErrResult(err);
                m_err.SetErrCall(result);
            }
            catch (Exception err)
            {
                result = ErrProcess.SetErrResult(err);
                m_err.SetErrCall(result);
            }
        }
    }
}

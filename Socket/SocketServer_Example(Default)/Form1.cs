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

namespace SocketServer_Example_Default_
{
    public partial class Form1 : Form
    {
        /***********************************
         * 
         *        Delegate Function
         * 
         * *********************************/
        public delegate void del_thread(string message); //Form Control을 사용하기 위한 delegate 함수
        public delegate void del_status(string msg, int idx);
        public delegate void del_thread2(string msg);
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
        public string m_recvData; //Receive Data

        /*********************************
        * 
        *           Constructor 
        * 
        **********************************/
        public Form1()
        {
            InitializeComponent();
            m_SocketClass = new SocketServer();
            m_asyncObject = new AsyncObjectClass(1024);
            m_clientList = new List<Socket>();
            m_err = new ErrProcess();
            m_recvData = string.Empty;
        }

        private void CheckConnect()
        {
            while (true)
            {
                for (int i = 0; i < m_clientList.Count; i++)
                {
                    if (m_clientList[i].Connected)
                        this.BeginInvoke(new del_status(UpdataStatus), new object[] { "Connected Client", 1 });
                    else
                        this.BeginInvoke(new del_status(UpdataStatus), new object[] { "Waiting...", 2 });
                }

                Thread.Sleep(300);
            }
        }

        void UpdataStatus(string msg, int idx)
        {
            if (idx == 1)
                pictureBox1.BackColor = Color.Green;
            else
                pictureBox1.BackColor = Color.Red;

            lb_Status.Text = msg;
        }

        /**********************************
        * 
        *        Form Load Event
        * 
        **********************************/
        private void Form1_Load(object sender, EventArgs e)
        {
            ERR_RESULT result = new ERR_RESULT();

            try
            {
                m_SocketClass.CreateSocket(); //서버 소켓 생성
                m_server = m_SocketClass.Server; //생성된 소켓
                m_server.BeginAccept(new AsyncCallback(AcceptCallBack), m_server); //접속할 클라이언트를 비동기적으로 받을 준비
                Thread newThread = new Thread(() => CheckConnect());
                newThread.IsBackground = true;
                newThread.Start();
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

                m_server.BeginAccept(AcceptCallBack, null); //다른 클라이언트 접속 대기

                m_clientList.Add(client); //클라이언트 List에 추가

                m_asyncObject.WorkingSocket = client; //비동기 처리를 위한 클래스의 소켓에 클라이언트 저장

                client.BeginReceive(m_asyncObject.Buffer, 0, m_asyncObject.Size, 0, DataReceived, m_asyncObject); //Message 받을 준비
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
        *      Connect Status Invoke
        * 
        **********************************/
        void InputText(string msg)
        {
            ERR_RESULT result = new ERR_RESULT();

            try
            {
                this.BeginInvoke(new del_thread2(InputData), new object[] {msg});
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

        void InputData(string msg)
        {
            tx_log.Text += msg;
        }

        /*********************************
        * 
        *    Receive Call Back Function
        * 
        **********************************/
        void DataReceived(IAsyncResult ar)
        {
            ERR_RESULT result = new ERR_RESULT();

            try
            {
                m_asyncObject = (AsyncObjectClass)ar.AsyncState;
                Byte[] recvByte; //받은 데이터 바이트 배열

                int RecvSize = m_asyncObject.WorkingSocket.EndReceive(ar); //받은 메시지 크기
                recvByte = new Byte[RecvSize]; //크기만틈 배열 동적 할당

                Array.Copy(m_asyncObject.Buffer, recvByte, RecvSize);

                m_recvData = System.Text.Encoding.Default.GetString(recvByte); //Byte Array -> String

                WriteLogMsg(m_recvData); //받은 메시지 출력

                m_asyncObject.ClearBuffer(); //버퍼 비우기

                ReturnClientSignal(); //Message 보내는 함수 호출

                m_asyncObject.WorkingSocket.BeginReceive(m_asyncObject.Buffer, 0, m_asyncObject.Size, 0, DataReceived, m_asyncObject); //다시 메세지를 받을 준비
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
        *    Write Message Function
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

                msg = Convert.ToString(msgLog);

                this.Invoke(new del_thread(InputText), new object[] { msg }); //메시지 출력 함수 Invoke
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
        private void OnMessageBox(String message)
        {
            MessageBox.Show(this, message);
        }
    }
}

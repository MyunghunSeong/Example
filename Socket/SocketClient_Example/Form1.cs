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
using System.Threading.Tasks;
using System.Threading;
using SimpleInspect_template.Err;

namespace SocketClient_Example
{
    public partial class Form1 : Form
    {
        /*********************************
        * 
        *       Delegate Function
        * 
        **********************************/
        public delegate void SYSTEMERROR(String messge); //Form Control을 사용하기 위한 delegate 함수
        public delegate void del_thread(StringBuilder msg); //Error Process를 위한 delegate 함수
        public delegate void del_status(int idx, string msg);
        public delegate void del_thread2(StringBuilder msg, int idx);

        /*********************************
        * 
        *           Class Object
        * 
        **********************************/
        public ErrProcess m_err; //Class for Error Process
        public SocketClass m_clientClass; //Client Socket Create Class Object
        public AsyncObjectClass m_asyncObject; //Class for Async Procee

        /*********************************
        * 
        *         Global Variable
        * 
        **********************************/
        public Socket m_client; // Client Socket
        public Byte[] m_Sendbuffer; //Send Message Buffer
        public IPEndPoint m_remote; //Server Address & Port
        public string m_ip;  //Server ip
        public int m_port; //Port Number

        /*********************************
        * 
        *           Constructor
        * 
        **********************************/
        public Form1()
        {
            InitializeComponent();
            m_clientClass = new SocketClass();
            m_err = new ErrProcess();
            m_asyncObject = new AsyncObjectClass(1024);
            m_ip = string.Empty;
            m_port = 0;
        }

        /*********************************
        * 
        *         Form Load Event
        * 
        **********************************/
        private void Form1_Load(object sender, EventArgs e)
        {
            lb_status.Text = "Server Waiting...."; //연결 상태 메세지 설정
        }

        /*********************************
        * 
        *    Socket Connect Button Event
        * 
        **********************************/
        private void btn_connect_Click(object sender, EventArgs e)
        {
            ERR_RESULT result = new ERR_RESULT();

            try
            {
                m_ip = tx_Ip.Text;
                m_port = Int32.Parse(tx_Port.Text);

                m_clientClass.ConnectServer(m_ip, m_port); //사용자가 입력한 ip와 port번호로 접속
                m_client = m_clientClass.Client; //생성 소켓 객체
                m_remote = m_clientClass.RemoteEp; //생성 소켓 주소정보 
                m_client.BeginConnect(m_remote, new AsyncCallback(ConnectCallBack), m_client); //서버와 연결 준비    
                Thread newThread = new Thread(() => CheckConnect());
                newThread.IsBackground = true;
                newThread.Start();
   
                if (m_client.Connected)
                    lb_status.Text = "Connected to Server!"; //연결 상태 메세지 변경
                else
                    lb_status.Text = "Server Waiting....";
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

        void CheckConnect()
        {
            while (true)
            {
                if (m_client.Connected)
                    this.BeginInvoke(new del_status(UpdataStatus), new object[] { 1, "Connected Client" });
                else
                    this.BeginInvoke(new del_status(UpdataStatus), new object[] { 2, "Waiting..." });

                Thread.Sleep(300);
            }
        }

        void UpdataStatus(int idx, string msg)
        {
            if (idx == 1)
                lb_status.Text = msg;
            else
                lb_status.Text = msg;
        }

        /*********************************
        * 
        *    Connect Call Back Function
        * 
        **********************************/
        void ConnectCallBack(IAsyncResult ar)
        {
            ERR_RESULT result = new ERR_RESULT();

            try
            {
                m_client = (Socket)ar.AsyncState; 
                m_client.EndConnect(ar); //소켓 연결 완료
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
        *   Message Send Button Event
        * 
        **********************************/
        private void btn_Send_Click(object sender, EventArgs e)
        {
            ERR_RESULT result = new ERR_RESULT();

            try
            {
                string[] str;

                str = tx_msg.Text.Split(' ');

                m_Sendbuffer = new Byte[str.Length]; //버퍼 크기 동적 할당

                for (int i = 0; i < str.Length; i++)
                    m_Sendbuffer[i] = Convert.ToByte(str[i], 16); //사용자 입력 메시지(String -> Byte Array)

                tx_msg.Clear(); //사용자 입력 텍스트박스 지우기

                tx_msg.Focus(); //포커스를 텍스트박스로 지정

                m_client.BeginSend(m_Sendbuffer, 0, m_Sendbuffer.Length, 0, new AsyncCallback(SendCallBack), m_client); //서버에 메시지를 보낼 준비

                Receive(); //메시지 보내는 함수 호출
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
        *     Send Call Back Function
        * 
        **********************************/
        void SendCallBack(IAsyncResult ar)
        {
            ERR_RESULT result = new ERR_RESULT();

            try
            {
                m_client = (Socket)ar.AsyncState;
                int SentSize = m_client.EndSend(ar); //메시지 전송 완료
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
        *    Message Receive Function
        * 
        **********************************/
        void Receive()
        {
            ERR_RESULT result = new ERR_RESULT();

            try
            {
                m_asyncObject.WorkingSocket = m_client; //비동기 처리를 위해 소켓 객체를 대입

                m_client.BeginReceive(m_asyncObject.Buffer, 0, m_asyncObject.Size, 
                    0, new AsyncCallback(ReceiveCallBack), m_asyncObject); //메세지 받을 준비 
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
        *    Receive Call Back Event
        * 
        **********************************/
        void ReceiveCallBack(IAsyncResult ar)
        {
            ERR_RESULT result = new ERR_RESULT();

            try
            {
                m_asyncObject = (AsyncObjectClass)ar.AsyncState;
                m_client = m_asyncObject.WorkingSocket; 
                Byte[] RecvBuffer; //받은 메시지를 저장할 바이트 배열 
                StringBuilder stringbuild = new StringBuilder();

                int ReadByteSize = m_client.EndReceive(ar); //받은 데이터 크기

                string[] str = new string[ReadByteSize];

                RecvBuffer = new Byte[ReadByteSize]; //크기만큼 바이트 배열 동적 할당

                Array.Copy(m_asyncObject.Buffer, RecvBuffer, ReadByteSize); //ReadBuffer(전역)에 복사

                for (int i = 0; i < ReadByteSize; i++)
                    str[i] = Convert.ToString(RecvBuffer[i], 16);

                string msg = string.Empty;

                for (int i = 0; i < str.Length; i++)
                {
                    if (str[i].Length != 2)
                        msg += "0" + str[i] + " ";
                    else
                        msg += str[i] + " ";
                }

                stringbuild.Append("Response : " + msg + Environment.NewLine);           

                string str2 = string.Empty;

                for (int i = 7; i > 0; i--)
                    str2 += msg[msg.Length - i];

                if(str[7] == "1" || str[7] == "2" || str[7] == "3")
                    stringbuild.Append("Data : " + str2 + Environment.NewLine);

                if (ReadByteSize > 0) //들어온 메시지가 있는 경우
                    this.Invoke(new del_thread(InputText), new object[] { stringbuild }); //텍스트 입력
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
        *  Received Messge Input Function
        * 
        **********************************/
        void InputText(StringBuilder msg)
        {
            ERR_RESULT result = new ERR_RESULT();

            try
            {
                this.BeginInvoke(new del_thread2(InputData), new object[] { msg, 1 });
            }
            catch (_MainException err)
            {
                result = ErrProcess.SetErrResult(err);
                m_err.SetErrCall(result);
            }
            catch(Exception err)
            {
                result = ErrProcess.SetErrResult(err);
                m_err.SetErrCall(result);
            }
        }

        void InputData(StringBuilder msg, int idx)
        {
            if (idx == 1)
                tx_Recv.Text += msg;
            else
                tx_Recv.Clear();
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

        private void btn_clear_Click(object sender, EventArgs e)
        {
            this.BeginInvoke(new del_thread2(InputData), new object[] { null, 2 });
        }

        private void tx_msg_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btn_Send_Click(sender, e);
        }

        private void tx_Port_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btn_connect_Click(sender, e);
        }

    }
}

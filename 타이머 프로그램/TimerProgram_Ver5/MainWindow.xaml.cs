using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Timers;
using System.Threading;
using System.Windows.Threading;
using System.Diagnostics;

namespace TimerProgram
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        private TimerClass timeClass;
        private GetTimeClass getTimeClass = new GetTimeClass();
        private System.Timers.Timer m_timer;
        private string m_restTime;
        private Thread timeThread;
        private bool m_btnState;
        private bool m_isReset;

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = getTimeClass;
            m_restTime = string.Empty;
            m_btnState = true;
            m_isReset = false;
        }

        private void Grid_Initialized(object sender, EventArgs e)
        {
            pictureBox.Stroke = Brushes.Black;
            pictureBox.StrokeThickness = 2;
            pictureBox.Fill = Brushes.Ivory;
            text2.Focus();
        }

        private void inner_UIUpdate(int isEnd)
        {
            if (isEnd == 1)
            {
                btn.IsEnabled = false;
                pictureBox.Fill = Brushes.Green;
            }
            else if (isEnd == 2)
            {
                btn.IsEnabled = true;
                pictureBox.Fill = Brushes.Ivory;
                text2.Focus();
            }
            else
            {
                pictureBox.Fill = Brushes.Red;
            }
        }

        private void inner_BtnUpdate(bool isOntime)
        {
            SolidColorBrush brush = new SolidColorBrush(Color.FromArgb(255, 221, 221, 221));

            if (isOntime)
            {
                btn_on.IsChecked = false;
            }
            else
                btn_on.IsChecked = true;
        }

        private void Btn_Click(object sender, EventArgs args)
        {
            m_btnState = false;
            Application.Current.Dispatcher.Invoke((Action) (()=> inner_UIUpdate(3)));       

            int min = 0;

            if (text2.Text == "")
                text2.Text = "30";

            m_restTime = text2.Text;

            getTimeClass.SetRestTime(m_restTime);

            min = Convert.ToInt32(text2.Text);

            if (min == 0)
                min = 1;

            min = min * 60 * 1000;
            btn.IsEnabled = false;
            text2.Text = "";

            timeClass = new TimerClass(min, ref getTimeClass);
            timeClass.IsStop = false;
            timeClass.DataSetting += new TimerClass.EventDelegate(inner_UIUpdate);
            timeClass.TimerStart();
            m_timer = timeClass.Timer;
        }

        private void On_KeyDown(object sender, KeyEventArgs args)
        {
            if (args.Key == Key.Enter)
                Btn_Click(sender, args);
        }

        private void Btn_Reset_Click(object sender, EventArgs args)
        {
            m_isReset = true;

            Application.Current.Dispatcher.Invoke((Action)(() => inner_UIUpdate(2)));

            if (btn_on.IsChecked == true)
                Application.Current.Dispatcher.Invoke((Action)(() => inner_BtnUpdate(true)));
            else
                Application.Current.Dispatcher.Invoke((Action)(() => inner_BtnUpdate(false)));

            getTimeClass.RestTimer.Stop();
            getTimeClass.Rest = null;

            if(m_timer != null)
                m_timer.Stop();
        }

        private void OnTime_Click(object sender, EventArgs args)
        {
            bool isThread = true;
            if (btn_on.IsChecked == true)
            {
                Application.Current.Dispatcher.Invoke((Action)(() => inner_BtnUpdate(false)));
                timeThread = new Thread(() => inner_onTime(isThread));
                timeThread.Start();
            }
            else
            {
                if (m_btnState)
                    Application.Current.Dispatcher.Invoke((Action)(() => inner_UIUpdate(2)));
                else
                    Application.Current.Dispatcher.Invoke((Action)(() => inner_UIUpdate(3)));

                Application.Current.Dispatcher.Invoke((Action)(() => inner_BtnUpdate(true)));
                timeThread.Abort();
            }
        }

        private void inner_onTime(bool isThread)
        {
            while (isThread)
            {
                int minute = DateTime.Now.Minute;

                if (m_btnState && !m_isReset)
                    getTimeClass.Rest = (60 - minute).ToString();

                if (minute == 0)
                {
                    Application.Current.Dispatcher.Invoke((Action)(() => inner_UIUpdate(1)));
                    Process.Start(".\\resource\\RestTime.html");
                    if (m_btnState)
                        Application.Current.Dispatcher.Invoke((Action)(() => inner_UIUpdate(2)));
                    else
                        Application.Current.Dispatcher.Invoke((Action)(() => inner_UIUpdate(3)));
                    isThread = false;
                    Application.Current.Dispatcher.Invoke((Action)(() => inner_BtnUpdate(true)));
                    getTimeClass.Rest = null;
                    timeThread.Abort();  
                }
                else
                    continue;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (timeThread != null)
            {
                if (timeThread.ThreadState == System.Threading.ThreadState.Running)
                    timeThread.Abort();
            }
        }
    }
}

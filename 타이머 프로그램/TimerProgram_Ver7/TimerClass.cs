using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Windows;
using System.Windows.Media;
using System.Diagnostics;

namespace TimerProgram
{
    class TimerClass
    {
        public delegate void EventDelegate(int isEnd);
        public event EventDelegate DataSetting;

        private Timer m_timer;
        private int m_interval;
        private bool isStop;
        private GetTimeClass m_getTime;

        public TimerClass(int interval, ref GetTimeClass gettime)
        {
            m_timer = new Timer();
            m_interval = interval;
            isStop = false;
            m_getTime = gettime;
        }

        public TimerClass()
        {
            m_timer = new Timer();
        }

        public Timer Timer { get { return m_timer; } }
        public bool IsStop 
        { 
            get { return isStop; }
            set { isStop = value; }
        }

        public void TimerStart()
        {               
            m_timer.Interval = m_interval;
            m_timer.Elapsed += Timer_CallBack;
            m_timer.Start();
        }

        public void Timer_CallBack(object sender, ElapsedEventArgs args)
        {
            Application.Current.Dispatcher.Invoke((Action)(() => DataSetting(1)));
            m_timer.Stop();

            if (!isStop)
            {
                Process.Start(".\\resource\\RestTime.html");
                Application.Current.Dispatcher.Invoke((Action)(() => DataSetting(2)));
                m_getTime.Rest = "0";
            }
        }

        public void TimerStop()
        {
            m_timer.Stop();
            isStop = true;        
        }
    }
}

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

        public TimerClass(int interval)
        {
            m_timer = new Timer();
            m_interval = interval;
        }

        public Timer Timer { get { return m_timer; } }

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

            Process.Start(".\\resource\\RestTime.html");

            Application.Current.Dispatcher.Invoke((Action)(() => DataSetting(2)));
        }
    }
}

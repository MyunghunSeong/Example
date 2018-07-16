using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Threading;

namespace TimerProgram
{
    class GetTimeClass : INotifyPropertyChanged
    {
        private string now;
        private string rest;
        private DispatcherTimer rest_dispatcherTimer;

        public GetTimeClass()
        {
            now = DateTime.Now.ToString("hh:mm:ss");
            rest = string.Empty;

            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Tick += Timer_Tick;
            dispatcherTimer.Start();
        }

        public void SetRestTime(string restTime)
        {
            Rest = restTime;

            rest_dispatcherTimer = new DispatcherTimer();
            rest_dispatcherTimer.Interval = new TimeSpan(0, 1, 0);
            rest_dispatcherTimer.Tick += Timer_Tick_Rest;
            rest_dispatcherTimer.Start();
        }

        public void RestTImeStop(string restTime)
        {
            Rest = restTime;
            rest_dispatcherTimer.Stop();
        }

        public string Now
        {
            get { return now; }
            set { SetProperty(ref now, value, "Now"); }
        }

        public string Rest
        {
            get { return rest; }
            set { SetProperty(ref rest, value, "Rest"); }
        }

        private void SetProperty<T>(ref T field, T value, string propertyName)
        {
            if (!EqualityComparer<T>.Default.Equals(field, value))
            {
                field = value;
                var handler = PropertyChanged;
                if (handler != null)
                    handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void Timer_Tick(object sender, EventArgs args)
        {
            string time = DateTime.Now.ToString("hh:mm:ss");
            Now = time;
        }

        private void Timer_Tick_Rest(object sender, EventArgs args)
        {
            int restTime = Convert.ToInt32(rest) - 1;

            if (restTime <= 0)
            {
                restTime = 0;
                rest_dispatcherTimer.Stop();
            }
            else
                Rest = Convert.ToString(restTime);
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

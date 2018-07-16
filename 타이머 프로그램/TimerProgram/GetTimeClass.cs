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

        public GetTimeClass()
        {
            now = DateTime.Now.ToString("hh:mm:ss");
            rest = DateTime.Now.Add(TimeSpan.FromMinutes(30)).ToString("hh:mm:ss");

            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Tick += Timer_Tick;
            dispatcherTimer.Start();
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

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

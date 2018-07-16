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

namespace TimerProgram
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        private TimerClass timeClass;
        private GetTimeClass getTimeClass;
        private DispatcherTimer m_dispatcherTimer;
        private string m_restTime;

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new GetTimeClass();
            m_restTime = string.Empty;
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
                text2.Focus();
            }
            else
            {
                pictureBox.Fill = Brushes.Red;
            }
        }

        private void Btn_Click(object sender, EventArgs args)
        {
            Application.Current.Dispatcher.Invoke((Action) (()=> inner_UIUpdate(3)));
            //Application.Current.Dispatcher.Invoke((Action) (() => inner_SettingRestTime()));

            m_restTime = text2.Text;

            int min = 0;

            if (text2.Text == "")
                text2.Text = "30";

            min = Convert.ToInt32(text2.Text);

            if (min == 0)
                min = 1;

            min = min * 60 * 1000;
            btn.IsEnabled = false;
            text2.Text = "";

            timeClass = new TimerClass(min);
            timeClass.DataSetting += new TimerClass.EventDelegate(inner_UIUpdate);
            timeClass.TimerStart();
        }

        private void On_KeyDown(object sender, KeyEventArgs args)
        {
            if (args.Key == Key.Enter)
                Btn_Click(sender, args);
        }

        private void inner_SettingRestTime()
        {
            m_dispatcherTimer = new DispatcherTimer();
            m_dispatcherTimer.Interval = new TimeSpan(0, 1, 0);
            m_dispatcherTimer.Tick += Timer_Tick;
            m_dispatcherTimer.Start();
        }

        private void Timer_Tick(object sender, EventArgs args)
        {

        }
    }
}

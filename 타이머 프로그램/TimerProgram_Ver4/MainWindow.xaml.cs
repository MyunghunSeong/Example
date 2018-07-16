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
        private GetTimeClass getTimeClass = new GetTimeClass();
        private System.Timers.Timer m_timer;
        private string m_restTime;

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = getTimeClass;
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
                pictureBox.Fill = Brushes.Ivory;
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
            Application.Current.Dispatcher.Invoke((Action)(() => inner_UIUpdate(2)));
            getTimeClass.RestTImeStop("0");
            m_timer.Stop();
        }
    }
}

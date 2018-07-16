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
using System.Windows.Threading;

namespace TimerProgram
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new GetTimeClass();
        }

        private void Grid_Initialized(object sender, EventArgs e)
        {
            pictureBox.Stroke = Brushes.Black;
            pictureBox.StrokeThickness = 2;
            pictureBox.Fill = Brushes.Red;
          
            Timer timer = new Timer();
            timer.Interval = 1800000;
            timer.Start();
            timer.Elapsed += new ElapsedEventHandler((t, a) =>
                {
                    Application.Current.Dispatcher.Invoke((Action)(() => inner_UIUpdate(true)));
                    timer.Stop();
                    MessageBoxResult result = MessageBox.Show("쉬는 시간~~!!");

                    if (result == MessageBoxResult.OK)
                    {
                        timer.Start();
                        Application.Current.Dispatcher.Invoke((Action)(() => inner_UIUpdate(false)));
                    }
                }
            );
        }

        private void inner_UIUpdate(bool isEnd)
        {
            if(isEnd)
                pictureBox.Fill = Brushes.Green;
            else
                pictureBox.Fill = Brushes.Red;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ExtesionsMethods;


namespace ProgrammerTimer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainTimer mainTimer;
        int? workMinutes;
        int? restMinutes;
        public MainWindow()
        {
            mainTimer = new MainTimer();
            InitializeComponent();
            Task.Run(() => mainTimer.MainLoop());
            mainTimer.timeChange += UpdateTime;
            mainTimer.stateChange += UpdateColors;
            mainTimer.stateChange += AlertStateChange;
        }



        private async void Launch_Click(object sender, RoutedEventArgs e)
        {
            workMinutes = workTime.Text.ToInt();
            restMinutes = restTime.Text.ToInt();

            if (workMinutes == null || restMinutes == null)
            {
                MessageBox.Show("Введите числа в поля со временем", "Ошибка установки времени");
                return;
            }

            mainTimer.Work = new TimeSpan(0, workMinutes.Value, 0);
            mainTimer.Rest = new TimeSpan(0, restMinutes.Value, 0);

            mainTimer.State = TimerState.Work;
        }

        private void UpdateTime(TimeSpan time)
        {

            string minutes = time.Minutes.ToString();
            string seconds = time.Seconds.ToString();

            Dispatcher.Invoke(() =>
            {
                timer.Content = time.ToString(@"mm\:ss");
            });
        }

        private void UpdateColors(TimerState state)
        {

            System.Drawing.Color color = new System.Drawing.Color();
            switch (state)
            {
                case TimerState.Work:
                    color = System.Drawing.Color.DarkRed;
                    break;
                case TimerState.Rest:
                    color = System.Drawing.Color.DarkGreen;
                    break;
                case TimerState.Pause:
                    color = System.Drawing.Color.Gray;
                    break;
            }

            Dispatcher.Invoke(() =>
            {
                timer.Foreground = new SolidColorBrush(Color.FromRgb(color.R, color.G, color.B));
            });
        }
        private void AlertStateChange(TimerState state)
        {
            return;
        }
    }
}

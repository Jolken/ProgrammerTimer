using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
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

            workMinutes = workTime.Text.ToInt();
            restMinutes = restTime.Text.ToInt();
            mainTimer.Work = new TimeSpan(0, workMinutes.Value, 0);
            mainTimer.Rest = new TimeSpan(0, restMinutes.Value, 0);

            mainTimer.timeChange += UpdateTime;

            mainTimer.stateChange += UpdateColors;
            mainTimer.stateChange += AlertStateChange;
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
            if (mainTimer.lastState == TimerState.Pause || mainTimer.State == TimerState.Pause)
                return;
            Dispatcher.InvokeAsync(async() =>
            {
                Window1 window = new Window1();
                if (mainTimer.State == TimerState.Work)
                {
                    await window.SetStateText(System.Drawing.Color.DarkRed, "Работать");
                }
                else
                {
                    await window.SetStateText(System.Drawing.Color.DarkGreen, "Отдохнуть");
                }
            });
            

        }

        private async void Pause_Click(object sender, RoutedEventArgs e)
        {
            if (mainTimer.State != TimerState.Pause)
            {
                mainTimer.State = TimerState.Pause;
                pause.Content = "Возобновить";
            }
            else
            {
                mainTimer.State = mainTimer.lastState;
                pause.Content = "Пауза";
            }
            
            
        }

        private async void Apply_Click(object sender, RoutedEventArgs e)
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
        }
        private async void Launch_Click(object sender, RoutedEventArgs e)
        {
            mainTimer.State = TimerState.Work;
            mainTimer.Time = mainTimer.Work;

            launch.Content = "Рестарт";
            pause.IsEnabled = true;
        }

        private void WorkTime_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }
    }
}

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
using System.Windows.Shapes;

namespace ProgrammerTimer
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }
        public async Task SetStateText(System.Drawing.Color color, string text)
        {
            labelState.Foreground = new SolidColorBrush(Color.FromRgb(color.R, color.G, color.B));
            labelState.Content = text;
            await Dispatcher.Invoke(async() =>
            {
                this.Show();
                this.Activate();
                System.Media.SystemSounds.Exclamation.Play();
                await Task.Delay(5000);
                this.Close();
            });
        }
    }
}

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace MeioMundo.Editor.UsersControls
{
    /// <summary>
    /// Interaction logic for StatusBar.xaml
    /// </summary>
    public partial class StatusBar : UserControl
    {
        public static TextBlock TextBlock { get; set; }
        private int i = 0;
        DispatcherTimer timer;

        public StatusBar()
        {
            InitializeComponent();
            TextBlock = this.UI_TextBlock_BuildNumber;
            timer = new DispatcherTimer();
            UI_progressBar.ProgressBarCompleted += UI_progressBar_ProgressBarCompleted;
            timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void UI_progressBar_ProgressBarCompleted(object sender, EventArgs e)
        {
            timer.Stop();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            i++;
            UI_progressBar.SetBarValue(i);
        }

        public static void TestStatic(string text)
        {
            TextBlock.Text = text;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

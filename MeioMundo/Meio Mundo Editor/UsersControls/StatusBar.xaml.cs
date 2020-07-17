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
        public static TextBlock VersionText { get; set; }
        private int i = 0;
        DispatcherTimer timer;

        public StatusBar()
        {
            InitializeComponent();
            VersionText = this.UI_TextBlock_BuildNumber;
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        public static void SetVersionDisplay(string v)
        {
            VersionText.Text = v;
        }

        private void UI_progressBar_ProgressBarCompleted(object sender, EventArgs e)
        {
            timer.Stop();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            i++;
        }

        public static void TestStatic(string text)
        {
            VersionText.Text = text;
        }
        int id = 0;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            id += 10;
            if (id > 70)
                id = 20;
            UC_Progress.SetValue(id);
        }
    }
}

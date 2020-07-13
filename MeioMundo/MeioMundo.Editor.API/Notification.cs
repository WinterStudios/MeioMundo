using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using MeioMundo.Editor;

namespace MeioMundo.Editor.API
{
    public class Notification
    {
        private DispatcherTimer NotificationTimer { get; set; }
        public List<NotificationInformation> Notifications { get; set; }
        public DockPanel NotificationWindow { get; set; }
        private UserControl _not;

        public void ShowNotification(NotificationInformation notification)
        {
            StartTimer();
            UserControls.NotificationUserControl userControl = new UserControls.NotificationUserControl();
            userControl.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
            userControl.VerticalAlignment = System.Windows.VerticalAlignment.Bottom;
            userControl.Margin = new System.Windows.Thickness(20);
            userControl.Name = "notif";
            _not = userControl;
            NotificationWindow.Children.Add(userControl);
            NotificationWindow.Height = 100;
            NotificationWindow.Width = 200;
            NotificationWindow.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 0, 0));
        }
        private void CloseNotification()
        {
            NotificationWindow.Visibility = Visibility.Hidden;
            
        }

        private void StartTimer()
        {
            if (NotificationTimer == null)
                NotificationTimer = new DispatcherTimer(DispatcherPriority.Background);

            NotificationTimer.Interval = new TimeSpan(0, 0, 5);
            NotificationTimer.Tick += NotificationTimer_Tick;
            NotificationTimer.Start();
        }

        private void NotificationTimer_Tick(object sender, EventArgs e)
        {
            CloseNotification();
            NotificationTimer.Stop();
        }



        public void EnableNotifications()
        {
            Console.WriteLine("SDAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
        }
    }
    public struct NotificationInformation
    {
        public string Title { get; set; }
        public string Message { get; set; }
    }
}

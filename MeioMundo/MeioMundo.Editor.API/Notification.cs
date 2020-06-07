using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MeioMundo.Editor;

namespace MeioMundo.Editor.API
{
    public class Notification
    {
        public List<NotificationInformation> Notifications { get; set; }
        public DockPanel NotificationWindow { get; set; }

        public static void AddNotification(NotificationInformation notification)
        {

        }

        public void ShowNotification(NotificationInformation notification)
        {
            UserControls.NotificationUserControl userControl = new UserControls.NotificationUserControl();
            userControl.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
            userControl.VerticalAlignment = System.Windows.VerticalAlignment.Bottom;
            userControl.Margin = new System.Windows.Thickness(20);
            NotificationWindow.Children.Add(userControl);
            NotificationWindow.Height = 100;
            NotificationWindow.Width = 200;
            NotificationWindow.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 0, 0));
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

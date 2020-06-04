using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MeioMundo.Editor;

namespace MeioMundo.Editor.API
{
    public class Notification
    {
        public List<NotificationInformation> Notifications { get; set; }
        public Window MainWindow { get; set; }

        public static void AddNotification(NotificationInformation notification)
        {

        }

        public void ShowNotification(NotificationInformation notification)
        {
            UserControls.NotificationUserControl userControl = new UserControls.NotificationUserControl();
            userControl.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
            userControl.VerticalAlignment = System.Windows.VerticalAlignment.Bottom;
            userControl.Margin = new System.Windows.Thickness(20); 
            MainWindow.
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

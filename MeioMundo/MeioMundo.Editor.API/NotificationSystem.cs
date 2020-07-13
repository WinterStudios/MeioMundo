using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MeioMundo.Editor.API
{
    public static class NotificationSystem
    {
        public static DockPanel MainPanel { get; private set; }


        


        public static void Initialize(DockPanel UI_DockPanel_Notification)
        {
            MainPanel = UI_DockPanel_Notification;
        }

        private static UserControl CreateWindow()
        {
            Size size = new 
            Grid grid = new Grid();
            ColumnDefinition imageColumn = new ColumnDefinition();
        }
    }
}

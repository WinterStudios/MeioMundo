using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
using Tools;

namespace MeioMundoWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int d = 0;
        public MainWindow()
        {
            InitializeComponent();
            Console.WriteLine(System.IO.Directory.GetCurrentDirectory());
            var asm = Assembly.LoadFile(System.IO.Directory.GetCurrentDirectory() + "/Tools.dll");
            var types = asm.GetTypes().Where(x => x.IsSubclassOf(typeof(UserControl)));
            foreach (var item in types)
            {
                MenuItem i = new MenuItem();
                var menuItem = new MenuItem();
                menuItem.Click += (sender, e) =>
                {
                    object tool = Activator.CreateInstance(item);
                    Panel.Children.Add(tool as UserControl);
                };
                menuItem.Header = item.Name;
                MenuItemTools.Items.Add(menuItem);
            }            
        }

        private void Close_Application(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void MaximazeWindowEvent(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
                WindowState = WindowState.Maximized;
            else
                WindowState = WindowState.Normal;
        }
        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}

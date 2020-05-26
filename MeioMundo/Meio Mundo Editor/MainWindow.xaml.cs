using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
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
using MeioMundoEditor.API;
using MeioMundoEditor.UsersControls;
using MeioMundoEditor.UsersControls.Settings;


namespace Meio_Mundo_Editor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static TextBlock BottomBar_TextBloxk_Information { get; private set; }


        private bool m_windowStateMax = false;

        private struct Window
        {
            public struct Size
            {
                public static int MaxHeight { get; set; }
                public static int MaxWidth { get; set; }
                public static int WindowHeight { get; set; }
                public static int WindowWidth { get; set; }
            }
            public struct Location
            {
                public static int Left { get; set; }
                public static int Top { get; set; }
            }
            public static void SetWindowMaxArea(double height, double width)
            {
                Size.MaxHeight = (int)height;
                Size.MaxWidth = (int)width;
            }
            public static void SetSizeWindow(double height, double width)
            {
                Size.WindowHeight = (int)height;
                Size.WindowWidth = (int)width;
            }
            public static void SetLocation(double left, double top)
            {
                Location.Left = (int)left;
                Location.Top = (int)top;
            }
        }
        public MainWindow()
        {
            InitializeComponent();

            API.System.Initialize();


            InitializeUI();
        }
        private void InitializeUI()
        {
            UI_Button_MaxMin.DataContext = m_windowStateMax;
            Window.SetWindowMaxArea(SystemParameters.WorkArea.Height, SystemParameters.WorkArea.Width);
            Theme.ThemeManager.SetTheme(0);
            //UI_TextBlock_BuildNumber.Text = API.System.Version.CurrentBuild;
            this.UI_WebBrower_Welcome.Navigate(new Uri("https://winterstudios.github.io/HomeMedia/"));

            StatusBar.TestStatic(API.System.Version.CurrentBuild);

        }

        private void UI_MenuItem_Tema_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Menu_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine((sender as MenuItem).Role);
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine((sender as MenuItem).Role);
        }

        private void Exit_Application(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }


        private void DockPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
                WindowControl(!m_windowStateMax);
        }
        private void Window_DragMove(object sender, MouseButtonEventArgs e)
        {
            if (m_windowStateMax == true)
            {
                Window.Location.Top = -5;
                Window.Location.Left = (int)e.GetPosition(this).X - (Window.Size.WindowWidth / 2);
                WindowControl(!m_windowStateMax);
                DragMove();
            }
            else
                DragMove();
        }

        private void UI_Button_MaxMin_Click(object sender, RoutedEventArgs e)
        {
            WindowControl(!m_windowStateMax);
        }

        private void WindowControl(bool maximaze)
        {
            if (maximaze == true)
            {
                Window.SetSizeWindow(this.Height, this.Width);
                this.Width = Window.Size.MaxWidth;
                this.Height = Window.Size.MaxHeight;
                Window.SetLocation(this.Left, this.Top);
                this.Left = 0;
                this.Top = 0;
            }
            else
            {
                this.Left = Window.Location.Left;
                this.Top = Window.Location.Top;
                this.Width = Window.Size.WindowWidth;
                this.Height = Window.Size.WindowHeight;
            }
            m_windowStateMax = maximaze;
        }

        private void UI_MenuItem_Preferencias_Tema_Click(object sender, RoutedEventArgs e)
        {
            ColorTheme colorTheme = new ColorTheme();
            TabItem tab = new TabItem();
            tab.Content = colorTheme;
            tab.Header = "Colot Theme";
            UI_TabControl.Items.Add(tab);
            UI_TabControl.SelectedItem = tab;
        }
        private void OpenPopupWindow(object type)
        {

        }

        public static void DisplayInformation(string text, int time)
        {
            BottomBar_TextBloxk_Information.Text = text;
        }
    }
}

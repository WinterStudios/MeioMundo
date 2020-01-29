﻿using System;
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
        private static TabControl _tabControl;
        public static TabControl TabControl { get { return _tabControl; } }

        int d = 0;
        public double m_width;
        public double m_height;
        public static bool m_maximized;
        public MainWindow()
        {
            Debug.CheckLog();
            InitializeComponent();
            Debug.Log("[MAINWINDOW] " + "Initializing");
            _tabControl = tab_UI;
            m_width = this.Width;
            m_height = this.Height;
            var asm = Assembly.LoadFile(System.IO.Directory.GetCurrentDirectory() + "/Tools.dll");
            var types = asm.GetTypes().Where(x => x.IsSubclassOf(typeof(UserControl)));
            Debug.Log("[MAINWINDOW] " + "Loading Assemblies");
            foreach (var item in types)
            {
                try
                {
                    Debug.Log("[MAINWINDOW] " + "[" + item.Name + "] - Loading");
                    bool show = (bool)item.GetProperty("ShowMenu").GetValue(this, null);
                    string header = string.Empty;
                    try
                    {
                        if (item.GetProperty("Header").CanRead)
                            header = (string)item.GetProperty("Header").GetValue(this, null);
                        else
                            header = item.Name;
                    }
                    catch { }
                    if (show)
                    {
                        Debug.Log("[MAINWINDOW] " + "[" + item.Name + "] [MenuItem] - Loading ");
                        MenuItem i = new MenuItem();
                        var menuItem = new MenuItem();
                        menuItem.Click += (sender, e) =>
                        {
                            object tool = Activator.CreateInstance(item);
                            var tt = tool as UserControl;
                            tt.VerticalAlignment = VerticalAlignment.Stretch;
                            tt.HorizontalAlignment = HorizontalAlignment.Stretch;
                            TabItem tab = new TabItem();
                            tab.Content = tt;
                            tab.Header = header;
                            TabControl.Items.Add(tab);
                            TabControl.SelectedItem = tab;
                            Debug.Log("[MAINWINDOW] [LOADWINDOW] [" + menuItem.Header + "]");
                        };
                        menuItem.Header = item.Name;
                        MenuItemTools.Items.Add(menuItem);
                        Debug.Log("[MAINWINDOW] " + "[" + item.Name + "] [MenuItem] - Load");
                    }
                    Debug.Log("[MAINWINDOW] " + "[" + item.Name + "] - Load");
                }
                catch (Exception ex) 
                {
                    Debug.Error("[MAINWINDOW] [CLASS] - 'ShowMenu' not present");
                }
            }            
        }

        private void Close_Application(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void MaximazeWindowEvent(object sender, RoutedEventArgs e)
        {
            if (!m_maximized)
            {
                this.Height = SystemParameters.WorkArea.Height;
                this.Width = SystemParameters.WorkArea.Width;
                m_maximized = true;
                this.Left = 0;
                this.Top = 0;
            }
            else
            {
                this.Width = m_width;
                this.Height = m_height;
                m_maximized = false;
            }

        }
        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}

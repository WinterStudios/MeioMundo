using MeioMundo.Editor.API.Icon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MeioMundo.Editor.API
{
    public static class NotificationSystem
    {
        public static DockPanel MainPanel { get; private set; }
        public static DispatcherTimer Timer { get; private set; }
        public static TranslateTransform TranslateTransform { get; private set; }
        public static bool IsShowing { get; private set; }
        public static Grid GridContent { get; private set; }

        public static void Initialize(DockPanel UI_DockPanel_Notification, TranslateTransform translateTransform)
        {
            MainPanel = UI_DockPanel_Notification;
            TranslateTransform = translateTransform;
            MainPanel.RenderTransformOrigin = new Point(1, 1);
            
            CreateWindow();
            if (Timer == null)
                Timer = new DispatcherTimer();
            Timer.Interval = new TimeSpan(0, 0, 5);
            Timer.Tick += Timer_Tick;
            //Timer.Start();
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            Close();
            Timer.Stop();
        }

        private static void CreateWindow()
        {
            Size size = new Size(300, 150);
            
            MainPanel.Width = size.Width;
            MainPanel.Height = size.Height;
            
            MainPanel.Background = new SolidColorBrush(Color.FromRgb(40, 40, 40));
            Border border = new Border();

            GridContent = new Grid();
            MainPanel.Children.Add(border);
            border.Child = GridContent;
            //border.BorderThickness = new Thickness(1);
            //border.BorderBrush = new SolidColorBrush(Color.FromRgb(80, 80, 80));

            ColumnDefinition column1 = new ColumnDefinition();
            ColumnDefinition column2 = new ColumnDefinition();
            column1.Width = new GridLength(56, GridUnitType.Pixel);
            column2.Width = new GridLength(1, GridUnitType.Star);

            RowDefinition row1 = new RowDefinition();
            RowDefinition row2 = new RowDefinition();
            row1.Height = new GridLength(56, GridUnitType.Pixel);
            row2.Height = new GridLength(1, GridUnitType.Star);

            GridContent.ColumnDefinitions.Add(column1);
            GridContent.ColumnDefinitions.Add(column2);
            GridContent.RowDefinitions.Add(row1);
            GridContent.RowDefinitions.Add(row2);

            Image icon = new Image();
            icon.Width = 32;
            icon.Height = 32;
            icon.HorizontalAlignment = HorizontalAlignment.Center;
            icon.VerticalAlignment = VerticalAlignment.Center;
            GridContent.Children.Add(icon);
            Grid.SetRow(icon, 0);
            Grid.SetColumn(icon, 0);
            Icons.DarkTheme = true;
            icon.Source = Icons.GetImage(Icons.Icon.Download);


            StackPanel stackPanel_Header = new StackPanel();
            stackPanel_Header.Orientation = Orientation.Vertical;

            GridContent.Children.Add(stackPanel_Header);
            Grid.SetRow(stackPanel_Header, 0);
            Grid.SetColumn(stackPanel_Header, 1);

            TextBlock textBlock_Title = new TextBlock();
            textBlock_Title.Text = "Plugin Update";

            TextBlock textBlock_ = new TextBlock();



        }
        public static void Show()
        {
            IsShowing = true;
            DoubleAnimation doubleAnimation = new DoubleAnimation();
            doubleAnimation.From = 330;
            doubleAnimation.To = 0;
            doubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(1));
            doubleAnimation.EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut };
            TranslateTransform.BeginAnimation(TranslateTransform.XProperty, doubleAnimation);
        }
        private static void Close()
        {
            DoubleAnimation doubleAnimation = new DoubleAnimation();
            doubleAnimation.From = 0;
            doubleAnimation.To = 330;
            doubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(1));
            doubleAnimation.EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut };
            TranslateTransform.BeginAnimation(TranslateTransform.XProperty, doubleAnimation);

            IsShowing = false;
        }
    }
}

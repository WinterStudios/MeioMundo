using MeioMundo.Editor.API.Icon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
        /// <summary>
        /// Active Notification 
        /// </summary>
        public static Notification CurrentNotification { get; set; }
        

        private static List<Notification> NotificationsQueue { get; set; }

        private static NotificationControls NotificationControls { get; set; }


        public static void Initialize(DockPanel UI_DockPanel_Notification, TranslateTransform translateTransform)
        {
            CurrentNotification = new Notification();
            NotificationsQueue = new List<Notification>();
            MainPanel = UI_DockPanel_Notification;
            TranslateTransform = translateTransform;
            MainPanel.RenderTransformOrigin = new Point(1, 1);
            NotificationControls = new NotificationControls();
            CreateWindow();
            Timer = new DispatcherTimer();
            Timer.Interval = new TimeSpan(0, 0, 6);
            Timer.Tick += Timer_Tick;
            // Notification notification = new Notification { Icon = Icons.GetImage(Icons.Icon.Download), Title = "Teste Notification", Sender = typeof(NotificationSystem).FullName };
            // 
            // Show(notification);

            //Timer.Start();
        }


        private static void Timer_Tick(object sender, EventArgs e)
        {
            Close();
            Timer.Stop();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(1200);
            timer.Start();
            timer.Tick += (object obj, EventArgs arg) => {
                QUEUE(null);
                timer.Stop();
            };
            
        }

        private static void CreateWindow()
        {
            Size size = new Size(300, 150);
            TranslateTransform.X = 330;   
            MainPanel.Width = size.Width;
            //MainPanel.Height = size.Height;
            
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
            row2.Height = new GridLength(1, GridUnitType.Auto);

            GridContent.ColumnDefinitions.Add(column1);
            GridContent.ColumnDefinitions.Add(column2);
            GridContent.RowDefinitions.Add(row1);
            GridContent.RowDefinitions.Add(row2);



            NotificationControls.Icon = new Image();
            NotificationControls.Icon.Width = 32;
            NotificationControls.Icon.Height = 32;
            NotificationControls.Icon.HorizontalAlignment = HorizontalAlignment.Center;
            NotificationControls.Icon.VerticalAlignment = VerticalAlignment.Center;
            NotificationControls.Icon.Source = CurrentNotification.Icon;
            GridContent.Children.Add(NotificationControls.Icon);
            Grid.SetRow(NotificationControls.Icon, 0);
            Grid.SetColumn(NotificationControls.Icon, 0);



            StackPanel stackPanel_Header = new StackPanel();
            stackPanel_Header.Orientation = Orientation.Vertical;
            stackPanel_Header.VerticalAlignment = VerticalAlignment.Center;

            GridContent.Children.Add(stackPanel_Header);
            Grid.SetRow(stackPanel_Header, 0);
            Grid.SetColumn(stackPanel_Header, 1);

            NotificationControls.Title = new TextBlock();
            NotificationControls.Title.Text = CurrentNotification.Title;
            NotificationControls.Title.Foreground = (SolidColorBrush)Application.Current.Resources["WorkArea.Foreground"];

            NotificationControls.Origin = new TextBlock();
            NotificationControls.Origin.Text = CurrentNotification.Sender;
            NotificationControls.Origin.FontSize = 8;
            NotificationControls.Origin.Foreground = (SolidColorBrush)Application.Current.Resources["WorkArea.Foreground.Discrite"];


            stackPanel_Header.Children.Add(NotificationControls.Title);
            stackPanel_Header.Children.Add(NotificationControls.Origin);

            NotificationControls.Message = new TextBlock();
            NotificationControls.Message.Foreground = (SolidColorBrush)Application.Current.Resources["WorkArea.Foreground"];
            NotificationControls.Message.Text = CurrentNotification.Message;
            NotificationControls.Message.Margin = new Thickness(0, 0, 16, 16);
            NotificationControls.Message.TextWrapping = TextWrapping.WrapWithOverflow;
            Grid.SetColumn(NotificationControls.Message, 1);
            Grid.SetRow(NotificationControls.Message, 1);
            GridContent.Children.Add(NotificationControls.Message);


        }
        public static void PopUpWindow()
        {

            Timer.Start();
            NotificationControls.Icon.Source = CurrentNotification.Icon;
            NotificationControls.Title.Text = CurrentNotification.Title;
            NotificationControls.Origin.Text = CurrentNotification.Sender;
            NotificationControls.Message.Text = CurrentNotification.Message;

            IsShowing = true;
            DoubleAnimation doubleAnimation = new DoubleAnimation();
            doubleAnimation.From = 330;
            doubleAnimation.To = 0;
            doubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(1));
            doubleAnimation.EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut };
            TranslateTransform.BeginAnimation(TranslateTransform.XProperty, doubleAnimation);
        }
        public static void Show(Notification notification, [CallerMemberName]string sender = "")
        {
            if(notification.Sender == string.Empty)
                notification.Sender = sender;
            QUEUE(notification);
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

        private static void QUEUE(Notification notification)
        {
            if(notification == null)
            {
                if (NotificationsQueue.Count > 0)
                {
                    CurrentNotification = NotificationsQueue[0];
                    NotificationsQueue.RemoveAt(0);
                    PopUpWindow();
                }
                else
                    return;
            }
            else
            {
                NotificationsQueue.Add(notification); 
                if (NotificationsQueue.Count > 0 && !IsShowing)
                {
                    CurrentNotification = NotificationsQueue[0];
                    NotificationsQueue.RemoveAt(0);
                    PopUpWindow();
                }
                else
                    return;
            }
        }

    }

    public class Notification
    {
        public string Title { get; set; }
        public string Sender { get; set; }
        public string Message { get; set; }
        public BitmapImage Icon { get; set; }
    }
    class NotificationControls
    {
        public TextBlock Title { get; set; }
        public TextBlock Origin { get; set; }
        public TextBlock Message { get; set; }
        public Image Icon { get; set; }

        public void SetNotification(Notification notification)
        {
            Origin.Text = notification.Sender;
            Title.Text = notification.Title;
            Icon.Source = notification.Icon;
        }
    }
}

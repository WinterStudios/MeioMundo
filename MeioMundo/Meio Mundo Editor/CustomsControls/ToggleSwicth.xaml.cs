using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MeioMundoEditor.CustomsControls
{
    /// <summary>
    /// Interaction logic for ToggleSwicth.xaml
    /// </summary>
    public partial class ToggleSwicth : UserControl
    {


        #region DependyProperetys
        public bool SwicthState
        {
            get { return (bool)GetValue(SwicthStateProperty); }
            set { SetValue(SwicthStateProperty, value); }
        }
        // Using a DependencyProperty as the backing store for SwicthState.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SwicthStateProperty =
            DependencyProperty.Register("SwicthState", typeof(bool), typeof(ToggleSwicth), new PropertyMetadata(false, new PropertyChangedCallback(ToggleSwicthChanged)));

        private static void ToggleSwicthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Console.WriteLine("Old:{0},\nNew:{1}", e.OldValue, e.NewValue);
            ToggleSwicth toggle = (ToggleSwicth)d;
            toggle.UpdateUI((bool)e.NewValue);
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(ToggleSwicth), new PropertyMetadata(""));
        #endregion

        private SolidColorBrush BorderBackgroundOn { get => (SolidColorBrush)Application.Current.FindResource("ToggleSwicth.Border.Background.On"); }
        private SolidColorBrush BorderBackgroundOff { get => (SolidColorBrush)Application.Current.FindResource("ToggleSwicth.Border.Background.Off"); }
        public ToggleSwicth()
        {
            InitializeComponent();
            this.MouseDown += ToggleSwicth_MouseDown;
            this.Background = Brushes.Transparent;
        }

        public void SetValue(bool b)
        {
            SwicthState = !b;
            //UpdateUI(SwicthState);
        }

        private void UpdateUI(bool swicthState)
        {
            switch (swicthState)
            {
                case true:
                    text.Text = "On";
                    AnimationToggleSwicth(swicthState);
                    break;
                case false:
                    text.Text = "Off";                    
                    AnimationToggleSwicth(swicthState);
                    break;
            }
        }

        private void ToggleSwicth_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
                SetValue(SwicthState);
        }

        #region Animations

        private void AnimationToggleSwicth(bool b)
        {
            DoubleAnimation ballPosDoubleAnimation = new DoubleAnimation();
            ColorAnimation backgroundDoubleAnimation = new ColorAnimation();
            ThicknessAnimation borderThicknessDoubleAnimation = new ThicknessAnimation();
            Duration duration = new Duration(TimeSpan.FromMilliseconds(200));
            ballPosDoubleAnimation.Duration = duration;
            backgroundDoubleAnimation.Duration = duration;
            borderThicknessDoubleAnimation.Duration = duration;
            switch (b)
            {
                case true:
                    ballPosDoubleAnimation.From = 0;
                    ballPosDoubleAnimation.To = 20;
                    border.Background = new SolidColorBrush(BorderBackgroundOff.Color);
                    backgroundDoubleAnimation.To = BorderBackgroundOn.Color;
                    borderThicknessDoubleAnimation.To = new Thickness(0);
                    break;
                case false:
                    ballPosDoubleAnimation.From = 20;
                    ballPosDoubleAnimation.To = 0;
                    border.Background = new SolidColorBrush(BorderBackgroundOn.Color);
                    backgroundDoubleAnimation.To = BorderBackgroundOff.Color;
                    borderThicknessDoubleAnimation.To = new Thickness(1);
                    break;
            }
            //if(ball_transform.a)
            ball_transform.BeginAnimation(TranslateTransform.XProperty, ballPosDoubleAnimation);
            border.Background.BeginAnimation(SolidColorBrush.ColorProperty, backgroundDoubleAnimation);
            border.BeginAnimation(BorderThicknessProperty, borderThicknessDoubleAnimation);
        }

        #endregion
    }
}

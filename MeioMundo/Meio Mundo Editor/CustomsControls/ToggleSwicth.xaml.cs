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
            DependencyProperty.Register("SwicthState", typeof(bool), typeof(ToggleSwicth), new PropertyMetadata(false));

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
        }
        public void SetValue(bool b)
        {
            SwicthState = !b;
            UpdateUI(SwicthState);
        }

        private void UpdateUI(bool swicthState)
        {
            switch (swicthState)
            {
                case true:
                    text.Text = "On";
                    border.Background = BorderBackgroundOn;
                    border.BorderThickness = new Thickness(0);
                    AnimationToggleSwicth(swicthState);
                    break;
                case false:
                    text.Text = "Off";                    
                    border.Background = BorderBackgroundOff;
                    border.BorderThickness = new Thickness(1);
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
            DoubleAnimation sliceAnimation = new DoubleAnimation();
            Duration duration = new Duration(TimeSpan.FromMilliseconds(200));
            sliceAnimation.Duration = duration;
            switch (b)
            {
                case true:
                    sliceAnimation.From = 0;
                    sliceAnimation.To = 20; 

                    break;
                case false:
                    sliceAnimation.From = 20;
                    sliceAnimation.To = 0;
                    break;
            }
            ball_transform.BeginAnimation(TranslateTransform.XProperty, sliceAnimation);  
        }

        #endregion
    }
}

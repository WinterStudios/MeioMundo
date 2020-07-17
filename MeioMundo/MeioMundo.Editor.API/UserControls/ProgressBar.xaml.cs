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

namespace MeioMundo.Editor.API.UserControls
{
    /// <summary>
    /// Interaction logic for ProgressBar.xaml
    /// </summary>
    public partial class ProgressBar : UserControl
    {
        /// <summary>
        /// Percentagem
        /// </summary>
        public float Progress { get; private set; }

        private TimeSpan animationDuration { get { return TimeSpan.FromMilliseconds(1000); } }

        /// <summary>
        /// Custom Progress Bar
        /// </summary>
        public ProgressBar()
        {
            InitializeComponent();
            
            
        }
        /// <summary>
        /// Fill the Bar
        /// </summary>
        /// <param name="p">Value betwen 0 and 100</param>
        /// <example>Can be:40 or 87.4</example>
        public void SetValue(float p)
        {
            DoubleAnimation animation = new DoubleAnimation();
            animation.Duration = animationDuration;
            animation.EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut };
            animation.From = UC_Rect_Fill.ActualWidth;
            animation.To = UC_Border.ActualWidth * p / 100;

            UC_Rect_Fill.BeginAnimation(WidthProperty, animation);
            UC_Rect_Fill.SizeChanged += UC_Rect_Fill_SizeChanged;

        }

        private void UC_Rect_Fill_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UC_Text_Progress.Text = (UC_Rect_Fill.ActualWidth / UC_Border.ActualWidth * 100).ToString("00") + " %";
            if ((UC_Rect_Fill.ActualWidth / UC_Border.ActualWidth * 100) >= 99.5f)
                Finish();
        }
        private void Finish()
        {
            TranslateTransform translate = new TranslateTransform();
            UC_Grid.RenderTransform = translate;

            DoubleAnimation animation = new DoubleAnimation(-32, new Duration(TimeSpan.FromMilliseconds(500)));
            translate.BeginAnimation(TranslateTransform.YProperty, animation);
        }
    }
}

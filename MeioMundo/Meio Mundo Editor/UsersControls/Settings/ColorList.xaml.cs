using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MeioMundo.Editor.UsersControls.Settings
{
    /// <summary>
    /// Interaction logic for ColorList.xaml
    /// </summary>
    public partial class ColorList : UserControl
    {
        public static readonly DependencyProperty ObjectNameElement = DependencyProperty.Register("ObjectName", typeof(string), typeof(ColorList));
        public string ObjectName { get { return (string)GetValue(ObjectNameElement); } set { SetValue(ObjectNameElement, value); } }
        public static readonly DependencyProperty ObjectInformation = DependencyProperty.Register("Information", typeof(string), typeof(ColorList));
        public string Information { get { return (string)GetValue(ObjectInformation); } set { SetValue(ObjectInformation, value); } }
        public static readonly DependencyProperty ObjectColor = DependencyProperty.Register("Color", typeof(SolidColorBrush), typeof(ColorList));
        public SolidColorBrush Color { get { return (SolidColorBrush)GetValue(ObjectColor); } set { SetValue(ObjectColor, value); } }

        public ColorList()
        {
            InitializeComponent();
        }
    }
}

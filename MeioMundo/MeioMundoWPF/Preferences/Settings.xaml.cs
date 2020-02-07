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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MeioMundoWPF.Preferences
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : UserControl
    {

        public Theme.Themes _themes
        { 
            get
            {
                return (Theme.Themes)Properties.Settings.Default.Theme;
            }
            set
            {
                
            }
        }

        public Settings()
        {
            InitializeComponent();
            themeDropbox.ItemsSource = Enum.GetValues(typeof(Theme.Themes)).Cast<Theme.Themes>();
            themeDropbox.SelectedItem = _themes;
        }

        private void themeDropbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = themeDropbox.SelectedIndex;
            _themes = (Theme.Themes)index;
            Properties.Settings.Default.Theme = index;
            Properties.Settings.Default.Save();
            Theme.UpdateTheme(index);
        }
    }
}

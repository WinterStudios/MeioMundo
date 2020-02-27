using System;
using System.Linq;
using System.Windows.Controls;

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

using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public class Theme
    {
        public enum Themes
        {
            Light = 0,
            Dark = 1
        }
        public static Themes ThemesMode
        {
            get
            {
                int index = Properties.Settings.Default.Theme;
                Themes themes = (Themes)index;
                return themes;
            }
            set
            {
                Properties.Settings.Default.Theme = (int)value;
                Properties.Settings.Default.Save();
            }
        }

        public static void LoadTheme(int index)
        {
            Themes _themes = (Themes)index;
            ResourceDictionary dictionary = new ResourceDictionary();
            switch (_themes)
            {
                case Themes.Light:
                    dictionary.Source = new Uri("Themes/Light.xaml", UriKind.Relative);
                    Application.Current.Resources.MergedDictionaries.Add(dictionary);
                    break;
                case Themes.Dark:
                    dictionary.Source = new Uri("Themes/Dark.xaml", UriKind.Relative);
                    Application.Current.Resources.MergedDictionaries.Add(dictionary);
                    break;
                default:
                    break;
            }
        }

        public static void UpdateTheme(int index)
        {
            LoadTheme(index);

        }
        public static void UpdateTheme(Themes themes)
        {
            ResourceDictionary dictionary = new ResourceDictionary();
            switch (themes)
            {
                case Themes.Light:
                    dictionary.Source = new Uri("Light.xaml", UriKind.Relative);
                    Application.Current.Resources.MergedDictionaries.Add(dictionary);
                    break;
                case Themes.Dark:
                    dictionary.Source = new Uri("Light.xaml", UriKind.Relative);
                    Application.Current.Resources.MergedDictionaries.Add(dictionary);
                    break;
                default:
                    break;
            }
        }
    }
}


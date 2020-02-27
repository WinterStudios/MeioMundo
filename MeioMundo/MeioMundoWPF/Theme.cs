using System;
using System.Windows;

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


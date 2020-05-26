using System;
using System.Windows;

namespace MeioMundoEditor.Theme
{
    public class ThemeManager
    {
        public static void SetTheme(int index)
        {
            ResourceDictionary dictionary = new ResourceDictionary();
            dictionary.Source = new Uri("Theme/Default.xaml", UriKind.Relative);
            Application.Current.Resources.MergedDictionaries.Add(dictionary);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace MeioMundo.Editor.API.Icon
{
    public static class Icons
    {
        public static bool DarkTheme { get { return true; } }
        public static BitmapImage GetImage(Icon icon)
        {
            BitmapImage bitmapImage = new BitmapImage();

            string url = "pack://application:,,,/MeioMundo.Editor.API;component/Icons/";

            switch (icon)
            {
                case Icon.Download:
                    if (DarkTheme)
                        bitmapImage = new BitmapImage(new Uri(url + "download_light.png"));
                    else
                        bitmapImage = new BitmapImage(new Uri(url + "download_dark.png"));
                    break;
            }

            return bitmapImage;
        }

        public enum Icon
        {
            Download
        }
    }
}

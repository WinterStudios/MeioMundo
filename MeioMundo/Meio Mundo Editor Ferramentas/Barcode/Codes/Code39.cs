using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace MeioMundo.Editor.Ferramentas.Barcode.Codes
{
    public class Code39
    {
        public static System.Windows.Media.FontFamily Font
        {
            get
            {
                return new System.Windows.Media.FontFamily(new Uri("pack://application:,,,/MeioMundo.Editor.Ferramentas;Component/"), "./Barcode/Fonts/#Code 39");
            }
        }

        public Code39()
        {

        }
        public BitmapImage CreateImage(BarcodeInternal.CODE code, Size size)
        {
            Bitmap bitmap = new Bitmap(size.Width, size.Height);
            Graphics g = Graphics.FromImage(bitmap);

            g.DrawString(code.Descrição, new System.Drawing.Font("Calibri Light",12),Brushes.White,0,12);

            MemoryStream ms = new MemoryStream();
            ((System.Drawing.Bitmap)bitmap).Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            ms.Seek(0, SeekOrigin.Begin);
            image.StreamSource = ms;
            image.EndInit();
            return image;
        }

        void CreateGrid(double width, double height)
        {
            float desc_width_centerPoint = (float)width / 2;
            float desc_height;
            float bar_widht;
            float bar_height;
        }

    }
}

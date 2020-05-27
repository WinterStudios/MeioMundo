using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace MeioMundo.Editor.Ferramentas.Barcode
{
    public class BarcodeInternal
    {
        public enum CodeType
        {
            Code39,
            EAN13
        }
        public struct CODE
        {
            public string Code { get; set; }
            public string Descrição { get; set; }
            public CodeType CodeType { get; set; }
        }

        public BitmapImage GetImageCode(CODE code, Size size)
        {
            return null;
        }
    }
}

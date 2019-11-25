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

namespace Tools.Barcode
{
    /// <summary>
    /// Interaction logic for Barcode.xaml
    /// </summary>
    public partial class Barcode : UserControl
    {
        public BarcodeInternal.TypesOfCodes _TypesOfCodes;
        public Barcode()
        {
            InitializeComponent();
            LoadListOfCodeTypes();
        }

        private void LoadListOfCodeTypes()
        {
            foreach (string item in Enum.GetNames(typeof(BarcodeInternal.TypesOfCodes)))
            {
                RadioButton button = new RadioButton();
                button.Content = item;
                button.Checked += CodeTypeChange;
                ListOfCodeTypess.Children.Add(button);
            }
            _TypesOfCodes = BarcodeInternal.TypesOfCodes.Codebar;
        }

        private void Code_TextChanged(object sender, TextChangedEventArgs e)
        {

             Code_Priview.Text = DrawPreview(Code.Text, _TypesOfCodes);
        }

        private string DrawPreview(string data, BarcodeInternal.TypesOfCodes types)
        {
            string preview = string.Empty;

            switch (types)
            {
                case BarcodeInternal.TypesOfCodes.Codebar:
                    break;
                case BarcodeInternal.TypesOfCodes.Code_39:
                    preview = BarcodeInternal.CODE_39.GetCode(data);
                    break;
                case BarcodeInternal.TypesOfCodes.ISBN_10:
                    break;
                case BarcodeInternal.TypesOfCodes.ISNB_13:
                    break;
                case BarcodeInternal.TypesOfCodes.EAN_8:
                    break;
                case BarcodeInternal.TypesOfCodes.EAN_13:
                    break;
                default:
                    break;
            }

            return preview;
        }

        public void CodeTypeChange(object sender, RoutedEventArgs e)
        {
            var radioButton = sender as RadioButton;
            if (radioButton == null)
                return;
            string content = radioButton.Content.ToString();
            _TypesOfCodes = (BarcodeInternal.TypesOfCodes)Enum.Parse(typeof(BarcodeInternal.TypesOfCodes), content);

            switch (_TypesOfCodes)
            {
                case BarcodeInternal.TypesOfCodes.Codebar:
                    break;
                case BarcodeInternal.TypesOfCodes.Code_39:
                    Uri uri = new Uri("pack://application:,,,/Tools;Component/");
                    FontFamily font = new FontFamily(uri, "./Barcode/Fonts/#Code 39");
                    Code_Priview.FontFamily = font;
                    break;
                case BarcodeInternal.TypesOfCodes.ISBN_10:
                    break;
                case BarcodeInternal.TypesOfCodes.ISNB_13:
                    break;
                case BarcodeInternal.TypesOfCodes.EAN_8:
                    break;
                case BarcodeInternal.TypesOfCodes.EAN_13:
                    break;
                default:
                    break;
            }
        }
    }
}

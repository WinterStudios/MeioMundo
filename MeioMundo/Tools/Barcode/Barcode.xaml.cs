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

        public List<CodeInfo> _codes { get; set; }

        public Barcode()
        {
            _codes = new List<CodeInfo> { new CodeInfo { _produt = "Pro 1", _reference = "00001" }, new CodeInfo { _produt = "Pro 2", _reference = "00012" } };
            InitializeComponent();
            LoadListOfCodeTypes();
            table.ItemsSource = _codes;
            
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

            // Code_Priview.Text = DrawPreview(Code.Text, _TypesOfCodes);
            Code_Priview.Text = Code.Text.ToString();
        }

        private string DrawPreview(string data, BarcodeInternal.TypesOfCodes types)
        {
            string preview = string.Empty;

            switch (types)
            {
                case BarcodeInternal.TypesOfCodes.Codebar:
                    break;
                case BarcodeInternal.TypesOfCodes.Code_39:
                    Code.MaxLength = 100;
                    preview = BarcodeInternal.CODE_39.GetCode(data);
                    break;
                case BarcodeInternal.TypesOfCodes.ISBN_10:
                    break;
                case BarcodeInternal.TypesOfCodes.ISNB_13:
                    break;
                case BarcodeInternal.TypesOfCodes.EAN_8:
                    Code.MaxLength = 7;
                    if (Code.Text.Length == 7)
                        preview = BarcodeInternal.EAN.EAN_8.GetCode(Code.Text);
                    break;
                case BarcodeInternal.TypesOfCodes.EAN_13:
                    Code.MaxLength = 13;
                    if (Code.Text.Length == 13 || Code.Text.Length == 12)
                        preview = BarcodeInternal.EAN.EAN_13.GetCode(Code.Text);
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
                    Code_Priview.FontFamily = BarcodeInternal.CODE_39._FONT;
                    Code_Priview.Text = DrawPreview(Code.Text, _TypesOfCodes);
                    break;
                case BarcodeInternal.TypesOfCodes.ISBN_10:
                    break;
                case BarcodeInternal.TypesOfCodes.ISNB_13:
                    break;
                case BarcodeInternal.TypesOfCodes.EAN_8:
                    Code_Priview.FontFamily = BarcodeInternal.EAN._Font;
                    if (Code.Text.Length == 7)
                        Code_Priview.Text = BarcodeInternal.EAN.EAN_8.GetCode(Code.Text);
                    if (Code.Text.Length > 7)
                    {
                        Code.Text = Code.Text.Remove(7);
                        Code_Priview.Text = BarcodeInternal.EAN.EAN_8.GetCode(Code.Text);
                        Code_Priview.Text = DrawPreview(Code.Text, _TypesOfCodes);
                    }
                    break;
                case BarcodeInternal.TypesOfCodes.EAN_13:
                    Code_Priview.FontFamily = BarcodeInternal.EAN._Font;
                    if (Code.Text.Length == 13)
                        Code_Priview.Text = BarcodeInternal.EAN.EAN_13.GetCode(Code.Text);
                    if (Code.Text.Length > 13)
                    {
                        Code.Text = Code.Text.Remove(13);
                        Code_Priview.Text = BarcodeInternal.EAN.EAN_13.GetCode(Code.Text);
                        Code_Priview.Text = DrawPreview(Code.Text, _TypesOfCodes);
                    }
                    break;
                default:
                    break;
            }
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var windows = new Window1();
            windows.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            BarcodeInternal.Print();
        }
    }
}

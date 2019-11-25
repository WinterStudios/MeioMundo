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
                ListOfCodeTypess.Children.Add(button);
            }
        }

        private void Code_TextChanged(object sender, TextChangedEventArgs e)
        {
            Code_Priview.Text = "*" + Code.Text + "*";
        }
    }
}

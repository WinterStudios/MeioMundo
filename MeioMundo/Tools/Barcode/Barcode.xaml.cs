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
        public static bool ShowMenu { get { return true; } }
        public Barcode()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<Print.PrintManager.CODE> _codes = new List<Print.PrintManager.CODE>();
            _codes.Add(new Print.PrintManager.CODE { m_Descrição = "JIgfdresrasedrtf" });
            _codes.Add(new Print.PrintManager.CODE { m_Descrição = "Pen 57" }); 
            _codes.Add(new Print.PrintManager.CODE { m_Descrição = "guarda chuva de floes fd metaklivp rosa" });


            Print.PrintManager.CODE39 P = new Print.PrintManager.CODE39();
            P._CODES = _codes;
            P.PrintBarCodes();
        }
    }
}

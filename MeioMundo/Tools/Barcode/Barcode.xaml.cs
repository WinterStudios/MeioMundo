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
        public List<BarcodeInternal.Code> _Codes = new List<BarcodeInternal.Code>();
        public List<BarcodeInternal.Code> Codes { get { return _Codes; } set { _Codes = value; } }
        public Barcode()
        {
            InitializeComponent();
            refs_tabela.ItemsSource = Codes;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<Print.PrintManager.CODE> _codes = new List<Print.PrintManager.CODE>();
            foreach (var item in Codes)
            {
                _codes.Add(new Print.PrintManager.CODE { m_Descrição = item.DESCRIÇÂO, m_Referencia = item.CODIGO });
            }
            Print.PrintManager.CODE39 P = new Print.PrintManager.CODE39();
            P._CODES = _codes;
            P.PrintBarCodes();
        }

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            _Codes.Add(new BarcodeInternal.Code { ID = refs_tabela.Items.Count, CODIGO = txt_box_Ref.Text, DESCRIÇÂO = txt_box_Desc.Text, Qtd = int.Parse(txt_box_Qtd.Text) });
            refs_tabela.Items.Refresh();
        }
    }
}

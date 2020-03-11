using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System;

namespace Tools.Barcode
{
    /// <summary>
    /// Interaction logic for Barcode.xaml
    /// </summary>
    public partial class Barcode : UserControl
    {
        public static bool ShowMenu { get { return true; } }
        public static string Header { get { return "Codigo de Barras"; } }
        public List<BarcodeInternal.Code> _Codes = new List<BarcodeInternal.Code>();
        public List<BarcodeInternal.Code> Codes { get { return _Codes; } set { _Codes = value; } }

        public BarcodeInternal.TypesOfCodes _TypesOfCodes = BarcodeInternal.TypesOfCodes.Code_39;
        public List<string> _typesOfCode = new List<string>();
        public List<string> TYPESOFCODES { get { return _typesOfCode; } set { _typesOfCode = value; } }

        public Barcode()
        {
            InitializeComponent();
            refs_tabela.ItemsSource = Codes;
            LoadTypeOfCodes();

        }
        private void LoadTypeOfCodes()
        {
            UI_ComboBox_typeOfCode.ItemsSource = TYPESOFCODES;
            string[] types = Enum.GetNames(typeof(BarcodeInternal.TypesOfCodes));
            for (int i = 0; i < types.Length; i++)
            {
                string t_code = BarcodeInternal.GetTypeOfCode((BarcodeInternal.TypesOfCodes)i);
                TYPESOFCODES.Add(t_code);
            }
            UI_ComboBox_typeOfCode.SelectedIndex = (int)BarcodeInternal.TypesOfCodes.Code_39;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<Print.PrintManager.CODE> _codes = new List<Print.PrintManager.CODE>();
            foreach (var item in Codes)
            {
                if(item.Qtd == 1)
                    _codes.Add(new Print.PrintManager.CODE { m_Descrição = item.DESCRIÇÂO, m_Referencia = item.CODIGO });
                if(item.Qtd > 1)
                {
                    List<Print.PrintManager.CODE> _cc = new List<Print.PrintManager.CODE>();
                    for (int i = 0; i < item.Qtd; i++)
                    {
                        _cc.Add(new Print.PrintManager.CODE { m_Descrição = item.DESCRIÇÂO, m_Referencia = item.CODIGO });
                    }
                    _codes.AddRange(_cc);
                }
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

        private void PlusQTDButton_Click(object sender, RoutedEventArgs e)
        {
            int i = 0;
            string s = txt_box_Qtd.Text;
            int.TryParse(s, out i);
            i++;
            txt_box_Qtd.Text = i.ToString();
        }
        private void LessQTDButton_Click(object sender, RoutedEventArgs e)
        {
            int i = 0;
            string s = txt_box_Qtd.Text;
            int.TryParse(s, out i);
            i--;
            txt_box_Qtd.Text = i.ToString();
        }

        private void Add_Code_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void PreviewCode()
        {

        }
    }
}

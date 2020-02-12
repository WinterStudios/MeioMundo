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

namespace Tools.Site
{
    /// <summary>
    /// Interaction logic for Gestao.xaml
    /// </summary>
    public partial class Gestao : UserControl
    {
        public static bool ShowMenu { get { return true; } }
        public static string Header { get { return "Gestor"; } }


        // Lista de Produtos
        public List<Dados.Produto> _produtos = new List<Dados.Produto>();
        public List<Dados.Produto> Produtos { get { return _produtos; } set { _produtos = value; } }

        public Gestao()
        {
            InitializeComponent();
            tabela_produtos.ItemsSource = Produtos;
        }

        private void btn_Add_Prod_Click(object sender, RoutedEventArgs e)
        {
            ProductPreview productPreview = new ProductPreview();
            AddProdut addProdut = new AddProdut(productPreview);
            addProdut.SizeToContent = SizeToContent.WidthAndHeight;
            if(addProdut.ShowDialog() == true)
            {
                Dados.Produto produto = addProdut.product.Produto;
                Produtos.Add(produto);
                tabela_produtos.Items.Refresh();
            }
            
        }
    }
}

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
    /// Interaction logic for ProductPreview.xaml
    /// </summary>
    public partial class ProductPreview : UserControl
    {
        public Dados.Produto Produto;
        public ProductPreview()
        {
            InitializeComponent();
        }

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.OriginalSource != null)
            {
                TextBox box = (TextBox)e.OriginalSource;
                if (box.Text.Length > 0)
                    box.Background = new SolidColorBrush(Color.FromArgb(1, 0, 0, 0));
                else
                    box.Background = new SolidColorBrush(Color.FromRgb(200, 200, 200));
            }
        }

        public void Save()
        {
            Produto = new Dados.Produto();
            Produto.REF = _REF.Text;
            Produto.Nome = _NOME.Text;
        }
    }
}

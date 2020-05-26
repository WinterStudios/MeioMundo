using System.Windows.Controls;
using System.Windows.Media;

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
                    box.Background = new SolidColorBrush(Color.FromArgb(1, 2, 2, 2));
            }
        }

        public void Save()
        {
            Produto = new Dados.Produto();
            Produto.REF = _REF.Text;
            Produto.Nome = _NOME.Text;
            Produto.Preço = _PREÇO.Text;
            Produto.DescriçãoBreve = _DESCRIÇÃO_BREVE.Text;
        }

        private void _STOCK_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.OriginalSource != null)
            {
                TextBox box = (TextBox)e.OriginalSource;
                int n = 0;

                if (box.Text.Length < 1)
                {
                    box.Text = "0";
                    box.SelectionStart = 1;
                    return;
                }
                box.SelectionStart = box.Text.Length;
                if (box.Text[0] == '0')
                {
                    string txt = box.Text;
                    txt = txt.Remove(0, 1);
                    int.TryParse(txt, out n);
                }
                else
                    int.TryParse(box.Text, out n);

                _STOCK.Text = n.ToString();
                if (n < 1)
                {
                    _Stock_Info.Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                    _Stock_Info.Text = "Esgotado";
                }
                else
                {
                    _Stock_Info.Foreground = new SolidColorBrush(Color.FromRgb(0, 255, 0));
                    _Stock_Info.Text = "Em Stock";
                }
            }
        }
    }
}

using System.Windows;
using System.Windows.Controls;

namespace Tools.Site
{
    /// <summary>
    /// Interaction logic for AddProdut.xaml
    /// </summary>
    public partial class AddProdut : Window
    {
        public ProductPreview product;


        public AddProdut(UserControl userControl)
        {
            InitializeComponent();
            panel.Children.Add(userControl);
            if (userControl.GetType() == typeof(ProductPreview))
                product = (ProductPreview)userControl;
        }

        private void Button_Gravar_Click(object sender, RoutedEventArgs e)
        {
            if (product != null)
            {
                product.Save();
                this.DialogResult = true;
            }

        }

    }
}

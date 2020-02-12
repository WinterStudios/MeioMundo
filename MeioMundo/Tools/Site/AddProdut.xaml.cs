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
using System.Windows.Shapes;

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
            if(product != null)
            {
                product.Save();
                this.DialogResult = true;
            }
            
        }
       
    }
}

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
using Tools.Site;
using System.Data;

namespace Tools
{
    /// <summary>
    /// Interaction logic for Manager_Website.xaml
    /// </summary>
    public partial class Manager_Website : UserControl
    {
        public static bool ShowMenu { get { return true; } }
        public Manager_Website()
        {
            InitializeComponent();
        }


        private void ManagerProducts(object sender, RoutedEventArgs e)
        {
            TabItem tab = new TabItem();
            tab.Content = new ProductManager();
            tab.Header = "Gerir Produtos";
            tab.HorizontalContentAlignment = HorizontalAlignment.Stretch;
            tab.VerticalContentAlignment = VerticalAlignment.Stretch;
            tab.VerticalAlignment = VerticalAlignment.Stretch;
            TabManager.Items.Add(tab);
        }
    }
}

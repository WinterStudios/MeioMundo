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
using System.Data;
namespace Tools.Site
{
    /// <summary>
    /// Interaction logic for ProductManager.xaml
    /// </summary>
    /// 
    
    public partial class ProductManager : UserControl
    {
        public static bool ShowMenu = true;
        public ProductManager()
        {
            InitializeComponent();
        }
        private void OpenSiteFileWindow(object sender, RoutedEventArgs e)
        {
            string File = FileManager.Window.OpenFileWindowDialog(FileManager.Window.Extensions.CSV);

            SiteList.ItemsSource = FileManager.CSV.ReadFileToTable(File).AsDataView();

        }
    }
}

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
        public Manager_Website()
        {
            InitializeComponent();
        }

        private void OpenSiteFileWindow(object sender, RoutedEventArgs e)
        {
            string File = FileManager.Window.OpenFileWindowDialog(FileManager.Window.Extensions.CSV);

            SiteList.ItemsSource = FileManager.CSV.ReadFileToTable(File).AsDataView();


        }
        private void ManagerProducts(object sender, RoutedEventArgs e)
        {

        }
    }
}

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
        public static bool ShowMenu { get { return true; } }
        public static string Header { get { return "Fazer a Gestao do Site"; } }


        public ProductManager()
        {
            InitializeComponent();
            web.Source = new Uri("https://www.papelariameiomundo.com/wp-login.php");
            //web.Source = new Uri("https://www.whatismybrowser.com/");
            web.IsJavaScriptEnabled = true;

            web.ContentLoading += Web_ContentLoading;
            web.DOMContentLoaded += Web_DOMContentLoaded;

        }

        private void Web_DOMContentLoaded(object sender, Microsoft.Toolkit.Win32.UI.Controls.Interop.WinRT.WebViewControlDOMContentLoadedEventArgs e)
        {
           
            Console.WriteLine("dsa");
            string[] cmd = new string[] {

                "document.getElementById('user_login').value = 'Meiomundo';"+
                "document.getElementById('user_pass').value = 'Mundo19!#$';"
            };

            web.InvokeScriptAsync("eval", cmd);

            System.Threading.Thread.Sleep(500);
            string login = "document.getElementById('wp-submit').click();";
            web.InvokeScriptAsync("eval", login);

            if (e.Uri.PathAndQuery == "/wp-admin/")
            {
                web.Navigate(new Uri("https://www.papelariameiomundo.com/wp-admin/edit.php?post_type=product"));
            }



        }

        private void Web_ContentLoading(object sender, Microsoft.Toolkit.Win32.UI.Controls.Interop.WinRT.WebViewControlContentLoadingEventArgs e)
        {

        }

        private void OpenSiteFileWindow(object sender, RoutedEventArgs e)
        {
            string File = FileManager.Window.OpenFileWindowDialog(FileManager.Window.Extensions.CSV);


        }
    }
}

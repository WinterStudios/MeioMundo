using System;
using System.CodeDom;
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

namespace MeioMundoEditor.Styles
{
    public partial class TabControl : ResourceDictionary
    { 
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is TabItem item)
            {
                var tabControl = (System.Windows.Controls.TabControl)item.Parent;
                tabControl.Items.Remove(item);
            }
        }
    }
}

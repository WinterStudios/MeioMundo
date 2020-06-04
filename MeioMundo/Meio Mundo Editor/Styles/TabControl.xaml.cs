using System.Windows;
using System.Windows.Controls;

namespace MeioMundo.Editor.Styles
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

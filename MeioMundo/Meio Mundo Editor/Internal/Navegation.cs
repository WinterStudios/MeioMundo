using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MeioMundo.Editor.Internal
{
    public class Navegation
    {
        public static Menu NavegationMenu { get; set; }
        /// <summary>
        /// Create a new menu item
        /// </summary>
        /// <param name="address">the path of menu<para>Ex: Tools/Options</para></param>
        public static void AddMenu(string address, Type type)
        {
            string[] dirs = address.Split('/');
            int _treeIndex = 0;
            MenuItem[] menuItems = NavegationMenu.Items.Cast<MenuItem>().ToArray();
            int menuItems_index = 0;
            while (menuItems_index <= menuItems.Length && _treeIndex < dirs.Length)
            {
                if(menuItems_index == menuItems.Length)
                {
                    MenuItem _NewItem = new MenuItem();
                    _NewItem.Header = dirs[_treeIndex];
                }
                if (menuItems[menuItems_index].Header.ToString() == dirs[_treeIndex])
                {
                    _treeIndex++;
                    menuItems = menuItems[menuItems_index].Items.Cast<MenuItem>().ToArray();
                    menuItems_index = 0;
                    Console.WriteLine("Exits - {0}", menuItems[menuItems_index].Header.ToString());
                }
                return;
                //else
                //{
                //    MenuItem _NewItem = new MenuItem();
                //    _NewItem.Header = dirs[_treeIndex];
                //    if(dirs.Length == _treeIndex - 1)
                //    {
                //        _NewItem.Click += (sender, e) =>
                //        {
                //            object content = Activator.CreateInstance(type);
                //            TabItem tab = new TabItem();
                //        };
                //    }
                //    menuItems[menuItems_index].Items.Add(_NewItem);
                //    menuItems_index++;

                //}
            }
        }

    }
}

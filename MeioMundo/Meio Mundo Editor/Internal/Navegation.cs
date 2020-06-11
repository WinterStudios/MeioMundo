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
        public static TabControl TabControl { get; internal set; }

        /// <summary>
        /// Create a new menu item
        /// </summary>
        /// <param name="address">the path of menu<para>Ex: Tools/Options</para></param>
        public static void AddMenu(string address, Type type)
        {
            string[] dirs = address.Split('/');
            int _treeIndex = 0;

            List<MenuItem> menuItems = NavegationMenu.Items.Cast<MenuItem>().ToList();
            MenuItem parentMenuItem = new MenuItem();
            while (_treeIndex != dirs.Length)
            {
                for (int i = 0; i < menuItems.Count; i++)
                {
                    if((string)menuItems[i].Header == dirs[_treeIndex])
                    {
                        parentMenuItem = menuItems[i];
                        _treeIndex++;
                        menuItems = menuItems[i].Items.Cast<MenuItem>().ToList();

                        continue;
                    }    
                }
                MenuItem newMenu = new MenuItem();
                newMenu.Header = dirs[_treeIndex];
                parentMenuItem.Items.Add(newMenu);


                if(_treeIndex == dirs.Length - 1)
                {
                    newMenu.Click += (sender, e) =>
                    {
                        object content = new object();
                        try
                        {
                            Activator.CreateInstance(type);
                        }
                        catch (Exception ex)
                        {
                            content = ex.Message;
                        }
                        TabItem tab = new TabItem();
                        tab.Content = content;
                        tab.Header = dirs[dirs.Length - 1];
                        TabControl.Items.Add(tab);
                        TabControl.SelectedItem = tab;
                        
                    };
                }
                _treeIndex++;
            }
            

            // -> Add Menut item on finish the tree loop

            //while (menuItems_index < menuItems.Length)
            //{
                //if (menuItems[menuItems_index].Header.ToString() == dirs[_treeIndex])
                //{
                    //_treeIndex++;
                    //menuItems = menuItems[menuItems_index].Items.Cast<MenuItem>().ToArray();
                    //menuItems_index = 0;
                    //Console.WriteLine("Exits - {0}", menuItems[menuItems_index].Header.ToString());
                //}
                //else
                //{
                    //MenuItem _NewItem = new MenuItem();
                    //_NewItem.Header = dirs[_treeIndex];
                    //_NewItem.Click += (sender, e) =>
                    //{
                        //object content = Activator.CreateInstance(type);
                        //TabItem tab = new TabItem();
                    //};
                    //menuItems[menuItems_index].Items.Add(_NewItem);
                //}
                //menuItems_index++;
            //}
        }
        public static bool Exits(string name, int index)
        {
            MenuItem[] menuItems = NavegationMenu.Items.Cast<MenuItem>().ToArray();
            if (index == 0)
            {
                bool exits = NavegationMenu.Items.Cast<MenuItem>().Any<MenuItem>(x => (string)x.Header == name);
                return exits;
            }
            else
                return false;
        }
    }
}

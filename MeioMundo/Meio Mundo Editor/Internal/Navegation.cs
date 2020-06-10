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
        public static void AddMenu(string address, object obj)
        {
            string[] dirs = address.Split('/');

            for (int i = 0; i < dirs.Length; i++)
            {
                Console.WriteLine(NavegationMenu.ToString());
            }
        }

    }
}

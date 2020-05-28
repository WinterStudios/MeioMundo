using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
using MeioMundo.Editor.API;
using MeioMundoEditor.API.Plugin;

namespace MeioMundoEditor.UsersControls
{
    /// <summary>
    /// Interaction logic for PlugingManager.xaml
    /// </summary>
    public partial class PlugingManager : UserControl
    {
        public PlugingManager()
        {
            InitializeComponent();
            
        }
        public void LoadPlugins()
        {
            string currentPath = Directory.GetCurrentDirectory();
            string[] dlls = Directory.GetFiles(currentPath).Where(c => c.Contains(".dll")).ToArray();

            for (int i = 0; i < dlls.Length; i++)
            {
                Assembly asm = Assembly.LoadFile(dlls[i]);
                var types = asm.GetTypes();
                foreach (var item in types)
                {
                    var cs = item.GetInterfaces();
                    Console.WriteLine("ss");
                }
            }
        }
    }
}

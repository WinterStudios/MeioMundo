using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MeioMundoEditor.API.Plugin
{
    public class PluginManager
    {
        public static string PluginPath { get => Directory.GetCurrentDirectory() + "/Plugins"; }
        public PluginManager()
        {
            LoadPlugins();
        }
        public void LoadPlugins()
        {
            string[] dlls = Directory.GetFiles(PluginPath).Where(c => c.Contains(".dll")).ToArray();
            var type = typeof(IPlugin);
            Console.WriteLine("D");
            PluginInfo plugin = new PluginInfo();
            for (int i = 0; i < dlls.Length; i++)
            {
                Assembly asm = Assembly.LoadFile(dlls[i]);
                var types = asm.GetTypes().Where(x => x.GetInterfaces().Contains(typeof(IPlugin)));                     // -----> Pode ser Interface mas tudos os metedos e parametros tem que estar presentes na class que implementa a interface

                foreach (var plug in types)
                {
                    string namePlugin = (string)plug.GetProperty("Nome").GetValue(this, null);
                }
                

            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace MeioMundoEditor.API.Plugin
{
    public class PluginManager
    {
        public static List<PluginInfo> Plugins = new List<PluginInfo>();
        /// <summary>
        /// Return C:/User/[userame]/AppData/Local/Meio Mundo/Editor/Plugins.json
        /// </summary>
        public static string PluginManagerFile { get => Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\MeioMundo\\Editor\\Plugins.json"; }
        /// <summary>
        /// Return C:/User/[userame]/AppData/Local/Meio Mundo/Editor/
        /// </summary>
        public static string AppPluginPath { get => Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData); }
        public static string PluginPath { get => Directory.GetCurrentDirectory() + "/Plugins/"; }
        public PluginManager()
        {
            LoadPlugins();
        }

        /// <summary>
        /// Check if exist plugins with IPlugin implement and list
        /// </summary>
        public void LoadPlugins()
        {
            string[] dlls = Directory.GetFiles(PluginPath).Where(c => c.Contains(".dll")).ToArray();
            var type = typeof(IPlugin);
            for (int i = 0; i < dlls.Length; i++)
            {
                Assembly asm = Assembly.LoadFile(dlls[i]);
                var types = asm.GetTypes().Where(x => x.GetInterfaces().Contains(typeof(IPlugin)));                     // -----> Pode ser Interface mas tudos os metedos e parametros tem que estar presentes na class que implementa a interface

                PluginInfo info = new PluginInfo();
                List<PluginClass> classes = new List<PluginClass>();

                string t_name = asm.FullName;
                info.AssemblyName = t_name.Remove(t_name.IndexOf(','));

                string t_version = t_name.Substring(t_name.IndexOf(',') + 10, 7);
                info.Version = t_version;

                foreach (var plug in types)
                {
                    var t = Activator.CreateInstance(plug);

                    PluginClass pluginClass = new PluginClass();

                    pluginClass.Nome = (string)t.GetType().GetProperty("Nome").GetValue(t);
                    pluginClass.Descrição = (string)t.GetType().GetProperty("Descrição").GetValue(t);
                    pluginClass.Type = (PluginType)t.GetType().GetProperty("Type").GetValue(t);
                    pluginClass.TypeArgs = (string[])t.GetType().GetProperty("Args").GetValue(t);
                    pluginClass.Versao = (string)t.GetType().GetProperty("Versão").GetValue(t);
                    pluginClass.Class = plug;

                    classes.Add(pluginClass);
                }
                info.PluginClasses = classes.ToArray();
                Plugins.Add(info);
            }
            LoadSettings();
        }
        /// <summary>
        /// Load the setting of plugins
        /// </summary>
        public static void LoadSettings()
        {
            if (!Storage.Files.Exists(PluginManagerFile))
                return;

            PluginMangerJson json = new PluginMangerJson();
            json = (PluginMangerJson)Storage.Json.GetJsonData<PluginMangerJson>(PluginManagerFile);

            for (int i = 0; i < json.PluginMangers.Length; i++)
            {
                for (int z = 0; z < Plugins.Count; z++)
                {
                    var plug = json.PluginMangers[i];
                    //if(plug.GUID == Plugins[i].PluginType.GUID)
                    //    Plugins[i].Enable = plug.Enable;
                }
            }

        }
        /// <summary>
        /// Save the setting of plugins
        /// </summary>
        public static void SaveSetting()
        {
            PluginMangerJson t_json = new PluginMangerJson();
            List<PluginMangerInfomationJson> t_plugins = new List<PluginMangerInfomationJson>();
            for (int i = 0; i < Plugins.Count; i++)
            {
                //t_plugins.Add(new PluginMangerInfomationJson { Name = Plugins[i].Nome, Assembly = Plugins[i].AssemblyName, Enable = Plugins[i].Enable, Version = Plugins[i].Versão, GUID = Plugins[i].PluginType.GUID });
            }
            t_json.PluginMangers = t_plugins.ToArray();
            Storage.Json.SaveJsonFile(PluginManagerFile, t_json);
        }
    }

    public class PluginMangerJson
    {
        public PluginMangerInfomationJson[] PluginMangers { get; set; }
    }
    public class PluginMangerInfomationJson
    {
        public string Name { get; set; }
        public string Assembly { get; set; }
        public bool Enable { get; set; }
        public string Version { get; set; }
        public Guid GUID { get; set; }
        
        
    }
}

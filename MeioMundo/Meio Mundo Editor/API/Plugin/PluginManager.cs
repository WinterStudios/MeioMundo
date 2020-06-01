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
                
                foreach (var plug in types)
                {
                    var t = Activator.CreateInstance(plug);

                    PropertyInfo pluginInfoNome = t.GetType().GetProperty("Nome");
                    PropertyInfo pluginInfoDescrição = t.GetType().GetProperty("Descrição");
                    PropertyInfo pluginInfoVersion = t.GetType().GetProperty("Versão");

                    PluginInfo pluginInfo = new PluginInfo();

                    pluginInfo.Nome = (string)pluginInfoNome.GetValue(t, null);
                    pluginInfo.Descrição = (string)pluginInfoDescrição.GetValue(t, null);
                    pluginInfo.Versão = (string)pluginInfoVersion.GetValue(t, null);
                    pluginInfo.PluginType = t.GetType();
                    pluginInfo.AssemblyName = plug.Assembly.FullName;

                    Plugins.Add(pluginInfo);
                }
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
                    if(plug.GUID == Plugins[i].PluginType.GUID)
                        Plugins[i].Enable = plug.Enable;
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
                t_plugins.Add(new PluginMangerInfomationJson { Name = Plugins[i].Nome, Assembly = Plugins[i].AssemblyName, Enable = Plugins[i].Enable, Version = Plugins[i].Versão, GUID = Plugins[i].PluginType.GUID });
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

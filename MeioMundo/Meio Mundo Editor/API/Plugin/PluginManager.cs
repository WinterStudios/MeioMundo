using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Policy;
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
        public static string AppLocalPath { get => Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\MeioMundo\\Editor\\"; }
        public static string PluginPath { get => Directory.GetCurrentDirectory() + "/Plugins/"; }
        public string[] GetDLLs { get { return Directory.GetFiles(PluginPath).Where(c => c.Contains(".dll")).ToArray(); } }

        /// <summary>
        /// Url from there the release is host on GitHub
        /// </summary>
        public string[] URLs { 
            get {
                if (File.Exists(AppLocalPath + "PluginsUrls.json"))
                    return (string[])Storage.Json.GetJsonData<string[]>(AppLocalPath + "PluginsUrls.json");
                else
                    return new string[0];
            }
        }
        public List<PluginAssemblyInfo> PluginOnlineAssemblyInfos { get; set; }

        public PluginManager()
        {
            //LoadPlugins();
            GetPluginsInfo();

        }

        private void GetPluginsInfo()
        {
            //
            for (int i = 0; i < URLs.Length; i++)
            {

            }
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
                AssemblyName assembly = AssemblyName.GetAssemblyName(dlls[i]);
                AppDomain domain = AppDomain.CreateDomain("ds"); 
                Assembly asm = domain.Load(assembly); // .Load(dlls[i]);
                var types = asm.GetTypes().Where(x => x.GetInterfaces().Contains(typeof(IPlugin)));                     // -----> Pode ser Interface mas tudos os metedos e parametros tem que estar presentes na class que implementa a interface
                Guid asmGuid = Guid.Parse(asm.CustomAttributes.First(x => x.AttributeType.Name == "GuidAttribute").ConstructorArguments[0].Value.ToString());
                PluginInfo info = new PluginInfo();
                List<PluginClass> classes = new List<PluginClass>();

                string t_name = asm.FullName;
                info.AssemblyName = t_name.Remove(t_name.IndexOf(','));

                string t_version = t_name.Substring(t_name.IndexOf(',') + 10, 7);
                info.Version = t_version;
                info.GUID = asmGuid;
                info.Location = dlls[i];
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
            EnablePlugins();
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
                    if(plug.GUID == Plugins[i].GUID)
                    {
                        Plugins[i].AssemblyEnable = plug.Enable;
                    }
                        
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
                t_plugins.Add(new PluginMangerInfomationJson { Assembly = Plugins[i].AssemblyName, Enable = Plugins[i].AssemblyEnable, GUID = Plugins[i].GUID});
            }
            t_json.PluginMangers = t_plugins.ToArray();
            Storage.Json.SaveJsonFile(PluginManagerFile, t_json);
        }

        private void EnablePlugins()
        {
            for (int i = 0; i < Plugins.Count; i++)
            {

            }
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
    public struct PluginAssemblyInfo 
    { 
        public string Name { get; set; }
        public string Path { get; set; }
        public Version Version { get; set; }
    }
}

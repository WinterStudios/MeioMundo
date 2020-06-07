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
using MeioMundo.Editor.API.Plugin;
using Octokit;
using System.Net;
using System.Runtime.Remoting.Channels;

namespace MeioMundo.Editor.Internal.Plugin
{
    public class PluginManager
    {
        #region Propriedades
        public static List<PluginInfo> Plugins = new List<PluginInfo>();
        public static List<AssemblyName> LocalPluginsAsmNames { get; set; }
        /// <summary>
        /// Return C:/User/[userame]/AppData/Local/Meio Mundo/Editor/Plugins.json
        /// </summary>
        public static string PluginManagerFile { get => Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\MeioMundo\\Editor\\Plugins.json"; }
        /// <summary>
        /// Return C:/User/[userame]/AppData/Local/Meio Mundo/Editor/
        /// </summary>
        public static string AppLocalPath { get => Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\MeioMundo\\Editor\\"; }
        /// <summary>
        /// Return -> C:/[path.exe]/Plugins/
        /// </summary>
        public static string PluginPath { get => Directory.GetCurrentDirectory() + "/Plugins/"; }
        public static string[] GetDLLs { get { return Directory.GetFiles(PluginPath).Where(c => c.Contains(".dll")).ToArray(); } }

        /// <summary>
        /// Url from there the release is host on GitHub
        /// </summary>
        public static string[] URLs { 
            get {
                if (File.Exists(AppLocalPath + "PluginsUrls.json"))
                    return (string[])Storage.Json.GetJsonData<string[]>(AppLocalPath + "PluginsUrls.json");
                else
                {
                    Storage.Json.SaveJsonFile(AppLocalPath + "PluginsUrls.json", (object)new string[] { "" });
                    return new string[0];
                }
            }
        }

        public List<PluginAssemblyInfo> PluginOnlineAssemblyInfos { get; set; }

        #endregion

        public PluginManager()
        {
            //LoadPlugins();
            GetPluginsInfo();
            LoadPlugins();
        }

        private async void GetPluginsInfo()
        {
            List<string> pluginsToDownload = new List<string>();
            List<string> pluginsAssetsNames = new List<string>();
            LocalPluginsAsmNames = new List<AssemblyName>();
            for (int i = 0; i < GetDLLs.Length; i++)
            {
                AssemblyName assembly = AssemblyName.GetAssemblyName(GetDLLs[i]);
                
                LocalPluginsAsmNames.Add(assembly);
            }
            
            for (int i = 0; i < URLs.Length; i++)
            {
                var client = new GitHubClient(new ProductHeaderValue("my-cool-app"));
                var basicAuth = new Credentials("WinterStudios", "ikPnxCVEMuphF35"); // NOTE: not real credentials
                client.Credentials = basicAuth;
                var releases = await client.Repository.Release.GetAll("WinterStudios", URLs[i]);
                var latest = releases[0];
                
                string lastBuild = latest.TagName;

                //Check local for update
                if(LocalPluginsAsmNames.Count > 0)
                {
                    for (int z = 0; z < LocalPluginsAsmNames.Count; z++)
                    {
                        if (LocalPluginsAsmNames[z].Version.ToString() != lastBuild)
                        {
                            pluginsToDownload.Add(releases[0].Assets[0].BrowserDownloadUrl);
                            pluginsAssetsNames.Add(releases[0].Assets[0].Name);
                        }
                    }
                }
                // if doesnt exits add list to download
                else
                {
                    pluginsToDownload.Add(releases[0].Assets[0].BrowserDownloadUrl);
                    pluginsAssetsNames.Add(releases[0].Assets[0].Name);
                }
                
            }
            DownloadPlugins(pluginsToDownload.ToArray(), pluginsAssetsNames.ToArray());
        }


        public void DownloadPlugins(string[] urls, string[] filesNames)
        {
            for (int i = 0; i < urls.Length; i++)
            {

                using (WebClient client = new WebClient())
                {
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    client.Headers.Add("Token", "4b4301af40ecf75f6eef36e883425bec42dd456a");
                    byte[] data = client.DownloadData(urls[i]);
                    using (FileStream fileStream = new FileStream(PluginPath + filesNames[i], System.IO.FileMode.Create, FileAccess.Write))
                    {
                        fileStream.Write(data, 0, data.Length);
                        return;
                    }
                }


                //var githubToken = "4b4301af40ecf75f6eef36e883425bec42dd456a";
                //var request = (HttpWebRequest)WebRequest.Create("https://github.com/WinterStudios/MeioMundo.Editor.Ferramentas/releases/download/0.2.45/MeioMundo.Editor.Ferramentas.dll");
                //request.Headers.Add(HttpRequestHeader.Authorization, string.Concat("token ", githubToken));
                //request.Accept = "application/vnd.github.v3.raw";
                //request.UserAgent = "test app"; //user agent is required https://developer.github.com/v3/#user-agent-required
                //using (var response = request.GetResponse())
                //{
                //    var encoding = System.Text.ASCIIEncoding.UTF8;

                //    using (var reader = new System.IO.StreamReader(response.GetResponseStream(), encoding))
                //    {
                //        string fileContent = reader.ReadToEnd();
                //    }
                //}
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

                Assembly asm =Assembly.Load(assembly); // .Load(dlls[i]);
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
        /// </summary>)
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

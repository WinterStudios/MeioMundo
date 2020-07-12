using MeioMundo.Editor.API;
using MeioMundo.Editor.API.Plugin;
using Octokit;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace MeioMundo.Editor.Internal
{
    public class PluginEngine
    {
        /// <summary>
        /// Return -> C:/User/[userame]/AppData/Local/Meio Mundo/Editor/Plugins/
        /// </summary>
        public static string PluginAppLocalPath { get => Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\MeioMundo\\Editor\\Plugins\\"; }
        /// <summary>
        /// Return 
        /// <para> C:/User/[username]/AppData/Local/Meio Mundo/Editor/Plugins_URLs.json</para>
        /// </summary>
        public static string PluginUrlsAppLocalPath { get => Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\MeioMundo\\Editor\\Plugins_URLs.json"; }
        public static string StoragePluginsPath { get => Environment.CurrentDirectory + "\\Plugins\\"; }
        /// <summary>
        /// The list of urls of the reposities names 
        /// <para>(The reposity name has to be equal of assembly name)</para>
        /// </summary>
        public static string[] PluginUrls { get; set; }

        /// <summary>
        /// Storage local information before the plugins get load
        /// </summary>
        public static List<PluginInformation> Plugins { get; set; }

        #region Privates Fields and Propeties

        #endregion


        public static void Initialize()
        {
            GetLocalPlugins();

            PluginUrls = (string[])Storage.Json.GetJsonData<string[]>(PluginUrlsAppLocalPath) ?? new string[0];

            if (Properties.Settings.Default.Update)
            {
                GetLastVersionReposities();
                GetLocalPlugins();
            }
            LoadPlugins();
        }

        private static void GetLocalPlugins()
        {
            Plugins = new List<PluginInformation>();
            string[] filesDLLs = Directory.GetFiles(StoragePluginsPath).Where(x => x.Contains(".dll")).ToArray();
            for (int i = 0; i < filesDLLs.Length; i++)
            {
                AssemblyName AsmName = AssemblyName.GetAssemblyName(filesDLLs[i]);
                PluginInformation pluginInformation = new PluginInformation();
                pluginInformation.AssemblyName = null;
                pluginInformation.Name = AsmName.Name;
                pluginInformation.Version = AsmName.Version;
                pluginInformation.Enable = false;
                pluginInformation.Location = filesDLLs[i];
                pluginInformation.LocalVersion = VersionSystem.ParseFromLocalAssembly(AsmName.Version.ToString());
                if(Properties.Settings.Default.Update)
                    pluginInformation.OnlineVersion = Task<VersionSystem>.Run(() => GetLastVersion(AsmName.Name)).Result;

                Plugins.Add(pluginInformation);
            }
        }

        public static string[] GetUrls(string location)
        {
            string[] urlsReposity = (string[])Storage.Json.GetJsonData<string[]>(location);
            return urlsReposity;
        }

        /// <summary>
        /// Get the last version from the reposity
        /// </summary>
        /// <param name="url">name of the plugin</param>
        /// <returns>VerstionSystem of the last version</returns>
        public static async Task<VersionSystem> GetLastVersion(string url)
        {
            var client = new GitHubClient(new ProductHeaderValue("meio-mundo-editor"));
            //var basicAuth = new Credentials("WinterStudios", "ikPnxCVEMuphF35"); // NOTE: not real credentials
            //client.Credentials = basicAuth;

            var releases = await client.Repository.Release.GetAll("WinterStudios", url);
            var latest = releases[0];

            string lastBuild = latest.TagName;
            Console.WriteLine((lastBuild));
            return VersionSystem.Parse(lastBuild);
        }

        /// <summary>
        /// Download the plugin from reposity to the folder
        /// </summary>
        /// <param name="url">nama</param>
        public static void DownloadPlugin(string url)
        {

            var client = new GitHubClient(new ProductHeaderValue("my-cool-app"));
            var basicAuth = new Credentials("WinterStudios", "ikPnxCVEMuphF35"); // NOTE: not real credentials
            client.Credentials = basicAuth;

            var releases = client.Repository.Release.GetAll("WinterStudios", url).Result;
            var latest = releases[0];



            using (WebClient _webClient = new WebClient())
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                _webClient.Headers.Add("Token", "4b4301af40ecf75f6eef36e883425bec42dd456a");
                byte[] data = _webClient.DownloadData(latest.Assets[0].BrowserDownloadUrl);
                using (FileStream fileStream = new FileStream(PluginAppLocalPath + latest.Assets[0].Name, System.IO.FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    fileStream.Write(data, 0, data.Length);
                    fileStream.Close();
                }

            }
            if (latest.Assets[0].Name.Contains(".zip"))
            {
                ZipFile.ExtractToDirectory(PluginAppLocalPath + latest.Assets[0].Name, StoragePluginsPath);
                File.Delete(PluginAppLocalPath + latest.Assets[0].Name);
            }
            else
            {
                if (File.Exists(StoragePluginsPath + latest.Assets[0].Name))
                    File.Delete(StoragePluginsPath + latest.Assets[0].Name);
                File.Move(PluginAppLocalPath + latest.Assets[0].Name, StoragePluginsPath + latest.Assets[0].Name);
            }

        }


        /// <summary>
        /// Check if the plugin exist localy
        /// </summary>
        /// <param name="name">Name the plu</param>
        /// <returns></returns>
        private static bool CheckLocal(string name)
        {
            bool check = false;
            for (int i = 0; i < Plugins.Count; i++)
            {
                if (name == Plugins[i].Name)
                    check = true;
            }
            return check;
        }

        private static bool CheckForUpdate(PluginInformation p)
        {
            p.OnlineVersion = Task<VersionSystem>.Run(() => GetLastVersion(p.Name)).Result;
            if (VersionSystem.Compare(p.LocalVersion, p.OnlineVersion))
                return false;

            return true;
        }

        private static void GetLastVersionReposities()
        {
            for (int i = 0; i < PluginUrls.Length; i++)
            {
                // Check for Local plugin
                if (CheckLocal(PluginUrls[i]))
                {
                    // If Exist, check for updates and if true download the update
                    if (CheckForUpdate(Plugins.First<PluginInformation>(x => x.Name == PluginUrls[i])))
                        DownloadPlugin(PluginUrls[i]);

                }
                else
                {   // Download the plugin, one theres no prove that he exits localy
                    DownloadPlugin(PluginUrls[i]);
                }

            }

        }

        #region Loading Plugins Region

        private static void LoadPlugins()
        {
            for (int i = 0; i < Plugins.Count; i++)
            {
                //try
                //{
                    //Console.WriteLine("Loading - {0}", Plugins[i]);
                    LoadPlugin(Plugins[i]);
                //}
                //catch (Exception ex)
                //{
                    //Console.WriteLine(ex);
                //}
            }
        }

        private static void LoadPlugin(PluginInformation plugin)
        {

            byte[] file_dll = File.ReadAllBytes(plugin.Location);
            Assembly assembly = Assembly.Load(file_dll);

            var _Iplugin = assembly.GetTypes().Where(x => x.GetInterfaces().Contains(typeof(IPlugin)));
            plugin.Details = new List<AdicionalInformation>();
            foreach (var item in _Iplugin)
            {
                var obj = (IPlugin)Activator.CreateInstance(item);
                plugin.Details.Add(new AdicionalInformation { Name = obj.Nome, Descrição = obj.Descrição, _Version = obj.Version });
                switch (obj.Type)
                {
                    case PluginType.Control:
                        break;
                    case PluginType.TabPage:
                        Navegation.AddMenu(obj.args, obj.ObjectType);
                        break;
                    case PluginType.Window:
                        break;
                    case PluginType.None:
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion

    }

    public class PluginInformation
    {
        public string Name { get; set; }
        public Version Version { get; set; }
        public string Information { get; set; }
        public AssemblyName AssemblyName { get; set; }
        public AppDomain Domain { get; set; }
        public bool Enable { get; set; }
        public string Location { get; set; }
        /// <summary>
        /// Repository Name
        /// </summary>
        public string Repository { get; set; }
        /// <summary>
        /// Local Version
        /// </summary>
        public VersionSystem LocalVersion { get; set; }
        /// <summary>
        /// Repository Last Version
        /// </summary>
        public VersionSystem OnlineVersion { get; set; }

        public List<AdicionalInformation> Details { get; set; }

    }
    public class AdicionalInformation
    {
        public string Name { get; set; }
        public string Descrição { get; set; }
        public VersionSystem _Version { get; set; }
    }
}

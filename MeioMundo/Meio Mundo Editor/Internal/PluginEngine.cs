using MeioMundo.Editor.API;
using MeioMundo.Editor.UsersControls;
using Octokit;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Policy;
using System.Text;
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

        public static List<AppDomain> PluginsDomains { get; set; }

        public static void Initialize()
        {
            GetLocalPlugins();

            PluginUrls = (string[])Storage.Json.GetJsonData<string[]>(PluginAppLocalPath + "PluginsURLs.json") ?? new string[0];
            //LoadLocalPlugins();
            GetLastVersionReposities();
            var testvar = Task.Run(() => GetLastVersion("MeioMundo.Editor.Ferramentas")).Result;
            Console.WriteLine(testvar.ToString());
        }

        private static void GetLocalPlugins()
        {
            Plugins = new List<PluginInformation>();
            string[] filesDLLs = Directory.GetFiles(StoragePluginsPath).Where(x => x.Contains(".dll")).ToArray();
            for (int i = 0; i < filesDLLs.Length; i++)
            {
                AssemblyName AsmName = AssemblyName.GetAssemblyName(filesDLLs[i]);
                PluginInformation pluginInformation = new PluginInformation();
                pluginInformation.AssemblyName = AsmName;
                pluginInformation.Name = AsmName.Name;
                pluginInformation.Version = AsmName.Version;
                pluginInformation.Enable = false;
                pluginInformation.Location = filesDLLs[i];
                pluginInformation.LocalVersion = VersionSystem.Parse(String.Format("{0}.{1}.{2}", AsmName.Version.Major, AsmName.Version.Minor, AsmName.Version.Build));
                Plugins.Add(pluginInformation);
            }
        }

        public static string[] GetUrls(string location)
        {
            string[] urlsReposity = (string[])Storage.Json.GetJsonData<string[]>(location);
            return urlsReposity;
        }

        public static async Task<VersionSystem> GetLastVersion(string url)
        {
            var client = new GitHubClient(new ProductHeaderValue("my-cool-app"));
            var basicAuth = new Credentials("WinterStudios", "ikPnxCVEMuphF35"); // NOTE: not real credentials
            client.Credentials = basicAuth;

            var releases = await client.Repository.Release.GetAll("WinterStudios", url);
            var latest = releases[0];

            string lastBuild = latest.TagName;

            return VersionSystem.Parse(lastBuild);
        }

        public static async Task DownloadPlugin(string[] urls)
        {

            for (int i = 0; i < urls.Length; i++)
            {
                var client = new GitHubClient(new ProductHeaderValue("my-cool-app"));
                var basicAuth = new Credentials("WinterStudios", "ikPnxCVEMuphF35"); // NOTE: not real credentials
                client.Credentials = basicAuth;

                var releases = await client.Repository.Release.GetAll("WinterStudios", urls[i]);
                var latest = releases[0];

                string lastBuild = latest.TagName;

                using (WebClient _webClient = new WebClient())
                {
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    _webClient.Headers.Add("Token", "4b4301af40ecf75f6eef36e883425bec42dd456a");
                    byte[] data = _webClient.DownloadData(urls[i]);
                    //using (FileStream fileStream = new FileStream(AppLocalPluginPath + filesNames[i], System.IO.FileMode.Create, FileAccess.Write))
                    //{
                    //    fileStream.Write(data, 0, data.Length);
                    //    return;
                    //}
                }
            }
            

        }

        private static void LoadLocalPlugins()
        {
            for (int i = 0; i < Plugins.Count; i++)
            {
                LoadPlugin(Plugins[i]);
            }
        }
        private static void LoadPlugin(PluginInformation plugin)
        {

            byte[] file_dll = File.ReadAllBytes(plugin.Location);

            

        }
        private static void GetLastVersionReposities() 
        {
            for (int i = 0; i < Plugins.Count; i++)
            {
                string pluginsName = Plugins[i].Name;
                bool exitsLocal = false;
                for (int z = 0; z < PluginUrls.Length; z++)
                {
                    if (pluginsName == PluginUrls[z])
                    {
                        exitsLocal = true;
                        Plugins[i].OnlineVersion = Task.Run(() => GetLastVersion(PluginUrls[z])).Result;
                    }

                }
            }
        }

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

    }
}

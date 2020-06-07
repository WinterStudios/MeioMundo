using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MeioMundo.Editor.Internal
{
    public class PluginEngine
    {
        /// <summary>
        /// Return -> C:/User/[userame]/AppData/Local/Meio Mundo/Editor/Plugins/
        /// </summary>
        public static string PluginAppLocalPath { get => Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\MeioMundo\\Editor\\Plugins\\"; }
        public static string[] PluginUrls { get; set; }

        /// <summary>
        /// Storage local information before the plugins get load
        /// </summary>
        public static AssemblyName[] LocalAssemblyNamePlugins { get; set; }

        public static AppDomain[] AppDomainPlugins { get; set; }

        #region Privates Fields and Propeties


        #endregion

        public static void Initialize()
        {
            GetLocalPlugins();
            PluginUrls = (string[])Storage.Json.GetJsonData<string[]>(PluginAppLocalPath + "PluginsURLs.json");
            
        }

        private static void GetLocalPlugins()
        {
            List<AssemblyName> assemblies = new List<AssemblyName>();
            string[] filesDLLs = Directory.GetFiles(PluginAppLocalPath)
        }
    }

    public class PluginInformation
}

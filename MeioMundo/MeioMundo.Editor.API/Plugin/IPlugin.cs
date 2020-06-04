using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeioMundoEditor.API.Plugin
{
    public enum PluginType
    {
        Control,
        TabPage,
        Window
    }
    /// <summary>
    /// Interface para o programa identifar como plugin
    /// </summary>
    public interface IPlugin
    {
        /// <summary>
        /// Nome do Plugin
        /// </summary>
        string Nome { get; }
        /// <summary>
        /// Descrição do Plugin
        /// </summary>
        string Descrição { get; }
        /// <summary>
        /// Versão actual do Plugin
        /// </summary>
        string Versão { get; }
        /// <summary>
        /// Aurgumentos para o tipo de implementação do plugin
        /// </summary>
        string[] Args { get; }
        /// <summary>
        /// Tipo de implementação do plugin
        /// </summary>
        PluginType Type { get; }
    }
    public class PluginCommand
    {
       
    }
    public class PluginInfo
    {
        /// <summary>
        /// Nome do Plugin
        /// </summary>
        public string AssemblyName { get; set; }
        
        public PluginClass[] PluginClasses { get; set; }
        
        public string Version { get; set; }
        
        public bool AssemblyEnable { get; set; }

        public Guid GUID { get; set; }

        public string Location { get; set; }
    }
    public class PluginClass
    {
        public string Nome { get; set; }
        public string Descrição { get; set; }
        public string Versao { get; set; }
        public Type Class { get; set; }
        public PluginType Type { get; set; }
        public string[] TypeArgs { get; set; }
    }
}

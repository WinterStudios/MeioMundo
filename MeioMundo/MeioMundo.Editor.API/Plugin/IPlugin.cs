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
        PluginType Type { get;  }
    }
    public class PluginCommand
    {
       
    }
    public class PluginInfo
    {
        /// <summary>
        /// Nome do Plugin
        /// </summary>
        public string Nome { get; set; }
        /// <summary>
        /// Descrição do Plugin
        /// </summary>
        public string Descrição { get; set; }
        /// <summary>
        /// Versão do Plugin
        /// </summary>
        public string Versão { get; set; }
        /// <summary>
        /// Guarda o tipo de class do plugin para iniciar
        /// </summary>
        public Type PluginType { get; set; }
        /// <summary>
        /// Esta ativo ou não o plugin
        /// </summary>
        public bool Enable { get; set; }
        /// <summary>
        /// Guarda o nome da Assembly
        /// </summary>
        public string AssemblyName { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeioMundo.Editor.API.Plugin
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
        VersionSystem Version { get; }
      
        /// <summary>
        /// Tipo de implementação do plugin
        /// </summary>
        PluginType Type { get; }

        string args { get; }

        /// <summary>
        /// Represent the object class to iniciate
        /// </summary>
        Type ObjectType { get; }
    }
}

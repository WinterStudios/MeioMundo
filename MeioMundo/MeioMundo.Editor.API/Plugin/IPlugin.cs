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
        Page,
        Window
    }
    public interface IPlugin
    {
        string Nome { get; }
        string Descrição { get; }
        string Versão { get; }
    }
    public class PluginInfo
    {
        public string Nome { get; set; }
        public string Descrição { get; set; }
        public string Versão { get; set; }
        /// <summary>
        /// Guarda o tipo de class do plugin para iniciar
        /// </summary>
        public Type PluginType { get; set; }
        
    }
}

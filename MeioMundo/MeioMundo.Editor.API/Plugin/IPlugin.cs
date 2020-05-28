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
    public interface Plugin
    {
        string PluginName { get; }
        string PluginDescription { get; }
        PluginType PluginType { get; }
        string PluginMenuItemLocation { get; }
        PluginInfo PluginInfo { get; }
    }
    public class PluginInfo
    {
        public string Nome { get; set; }
        public string Descrição { get; set; }
        public string Versão { get; set; }
    }
}

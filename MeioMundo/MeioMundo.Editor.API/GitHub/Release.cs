using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeioMundo.Editor.API.GitHub
{
    public class Release
    {
        public string Url { get; set; }
        public string Tag { get; set; }
        public bool PreRelease { get; set; }
        public DateTime PublishDate { get; set; }
        public Asset[] Assets { get; set; }
    }

    public struct Asset
    {
        public string Url { get; set; }
        public string Name { get; set; }
        public int Size { get; set; }
        public string DownloadLink { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MeioMundo.Editor.API.GitHub
{
    public class ReleaseAsset
    {
        public string Url { get; protected set; }
        public string Name { get; protected set; }
        public long Size { get; protected set; }
        public string BrowerDownloadUrl { get; protected set; }

        public ReleaseAsset()
        {

        }
        public ReleaseAsset(string url, string name, long size, string downloadUrl)
        {

        }
    }
}

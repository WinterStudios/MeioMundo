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
        /// <summary>
        /// <para>[API]</para>
        /// Url of the release
        /// </summary>
        /// <value>https://api.github.com/repos/[user]/[repos]/release/[id-release]"</value>
        public string Url { get; set; }
        /// <summary>
        /// <para>[API]</para>
        /// Url of the release
        /// </summary>
        /// <value>https://api.github.com/repos/[user]/[repos]/release/[id-release]/assets"</value>
        public string AssetsUrl { get; set; }
        /// <summary>
        /// Brower url
        /// </summary>
        /// <value>https//gihub.com/[user]/[repos]/releases/tag/[tag-name]</value>
        public string WebUrl { get; protected set; }
        /// <summary>
        /// Release ID
        /// </summary>
        public int ID { get; protected set; }
        /// <summary>
        /// Release Tag 
        /// </summary>
        public string TagName { get; protected set; }
        /// <summary>
        /// If the release is in prerelease mode
        /// </summary>
        public bool PreRelease { get; protected set; }
        /// <summary>
        /// Published date of the release
        /// </summary>
        public DateTime PublishedDate { get; protected set; }
    }

}

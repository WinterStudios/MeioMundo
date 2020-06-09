using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeioMundo.Editor.Internal
{
    public class VersionSystem
    {
        /// <summary>
        /// Major Version
        /// </summary>
        public string Major { get; set; }
        /// <summary>
        /// Minor - ex: features on this major
        /// </summary>
        public string Minor { get; set; }
        /// <summary>
        /// How many builds was need to fix this
        /// </summary>
        public string Build { get; set; }
        /// <summary>
        /// beta or alpha
        /// </summary>
        public string Revision { get; set; }

        /// <summary>
        /// Has to be in this format: m.n.b.r
        /// </summary>
        /// <param name="version"></param>
        /// <returns></returns>
        public static VersionSystem Parse(string version)
        {
            VersionSystem _v = new VersionSystem();
            string[] _s = version.Split('.');
            _v.Major = _s[0];
            _v.Minor = _s[1];
            _v.Build = _s[2];
            if (_s.Length > 3)
                _v.Revision = _s[3];
            else
                _v.Revision = string.Empty;
            return _v;
        }
        public override string ToString()
        {
            if(Revision.Length > 0)
                return string.Format("{0}.{1}.{2}.{3}", Major, Minor, Build, Revision);
            else
                return string.Format("{0}.{1}.{2}", Major, Minor, Build);
        }
    }
}

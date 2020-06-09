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
        public int Major { get; set; }
        /// <summary>
        /// Minor - ex: features on this major
        /// </summary>
        public int Minor { get; set; }
        /// <summary>
        /// How many builds was need to fix this
        /// </summary>
        public int Build { get; set; }
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
            _v.Major = int.Parse(_s[0]);
            _v.Minor = int.Parse(_s[1]);
            _v.Build = int.Parse(_s[2]);
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
        public static bool Compare(VersionSystem v1, VersionSystem v2)
        {
            bool equal = false;
            if (v1.Major == v2.Major && v1.Minor == v2.Minor && v1.Build == v2.Build && v1.Revision == v2.Revision)
                equal = true;

            return equal;
        }
    }
}

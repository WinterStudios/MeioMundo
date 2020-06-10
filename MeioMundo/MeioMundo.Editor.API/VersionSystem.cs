using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeioMundo.Editor.API
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
            string[] _s = version.Split(new char[] { '.' }, 3);
            _v.Major = int.Parse(_s[0]);
            _v.Minor = int.Parse(_s[1]);
            
            if (_s[2].Contains("-"))
            {
                string[] _b_r = _s[2].Split('-');
                _v.Build = int.Parse(_b_r[0]);
                _v.Revision = _b_r[1];
            }
            else
            {
                _v.Build = int.Parse(_s[2]);
                _v.Revision = string.Empty;
            }
            return _v;
        }
        public static VersionSystem ParseFromLocalAssembly(string version)
        {
            VersionSystem versionSystem = new VersionSystem();
            string[] versions = version.Split('.');
            versionSystem.Major = int.Parse(versions[0]);
            versionSystem.Minor = int.Parse(versions[1]);
            versionSystem.Build = int.Parse(versions[2]);

            if (versions[3].StartsWith("0")) // If start with [0] is null
                versionSystem.Revision = string.Empty;

            if (versions[3].StartsWith("1")) // If start with [1] is Alpha
            {
                string rev = versions[3].Remove(0, 1);
                if (rev != string.Empty)
                {
                    string revision = string.Format("aplha-{0}", rev);
                    versionSystem.Revision = revision;
                }
                else
                {
                    versionSystem.Revision = "aplha";
                }
            }
            if (versions[3].StartsWith("1")) // If start with [2] is Beta
            {
                string rev = versions[3].Remove(0, 1);
                if (rev != string.Empty)
                {
                    string revision = string.Format("beta-{0}", rev);
                    versionSystem.Revision = revision;
                }
                else
                {
                    versionSystem.Revision = "beta";
                }
            }


            return versionSystem;
        }
        public override string ToString()
        {
            if(!string.IsNullOrEmpty(Revision))
                return string.Format("{0}.{1}.{2}-{3}", Major, Minor, Build, Revision);
            else
                return string.Format("{0}.{1}.{2}", Major, Minor, Build);
        }
        public static bool Compare(VersionSystem v1, VersionSystem v2)
        {
            bool equal = false;
            Console.WriteLine("v1:{0}-v2:{1}",v1.ToString(), v2.ToString());
            if (v1.Major == v2.Major && v1.Minor == v2.Minor && v1.Build == v2.Build && v1.Revision == v2.Revision)
                equal = true;

            return equal;
        }
        public static VersionSystem SetVersion(string version)
        {
            return Parse(version);
        }
    }
}

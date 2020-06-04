using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeioMundo.Editor.API
{
    public struct Version
    {
        /// <summary>
        /// Represent the Major Build (Year)
        /// </summary>
        public string Major { get; set; }
        /// <summary>
        /// Represent the Features add
        /// </summary>
        public string Minor { get; set; }
        /// <summary>
        /// Build of the day (Performe or enhacnge)
        /// </summary>
        public string Build { get; set; }
        /// <summary>
        /// Revision of the code (bugs)
        /// </summary>
        public string Revesion { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MeioMundo.Editor.API.GitHub.Extensions
{
    public static class ExRelease
    {
        /// <summary>
        /// Get last release
        /// </summary>
        /// <param name="releases"></param>
        /// <returns>Return the last release (includin prerelease)</returns>
        public static Release GetLastRelease(this IEnumerable<Release> releases)
        {
            if (releases.Count() > 0)
                return releases.First();
            else
                return null;
        }
        /// <summary>
        /// Get last release depedent on prerelease value
        /// </summary>
        /// <param name="releases"></param>
        /// <param name="preRelease"></param>
        /// <returns>Return the Last Release publish if is pre release or not</returns>
        public static Release GetLastRelease(this IEnumerable<Release> releases, bool preRelease)
        {
            if (releases.Count() > 0)
                if (preRelease && releases.Where(x => x.prerelease == true).Count() > 0)
                    return releases.FirstOrDefault(x => x.prerelease == true);
                if(!preRelease)
                    return releases.FirstOrDefault(x => x.prerelease == true);
            else
                return null;
        }
    }
}

using MeioMundo.Editor.API;
using MeioMundo.Editor.API.GitHub;
using MeioMundo.Editor.API.GitHub.Extensions;
using MeioMundo.Editor.UsersControls;
using Octokit;
using System;
using System.Threading.Tasks;

namespace MeioMundo.Editor.Internal
{
    public class UpdateSystem
    {

        public static API.VersionSystem Version { get; private set; }

        public static GitHub GitHubApp { get; private set; }
        public static void Initialize()
        {
#if DEBUG
            VersionSystem version = new VersionSystem();
            version.Major = 0;
            version.Minor = 5;
            version.Build = int.Parse(string.Format("{0}{1}", DateTime.Today.Month, DateTime.Today.Day));
            version.Revision = "rc.1";
            Properties.Settings.Default.Version = VersionSystem.ToString(version);
            Properties.Settings.Default.Save();
#endif

            Version = VersionSystem.Parse(Properties.Settings.Default.Version);
            StatusBar.SetVersionDisplay(version.ToString());

            GitHubApp = new GitHub("winterstudios", "MeioMundo");
            VersionSystem OnlineVersion = VersionSystem.Parse(GitHubApp.Releases.GetLastRelease().tag_name);
            bool update = VersionSystem.Compare(Version, OnlineVersion);

        }

        public static async Task GetLastUpdateAsync()
        {
            
        }
    }
}

using MeioMundo.Editor.API;
using MeioMundo.Editor.UsersControls;
using Octokit;
using System;
using System.Threading.Tasks;

namespace MeioMundo.Editor.Internal
{
    public class UpdateSystem
    {

        public static API.VersionSystem Version { get; private set; }


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
        }

        public static async Task GetLastUpdateAsync()
        {
            var client = new GitHubClient(new ProductHeaderValue("my-cool-app"));
            var basicAuth = new Credentials("WinterStudios", "ikPnxCVEMuphF35"); // NOTE: not real credentials
            client.Credentials = basicAuth;
            var releases = await client.Repository.Release.GetAll("WinterStudios", "HomeMedia");
            var latest = releases[0];
            Console.WriteLine(
                "The latest release is tagged at {0} and is named {1}",
                latest.TagName,
                latest.Name);

            /// Para fazer o download --> https://github.com/WinterStudios/HomeMedia/releases/latest/download/HomeServerEditor.zip
            ///                           https://github.com/WinterStudios/HomeMedia/releases/download/0.1.6/Release.zip
        }
    }
}

using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeioMundoEditor.API
{
    public class System
    {
        public class Version
        {
            public static string CurrentBuild { get { return Properties.Settings.Default.Version; } set { Properties.Settings.Default.Version = value; } }
            public static string LastBuild { get; set; }
            public struct BuildVersion
            {
                public static int Major { get; set; }
                public static int Mirror { get; set; }
                public static int Build { get; set; }
                public static void SetBuildVersion()
                {
                    BuildVersion.Major = DateTime.Today.Year;
                    BuildVersion.Mirror = DateTime.Today.Month;

                    CurrentBuild = string.Format("{0}.{1}.{2}", Major, Mirror, Build);
                    Properties.Settings.Default.Save();
                }
            }

            public static void Initiazlize()
            {
#if DEBUG
                BuildVersion.SetBuildVersion();
#else
                Console.WriteLine("Release!!");
#endif
                CheckVersion();
            }

            public async static void CheckVersion()
            {
                var client = new GitHubClient(new ProductHeaderValue("my-cool-app"));
                var basicAuth = new Credentials("WinterStudios", "ikPnxCVEMuphF35"); // NOTE: not real credentials
                client.Credentials = basicAuth;
                var releases = await client.Repository.Release.GetAll("WinterStudios", "HomeMedia");
                var latest = releases[0];
                LastBuild = latest.TagName;
                if (LastBuild != CurrentBuild)
                {
                    Console.WriteLine("Update Avalable:{0}", LastBuild);
                }

                /// Para fazer o download --> https://github.com/WinterStudios/HomeMedia/releases/latest/download/HomeServerEditor.zip
                ///   é possivel tirar os links dos fichiros atraves da var release acima
            }
        }

        internal static void Initialize()
        {
            Version.Initiazlize();
        }
    }
}

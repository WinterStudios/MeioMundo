using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Octokit;

namespace MeioMundoEditor.API
{
    public class UpdateSystem
    {
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

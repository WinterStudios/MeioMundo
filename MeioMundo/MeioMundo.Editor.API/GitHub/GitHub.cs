using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MeioMundo.Editor.API.GitHub
{
    public class GitHub
    {

        #region Static Zone
        public static string User { get { return "WinterStudios"; } }
        public static string Repository { get { return "MeioMundo"; } }
        public static string GitHubUrl { get { return "https://api.github.com/repos/"; } }
        /// <summary>
        /// Get the Last Version of the Reposity
        /// </summary>
        public static void GetRelease()
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create("https://api.github.com/repos/WinterStudios/MeioMundo/releases/latest");
            webRequest.Method = "GET";
            webRequest.UserAgent = "Anything";
            webRequest.ServicePoint.Expect100Continue = false;

            WebResponse response = webRequest.GetResponse();

            StreamReader reader = new StreamReader(response.GetResponseStream());
            Console.WriteLine(reader.ReadToEnd());
        }
        /// <summary>
        /// Get the Last Version of the Reposity
        /// </summary>
        /// <param name="user">Username</param>
        /// <param name="reposity">Reposity Name</param>
        public static void GetRelease(string user, string reposity)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create("https://api.github.com/repos/"+ user +"/" + reposity + "/releases/latest");
            webRequest.Method = "GET";
            webRequest.UserAgent = "Anything";
            webRequest.ServicePoint.Expect100Continue = false;

            WebResponse response = webRequest.GetResponse();

            StreamReader reader = new StreamReader(response.GetResponseStream());
            Console.WriteLine(reader.ReadToEnd());
        }


        // ------------------------------- Zone Finish --------------------------------------------------------------------------------------------------
        #endregion


        public string Username { get; set; }
        public string RepositoryName { get; set; }
        public List<Release> Releases { get; set; }

        public string ReleaseURL { get; protected set; }
        public 

        public GitHub()
        {
            Releases = new List<Release>();

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create("https://api.github.com/repos/" + Username + "/" + RepositoryName);
            webRequest.Method = "GET";
            webRequest.UserAgent = "Anything";
            webRequest.ServicePoint.Expect100Continue = false;
        }
        /// <summary>
        /// Create a GitHub Client with all the stuff
        /// </summary>
        /// <param name="username">User</param>
        /// <param name="reposities">Repository Name</param>
        public GitHub(string username, string reposities)
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(RepositoryName))
                return;
            Releases = new List<Release>();
            Release release = new Release();
            

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create("https://api.github.com/repos/" + Username + "/" + RepositoryName);
            webRequest.Method = "GET";
            webRequest.UserAgent = "Anything";
            webRequest.ServicePoint.Expect100Continue = false;

            var responseJson = Newtonsoft.Json.

        }
        public Release[] GetReleases() => Releases.ToArray();

        
    }
}

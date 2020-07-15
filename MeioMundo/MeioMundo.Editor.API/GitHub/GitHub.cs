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
    }
}

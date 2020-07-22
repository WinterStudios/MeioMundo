using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using MeioMundo.Editor.API.GitHub.Extensions;
using MeioMundo.Editor.API;

namespace MeioMundo.Test_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Editor.API.GitHub.GitHub gitHub = new Editor.API.GitHub.GitHub("winterstudios", "MeioMundo");

            VersionSystem version = new VersionSystem();
            version = VersionSystem.Parse(gitHub.Releases.GetLastRelease(true).tag_name);
            Console.WriteLine(version);

            Console.ReadLine();
        }
    }

}


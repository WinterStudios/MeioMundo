using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace MeioMundo.Test.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            MeioMundo.Editor.API.GitHub.GitHub.GetRelease();

            System.Console.ReadLine(); 
        }
    }

}


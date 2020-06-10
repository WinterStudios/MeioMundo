using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeioMundo.Editor.API
{
    public class Debug
    {
        public static string _FilePath => Environment.CurrentDirectory + "\\log.txt";
    

        public static void Log(string message)
        {
            if (!File.Exists(_FilePath))
                File.Create(_FilePath);
            using (StreamWriter w = File.AppendText(_FilePath))
            {
                w.WriteLine(message);
            }
        }

    }
}

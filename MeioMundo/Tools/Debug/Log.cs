using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Tools
{
    public static class Debug
    {
        public static void CheckLog()
        {
            if (File.Exists(path))
                File.Delete(path);
        }
        private static string path 
        { 
            get 
            {
                string p = Directory.GetCurrentDirectory() + "/Log.txt";
                //if (File.Exists(p))
                 //   File.Delete(p);
                return p;
            }
        }
        
        public static void Log(string message)
        {
            using (StreamWriter w = File.AppendText(path))
            {
                string log = "[" + DateTime.Now + "] [LOG] ";

                w.WriteLine(log + message);
            }
        }
        public static void Error(string message)
        {
            using (StreamWriter w = File.AppendText(path))
            {
                string log = "[" + DateTime.Now + "] [ERROR] ";

                w.WriteLine(log + message);
            }
        }

    }
}

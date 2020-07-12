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
            string filePath = "C:/Users/X510/Desktop/wc-product-export-9-7-2020-1594287039683.csv";


            StreamReader reader = new StreamReader(filePath);
            int i = 0;
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();

                string pattern = @"(?:,|\n|^)(""(?:(?:"""")*[^""]*)*""|[^"",\n]*|(?:\n|$))";
                RegexOptions options = RegexOptions.Multiline;

                List<string> colluns = new List<string>();
                foreach (Match m in Regex.Matches(line, pattern, options))
                {
                    //System.Console.WriteLine("'{0}' found at index {1}.", m.Value, m.Index);

                    string s = m.ToString().CleanCSV();
                    colluns.Add(s);
                }
                i++;
                System.Console.WriteLine(i);
            }
        }
    }
    public static class Extensitions
    {
        public static string CleanCSV(this string s)
        {
            
            string pattern1 = @",(?:\\|\"")*";
            Regex r = new Regex(pattern1);
            string sr = r.Replace(s, string.Empty);
            string pattern2 = @"[(\?\\\"")]|(\\\"")$|(\"")$";
            r = new Regex(pattern2);
            string srt = r.Replace(sr, string.Empty);
            return srt;
        }
    }
}


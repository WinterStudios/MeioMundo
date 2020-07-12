using System;
using System.IO;
using Microsoft.Win32;

namespace MeioMundo.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = "C:/Users/X510/Desktop/wc-product-export-9-7-2020-1594287039683.csv";

            StreamReader reader = new StreamReader(filePath);
            while(reader.EndOfStream)
            {
                string line = reader.ReadLine();
                string[] coll = line.Split(',');
                System.Console.WriteLine("d");
            }
        }
    }
}

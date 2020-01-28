using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = "C:/Users/X510/Downloads/wc-product-export-27-1-2020-1580154044240.csv";

            string[] rows = System.IO.File.ReadAllLines(path);

            for (int i = 0; i < rows.Length; i++)
            {
                rows[i] = rows[i].Replace("\"","");
                Console.WriteLine(rows[i]);
            }
            Console.ReadLine();
        }
    }
}

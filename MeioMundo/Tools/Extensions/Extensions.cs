using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Extensions
{
    public static class ExtensionString
    {
        public static string[] RemoveFrist(this string[] stringArray)
        {
            List<string> strings = new List<string>();
            for (int i = 1; i < stringArray.Length; i++)
            {
                strings.Add(stringArray[i]);
            }
            return strings.ToArray();
        }
    }
}

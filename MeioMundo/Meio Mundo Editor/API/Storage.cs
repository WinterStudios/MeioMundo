using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;
using MeioMundo.Editor.API;

namespace MeioMundoEditor.API
{
    public class Storage
    {
        public class Files
        {
            public static bool Exists(string file) => File.Exists(file);
            public static void CreateDirectory(string directory)
            {
                if (directory.Contains('\\'))
                    directory = directory.Replace('\\', '/');
                string[] diretoryNames = directory.Split('/');
                string t_currentDirectory = diretoryNames[0] + "/" + diretoryNames[1];
                for (int i = 1; i < diretoryNames.Length - 1; i++)
                {
                    string t_nextDirectory = t_currentDirectory + "/" + diretoryNames[i+1];
                    if (!Directory.Exists(t_nextDirectory))
                        Directory.CreateDirectory(t_nextDirectory);
                    t_currentDirectory = t_nextDirectory;
                }
            }
            public static void CreateFile(string location)
            {
                string directory = location.Remove(location.LastIndexOf('\\'));
                if (!Directory.Exists(directory))
                    CreateDirectory(directory);
            }
        }
        public class Json
        {
            //public static void CreateJsonFile(string location, object data)
            //{
            //    File.WriteAllText(location, JsonSerializer.Serialize(data));
            //}


            /// <summary>
            /// Save the object data into location
            /// </summary>
            /// <param name="location">Location of the file + plus the name (without extension)</param>
            /// <param name="data">Data to save</param>
            public static void SaveJsonFile(string location, object data)
            {
                if (!Files.Exists(location))
                    Files.CreateFile(location);

                using (StreamWriter file = File.CreateText(location))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Formatting = Formatting.Indented;
                    serializer.Serialize(file, data);
                }
            }
            /// <summary>
            /// Read the Json File and return the data as object
            /// </summary>
            /// <typeparam name="T">Type of object Type that the file was save</typeparam>
            /// <param name="location">Location of the Json File</param>
            /// <returns></returns>
            public static object GetJsonData<T>(string location)
            {
                using (StreamReader file = File.OpenText(location))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    object obj = serializer.Deserialize(file, typeof(T)); 
                    return obj;
                }
                
            }
            
        }
    }
}

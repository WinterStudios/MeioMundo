using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;
using System.Text.RegularExpressions;
using System.Data.OleDb;
using System.Globalization;
using CsvHelper;

namespace Tools.Site
{
    public class FileManager
    {

        public static class Window
        {
            public enum Extensions
            {
                CSV = 0,
                XLXS = 1,
            }
            public static Extensions _extensions = Extensions.CSV;

            private static string m_extensions(Extensions fileExtension)
            {
                switch (_extensions)
                {
                    case Extensions.CSV:
                        return "CSV Files (*.csv)|*.csv";
                    case Extensions.XLXS:
                        return "";
                    default:
                        return "";
                }
            }

            public static string OpenFileWindowDialog(Extensions FileExtension)
            {
                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
                // Set filter for file extension and default file extension 
                dlg.DefaultExt = ".csv";
                dlg.Filter = m_extensions(FileExtension);


                // Display OpenFileDialog by calling ShowDialog method 
                Nullable<bool> result = dlg.ShowDialog();

                string ss = string.Empty;
                // Get the selected file name and display in a TextBox 
                if (result == true)
                {
                    // Open document 
                    string filename = dlg.FileName;
                    ss = filename;
                }
                return ss;
            }
        }

        public static void OpenFile()
        {

        }

        public static class CSV
        {
            public static DataTable ReadFileToTable(string file)
            {
                using (var reader = new StreamReader(file))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    using (var dr = new CsvDataReader(csv))
                    {
                        var dt = new DataTable();
                        
                        dt.Load(dr);

                        
                        return dt;
                    }
                }

                
            }
        
            static DataColumn[] GetDataColumn()
            {
                List<DataColumn> Colluns = new List<DataColumn>();
                Colluns.Add(new DataColumn("ID"));
                Colluns.Add(new DataColumn("Nome"));
                Colluns.Add(new DataColumn("Preço"));
                Colluns.Add(new DataColumn("Stock"));
                return Colluns.ToArray();
            }
            static DataRow[] GetDataRows(string path)
            {
                List<DataRow> rows = new List<DataRow>();
                return rows.ToArray();
               
            }
        }
    }
}

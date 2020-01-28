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
using Tools.Extensions;
using ExcelDataReader;

namespace Tools.Site
{
    public class FileManager
    {

        public static class Window
        {
            public enum Extensions
            {
                CSV = 0,
                XLSX = 1,
            }
            public static Extensions _extensions = Extensions.CSV;

            private static string m_extensions(Extensions fileExtension)
            {
                switch (_extensions)
                {
                    case Extensions.CSV:
                        return "CSV Files (*.csv)|*.csv";
                    case Extensions.XLSX:
                        return "Excel Files (*.xlsx;*.xls)|*.xlsx;*.xls";
                    default:
                        return "";
                }
            }

            public static string OpenFileWindowDialog(Extensions FileExtension)
            {
                _extensions = FileExtension;
                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
                // Set filter for file extension and default file extension 
                //dlg.DefaultExt = ".csv";
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

        public static List<Dados.Site> GetDados(string path)
        {
            string extension = path.Remove(0, path.LastIndexOf('.'));

            string[] rows = new string[0];
            switch (extension)
            {
                case ".csv":
                    rows = CSV.CSVReader(path);
                    break;
                case ".XLSX":
                    return XLSX.ExcelReader(path);
                
            }
            List<Dados.Site> produtos = new List<Dados.Site>();
            for (int i = 1; i < rows.Length; i++)
            {
                string[] collumns = rows[i].Split(',');
                Dados.Site produto = new Dados.Site();
                produto.ID = int.Parse(collumns[0]);
                produto.Ref = collumns[1];
                produto.Name = collumns[2];
                int stock = 0;
                int.TryParse(collumns[3], out stock);
                produto.Stock = stock;
                produto.Price = collumns[4];
                produtos.Add(produto);
            }

            return produtos;

        }


        public class CSV
        {
            public static string[] CSVReader(string path)
            {
                string[] rows = System.IO.File.ReadAllLines(path);
                rows = rows.RemoveFrist();
                for (int i = 0; i < rows.Length; i++)
                {
                    rows[i] = rows[i].Replace("\"", "");
                    rows[i] = rows[i].Replace("'", "");

                }
                return rows;
            }
        }
        public class XLSX
        {
            public static List<Dados.Site> ExcelReader(string path)
            {

                DataSet data = new DataSet();
                using (var stream = File.Open(path, FileMode.Open, FileAccess.Read))
                {
                    // Auto-detect format, supports:
                    //  - Binary Excel files (2.0-2003 format; *.xls)
                    //  - OpenXml Excel files (2007 format; *.xlsx)
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        // Choose one of either 1 or 2:

                        // 1. Use the reader methods
                        do
                        {
                            while (reader.Read())
                            {
                                // reader.GetDouble(0);
                            }
                        } while (reader.NextResult());

                        // 2. Use the AsDataSet extension method
                        var result = reader.AsDataSet();

                        data = result;

                    }
                }

                List<Dados.Site> dados = new List<Dados.Site>();
                DataTable table = data.Tables[0];
                for (int i = 1; i < table.Rows.Count; i++)
                {
                    Dados.Site site = new Dados.Site();
                    site.Name = table.Rows[i][1].ToString();
                    site.Ref = table.Rows[i][0].ToString();
                    site.Price = table.Rows[i][4].ToString();
                    float stock = 0;
                    bool canConvert = float.TryParse(table.Rows[i][10].ToString(), out stock);
                    if (!canConvert)
                        Debug.Error("[FILE_MANAGER][XLSX] - Fall to convert:" + table.Rows[i][10].ToString());
                        site.Stock = stock;
                    dados.Add(site);
                }

                return dados;

            }
        }
    }
}

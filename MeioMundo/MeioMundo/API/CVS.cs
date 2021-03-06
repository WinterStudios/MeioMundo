﻿using System;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace MeioMundo.API
{
    class CVS
    {
        public class CVSReader
        {
            public static string OpenCSV()
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Text files (*.csv)|*.csv";
                string output = "";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    output = openFileDialog.FileName;
                    return output;
                }
                else
                    return null;

            }
            public static DataTable Read(string fileSource)
            {
                if (fileSource == null)
                    return null;
                StreamReader reader = new StreamReader(fileSource);

                string content = reader.ReadToEnd();

                string[] rows = content.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);

                DataTable dataTable = new DataTable("WebSite");

                DataColumn m_id = new DataColumn();
                m_id.DataType = System.Type.GetType("System.Int32");
                m_id.ColumnName = "ID";
                dataTable.Columns.Add(m_id);

                DataColumn m_ref = new DataColumn();
                m_ref.DataType = System.Type.GetType("System.String");
                m_ref.ColumnName = "REF";
                dataTable.Columns.Add(m_ref);

                DataColumn m_nome = new DataColumn();
                m_nome.DataType = Type.GetType("System.String");
                m_nome.ColumnName = "Nome";
                dataTable.Columns.Add(m_nome);

                DataColumn m_fiscal = new DataColumn();
                m_fiscal.DataType = Type.GetType("System.String");
                m_fiscal.ColumnName = "Taxa";
                dataTable.Columns.Add(m_fiscal);

                DataColumn m_stock = new DataColumn();
                m_stock.DataType = Type.GetType("System.Int32");
                m_stock.ColumnName = "Stock";
                dataTable.Columns.Add(m_stock);

                DataColumn m_preço = new DataColumn();
                m_preço.DataType = Type.GetType("System.String");
                m_preço.ColumnName = "Preço";
                dataTable.Columns.Add(m_preço);


                for (int i = 1; i < rows.Length; i++)
                {
                    string[] colluns = Regex.Split(rows[i], ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");
                    DataRow row = dataTable.NewRow();
                    row["ID"] = colluns[0];
                    row["REF"] = colluns[1];
                    row["Nome"] = colluns[2];
                    row["Taxa"] = colluns[3];

                    float m_s = 0;
                    float.TryParse(colluns[4], out m_s);
                    row["Stock"] = m_s;

                    var m_p = Regex.Matches(colluns[5], @"\d+(\,\d+)");
                    float f_p = 0;
                    if (m_p.Count > 0)
                        float.TryParse(m_p[0].Value, out f_p);
                    row["Preço"] = f_p;
                    dataTable.Rows.Add(row);
                }
                return dataTable;
            }
        }
    }
}

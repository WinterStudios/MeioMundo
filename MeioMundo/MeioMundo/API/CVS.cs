using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;
using System.IO;

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
                }
                return output;
            }
            public static DataTable Read(string fileSource)
            {
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
                m_preço.DataType = Type.GetType("System.Single");
                m_preço.ColumnName = "Preço";
                dataTable.Columns.Add(m_preço);
                   

                for (int i = 1; i < rows.Length; i++)
                {
                    string rowString = rows[i].Replace('"', ' ').Replace("/","");
                    string[] colluns = rowString.Split(',');
                    DataRow row = dataTable.NewRow();
                    row["ID"] = colluns[0];
                    row["REF"] = colluns[1];
                    row["Nome"] = colluns[2];
                    row["Taxa"] = colluns[3];

                    float m_s = 0;
                    float.TryParse(colluns[4], out m_s);


                    row["Stock"] = m
                    string preco = "";
                    if (colluns.Length > 6)
                        preco = colluns[5] + "," + colluns[6];
                    else
                        preco = colluns[5];
                    float m_p = 0;
                    float.TryParse(preco, out m_p);
                    row["Preço"] = m_p;
                    dataTable.Rows.Add(row);
                }
                return dataTable;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ExcelDataReader;
using System.IO;
using MeioMundo.API;
using System.Globalization;

namespace MeioMundo.Controls
{
    public partial class UpdateStockControl : UserControl
    {
        public UpdateStockControl()
        {
            InitializeComponent();
            //OpenFileSage();
            dataGridView1.DoubleBuffered(true);
            dataGridView2.DoubleBuffered(true);

        }
        public void OpenFileSage()
        {
            string fileSource = string.Empty;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                fileSource = openFileDialog.FileName;
            }
            else
                return;


            using (var stream = File.Open(fileSource, FileMode.Open, FileAccess.Read))
            {

                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    do
                    {
                        while (reader.Read())
                        {

                        }
                    } while (reader.NextResult());


                    var result = reader.AsDataSet();

                    dataGridView1.DataSource = result.Tables[0];
                    for (int i = 0; i < result.Tables[0].Columns.Count; i++)
                    {
                        dataGridView1.Columns[i].HeaderText = result.Tables[0].Rows[0].ItemArray[i].ToString();
                    }
                    dataGridView1.Rows.RemoveAt(0);
                    
                }
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            dataGridView2.DataSource = CVS.CVSReader.Read(CVS.CVSReader.OpenCSV());
            dataGridView2.Columns[5].DefaultCellStyle.Format = "# ##0.00 \u20ac";
            dataGridView2.Columns[5].DefaultCellStyle.FormatProvider = new CultureInfo("en-US");
            dataGridView2.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView2.Columns[5].DefaultCellStyle.Font = new Font("Segoi UI", 11);
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            int m_numberOfReferncesSage = dataGridView1.Rows.Count;
            int m_numberOfReferncesWeb = dataGridView2.Rows.Count;

            List<Classes.Produtos> m_sage = new List<Classes.Produtos>();
            List<Classes.Produtos> m_website = new List<Classes.Produtos>();


            for (int i = 0; i < m_numberOfReferncesSage; i++)
            {
                string t1 = string.Empty;
                int i1 = 0;
                float f1 = 0;
                if (dataGridView1.Rows[i].Cells[0].Value != null)
                    t1 = dataGridView1.Rows[i].Cells[0].Value.ToString();
                if (dataGridView1.Rows[i].Cells[5].Value != null)
                {
                    int t = 0;
                    int.TryParse(dataGridView1.Rows[i].Cells[5].Value.ToString(), out t);
                    i1 = t;
                }
                if(dataGridView1.Rows[i].Cells[3].Value != null)
                {
                    float f = 0;
                    float.TryParse(dataGridView1.Rows[i].Cells[3].Value.ToString(), out f);
                    f1 = f;
                }
                m_sage.Add(new Classes.Produtos { _Ref = t1, _Stock = i1 , _Preço = f1});
                    
            }
            for (int i = 0; i < m_numberOfReferncesWeb; i++)
            {
                string t1 = string.Empty;
                int i1 = 0;
                float f1 = 0;
                if (dataGridView2.Rows[i].Cells[1].Value != null)
                    t1 = dataGridView2.Rows[i].Cells[1].Value.ToString();
                if (dataGridView2.Rows[i].Cells[4].Value != null)
                {
                    int t = 0;
                    int.TryParse(dataGridView2.Rows[i].Cells[4].Value.ToString(), out t);
                    i1 = t;
                }
                if (dataGridView2.Rows[i].Cells[5].Value != null)
                {
                    float f = 0;
                    float.TryParse(dataGridView2.Rows[i].Cells[5].Value.ToString(), out f);
                    f1 = f;
                }
                m_website.Add(new Classes.Produtos { _Ref = t1, _Stock = i1 , _Preço = f1});
            }
            int[] m_siteMod;
            Classes.Produtos[] site_Stock_Update = Tools.Stock.UpdateStock(m_sage.ToArray(), m_website.ToArray(), out m_siteMod);
            for (int i = 0; i < site_Stock_Update.Length; i++)
            {
                dataGridView2.Rows[i].Cells[4].Style.BackColor = Color.Aqua;
                dataGridView2.Rows[i].Cells[4].Value = site_Stock_Update[i]._Stock;
                dataGridView2.Rows[i].Cells[5].Value = site_Stock_Update[i]._Preço.ToString().Replace(',','.');
            }
            
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            OpenFileSage();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            var sb = new StringBuilder();

            var headers = dataGridView2.Columns.Cast<DataGridViewColumn>();
            sb.AppendLine(string.Join(",", headers.Select(column => "\"" + column.HeaderText + "\"").ToArray()));

            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                var cells = row.Cells.Cast<DataGridViewCell>();
                sb.AppendLine(string.Join(",", cells.Select(cell => "\"" + cell.Value + "\"").ToArray()));
            }

            File.WriteAllText(Application.StartupPath + "/test.csv", sb.ToString());
        }
    }
}

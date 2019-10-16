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

namespace MeioMundo.Controls
{
    public partial class UpdateStockControl : UserControl
    {
        public UpdateStockControl()
        {
            InitializeComponent();
            OpenFileSage();
            dataGridView1.DoubleBuffered(true);
            dataGridView2.DoubleBuffered(true);

        }
        public void OpenFileSage()
        {
            string fileSource = string.Empty;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if(openFileDialog.ShowDialog() == DialogResult.OK) 
            {
                fileSource = openFileDialog.FileName;
            }


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
        }
    }
}

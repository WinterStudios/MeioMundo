using System;
using System.Windows.Forms;

namespace MeioMundo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            // tabSystem1.FormParent = this;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void updateStockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Controls.UpdateStockControl updateStockControl = new Controls.UpdateStockControl
            {
                Dock = DockStyle.Fill
            };
            panel1.Controls.Add(updateStockControl);
        }

        private void barcodesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Controls.Barcode barcodes = new Controls.Barcode
            {
                Dock = DockStyle.Fill
            };
            // panel_window.Controls.Add(barcodes);
        }

        private void tabSystem1_Load(object sender, EventArgs e)
        {
            //tabSystem1.SetComponets();
        }
    }
}

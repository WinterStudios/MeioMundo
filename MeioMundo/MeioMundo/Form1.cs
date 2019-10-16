using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MeioMundo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void updateStockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Controls.UpdateStockControl updateStockControl = new Controls.UpdateStockControl();
            updateStockControl.Dock = DockStyle.Fill;
            panel_window.Controls.Add(updateStockControl);
        }
    }
}

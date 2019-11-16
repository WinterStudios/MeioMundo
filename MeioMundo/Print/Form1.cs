using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Print
{
    public partial class Form1 : Form
    {
        public const float InchToMm = 0.254f;
        public Form1()
        {
            InitializeComponent();
            Console.WriteLine(Properties.Resources.Voucher_50);
        }

        private void printToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            PrintDocument pd = new PrintDocument();
            var printerSettings = new PrinterSettings();
            
            printerSettings.DefaultPageSettings.PrinterResolution.Kind = PrinterResolutionKind.High;
            printerSettings.DefaultPageSettings.Margins = new Margins((int)Math.Round(5 * InchToMm), (int)Math.Round(0.5f * InchToMm), (int)Math.Round(0.5f * InchToMm), (int)Math.Round(0.5f * InchToMm));

            pd.OriginAtMargins = false;
            pd.PrintPage += new PrintPageEventHandler(this.pd_PrintPage);
            pd.DocumentName = "Vouchers";
            
            PrintDialog printdlg = new PrintDialog();
            PrintPreviewDialog printPrvDlg = new PrintPreviewDialog();
            printdlg.Document = pd;
            // preview the assigned document or you can create a different previewButton for it
            //printPrvDlg.Document = pd;
            //printPrvDlg.ShowDialog(); // this shows the preview and then show the Printer Dlg below
            if(printdlg.ShowDialog() == DialogResult.OK)
            {
                pd.Print();
            }
            
        }

        private void pd_PrintPage(object sender, PrintPageEventArgs e)
        {
            //e.Graphics.DrawString("0", DefaultFont, Brushes.Black, e.PageSettings.Bounds.Width - 30, 0);
            //e.Graphics.DrawString("1", DefaultFont, Brushes.Black, e.PageSettings.Bounds.Width - 40, 0);
            e.Graphics.DrawImage(Properties.Resources.Voucher_25, new Rectangle(0, 0, e.PageBounds.Width, (1132 * e.PageSettings.Bounds.Width / 2380)));
            e.Graphics.DrawImage(Properties.Resources.Voucher_50, new Rectangle(0, (1132 * e.PageSettings.Bounds.Width / 2380), e.PageBounds.Width, (1132 * e.PageSettings.Bounds.Width / 2380)));
            e.Graphics.DrawImage(Properties.Resources.Voucher_75, new Rectangle(0, (1132 * e.PageSettings.Bounds.Width / 2380) * 2, e.PageBounds.Width, (1132 * e.PageSettings.Bounds.Width / 2380)));

        }

    }
}

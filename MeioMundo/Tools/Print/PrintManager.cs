using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace Tools.Print
{
    public class PrintManager
    {
        public static Font printFont;
        public struct CODE
        {
            public string m_Descrição;
            public string m_Referencia;
        }
        public class CODE39
        {
            public List<CODE> _CODES = new List<CODE>();
            public void PrintBarCodes()
            {
                try
                {
                    printFont = new Font("Calibri Light", 8);
                    PrintDocument pd = new PrintDocument();
                    pd.PrintPage += new PrintPageEventHandler(pd_PrintPage);
                    // Create a new instance of Margins with 1-inch margins. // ou nao!!
                    pd.OriginAtMargins = true;
                    Margins margins = new Margins(30, 30, 30, 30);
                    pd.DefaultPageSettings.Margins = margins;

                    PrintPreviewDialog printPrvDlg = new PrintPreviewDialog();

                    // preview the assigned document or you can create a different previewButton for it
                    printPrvDlg.Document = pd;
                    printPrvDlg.ShowDialog();
                    pd.Print();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            private void pd_PrintPage(object sender, PrintPageEventArgs ev)
            {
                ev.HasMorePages = false;
                // DO THE STUFF TO PRINT
                int[] col = new int[]
                {
                    ev.MarginBounds.Width/3*0,
                    ev.MarginBounds.Width/3*1,
                    ev.MarginBounds.Width/3*2,
                    ev.MarginBounds.Width/3*3,
                };

                int t_celula_altura = 51;
                // Draw top Table Line
                ev.Graphics.DrawLine(Pens.Black, 0, 0, ev.MarginBounds.Width, 0);
                for (int z = 0; z < _CODES.Count; z++)
                {
                    ev.Graphics.DrawLine(Pens.Black, col[0], t_celula_altura * (z), col[0], t_celula_altura * (z + 1));
                    ev.Graphics.DrawLine(Pens.Black, col[1], t_celula_altura * (z), col[1], t_celula_altura * (z + 1));
                    ev.Graphics.DrawLine(Pens.Black, col[2], t_celula_altura * (z), col[2], t_celula_altura * (z + 1));
                    ev.Graphics.DrawLine(Pens.Black, col[3], t_celula_altura * (z), col[3], t_celula_altura * (z + 1));
                    ev.Graphics.DrawLine(Pens.Black, 0, t_celula_altura * (z + 1), ev.MarginBounds.Width, t_celula_altura * (z + 1));

                    int centerPointX = (ev.MarginBounds.Width / 3);
                    int centerPointY = t_celula_altura / 2;

                    SizeF desc_size = ev.Graphics.MeasureString(_CODES[z].m_Descrição, printFont);
                    SizeF ref_size = ev.Graphics.MeasureString("*" + _CODES[z].m_Referencia + "*", Barcode.BarcodeInternal.CODE_39.Font);
                    SizeF ref_size_num_only = ev.Graphics.MeasureString(_CODES[z].m_Referencia, printFont);
                    // Frist Collmn
                    ev.Graphics.DrawString(_CODES[z].m_Descrição, printFont, Brushes.Black, centerPointX * 0 + ((centerPointX - desc_size.Width) / 2), 5 + t_celula_altura * z);
                    ev.Graphics.DrawString("*" + _CODES[z].m_Referencia + "*", Barcode.BarcodeInternal.CODE_39.Font, Brushes.Black, ((centerPointX - ref_size.Width) / 2) + centerPointX * 0, 18 + t_celula_altura * z);
                    ev.Graphics.DrawString(_CODES[z].m_Referencia, printFont, Brushes.Black, centerPointX * 0 + ((centerPointX - ref_size_num_only.Width) / 2), 35 + t_celula_altura * z);

                    ev.Graphics.DrawString(_CODES[z].m_Descrição, printFont, Brushes.Black, centerPointX * 1 + ((centerPointX - desc_size.Width) / 2), 5 + t_celula_altura * z);
                    ev.Graphics.DrawString("*" + _CODES[z].m_Referencia + "*", Barcode.BarcodeInternal.CODE_39.Font, Brushes.Black, ((centerPointX - ref_size.Width) / 2) + centerPointX * 1, 18 + t_celula_altura * z);
                    ev.Graphics.DrawString(_CODES[z].m_Referencia, printFont, Brushes.Black, centerPointX * 1 + ((centerPointX - ref_size_num_only.Width) / 2), 35 + t_celula_altura * z);

                    ev.Graphics.DrawString(_CODES[z].m_Descrição, printFont, Brushes.Black, centerPointX * 2 + ((centerPointX - desc_size.Width) / 2), 5 + t_celula_altura * z);
                    ev.Graphics.DrawString("*" + _CODES[z].m_Referencia + "*", Barcode.BarcodeInternal.CODE_39.Font, Brushes.Black, ((centerPointX - ref_size.Width) / 2) + centerPointX * 2, 18 + t_celula_altura * z);
                    ev.Graphics.DrawString(_CODES[z].m_Referencia, printFont, Brushes.Black, centerPointX * 2 + ((centerPointX - ref_size_num_only.Width) / 2), 35 + t_celula_altura * z);


                }

            }
        }
    }
}

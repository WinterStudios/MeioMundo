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
                    PrintDialog printDialog = new PrintDialog();

                    // preview the assigned document or you can create a different previewButton for it
                    printPrvDlg.Document = pd;
                    printPrvDlg.ShowDialog();
                    printDialog.Document = pd;
                    if (printDialog.ShowDialog() == DialogResult.OK)
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
                int lines = (_CODES.Count / 3) + (_CODES.Count % 3);

                // Drawing Lines for rows and collumns
                for (int z = 0; z < lines; z++)
                {
                    ev.Graphics.DrawLine(Pens.Black, col[0], t_celula_altura * (z), col[0], t_celula_altura * (z + 1));
                    ev.Graphics.DrawLine(Pens.Black, col[1], t_celula_altura * (z), col[1], t_celula_altura * (z + 1));
                    ev.Graphics.DrawLine(Pens.Black, col[2], t_celula_altura * (z), col[2], t_celula_altura * (z + 1));
                    ev.Graphics.DrawLine(Pens.Black, col[3], t_celula_altura * (z), col[3], t_celula_altura * (z + 1));
                    ev.Graphics.DrawLine(Pens.Black, 0, t_celula_altura * (z + 1), ev.MarginBounds.Width, t_celula_altura * (z + 1));

                }

                int centerPointX = (ev.MarginBounds.Width / 3);
                int centerPointY = t_celula_altura / 2;
                // Draw the codes
                for (int x = 0; x < _CODES.Count; x++)
                {
                    int line = x / 3;
                    // Frist Collmn
                    if (x % 3 == 0)
                    {
                        SizeF _desc_size = ev.Graphics.MeasureString(_CODES[x].m_Descrição, printFont);
                        SizeF _ref_size = ev.Graphics.MeasureString("*" + _CODES[x].m_Referencia + "*", Barcode.BarcodeInternal.CODE_39.Font);
                        SizeF _ref_size_num_only = ev.Graphics.MeasureString(_CODES[x].m_Referencia, printFont);


                        ev.Graphics.DrawString(_CODES[x].m_Descrição, printFont, Brushes.Black, centerPointX * 0 + ((centerPointX - _desc_size.Width) / 2), 5 + t_celula_altura * line);
                        ev.Graphics.DrawString("*" + _CODES[x].m_Referencia + "*", Barcode.BarcodeInternal.CODE_39.Font, Brushes.Black, ((centerPointX - _ref_size.Width) / 2) + centerPointX * 0, 18 + t_celula_altura * line);
                        ev.Graphics.DrawString(_CODES[x].m_Referencia, printFont, Brushes.Black, centerPointX * 0 + ((centerPointX - _ref_size_num_only.Width) / 2), 35 + t_celula_altura * line);
                    }
                    // Second Collumn
                    if (x % 3 == 1)
                    {
                        SizeF _desc_size = ev.Graphics.MeasureString(_CODES[x].m_Descrição, printFont);
                        SizeF _ref_size = ev.Graphics.MeasureString("*" + _CODES[x].m_Referencia + "*", Barcode.BarcodeInternal.CODE_39.Font);
                        SizeF _ref_size_num_only = ev.Graphics.MeasureString(_CODES[x].m_Referencia, printFont);


                        ev.Graphics.DrawString(_CODES[x].m_Descrição, printFont, Brushes.Black, centerPointX * 1 + ((centerPointX - _desc_size.Width) / 2), 5 + t_celula_altura * line);
                        ev.Graphics.DrawString("*" + _CODES[x].m_Referencia + "*", Barcode.BarcodeInternal.CODE_39.Font, Brushes.Black, ((centerPointX - _ref_size.Width) / 2) + centerPointX * 1, 18 + t_celula_altura * line);
                        ev.Graphics.DrawString(_CODES[x].m_Referencia, printFont, Brushes.Black, centerPointX * 1 + ((centerPointX - _ref_size_num_only.Width) / 2), 35 + t_celula_altura * line);
                    }
                    //// Threed Collumn
                    if (x % 3 == 2)
                    {
                        SizeF _desc_size = ev.Graphics.MeasureString(_CODES[x].m_Descrição, printFont);
                        SizeF _ref_size = ev.Graphics.MeasureString("*" + _CODES[x].m_Referencia + "*", Barcode.BarcodeInternal.CODE_39.Font);
                        SizeF _ref_size_num_only = ev.Graphics.MeasureString(_CODES[x].m_Referencia, printFont);


                        ev.Graphics.DrawString(_CODES[x].m_Descrição, printFont, Brushes.Black, centerPointX * 2 + ((centerPointX - _desc_size.Width) / 2), 5 + t_celula_altura * line);
                        ev.Graphics.DrawString("*" + _CODES[x].m_Referencia + "*", Barcode.BarcodeInternal.CODE_39.Font, Brushes.Black, ((centerPointX - _ref_size.Width) / 2) + centerPointX * 2, 18 + t_celula_altura * line);
                        ev.Graphics.DrawString(_CODES[x].m_Referencia, printFont, Brushes.Black, centerPointX * 2 + ((centerPointX - _ref_size_num_only.Width) / 2), 35 + t_celula_altura * line);
                    }
                }



            }


        }
    }
}

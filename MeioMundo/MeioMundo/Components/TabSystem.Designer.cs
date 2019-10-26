namespace MeioMundo.Components
{
    partial class TabSystem
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.TabsPages = new System.Windows.Forms.Panel();
            this.Pages = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // TabsPages
            // 
            this.TabsPages.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TabsPages.BackColor = System.Drawing.Color.YellowGreen;
            this.TabsPages.Location = new System.Drawing.Point(0, 0);
            this.TabsPages.Margin = new System.Windows.Forms.Padding(0);
            this.TabsPages.Name = "TabsPages";
            this.TabsPages.Size = new System.Drawing.Size(150, 30);
            this.TabsPages.TabIndex = 0;
            // 
            // Pages
            // 
            this.Pages.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Pages.BackColor = System.Drawing.Color.Gainsboro;
            this.Pages.Location = new System.Drawing.Point(0, 30);
            this.Pages.Margin = new System.Windows.Forms.Padding(0);
            this.Pages.Name = "Pages";
            this.Pages.Size = new System.Drawing.Size(150, 120);
            this.Pages.TabIndex = 1;
            // 
            // TabSystem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Pages);
            this.Controls.Add(this.TabsPages);
            this.Name = "TabSystem";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel TabsPages;
        private System.Windows.Forms.Panel Pages;
    }
}

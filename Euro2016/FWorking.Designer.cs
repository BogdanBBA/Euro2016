namespace Euro2016
{
    partial class FWorking
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.startT = new System.Windows.Forms.Timer(this.components);
            this.closeT = new System.Windows.Forms.Timer(this.components);
            this.statusTL = new Euro2016.VisualComponents.TitleLabel();
            this.SuspendLayout();
            // 
            // startT
            // 
            this.startT.Tick += new System.EventHandler(this.startT_Tick);
            // 
            // closeT
            // 
            this.closeT.Tick += new System.EventHandler(this.closeT_Tick);
            // 
            // statusTL
            // 
            this.statusTL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(50)))), ((int)(((byte)(74)))));
            this.statusTL.BigBar = false;
            this.statusTL.Checked = false;
            this.statusTL.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.statusTL.DrawBar = true;
            this.statusTL.Location = new System.Drawing.Point(12, 12);
            this.statusTL.MouseIsClicked = false;
            this.statusTL.MouseIsOver = false;
            this.statusTL.Name = "statusTL";
            this.statusTL.Size = new System.Drawing.Size(459, 80);
            this.statusTL.TabIndex = 0;
            this.statusTL.Text = "titleLabel1";
            this.statusTL.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.statusTL.TextSubtitle = "Status";
            this.statusTL.TextTitle = "Working...";
            // 
            // FWorking
            // 
            this.ClientSize = new System.Drawing.Size(483, 104);
            this.Controls.Add(this.statusTL);
            this.Name = "FWorking";
            this.Load += new System.EventHandler(this.FWorking_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer startT;
        private System.Windows.Forms.Timer closeT;
        private VisualComponents.TitleLabel statusTL;

    }
}


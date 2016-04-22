namespace Euro2016
{
    partial class FMap
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
            this.titleLabel1 = new Euro2016.VisualComponents.TitleLabel();
            this.mapMSM = new Euro2016.VisualComponents.MySvgMap();
            this.SuspendLayout();
            // 
            // titleLabel1
            // 
            this.titleLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(50)))), ((int)(((byte)(74)))));
            this.titleLabel1.BigBar = false;
            this.titleLabel1.Checked = false;
            this.titleLabel1.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.titleLabel1.DrawBar = true;
            this.titleLabel1.Location = new System.Drawing.Point(12, 12);
            this.titleLabel1.Name = "titleLabel1";
            this.titleLabel1.Size = new System.Drawing.Size(1014, 78);
            this.titleLabel1.TabIndex = 1;
            this.titleLabel1.Text = "titleLabel1";
            this.titleLabel1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.titleLabel1.TextSubtitle = "Map of countries and their results in the tournament";
            this.titleLabel1.TextTitle = "Euro 2016 map";
            // 
            // mapMSM
            // 
            this.mapMSM.Location = new System.Drawing.Point(12, 96);
            this.mapMSM.Name = "mapMSM";
            this.mapMSM.OnDrawFinishCallback = null;
            this.mapMSM.Size = new System.Drawing.Size(1014, 912);
            this.mapMSM.TabIndex = 2;
            this.mapMSM.TabStop = false;
            // 
            // FMap
            // 
            this.ClientSize = new System.Drawing.Size(1037, 1020);
            this.Controls.Add(this.mapMSM);
            this.Controls.Add(this.titleLabel1);
            this.Name = "FMap";
            this.Load += new System.EventHandler(this.FMap_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private VisualComponents.TitleLabel titleLabel1;
        private VisualComponents.MySvgMap mapMSM;

    }
}
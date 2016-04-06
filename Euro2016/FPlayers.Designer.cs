namespace Euro2016
{
    partial class FPlayers
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
            this.countriesP = new Euro2016.VisualComponents.MyPanel();
            this.myPanel1 = new Euro2016.VisualComponents.MyPanel();
            this.flagPB = new System.Windows.Forms.PictureBox();
            this.countryIV = new Euro2016.VisualComponents.InfoView();
            ((System.ComponentModel.ISupportInitialize)(this.flagPB)).BeginInit();
            this.SuspendLayout();
            // 
            // myPanel2
            // 
            this.countriesP.DrawPanelAccent = false;
            this.countriesP.Location = new System.Drawing.Point(12, 12);
            this.countriesP.Name = "myPanel2";
            this.countriesP.Size = new System.Drawing.Size(200, 744);
            this.countriesP.TabIndex = 1;
            // 
            // myPanel1
            // 
            this.myPanel1.DrawPanelAccent = false;
            this.myPanel1.Location = new System.Drawing.Point(218, 244);
            this.myPanel1.Name = "myPanel1";
            this.myPanel1.Size = new System.Drawing.Size(500, 512);
            this.myPanel1.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.flagPB.BackColor = System.Drawing.Color.Transparent;
            this.flagPB.Location = new System.Drawing.Point(218, 12);
            this.flagPB.Name = "pictureBox1";
            this.flagPB.Size = new System.Drawing.Size(160, 100);
            this.flagPB.TabIndex = 2;
            this.flagPB.TabStop = false;
            // 
            // infoView1
            // 
            this.countryIV.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(50)))), ((int)(((byte)(74)))));
            this.countryIV.BigBar = false;
            this.countryIV.Checked = false;
            this.countryIV.DrawBar = true;
            this.countryIV.Location = new System.Drawing.Point(384, 12);
            this.countryIV.Name = "infoView1";
            this.countryIV.Size = new System.Drawing.Size(333, 52);
            this.countryIV.TabIndex = 3;
            this.countryIV.Text = "infoView1";
            this.countryIV.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.countryIV.TextDescription = "Players of";
            this.countryIV.TextText = "#text";
            // 
            // FPlayers
            // 
            this.ClientSize = new System.Drawing.Size(729, 769);
            this.Controls.Add(this.countryIV);
            this.Controls.Add(this.flagPB);
            this.Controls.Add(this.countriesP);
            this.Controls.Add(this.myPanel1);
            this.Name = "FPlayers";
            this.Load += new System.EventHandler(this.FPlayers_Load);
            ((System.ComponentModel.ISupportInitialize)(this.flagPB)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private VisualComponents.MyPanel myPanel1;
        private VisualComponents.MyPanel countriesP;
        private System.Windows.Forms.PictureBox flagPB;
        private VisualComponents.InfoView countryIV;
    }
}
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
            this.flagPB = new System.Windows.Forms.PictureBox();
            this.infoViewDetail2 = new Euro2016.VisualComponents.InfoViewDetail();
            this.coachIVD = new Euro2016.VisualComponents.InfoViewDetail();
            this.titleLabel1 = new Euro2016.VisualComponents.TitleLabel();
            this.countryIV = new Euro2016.VisualComponents.InfoView();
            this.countriesP = new Euro2016.VisualComponents.MyPanel();
            this.playerP = new Euro2016.VisualComponents.MyPanel();
            this.coachFlagPB = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.flagPB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.coachFlagPB)).BeginInit();
            this.SuspendLayout();
            // 
            // flagPB
            // 
            this.flagPB.BackColor = System.Drawing.Color.Transparent;
            this.flagPB.Cursor = System.Windows.Forms.Cursors.Hand;
            this.flagPB.Location = new System.Drawing.Point(218, 96);
            this.flagPB.Name = "flagPB";
            this.flagPB.Size = new System.Drawing.Size(160, 100);
            this.flagPB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.flagPB.TabIndex = 2;
            this.flagPB.TabStop = false;
            // 
            // infoViewDetail2
            // 
            this.infoViewDetail2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(50)))), ((int)(((byte)(74)))));
            this.infoViewDetail2.BigBar = false;
            this.infoViewDetail2.Checked = false;
            this.infoViewDetail2.DrawBar = false;
            this.infoViewDetail2.Location = new System.Drawing.Point(768, 161);
            this.infoViewDetail2.Name = "infoViewDetail2";
            this.infoViewDetail2.Size = new System.Drawing.Size(250, 35);
            this.infoViewDetail2.TabIndex = 6;
            this.infoViewDetail2.Text = "infoViewDetail2";
            this.infoViewDetail2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.infoViewDetail2.TextDescription = "Information calculated for";
            this.infoViewDetail2.TextText = "(...first match day)";
            // 
            // coachIVD
            // 
            this.coachIVD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(50)))), ((int)(((byte)(74)))));
            this.coachIVD.BigBar = false;
            this.coachIVD.Checked = false;
            this.coachIVD.DrawBar = false;
            this.coachIVD.Location = new System.Drawing.Point(454, 161);
            this.coachIVD.Name = "coachIVD";
            this.coachIVD.Size = new System.Drawing.Size(308, 35);
            this.coachIVD.TabIndex = 5;
            this.coachIVD.Text = "infoViewDetail1";
            this.coachIVD.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.coachIVD.TextDescription = "Coach";
            this.coachIVD.TextText = "#text";
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
            this.titleLabel1.Size = new System.Drawing.Size(1006, 78);
            this.titleLabel1.TabIndex = 4;
            this.titleLabel1.Text = "titleLabel1";
            this.titleLabel1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.titleLabel1.TextSubtitle = "Team squads and coaches";
            this.titleLabel1.TextTitle = "Players";
            // 
            // countryIV
            // 
            this.countryIV.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(50)))), ((int)(((byte)(74)))));
            this.countryIV.BigBar = false;
            this.countryIV.Checked = false;
            this.countryIV.Cursor = System.Windows.Forms.Cursors.Hand;
            this.countryIV.DrawBar = false;
            this.countryIV.Location = new System.Drawing.Point(384, 96);
            this.countryIV.Name = "countryIV";
            this.countryIV.Size = new System.Drawing.Size(634, 52);
            this.countryIV.TabIndex = 3;
            this.countryIV.Text = "infoView1";
            this.countryIV.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.countryIV.TextDescription = "Players of";
            this.countryIV.TextText = "#text";
            this.countryIV.Click += new System.EventHandler(this.countryIV_Click);
            // 
            // countriesP
            // 
            this.countriesP.DrawPanelAccent = false;
            this.countriesP.Location = new System.Drawing.Point(12, 96);
            this.countriesP.Name = "countriesP";
            this.countriesP.Size = new System.Drawing.Size(200, 682);
            this.countriesP.TabIndex = 1;
            // 
            // playerP
            // 
            this.playerP.DrawPanelAccent = false;
            this.playerP.Location = new System.Drawing.Point(218, 202);
            this.playerP.Name = "playerP";
            this.playerP.Size = new System.Drawing.Size(800, 576);
            this.playerP.TabIndex = 0;
            // 
            // coachFlagPB
            // 
            this.coachFlagPB.BackColor = System.Drawing.Color.Transparent;
            this.coachFlagPB.Location = new System.Drawing.Point(384, 156);
            this.coachFlagPB.Name = "coachFlagPB";
            this.coachFlagPB.Size = new System.Drawing.Size(64, 40);
            this.coachFlagPB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.coachFlagPB.TabIndex = 7;
            this.coachFlagPB.TabStop = false;
            // 
            // FPlayers
            // 
            this.ClientSize = new System.Drawing.Size(1030, 789);
            this.Controls.Add(this.coachFlagPB);
            this.Controls.Add(this.infoViewDetail2);
            this.Controls.Add(this.coachIVD);
            this.Controls.Add(this.titleLabel1);
            this.Controls.Add(this.countryIV);
            this.Controls.Add(this.flagPB);
            this.Controls.Add(this.countriesP);
            this.Controls.Add(this.playerP);
            this.Name = "FPlayers";
            this.Load += new System.EventHandler(this.FPlayers_Load);
            ((System.ComponentModel.ISupportInitialize)(this.flagPB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.coachFlagPB)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private VisualComponents.MyPanel playerP;
        private VisualComponents.MyPanel countriesP;
        private System.Windows.Forms.PictureBox flagPB;
        private VisualComponents.InfoView countryIV;
        private VisualComponents.TitleLabel titleLabel1;
        private VisualComponents.InfoViewDetail coachIVD;
        private VisualComponents.InfoViewDetail infoViewDetail2;
        private System.Windows.Forms.PictureBox coachFlagPB;
    }
}
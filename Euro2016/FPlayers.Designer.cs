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
            this.playersOwnCountryL = new Euro2016.VisualComponents.SmoothLabel();
            this.coachFlagPB = new System.Windows.Forms.PictureBox();
            this.infoViewDetail2 = new Euro2016.VisualComponents.InfoViewDetail();
            this.coachIVD = new Euro2016.VisualComponents.InfoViewDetail();
            this.titleLabel1 = new Euro2016.VisualComponents.TitleLabel();
            this.countryIV = new Euro2016.VisualComponents.InfoView();
            this.flagPB = new System.Windows.Forms.PictureBox();
            this.countriesP = new Euro2016.VisualComponents.MyPanel();
            this.playerP = new Euro2016.VisualComponents.MyPanel();
            this.playersOwnCountryPB = new Euro2016.VisualComponents.MyProgressBar();
            this.averageAgeIVD = new Euro2016.VisualComponents.InfoViewDetail();
            ((System.ComponentModel.ISupportInitialize)(this.coachFlagPB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.flagPB)).BeginInit();
            this.SuspendLayout();
            // 
            // playersOwnCountryL
            // 
            this.playersOwnCountryL.BackColor = System.Drawing.Color.Transparent;
            this.playersOwnCountryL.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.playersOwnCountryL.Location = new System.Drawing.Point(973, 143);
            this.playersOwnCountryL.Name = "playersOwnCountryL";
            this.playersOwnCountryL.Size = new System.Drawing.Size(215, 24);
            this.playersOwnCountryL.TabIndex = 9;
            this.playersOwnCountryL.Text = "Players playing in own country";
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
            // infoViewDetail2
            // 
            this.infoViewDetail2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(50)))), ((int)(((byte)(74)))));
            this.infoViewDetail2.BigBar = false;
            this.infoViewDetail2.Checked = false;
            this.infoViewDetail2.DrawBar = false;
            this.infoViewDetail2.Location = new System.Drawing.Point(973, 96);
            this.infoViewDetail2.MouseIsClicked = false;
            this.infoViewDetail2.MouseIsOver = false;
            this.infoViewDetail2.Name = "infoViewDetail2";
            this.infoViewDetail2.Size = new System.Drawing.Size(215, 35);
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
            this.coachIVD.MouseIsClicked = false;
            this.coachIVD.MouseIsOver = false;
            this.coachIVD.Name = "coachIVD";
            this.coachIVD.Size = new System.Drawing.Size(398, 35);
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
            this.titleLabel1.MouseIsClicked = false;
            this.titleLabel1.MouseIsOver = false;
            this.titleLabel1.Name = "titleLabel1";
            this.titleLabel1.Size = new System.Drawing.Size(1176, 78);
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
            this.countryIV.MouseIsClicked = false;
            this.countryIV.MouseIsOver = false;
            this.countryIV.Name = "countryIV";
            this.countryIV.Size = new System.Drawing.Size(583, 52);
            this.countryIV.TabIndex = 3;
            this.countryIV.Text = "infoView1";
            this.countryIV.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.countryIV.TextDescription = "Players of";
            this.countryIV.TextText = "#text";
            this.countryIV.Click += new System.EventHandler(this.flagPB_Click);
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
            this.flagPB.Click += new System.EventHandler(this.flagPB_Click);
            // 
            // countriesP
            // 
            this.countriesP.DrawPanelAccent = false;
            this.countriesP.Location = new System.Drawing.Point(12, 96);
            this.countriesP.Name = "countriesP";
            this.countriesP.Size = new System.Drawing.Size(200, 694);
            this.countriesP.TabIndex = 1;
            // 
            // playerP
            // 
            this.playerP.DrawPanelAccent = false;
            this.playerP.Location = new System.Drawing.Point(218, 202);
            this.playerP.Name = "playerP";
            this.playerP.Size = new System.Drawing.Size(970, 588);
            this.playerP.TabIndex = 0;
            // 
            // playersOwnCountryPB
            // 
            this.playersOwnCountryPB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(64)))), ((int)(((byte)(92)))));
            this.playersOwnCountryPB.Checked = false;
            this.playersOwnCountryPB.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.playersOwnCountryPB.FontBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.playersOwnCountryPB.FontForeColor = System.Drawing.Color.WhiteSmoke;
            this.playersOwnCountryPB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.playersOwnCountryPB.Location = new System.Drawing.Point(973, 161);
            this.playersOwnCountryPB.Maximum = 100;
            this.playersOwnCountryPB.Minimum = 0;
            this.playersOwnCountryPB.MinimumSize = new System.Drawing.Size(74, 13);
            this.playersOwnCountryPB.MouseIsClicked = false;
            this.playersOwnCountryPB.MouseIsOver = false;
            this.playersOwnCountryPB.Name = "playersOwnCountryPB";
            this.playersOwnCountryPB.Size = new System.Drawing.Size(215, 35);
            this.playersOwnCountryPB.TabIndex = 10;
            this.playersOwnCountryPB.Text = "myProgressBar1";
            this.playersOwnCountryPB.Value = 60;
            this.playersOwnCountryPB.ValueBoxWidth = 100;
            this.playersOwnCountryPB.ValueFormat = "{0} / {2}";
            // 
            // averageAgeIVD
            // 
            this.averageAgeIVD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(50)))), ((int)(((byte)(74)))));
            this.averageAgeIVD.BigBar = false;
            this.averageAgeIVD.Checked = false;
            this.averageAgeIVD.DrawBar = false;
            this.averageAgeIVD.Location = new System.Drawing.Point(858, 161);
            this.averageAgeIVD.MouseIsClicked = false;
            this.averageAgeIVD.MouseIsOver = false;
            this.averageAgeIVD.Name = "averageAgeIVD";
            this.averageAgeIVD.Size = new System.Drawing.Size(96, 35);
            this.averageAgeIVD.TabIndex = 11;
            this.averageAgeIVD.Text = "infoViewDetail1";
            this.averageAgeIVD.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.averageAgeIVD.TextDescription = "Average age";
            this.averageAgeIVD.TextText = "#text";
            // 
            // FPlayers
            // 
            this.ClientSize = new System.Drawing.Size(1200, 802);
            this.Controls.Add(this.averageAgeIVD);
            this.Controls.Add(this.playersOwnCountryPB);
            this.Controls.Add(this.playersOwnCountryL);
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
            ((System.ComponentModel.ISupportInitialize)(this.coachFlagPB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.flagPB)).EndInit();
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
        private VisualComponents.SmoothLabel playersOwnCountryL;
        private VisualComponents.MyProgressBar playersOwnCountryPB;
        private VisualComponents.InfoViewDetail averageAgeIVD;
    }
}
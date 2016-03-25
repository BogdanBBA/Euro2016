namespace Euro2016
{
    partial class FVenue
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

        private void InitializeComponent()
        {
            this.venueCityIV = new Euro2016.VisualComponents.InfoView();
            this.venueNameIV = new Euro2016.VisualComponents.InfoView();
            this.titleLabel1 = new Euro2016.VisualComponents.TitleLabel();
            this.stadiumInsidePB = new System.Windows.Forms.PictureBox();
            this.stadiumOutsidePB = new System.Windows.Forms.PictureBox();
            this.cityPB = new System.Windows.Forms.PictureBox();
            this.matchesP = new Euro2016.VisualComponents.MyPanel();
            this.locationPB = new System.Windows.Forms.PictureBox();
            this.venueButtonP = new Euro2016.VisualComponents.MyPanel();
            ((System.ComponentModel.ISupportInitialize)(this.stadiumInsidePB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stadiumOutsidePB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cityPB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.locationPB)).BeginInit();
            this.SuspendLayout();
            // 
            // infoView2
            // 
            this.venueCityIV.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(50)))), ((int)(((byte)(74)))));
            this.venueCityIV.BigBar = true;
            this.venueCityIV.Checked = false;
            this.venueCityIV.DrawBar = false;
            this.venueCityIV.Location = new System.Drawing.Point(218, 152);
            this.venueCityIV.Name = "infoView2";
            this.venueCityIV.Size = new System.Drawing.Size(385, 50);
            this.venueCityIV.TabIndex = 11;
            this.venueCityIV.Text = "infoView2";
            this.venueCityIV.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.venueCityIV.TextDescription = "City of";
            this.venueCityIV.TextText = "#text";
            // 
            // infoView1
            // 
            this.venueNameIV.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(50)))), ((int)(((byte)(74)))));
            this.venueNameIV.BigBar = true;
            this.venueNameIV.Checked = false;
            this.venueNameIV.DrawBar = false;
            this.venueNameIV.Location = new System.Drawing.Point(218, 96);
            this.venueNameIV.Name = "infoView1";
            this.venueNameIV.Size = new System.Drawing.Size(385, 50);
            this.venueNameIV.TabIndex = 10;
            this.venueNameIV.Text = "infoView1";
            this.venueNameIV.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.venueNameIV.TextDescription = "Stadium name";
            this.venueNameIV.TextText = "#text";
            // 
            // titleLabel1
            // 
            this.titleLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(50)))), ((int)(((byte)(74)))));
            this.titleLabel1.BigBar = false;
            this.titleLabel1.Checked = false;
            this.titleLabel1.DrawBar = true;
            this.titleLabel1.Location = new System.Drawing.Point(12, 12);
            this.titleLabel1.Name = "titleLabel1";
            this.titleLabel1.Size = new System.Drawing.Size(591, 78);
            this.titleLabel1.TabIndex = 9;
            this.titleLabel1.Text = "titleLabel1";
            this.titleLabel1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.titleLabel1.TextSubtitle = "Host cities and football stadiums";
            this.titleLabel1.TextTitle = "Venues";
            // 
            // stadiumInsidePB
            // 
            this.stadiumInsidePB.BackColor = System.Drawing.Color.Transparent;
            this.stadiumInsidePB.Location = new System.Drawing.Point(802, 403);
            this.stadiumInsidePB.Name = "stadiumInsidePB";
            this.stadiumInsidePB.Size = new System.Drawing.Size(286, 205);
            this.stadiumInsidePB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.stadiumInsidePB.TabIndex = 7;
            this.stadiumInsidePB.TabStop = false;
            // 
            // stadiumOutsidePB
            // 
            this.stadiumOutsidePB.BackColor = System.Drawing.Color.Transparent;
            this.stadiumOutsidePB.Location = new System.Drawing.Point(510, 403);
            this.stadiumOutsidePB.Name = "stadiumOutsidePB";
            this.stadiumOutsidePB.Size = new System.Drawing.Size(286, 205);
            this.stadiumOutsidePB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.stadiumOutsidePB.TabIndex = 6;
            this.stadiumOutsidePB.TabStop = false;
            // 
            // cityPB
            // 
            this.cityPB.BackColor = System.Drawing.Color.Transparent;
            this.cityPB.Location = new System.Drawing.Point(218, 403);
            this.cityPB.Name = "cityPB";
            this.cityPB.Size = new System.Drawing.Size(286, 205);
            this.cityPB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.cityPB.TabIndex = 5;
            this.cityPB.TabStop = false;
            // 
            // matchesP
            // 
            this.matchesP.DrawPanelAccent = false;
            this.matchesP.Location = new System.Drawing.Point(609, 12);
            this.matchesP.Name = "matchesP";
            this.matchesP.Size = new System.Drawing.Size(480, 385);
            this.matchesP.TabIndex = 4;
            // 
            // locationPB
            // 
            this.locationPB.BackColor = System.Drawing.Color.Transparent;
            this.locationPB.Location = new System.Drawing.Point(218, 208);
            this.locationPB.Name = "locationPB";
            this.locationPB.Size = new System.Drawing.Size(200, 189);
            this.locationPB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.locationPB.TabIndex = 3;
            this.locationPB.TabStop = false;
            // 
            // venueButtonP
            // 
            this.venueButtonP.DrawPanelAccent = false;
            this.venueButtonP.Location = new System.Drawing.Point(12, 96);
            this.venueButtonP.Name = "venueButtonP";
            this.venueButtonP.Size = new System.Drawing.Size(200, 512);
            this.venueButtonP.TabIndex = 1;
            // 
            // FVenue
            // 
            this.ClientSize = new System.Drawing.Size(1100, 620);
            this.Controls.Add(this.venueCityIV);
            this.Controls.Add(this.venueNameIV);
            this.Controls.Add(this.titleLabel1);
            this.Controls.Add(this.stadiumInsidePB);
            this.Controls.Add(this.stadiumOutsidePB);
            this.Controls.Add(this.cityPB);
            this.Controls.Add(this.matchesP);
            this.Controls.Add(this.locationPB);
            this.Controls.Add(this.venueButtonP);
            this.Name = "FVenue";
            this.Load += new System.EventHandler(this.FVenues_Load);
            ((System.ComponentModel.ISupportInitialize)(this.stadiumInsidePB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stadiumOutsidePB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cityPB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.locationPB)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private VisualComponents.MyPanel venueButtonP;
        private System.Windows.Forms.PictureBox locationPB;
        private VisualComponents.MyPanel matchesP;
        private System.Windows.Forms.PictureBox cityPB;
        private System.Windows.Forms.PictureBox stadiumOutsidePB;
        private System.Windows.Forms.PictureBox stadiumInsidePB;
        private VisualComponents.TitleLabel titleLabel1;
        private VisualComponents.InfoView venueNameIV;
        private VisualComponents.InfoView venueCityIV;
    }
}
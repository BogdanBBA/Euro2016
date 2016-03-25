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
            this.subtitleL = new System.Windows.Forms.Label();
            this.stadiumInsidePB = new System.Windows.Forms.PictureBox();
            this.stadiumOutsidePB = new System.Windows.Forms.PictureBox();
            this.cityPB = new System.Windows.Forms.PictureBox();
            this.locationPB = new System.Windows.Forms.PictureBox();
            this.titleL = new System.Windows.Forms.Label();
            this.titleLabel1 = new Euro2016.VisualComponents.TitleLabel();
            this.matchesP = new Euro2016.VisualComponents.MyPanel();
            this.venueButtonP = new Euro2016.VisualComponents.MyPanel();
            ((System.ComponentModel.ISupportInitialize)(this.stadiumInsidePB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stadiumOutsidePB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cityPB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.locationPB)).BeginInit();
            this.SuspendLayout();
            // 
            // subtitleL
            // 
            this.subtitleL.AutoSize = true;
            this.subtitleL.BackColor = System.Drawing.Color.Transparent;
            this.subtitleL.Font = new System.Drawing.Font("Segoe UI Semibold", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.subtitleL.ForeColor = System.Drawing.Color.White;
            this.subtitleL.Location = new System.Drawing.Point(218, 119);
            this.subtitleL.Name = "subtitleL";
            this.subtitleL.Size = new System.Drawing.Size(55, 23);
            this.subtitleL.TabIndex = 8;
            this.subtitleL.Text = "label2";
            // 
            // stadiumInsidePB
            // 
            this.stadiumInsidePB.BackColor = System.Drawing.Color.Transparent;
            this.stadiumInsidePB.Location = new System.Drawing.Point(805, 418);
            this.stadiumInsidePB.Name = "stadiumInsidePB";
            this.stadiumInsidePB.Size = new System.Drawing.Size(284, 205);
            this.stadiumInsidePB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.stadiumInsidePB.TabIndex = 7;
            this.stadiumInsidePB.TabStop = false;
            // 
            // stadiumOutsidePB
            // 
            this.stadiumOutsidePB.BackColor = System.Drawing.Color.Transparent;
            this.stadiumOutsidePB.Location = new System.Drawing.Point(512, 418);
            this.stadiumOutsidePB.Name = "stadiumOutsidePB";
            this.stadiumOutsidePB.Size = new System.Drawing.Size(287, 205);
            this.stadiumOutsidePB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.stadiumOutsidePB.TabIndex = 6;
            this.stadiumOutsidePB.TabStop = false;
            // 
            // cityPB
            // 
            this.cityPB.BackColor = System.Drawing.Color.Transparent;
            this.cityPB.Location = new System.Drawing.Point(222, 418);
            this.cityPB.Name = "cityPB";
            this.cityPB.Size = new System.Drawing.Size(284, 205);
            this.cityPB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.cityPB.TabIndex = 5;
            this.cityPB.TabStop = false;
            // 
            // locationPB
            // 
            this.locationPB.BackColor = System.Drawing.Color.Transparent;
            this.locationPB.Location = new System.Drawing.Point(222, 223);
            this.locationPB.Name = "locationPB";
            this.locationPB.Size = new System.Drawing.Size(200, 189);
            this.locationPB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.locationPB.TabIndex = 3;
            this.locationPB.TabStop = false;
            // 
            // titleL
            // 
            this.titleL.AutoSize = true;
            this.titleL.BackColor = System.Drawing.Color.Transparent;
            this.titleL.Font = new System.Drawing.Font("Segoe UI Semibold", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleL.ForeColor = System.Drawing.Color.White;
            this.titleL.Location = new System.Drawing.Point(218, 96);
            this.titleL.Name = "titleL";
            this.titleL.Size = new System.Drawing.Size(55, 23);
            this.titleL.TabIndex = 2;
            this.titleL.Text = "label1";
            // 
            // titleLabel1
            // 
            this.titleLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(50)))), ((int)(((byte)(74)))));
            this.titleLabel1.Location = new System.Drawing.Point(12, 12);
            this.titleLabel1.Name = "titleLabel1";
            this.titleLabel1.Size = new System.Drawing.Size(591, 78);
            this.titleLabel1.TabIndex = 9;
            this.titleLabel1.Text = "titleLabel1";
            this.titleLabel1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.titleLabel1.TextSubtitle = "Host cities and football stadiums";
            this.titleLabel1.TextTitle = "Venues";
            // 
            // matchesP
            // 
            this.matchesP.DrawPanelAccent = false;
            this.matchesP.Location = new System.Drawing.Point(609, 12);
            this.matchesP.Name = "matchesP";
            this.matchesP.Size = new System.Drawing.Size(480, 400);
            this.matchesP.TabIndex = 4;
            // 
            // venueButtonP
            // 
            this.venueButtonP.DrawPanelAccent = false;
            this.venueButtonP.Location = new System.Drawing.Point(12, 96);
            this.venueButtonP.Name = "venueButtonP";
            this.venueButtonP.Size = new System.Drawing.Size(200, 527);
            this.venueButtonP.TabIndex = 1;
            // 
            // FVenue
            // 
            this.ClientSize = new System.Drawing.Size(1100, 635);
            this.Controls.Add(this.titleLabel1);
            this.Controls.Add(this.subtitleL);
            this.Controls.Add(this.stadiumInsidePB);
            this.Controls.Add(this.stadiumOutsidePB);
            this.Controls.Add(this.cityPB);
            this.Controls.Add(this.matchesP);
            this.Controls.Add(this.locationPB);
            this.Controls.Add(this.titleL);
            this.Controls.Add(this.venueButtonP);
            this.Name = "FVenue";
            this.Load += new System.EventHandler(this.FVenues_Load);
            ((System.ComponentModel.ISupportInitialize)(this.stadiumInsidePB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stadiumOutsidePB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cityPB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.locationPB)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private VisualComponents.MyPanel venueButtonP;
        private System.Windows.Forms.Label titleL;
        private System.Windows.Forms.PictureBox locationPB;
        private VisualComponents.MyPanel matchesP;
        private System.Windows.Forms.PictureBox cityPB;
        private System.Windows.Forms.PictureBox stadiumOutsidePB;
        private System.Windows.Forms.PictureBox stadiumInsidePB;
        private System.Windows.Forms.Label subtitleL;
        private VisualComponents.TitleLabel titleLabel1;
    }
}
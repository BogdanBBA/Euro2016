namespace Euro2016
{
    partial class FAbout
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
            this.infoViewDetail1 = new Euro2016.VisualComponents.InfoViewDetail();
            this.titleLabel1 = new Euro2016.VisualComponents.TitleLabel();
            this.goTeamIV = new Euro2016.VisualComponents.InfoView();
            this.flagPB = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.flagPB)).BeginInit();
            this.SuspendLayout();
            // 
            // infoViewDetail1
            // 
            this.infoViewDetail1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(50)))), ((int)(((byte)(74)))));
            this.infoViewDetail1.BigBar = false;
            this.infoViewDetail1.Checked = false;
            this.infoViewDetail1.DrawBar = false;
            this.infoViewDetail1.Location = new System.Drawing.Point(29, 187);
            this.infoViewDetail1.Name = "infoViewDetail1";
            this.infoViewDetail1.Size = new System.Drawing.Size(352, 35);
            this.infoViewDetail1.TabIndex = 1;
            this.infoViewDetail1.Text = "infoViewDetail1";
            this.infoViewDetail1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.infoViewDetail1.TextDescription = "March 9th - April 1st, 2016";
            this.infoViewDetail1.TextText = "1.0 alpha";
            this.infoViewDetail1.Click += new System.EventHandler(this.FAbout_Click);
            // 
            // titleLabel1
            // 
            this.titleLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(50)))), ((int)(((byte)(74)))));
            this.titleLabel1.BigBar = true;
            this.titleLabel1.Checked = false;
            this.titleLabel1.DrawBar = true;
            this.titleLabel1.Location = new System.Drawing.Point(12, 12);
            this.titleLabel1.Name = "titleLabel1";
            this.titleLabel1.Size = new System.Drawing.Size(384, 87);
            this.titleLabel1.TabIndex = 0;
            this.titleLabel1.Text = "titleLabel1";
            this.titleLabel1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.titleLabel1.TextSubtitle = "by BogdanBBA";
            this.titleLabel1.TextTitle = "Euro 2016";
            this.titleLabel1.Click += new System.EventHandler(this.FAbout_Click);
            // 
            // goTeamIV
            // 
            this.goTeamIV.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(50)))), ((int)(((byte)(74)))));
            this.goTeamIV.BigBar = false;
            this.goTeamIV.Checked = false;
            this.goTeamIV.DrawBar = false;
            this.goTeamIV.Location = new System.Drawing.Point(12, 119);
            this.goTeamIV.Name = "goTeamIV";
            this.goTeamIV.Size = new System.Drawing.Size(283, 50);
            this.goTeamIV.TabIndex = 2;
            this.goTeamIV.Text = "infoView1";
            this.goTeamIV.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.goTeamIV.TextDescription = "Go, go";
            this.goTeamIV.TextText = "#text";
            // 
            // flagPB
            // 
            this.flagPB.BackColor = System.Drawing.Color.Transparent;
            this.flagPB.Location = new System.Drawing.Point(301, 119);
            this.flagPB.Name = "flagPB";
            this.flagPB.Size = new System.Drawing.Size(80, 50);
            this.flagPB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.flagPB.TabIndex = 3;
            this.flagPB.TabStop = false;
            // 
            // FAbout
            // 
            this.ClientSize = new System.Drawing.Size(408, 249);
            this.Controls.Add(this.flagPB);
            this.Controls.Add(this.goTeamIV);
            this.Controls.Add(this.infoViewDetail1);
            this.Controls.Add(this.titleLabel1);
            this.Name = "FAbout";
            this.Load += new System.EventHandler(this.FAbout_Load);
            this.Click += new System.EventHandler(this.FAbout_Click);
            ((System.ComponentModel.ISupportInitialize)(this.flagPB)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private VisualComponents.TitleLabel titleLabel1;
        private VisualComponents.InfoViewDetail infoViewDetail1;
        private VisualComponents.InfoView goTeamIV;
        private System.Windows.Forms.PictureBox flagPB;
    }
}
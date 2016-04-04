namespace Euro2016
{
    partial class FGroup
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
            this.currentGroupIV = new Euro2016.VisualComponents.InfoView();
            this.titleLabel1 = new Euro2016.VisualComponents.TitleLabel();
            this.matchesP = new Euro2016.VisualComponents.MyPanel();
            this.groupP = new Euro2016.VisualComponents.MyPanel();
            this.groupButtonP = new Euro2016.VisualComponents.MyPanel();
            this.SuspendLayout();
            // 
            // currentGroupIV
            // 
            this.currentGroupIV.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(50)))), ((int)(((byte)(74)))));
            this.currentGroupIV.BigBar = false;
            this.currentGroupIV.Checked = false;
            this.currentGroupIV.DrawBar = true;
            this.currentGroupIV.Location = new System.Drawing.Point(218, 96);
            this.currentGroupIV.Name = "currentGroupIV";
            this.currentGroupIV.Size = new System.Drawing.Size(650, 60);
            this.currentGroupIV.TabIndex = 4;
            this.currentGroupIV.Text = "infoView1";
            this.currentGroupIV.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.currentGroupIV.TextDescription = "Currently viewing";
            this.currentGroupIV.TextText = "#text";
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
            this.titleLabel1.Size = new System.Drawing.Size(856, 78);
            this.titleLabel1.TabIndex = 3;
            this.titleLabel1.Text = "titleLabel1";
            this.titleLabel1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.titleLabel1.TextSubtitle = "Euro 2016 group stage standings and third-placed teams group";
            this.titleLabel1.TextTitle = "Groups";
            // 
            // matchesP
            // 
            this.matchesP.DrawPanelAccent = false;
            this.matchesP.Location = new System.Drawing.Point(874, 12);
            this.matchesP.Name = "matchesP";
            this.matchesP.Size = new System.Drawing.Size(480, 400);
            this.matchesP.TabIndex = 2;
            // 
            // groupP
            // 
            this.groupP.DrawPanelAccent = false;
            this.groupP.Location = new System.Drawing.Point(218, 162);
            this.groupP.Name = "groupP";
            this.groupP.Size = new System.Drawing.Size(650, 250);
            this.groupP.TabIndex = 1;
            // 
            // groupButtonP
            // 
            this.groupButtonP.DrawPanelAccent = false;
            this.groupButtonP.Location = new System.Drawing.Point(12, 96);
            this.groupButtonP.Name = "groupButtonP";
            this.groupButtonP.Size = new System.Drawing.Size(200, 315);
            this.groupButtonP.TabIndex = 0;
            // 
            // FGroup
            // 
            this.ClientSize = new System.Drawing.Size(1366, 423);
            this.Controls.Add(this.currentGroupIV);
            this.Controls.Add(this.titleLabel1);
            this.Controls.Add(this.matchesP);
            this.Controls.Add(this.groupP);
            this.Controls.Add(this.groupButtonP);
            this.Name = "FGroup";
            this.Load += new System.EventHandler(this.FGroup_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private VisualComponents.MyPanel groupButtonP;
        private VisualComponents.MyPanel groupP;
        private VisualComponents.MyPanel matchesP;
        private VisualComponents.TitleLabel titleLabel1;
        private VisualComponents.InfoView currentGroupIV;
    }
}
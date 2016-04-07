namespace Euro2016
{
    partial class FMatchDays
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
            this.matchDayMatchCountIVD = new Euro2016.VisualComponents.InfoViewDetail();
            this.selectedDateIV = new Euro2016.VisualComponents.InfoView();
            this.matchesP = new Euro2016.VisualComponents.MyPanel();
            this.matchDaysP = new Euro2016.VisualComponents.MyPanel();
            this.titleLabel1 = new Euro2016.VisualComponents.TitleLabel();
            this.SuspendLayout();
            // 
            // matchDayMatchCountIVD
            // 
            this.matchDayMatchCountIVD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(50)))), ((int)(((byte)(74)))));
            this.matchDayMatchCountIVD.BigBar = false;
            this.matchDayMatchCountIVD.Checked = false;
            this.matchDayMatchCountIVD.DrawBar = false;
            this.matchDayMatchCountIVD.Location = new System.Drawing.Point(858, 154);
            this.matchDayMatchCountIVD.Name = "matchDayMatchCountIVD";
            this.matchDayMatchCountIVD.Size = new System.Drawing.Size(480, 35);
            this.matchDayMatchCountIVD.TabIndex = 4;
            this.matchDayMatchCountIVD.Text = "infoViewDetail1";
            this.matchDayMatchCountIVD.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.matchDayMatchCountIVD.TextDescription = "Number of matches";
            this.matchDayMatchCountIVD.TextText = "#text";
            // 
            // selectedDateIV
            // 
            this.selectedDateIV.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(50)))), ((int)(((byte)(74)))));
            this.selectedDateIV.BigBar = false;
            this.selectedDateIV.Checked = false;
            this.selectedDateIV.DrawBar = false;
            this.selectedDateIV.Location = new System.Drawing.Point(858, 96);
            this.selectedDateIV.Name = "selectedDateIV";
            this.selectedDateIV.Size = new System.Drawing.Size(480, 52);
            this.selectedDateIV.TabIndex = 3;
            this.selectedDateIV.Text = "infoView1";
            this.selectedDateIV.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.selectedDateIV.TextDescription = "Selected match day";
            this.selectedDateIV.TextText = "#text";
            // 
            // matchesP
            // 
            this.matchesP.DrawPanelAccent = false;
            this.matchesP.Location = new System.Drawing.Point(858, 195);
            this.matchesP.Name = "matchesP";
            this.matchesP.Size = new System.Drawing.Size(480, 441);
            this.matchesP.TabIndex = 2;
            // 
            // matchDaysP
            // 
            this.matchDaysP.DrawPanelAccent = false;
            this.matchDaysP.Location = new System.Drawing.Point(12, 96);
            this.matchDaysP.Name = "matchDaysP";
            this.matchDaysP.Size = new System.Drawing.Size(840, 540);
            this.matchDaysP.TabIndex = 1;
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
            this.titleLabel1.Size = new System.Drawing.Size(1326, 78);
            this.titleLabel1.TabIndex = 0;
            this.titleLabel1.Text = "titleLabel1";
            this.titleLabel1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.titleLabel1.TextSubtitle = "Calendar of the competition";
            this.titleLabel1.TextTitle = "Match days";
            // 
            // FMatchDays
            // 
            this.ClientSize = new System.Drawing.Size(1350, 648);
            this.Controls.Add(this.matchDayMatchCountIVD);
            this.Controls.Add(this.selectedDateIV);
            this.Controls.Add(this.matchesP);
            this.Controls.Add(this.matchDaysP);
            this.Controls.Add(this.titleLabel1);
            this.Name = "FMatchDays";
            this.Load += new System.EventHandler(this.FMatchDays_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private VisualComponents.TitleLabel titleLabel1;
        private VisualComponents.MyPanel matchDaysP;
        private VisualComponents.MyPanel matchesP;
        private VisualComponents.InfoView selectedDateIV;
        private VisualComponents.InfoViewDetail matchDayMatchCountIVD;
    }
}
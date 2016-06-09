namespace Euro2016
{
    partial class FTeamStats
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
            this.teamP = new Euro2016.VisualComponents.MyPanel();
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
            this.titleLabel1.MouseIsClicked = false;
            this.titleLabel1.MouseIsOver = false;
            this.titleLabel1.Name = "titleLabel1";
            this.titleLabel1.Size = new System.Drawing.Size(1342, 78);
            this.titleLabel1.TabIndex = 7;
            this.titleLabel1.Text = "titleLabel1";
            this.titleLabel1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.titleLabel1.TextSubtitle = "A stats overview of the participating teams // note: finish team stats sorting, a" +
    "nd add description for columns somewhere here";
            this.titleLabel1.TextTitle = "Team stats";
            // 
            // teamP
            // 
            this.teamP.DrawPanelAccent = false;
            this.teamP.Location = new System.Drawing.Point(12, 96);
            this.teamP.Name = "teamP";
            this.teamP.Size = new System.Drawing.Size(1342, 612);
            this.teamP.TabIndex = 5;
            // 
            // FTeamStats
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1366, 720);
            this.Controls.Add(this.titleLabel1);
            this.Controls.Add(this.teamP);
            this.Name = "FTeamStats";
            this.Text = "TeamStats";
            this.Load += new System.EventHandler(this.FTeamStats_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private VisualComponents.TitleLabel titleLabel1;
        private VisualComponents.MyPanel teamP;
    }
}
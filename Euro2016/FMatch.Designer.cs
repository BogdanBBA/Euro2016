namespace Euro2016
{
    partial class FMatch
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
            this.halvesL = new Euro2016.VisualComponents.SmoothLabel();
            this.mustWatchStV = new Euro2016.VisualComponents.StarView();
            this.whenL = new Euro2016.VisualComponents.SmoothLabel();
            this.titleLabel1 = new Euro2016.VisualComponents.TitleLabel();
            this.matchesP = new Euro2016.VisualComponents.MyPanel();
            this.awayNicknameL = new Euro2016.VisualComponents.SmoothLabel();
            this.homeNicknameL = new Euro2016.VisualComponents.SmoothLabel();
            this.awayTeamL = new Euro2016.VisualComponents.SmoothLabel();
            this.awayFlagPB = new System.Windows.Forms.PictureBox();
            this.scoreL = new Euro2016.VisualComponents.SmoothLabel();
            this.phaseL = new Euro2016.VisualComponents.SmoothLabel();
            this.homeTeamL = new Euro2016.VisualComponents.SmoothLabel();
            this.whereL = new Euro2016.VisualComponents.SmoothLabel();
            this.homeFlagPB = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.awayFlagPB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.homeFlagPB)).BeginInit();
            this.SuspendLayout();
            // 
            // halvesL
            // 
            this.halvesL.BackColor = System.Drawing.Color.Transparent;
            this.halvesL.Font = new System.Drawing.Font("Segoe UI Semilight", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.halvesL.ForeColor = System.Drawing.Color.White;
            this.halvesL.Location = new System.Drawing.Point(314, 258);
            this.halvesL.Name = "halvesL";
            this.halvesL.Size = new System.Drawing.Size(159, 60);
            this.halvesL.TabIndex = 13;
            this.halvesL.Text = "10-0";
            this.halvesL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // mustWatchStV
            // 
            this.mustWatchStV.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(50)))), ((int)(((byte)(74)))));
            this.mustWatchStV.Checked = false;
            this.mustWatchStV.Cursor = System.Windows.Forms.Cursors.Hand;
            this.mustWatchStV.Location = new System.Drawing.Point(695, 12);
            this.mustWatchStV.MouseIsClicked = false;
            this.mustWatchStV.MouseIsOver = false;
            this.mustWatchStV.Name = "mustWatchStV";
            this.mustWatchStV.Size = new System.Drawing.Size(78, 78);
            this.mustWatchStV.TabIndex = 16;
            this.mustWatchStV.Text = "starView1";
            this.mustWatchStV.Click += new System.EventHandler(this.mustWatchStV_Click);
            // 
            // whenL
            // 
            this.whenL.BackColor = System.Drawing.Color.Transparent;
            this.whenL.Cursor = System.Windows.Forms.Cursors.Hand;
            this.whenL.Font = new System.Drawing.Font("Arial", 16F);
            this.whenL.ForeColor = System.Drawing.Color.White;
            this.whenL.Location = new System.Drawing.Point(151, 144);
            this.whenL.Name = "whenL";
            this.whenL.Size = new System.Drawing.Size(488, 21);
            this.whenL.TabIndex = 14;
            this.whenL.Text = "label1";
            this.whenL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.whenL.Click += new System.EventHandler(this.whenL_Click);
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
            this.titleLabel1.Size = new System.Drawing.Size(677, 78);
            this.titleLabel1.TabIndex = 12;
            this.titleLabel1.Text = "titleLabel1";
            this.titleLabel1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.titleLabel1.TextSubtitle = "Individual match info";
            this.titleLabel1.TextTitle = "Match";
            // 
            // matchesP
            // 
            this.matchesP.DrawPanelAccent = false;
            this.matchesP.Location = new System.Drawing.Point(779, 12);
            this.matchesP.Name = "matchesP";
            this.matchesP.Size = new System.Drawing.Size(480, 349);
            this.matchesP.TabIndex = 11;
            // 
            // awayNicknameL
            // 
            this.awayNicknameL.BackColor = System.Drawing.Color.Transparent;
            this.awayNicknameL.Cursor = System.Windows.Forms.Cursors.Hand;
            this.awayNicknameL.Font = new System.Drawing.Font("Segoe UI", 12.75F);
            this.awayNicknameL.ForeColor = System.Drawing.Color.White;
            this.awayNicknameL.Location = new System.Drawing.Point(475, 313);
            this.awayNicknameL.Name = "awayNicknameL";
            this.awayNicknameL.Size = new System.Drawing.Size(298, 48);
            this.awayNicknameL.TabIndex = 9;
            this.awayNicknameL.Text = "Tuaisceart Éireann";
            this.awayNicknameL.Click += new System.EventHandler(this.awayFlagPB_Click);
            // 
            // homeNicknameL
            // 
            this.homeNicknameL.BackColor = System.Drawing.Color.Transparent;
            this.homeNicknameL.Cursor = System.Windows.Forms.Cursors.Hand;
            this.homeNicknameL.Font = new System.Drawing.Font("Segoe UI", 12.75F);
            this.homeNicknameL.ForeColor = System.Drawing.Color.White;
            this.homeNicknameL.Location = new System.Drawing.Point(10, 313);
            this.homeNicknameL.Name = "homeNicknameL";
            this.homeNicknameL.Size = new System.Drawing.Size(298, 48);
            this.homeNicknameL.TabIndex = 8;
            this.homeNicknameL.Text = "Tuaisceart Éireann";
            this.homeNicknameL.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.homeNicknameL.Click += new System.EventHandler(this.homeFlagPB_Click);
            // 
            // awayTeamL
            // 
            this.awayTeamL.BackColor = System.Drawing.Color.Transparent;
            this.awayTeamL.Cursor = System.Windows.Forms.Cursors.Hand;
            this.awayTeamL.Font = new System.Drawing.Font("Segoe UI Semibold", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.awayTeamL.ForeColor = System.Drawing.Color.White;
            this.awayTeamL.Location = new System.Drawing.Point(472, 271);
            this.awayTeamL.Name = "awayTeamL";
            this.awayTeamL.Size = new System.Drawing.Size(301, 42);
            this.awayTeamL.TabIndex = 7;
            this.awayTeamL.Text = "Tuaisceart Éireann";
            this.awayTeamL.Click += new System.EventHandler(this.awayFlagPB_Click);
            // 
            // awayFlagPB
            // 
            this.awayFlagPB.BackColor = System.Drawing.Color.Transparent;
            this.awayFlagPB.Cursor = System.Windows.Forms.Cursors.Hand;
            this.awayFlagPB.Location = new System.Drawing.Point(479, 168);
            this.awayFlagPB.Name = "awayFlagPB";
            this.awayFlagPB.Size = new System.Drawing.Size(160, 100);
            this.awayFlagPB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.awayFlagPB.TabIndex = 6;
            this.awayFlagPB.TabStop = false;
            this.awayFlagPB.Click += new System.EventHandler(this.awayFlagPB_Click);
            // 
            // scoreL
            // 
            this.scoreL.BackColor = System.Drawing.Color.Transparent;
            this.scoreL.Cursor = System.Windows.Forms.Cursors.Hand;
            this.scoreL.Font = new System.Drawing.Font("Segoe UI Semibold", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scoreL.ForeColor = System.Drawing.Color.White;
            this.scoreL.Location = new System.Drawing.Point(317, 168);
            this.scoreL.Name = "scoreL";
            this.scoreL.Size = new System.Drawing.Size(156, 100);
            this.scoreL.TabIndex = 5;
            this.scoreL.Text = "10-0";
            this.scoreL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.scoreL.Click += new System.EventHandler(this.scoreL_Click);
            // 
            // phaseL
            // 
            this.phaseL.BackColor = System.Drawing.Color.Transparent;
            this.phaseL.Cursor = System.Windows.Forms.Cursors.Hand;
            this.phaseL.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.phaseL.ForeColor = System.Drawing.Color.White;
            this.phaseL.Location = new System.Drawing.Point(151, 93);
            this.phaseL.Name = "phaseL";
            this.phaseL.Size = new System.Drawing.Size(488, 30);
            this.phaseL.TabIndex = 4;
            this.phaseL.Text = "label4";
            this.phaseL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.phaseL.Click += new System.EventHandler(this.phaseL_Click);
            // 
            // homeTeamL
            // 
            this.homeTeamL.BackColor = System.Drawing.Color.Transparent;
            this.homeTeamL.Cursor = System.Windows.Forms.Cursors.Hand;
            this.homeTeamL.Font = new System.Drawing.Font("Segoe UI Semibold", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.homeTeamL.ForeColor = System.Drawing.Color.White;
            this.homeTeamL.Location = new System.Drawing.Point(10, 271);
            this.homeTeamL.Name = "homeTeamL";
            this.homeTeamL.Size = new System.Drawing.Size(301, 42);
            this.homeTeamL.TabIndex = 3;
            this.homeTeamL.Text = "Tuaisceart Éireann";
            this.homeTeamL.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.homeTeamL.Click += new System.EventHandler(this.homeFlagPB_Click);
            // 
            // whereL
            // 
            this.whereL.BackColor = System.Drawing.Color.Transparent;
            this.whereL.Cursor = System.Windows.Forms.Cursors.Hand;
            this.whereL.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.whereL.ForeColor = System.Drawing.Color.White;
            this.whereL.Location = new System.Drawing.Point(151, 123);
            this.whereL.Name = "whereL";
            this.whereL.Size = new System.Drawing.Size(488, 21);
            this.whereL.TabIndex = 2;
            this.whereL.Text = "label2";
            this.whereL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.whereL.Click += new System.EventHandler(this.whereL_Click);
            // 
            // homeFlagPB
            // 
            this.homeFlagPB.BackColor = System.Drawing.Color.Transparent;
            this.homeFlagPB.Cursor = System.Windows.Forms.Cursors.Hand;
            this.homeFlagPB.Location = new System.Drawing.Point(151, 168);
            this.homeFlagPB.Name = "homeFlagPB";
            this.homeFlagPB.Size = new System.Drawing.Size(160, 100);
            this.homeFlagPB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.homeFlagPB.TabIndex = 0;
            this.homeFlagPB.TabStop = false;
            this.homeFlagPB.Click += new System.EventHandler(this.homeFlagPB_Click);
            // 
            // FMatch
            // 
            this.ClientSize = new System.Drawing.Size(1270, 373);
            this.Controls.Add(this.mustWatchStV);
            this.Controls.Add(this.halvesL);
            this.Controls.Add(this.whenL);
            this.Controls.Add(this.titleLabel1);
            this.Controls.Add(this.matchesP);
            this.Controls.Add(this.awayNicknameL);
            this.Controls.Add(this.homeNicknameL);
            this.Controls.Add(this.awayTeamL);
            this.Controls.Add(this.awayFlagPB);
            this.Controls.Add(this.scoreL);
            this.Controls.Add(this.phaseL);
            this.Controls.Add(this.homeTeamL);
            this.Controls.Add(this.whereL);
            this.Controls.Add(this.homeFlagPB);
            this.Name = "FMatch";
            this.Load += new System.EventHandler(this.FMatch_Load);
            ((System.ComponentModel.ISupportInitialize)(this.awayFlagPB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.homeFlagPB)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox homeFlagPB;
        private Euro2016.VisualComponents.SmoothLabel whereL;
        private Euro2016.VisualComponents.SmoothLabel homeTeamL;
        private Euro2016.VisualComponents.SmoothLabel phaseL;
        private Euro2016.VisualComponents.SmoothLabel scoreL;
        private Euro2016.VisualComponents.SmoothLabel awayTeamL;
        private System.Windows.Forms.PictureBox awayFlagPB;
        private Euro2016.VisualComponents.SmoothLabel awayNicknameL;
        private Euro2016.VisualComponents.SmoothLabel homeNicknameL;
        private VisualComponents.MyPanel matchesP;
        private VisualComponents.TitleLabel titleLabel1;
        private VisualComponents.SmoothLabel halvesL;
        private VisualComponents.SmoothLabel whenL;
        private VisualComponents.StarView mustWatchStV;
    }
}
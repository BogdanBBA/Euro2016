namespace Euro2016
{
    partial class FSettings
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
            this.label1 = new System.Windows.Forms.Label();
            this.favoriteTeamCB = new System.Windows.Forms.ComboBox();
            this.showKnockoutPhaseOnStartupChB = new System.Windows.Forms.CheckBox();
            this.okB = new Euro2016.VisualComponents.MyButton();
            this.showCountryNamesInNativeLanguageChB = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // titleLabel1
            // 
            this.titleLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(50)))), ((int)(((byte)(74)))));
            this.titleLabel1.BigBar = false;
            this.titleLabel1.DrawBar = true;
            this.titleLabel1.Location = new System.Drawing.Point(12, 12);
            this.titleLabel1.Name = "titleLabel1";
            this.titleLabel1.Size = new System.Drawing.Size(520, 78);
            this.titleLabel1.TabIndex = 0;
            this.titleLabel1.Text = "titleLabel1";
            this.titleLabel1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.titleLabel1.TextSubtitle = "App configuration and preferences";
            this.titleLabel1.TextTitle = "Settings";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(64, 112);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 21);
            this.label1.TabIndex = 1;
            this.label1.Text = "Favorite team";
            // 
            // favoriteTeamCB
            // 
            this.favoriteTeamCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.favoriteTeamCB.FormattingEnabled = true;
            this.favoriteTeamCB.Location = new System.Drawing.Point(68, 136);
            this.favoriteTeamCB.MaxDropDownItems = 24;
            this.favoriteTeamCB.Name = "favoriteTeamCB";
            this.favoriteTeamCB.Size = new System.Drawing.Size(407, 29);
            this.favoriteTeamCB.TabIndex = 2;
            // 
            // showKnockoutPhaseOnStartupChB
            // 
            this.showKnockoutPhaseOnStartupChB.AutoSize = true;
            this.showKnockoutPhaseOnStartupChB.BackColor = System.Drawing.Color.Transparent;
            this.showKnockoutPhaseOnStartupChB.Cursor = System.Windows.Forms.Cursors.Hand;
            this.showKnockoutPhaseOnStartupChB.Location = new System.Drawing.Point(68, 202);
            this.showKnockoutPhaseOnStartupChB.Name = "showKnockoutPhaseOnStartupChB";
            this.showKnockoutPhaseOnStartupChB.Size = new System.Drawing.Size(407, 25);
            this.showKnockoutPhaseOnStartupChB.TabIndex = 3;
            this.showKnockoutPhaseOnStartupChB.Text = "Show knockout stage on start-up (if group phase over)";
            this.showKnockoutPhaseOnStartupChB.UseVisualStyleBackColor = false;
            // 
            // okB
            // 
            this.okB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(50)))), ((int)(((byte)(74)))));
            this.okB.BigBar = true;
            this.okB.Cursor = System.Windows.Forms.Cursors.Hand;
            this.okB.DrawBar = true;
            this.okB.Image = null;
            this.okB.Location = new System.Drawing.Point(164, 252);
            this.okB.Name = "okB";
            this.okB.Size = new System.Drawing.Size(200, 50);
            this.okB.TabIndex = 4;
            this.okB.Text = "OK";
            this.okB.Click += new System.EventHandler(this.okB_Click);
            // 
            // showCountryNamesInNativeLanguageChB
            // 
            this.showCountryNamesInNativeLanguageChB.AutoSize = true;
            this.showCountryNamesInNativeLanguageChB.BackColor = System.Drawing.Color.Transparent;
            this.showCountryNamesInNativeLanguageChB.Cursor = System.Windows.Forms.Cursors.Hand;
            this.showCountryNamesInNativeLanguageChB.Location = new System.Drawing.Point(68, 171);
            this.showCountryNamesInNativeLanguageChB.Name = "showCountryNamesInNativeLanguageChB";
            this.showCountryNamesInNativeLanguageChB.Size = new System.Drawing.Size(306, 25);
            this.showCountryNamesInNativeLanguageChB.TabIndex = 5;
            this.showCountryNamesInNativeLanguageChB.Text = "Show country names in native language";
            this.showCountryNamesInNativeLanguageChB.UseVisualStyleBackColor = false;
            // 
            // FSettings
            // 
            this.ClientSize = new System.Drawing.Size(544, 317);
            this.Controls.Add(this.showCountryNamesInNativeLanguageChB);
            this.Controls.Add(this.okB);
            this.Controls.Add(this.showKnockoutPhaseOnStartupChB);
            this.Controls.Add(this.favoriteTeamCB);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.titleLabel1);
            this.Name = "FSettings";
            this.Load += new System.EventHandler(this.FSettings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private VisualComponents.TitleLabel titleLabel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox favoriteTeamCB;
        private System.Windows.Forms.CheckBox showKnockoutPhaseOnStartupChB;
        private VisualComponents.MyButton okB;
        private System.Windows.Forms.CheckBox showCountryNamesInNativeLanguageChB;


    }
}
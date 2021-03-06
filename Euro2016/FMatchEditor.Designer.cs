﻿namespace Euro2016
{
    partial class FMatchEditor
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
            this.awayTeamCV = new Euro2016.VisualComponents.CountryView();
            this.homeTeamCV = new Euro2016.VisualComponents.CountryView();
            this.titleIV = new Euro2016.VisualComponents.InfoView();
            this.okB = new Euro2016.VisualComponents.MyButton();
            this.matchPlayedChB = new System.Windows.Forms.CheckBox();
            this.regularFirstHalfTB = new System.Windows.Forms.TextBox();
            this.regularSecondHalfTB = new System.Windows.Forms.TextBox();
            this.extraFirstHalfTB = new System.Windows.Forms.TextBox();
            this.extraSecondHalfTB = new System.Windows.Forms.TextBox();
            this.matchExtraTimeChB = new System.Windows.Forms.CheckBox();
            this.penaltiesTB = new System.Windows.Forms.TextBox();
            this.matchPenaltiesChB = new System.Windows.Forms.CheckBox();
            this.scoreL = new Euro2016.VisualComponents.SmoothLabel();
            this.errorL = new System.Windows.Forms.Label();
            this.cancelB = new Euro2016.VisualComponents.MyButton();
            this.SuspendLayout();
            // 
            // awayTeamCV
            // 
            this.awayTeamCV.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(50)))), ((int)(((byte)(74)))));
            this.awayTeamCV.Checked = false;
            this.awayTeamCV.Country = null;
            this.awayTeamCV.Cursor = System.Windows.Forms.Cursors.Default;
            this.awayTeamCV.InverseFlag = false;
            this.awayTeamCV.Location = new System.Drawing.Point(277, 70);
            this.awayTeamCV.MouseIsClicked = false;
            this.awayTeamCV.MouseIsOver = false;
            this.awayTeamCV.Name = "awayTeamCV";
            this.awayTeamCV.Settings = null;
            this.awayTeamCV.Size = new System.Drawing.Size(190, 28);
            this.awayTeamCV.TabIndex = 3;
            this.awayTeamCV.Text = "countryView2";
            // 
            // homeTeamCV
            // 
            this.homeTeamCV.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(50)))), ((int)(((byte)(74)))));
            this.homeTeamCV.Checked = false;
            this.homeTeamCV.Country = null;
            this.homeTeamCV.Cursor = System.Windows.Forms.Cursors.Default;
            this.homeTeamCV.InverseFlag = true;
            this.homeTeamCV.Location = new System.Drawing.Point(12, 70);
            this.homeTeamCV.MouseIsClicked = false;
            this.homeTeamCV.MouseIsOver = false;
            this.homeTeamCV.Name = "homeTeamCV";
            this.homeTeamCV.Settings = null;
            this.homeTeamCV.Size = new System.Drawing.Size(190, 28);
            this.homeTeamCV.TabIndex = 2;
            this.homeTeamCV.Text = "countryView1";
            // 
            // titleIV
            // 
            this.titleIV.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(50)))), ((int)(((byte)(74)))));
            this.titleIV.BigBar = false;
            this.titleIV.Checked = false;
            this.titleIV.DrawBar = true;
            this.titleIV.Location = new System.Drawing.Point(12, 12);
            this.titleIV.MouseIsClicked = false;
            this.titleIV.MouseIsOver = false;
            this.titleIV.Name = "titleIV";
            this.titleIV.Size = new System.Drawing.Size(455, 52);
            this.titleIV.TabIndex = 1;
            this.titleIV.Text = "infoView1";
            this.titleIV.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.titleIV.TextDescription = "#description";
            this.titleIV.TextText = "Edit match";
            // 
            // okB
            // 
            this.okB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(50)))), ((int)(((byte)(74)))));
            this.okB.BigBar = false;
            this.okB.Checked = false;
            this.okB.Cursor = System.Windows.Forms.Cursors.Hand;
            this.okB.DrawBar = true;
            this.okB.Image = null;
            this.okB.Location = new System.Drawing.Point(12, 384);
            this.okB.MouseIsClicked = false;
            this.okB.MouseIsOver = false;
            this.okB.Name = "okB";
            this.okB.Size = new System.Drawing.Size(223, 50);
            this.okB.TabIndex = 0;
            this.okB.Text = "Save and close";
            this.okB.Click += new System.EventHandler(this.okB_Click);
            // 
            // matchPlayedChB
            // 
            this.matchPlayedChB.AutoSize = true;
            this.matchPlayedChB.BackColor = System.Drawing.Color.Transparent;
            this.matchPlayedChB.Cursor = System.Windows.Forms.Cursors.Hand;
            this.matchPlayedChB.Location = new System.Drawing.Point(103, 101);
            this.matchPlayedChB.Name = "matchPlayedChB";
            this.matchPlayedChB.Size = new System.Drawing.Size(122, 25);
            this.matchPlayedChB.TabIndex = 6;
            this.matchPlayedChB.Text = "Match played";
            this.matchPlayedChB.UseVisualStyleBackColor = false;
            this.matchPlayedChB.CheckedChanged += new System.EventHandler(this.matchPlayedChB_CheckedChanged);
            // 
            // regularFirstHalfTB
            // 
            this.regularFirstHalfTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.regularFirstHalfTB.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.regularFirstHalfTB.ForeColor = System.Drawing.Color.White;
            this.regularFirstHalfTB.Location = new System.Drawing.Point(135, 132);
            this.regularFirstHalfTB.Name = "regularFirstHalfTB";
            this.regularFirstHalfTB.Size = new System.Drawing.Size(100, 22);
            this.regularFirstHalfTB.TabIndex = 7;
            this.regularFirstHalfTB.Text = "10-10";
            this.regularFirstHalfTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.regularFirstHalfTB.TextChanged += new System.EventHandler(this.Checks_Event);
            // 
            // regularSecondHalfTB
            // 
            this.regularSecondHalfTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.regularSecondHalfTB.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.regularSecondHalfTB.ForeColor = System.Drawing.Color.White;
            this.regularSecondHalfTB.Location = new System.Drawing.Point(241, 132);
            this.regularSecondHalfTB.Name = "regularSecondHalfTB";
            this.regularSecondHalfTB.Size = new System.Drawing.Size(100, 22);
            this.regularSecondHalfTB.TabIndex = 8;
            this.regularSecondHalfTB.Text = "10-10";
            this.regularSecondHalfTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.regularSecondHalfTB.TextChanged += new System.EventHandler(this.Checks_Event);
            // 
            // extraFirstHalfTB
            // 
            this.extraFirstHalfTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.extraFirstHalfTB.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.extraFirstHalfTB.ForeColor = System.Drawing.Color.White;
            this.extraFirstHalfTB.Location = new System.Drawing.Point(135, 198);
            this.extraFirstHalfTB.Name = "extraFirstHalfTB";
            this.extraFirstHalfTB.Size = new System.Drawing.Size(100, 22);
            this.extraFirstHalfTB.TabIndex = 11;
            this.extraFirstHalfTB.Text = "10-10";
            this.extraFirstHalfTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.extraFirstHalfTB.TextChanged += new System.EventHandler(this.Checks_Event);
            // 
            // extraSecondHalfTB
            // 
            this.extraSecondHalfTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.extraSecondHalfTB.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.extraSecondHalfTB.ForeColor = System.Drawing.Color.White;
            this.extraSecondHalfTB.Location = new System.Drawing.Point(241, 198);
            this.extraSecondHalfTB.Name = "extraSecondHalfTB";
            this.extraSecondHalfTB.Size = new System.Drawing.Size(100, 22);
            this.extraSecondHalfTB.TabIndex = 10;
            this.extraSecondHalfTB.Text = "10-10";
            this.extraSecondHalfTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.extraSecondHalfTB.TextChanged += new System.EventHandler(this.Checks_Event);
            // 
            // matchExtraTimeChB
            // 
            this.matchExtraTimeChB.AutoSize = true;
            this.matchExtraTimeChB.BackColor = System.Drawing.Color.Transparent;
            this.matchExtraTimeChB.Cursor = System.Windows.Forms.Cursors.Hand;
            this.matchExtraTimeChB.Location = new System.Drawing.Point(103, 167);
            this.matchExtraTimeChB.Name = "matchExtraTimeChB";
            this.matchExtraTimeChB.Size = new System.Drawing.Size(206, 25);
            this.matchExtraTimeChB.TabIndex = 9;
            this.matchExtraTimeChB.Text = "Match reached extra-time";
            this.matchExtraTimeChB.UseVisualStyleBackColor = false;
            this.matchExtraTimeChB.CheckedChanged += new System.EventHandler(this.matchExtraTimeChB_CheckedChanged);
            // 
            // penaltiesTB
            // 
            this.penaltiesTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.penaltiesTB.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.penaltiesTB.ForeColor = System.Drawing.Color.White;
            this.penaltiesTB.Location = new System.Drawing.Point(186, 264);
            this.penaltiesTB.Name = "penaltiesTB";
            this.penaltiesTB.Size = new System.Drawing.Size(100, 22);
            this.penaltiesTB.TabIndex = 13;
            this.penaltiesTB.Text = "10-10";
            this.penaltiesTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.penaltiesTB.TextChanged += new System.EventHandler(this.Checks_Event);
            // 
            // matchPenaltiesChB
            // 
            this.matchPenaltiesChB.AutoSize = true;
            this.matchPenaltiesChB.BackColor = System.Drawing.Color.Transparent;
            this.matchPenaltiesChB.Cursor = System.Windows.Forms.Cursors.Hand;
            this.matchPenaltiesChB.Location = new System.Drawing.Point(103, 233);
            this.matchPenaltiesChB.Name = "matchPenaltiesChB";
            this.matchPenaltiesChB.Size = new System.Drawing.Size(266, 25);
            this.matchPenaltiesChB.TabIndex = 12;
            this.matchPenaltiesChB.Text = "Match settled at penalty shoot-out";
            this.matchPenaltiesChB.UseVisualStyleBackColor = false;
            this.matchPenaltiesChB.CheckedChanged += new System.EventHandler(this.matchPenaltiesChB_CheckedChanged);
            // 
            // scoreL
            // 
            this.scoreL.BackColor = System.Drawing.Color.Transparent;
            this.scoreL.Location = new System.Drawing.Point(208, 70);
            this.scoreL.Name = "scoreL";
            this.scoreL.Size = new System.Drawing.Size(63, 28);
            this.scoreL.TabIndex = 14;
            this.scoreL.Text = "-";
            this.scoreL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // errorL
            // 
            this.errorL.BackColor = System.Drawing.Color.Transparent;
            this.errorL.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.errorL.ForeColor = System.Drawing.Color.Chocolate;
            this.errorL.Location = new System.Drawing.Point(12, 295);
            this.errorL.Name = "errorL";
            this.errorL.Size = new System.Drawing.Size(455, 64);
            this.errorL.TabIndex = 15;
            this.errorL.Text = "test test here";
            this.errorL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cancelB
            // 
            this.cancelB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(50)))), ((int)(((byte)(74)))));
            this.cancelB.BigBar = false;
            this.cancelB.Checked = false;
            this.cancelB.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cancelB.DrawBar = true;
            this.cancelB.Image = null;
            this.cancelB.Location = new System.Drawing.Point(241, 384);
            this.cancelB.MouseIsClicked = false;
            this.cancelB.MouseIsOver = false;
            this.cancelB.Name = "cancelB";
            this.cancelB.Size = new System.Drawing.Size(226, 50);
            this.cancelB.TabIndex = 16;
            this.cancelB.Text = "Cancel";
            this.cancelB.Click += new System.EventHandler(this.cancelB_Click);
            // 
            // FMatchEditor
            // 
            this.ClientSize = new System.Drawing.Size(480, 446);
            this.Controls.Add(this.cancelB);
            this.Controls.Add(this.errorL);
            this.Controls.Add(this.scoreL);
            this.Controls.Add(this.penaltiesTB);
            this.Controls.Add(this.matchPenaltiesChB);
            this.Controls.Add(this.extraFirstHalfTB);
            this.Controls.Add(this.extraSecondHalfTB);
            this.Controls.Add(this.matchExtraTimeChB);
            this.Controls.Add(this.regularSecondHalfTB);
            this.Controls.Add(this.regularFirstHalfTB);
            this.Controls.Add(this.matchPlayedChB);
            this.Controls.Add(this.awayTeamCV);
            this.Controls.Add(this.homeTeamCV);
            this.Controls.Add(this.titleIV);
            this.Controls.Add(this.okB);
            this.Name = "FMatchEditor";
            this.Load += new System.EventHandler(this.FMatchEditor_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private VisualComponents.MyButton okB;
        private VisualComponents.InfoView titleIV;
        private VisualComponents.CountryView homeTeamCV;
        private VisualComponents.CountryView awayTeamCV;
        private System.Windows.Forms.CheckBox matchPlayedChB;
        private System.Windows.Forms.TextBox regularFirstHalfTB;
        private System.Windows.Forms.TextBox regularSecondHalfTB;
        private System.Windows.Forms.TextBox extraFirstHalfTB;
        private System.Windows.Forms.TextBox extraSecondHalfTB;
        private System.Windows.Forms.CheckBox matchExtraTimeChB;
        private System.Windows.Forms.TextBox penaltiesTB;
        private System.Windows.Forms.CheckBox matchPenaltiesChB;
        private Euro2016.VisualComponents.SmoothLabel scoreL;
        private System.Windows.Forms.Label errorL;
        private VisualComponents.MyButton cancelB;
    }
}
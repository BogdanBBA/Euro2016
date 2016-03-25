namespace Euro2016
{
    partial class FWorking
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
            this.components = new System.ComponentModel.Container();
            this.startT = new System.Windows.Forms.Timer(this.components);
            this.closeT = new System.Windows.Forms.Timer(this.components);
            this.titleLabel1 = new Euro2016.VisualComponents.TitleLabel();
            this.SuspendLayout();
            // 
            // startT
            // 
            this.startT.Tick += new System.EventHandler(this.startT_Tick);
            // 
            // closeT
            // 
            this.closeT.Tick += new System.EventHandler(this.closeT_Tick);
            // 
            // titleLabel1
            // 
            this.titleLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(50)))), ((int)(((byte)(74)))));
            this.titleLabel1.BigBar = false;
            this.titleLabel1.DrawBar = true;
            this.titleLabel1.Location = new System.Drawing.Point(12, 12);
            this.titleLabel1.Name = "titleLabel1";
            this.titleLabel1.Size = new System.Drawing.Size(439, 78);
            this.titleLabel1.TabIndex = 0;
            this.titleLabel1.Text = "titleLabel1";
            this.titleLabel1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.titleLabel1.TextSubtitle = "Status";
            this.titleLabel1.TextTitle = "Working...";
            // 
            // FWorking
            // 
            this.ClientSize = new System.Drawing.Size(463, 102);
            this.Controls.Add(this.titleLabel1);
            this.Name = "FWorking";
            this.Load += new System.EventHandler(this.FWorking_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer startT;
        private System.Windows.Forms.Timer closeT;
        private VisualComponents.TitleLabel titleLabel1;

    }
}


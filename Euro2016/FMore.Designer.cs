namespace Euro2016
{
    partial class FMore
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
            this.buttonsP = new Euro2016.VisualComponents.MyPanel();
            this.SuspendLayout();
            // 
            // buttonsP
            // 
            this.buttonsP.DrawPanelAccent = false;
            this.buttonsP.Location = new System.Drawing.Point(12, 12);
            this.buttonsP.Name = "buttonsP";
            this.buttonsP.Size = new System.Drawing.Size(200, 120);
            this.buttonsP.TabIndex = 0;
            // 
            // FMore
            // 
            this.ClientSize = new System.Drawing.Size(224, 224);
            this.Controls.Add(this.buttonsP);
            this.Name = "FMore";
            this.Load += new System.EventHandler(this.FMore_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private VisualComponents.MyPanel buttonsP;
    }
}
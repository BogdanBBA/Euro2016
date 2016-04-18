namespace Euro2016
{
    partial class FMap
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
            this.mapPB = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.mapPB)).BeginInit();
            this.SuspendLayout();
            // 
            // mapPB
            // 
            this.mapPB.BackColor = System.Drawing.Color.Transparent;
            this.mapPB.Location = new System.Drawing.Point(12, 12);
            this.mapPB.Name = "mapPB";
            this.mapPB.Size = new System.Drawing.Size(799, 718);
            this.mapPB.TabIndex = 0;
            this.mapPB.TabStop = false;
            // 
            // FMap
            // 
            this.ClientSize = new System.Drawing.Size(822, 742);
            this.Controls.Add(this.mapPB);
            this.Name = "FMap";
            this.Load += new System.EventHandler(this.FMap_Load);
            ((System.ComponentModel.ISupportInitialize)(this.mapPB)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox mapPB;

    }
}
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
            this.map = new DotSpatial.Controls.Map();
            this.SuspendLayout();
            // 
            // map
            // 
            this.map.AllowDrop = true;
            this.map.BackColor = System.Drawing.Color.White;
            this.map.CollectAfterDraw = false;
            this.map.CollisionDetection = false;
            this.map.ExtendBuffer = false;
            this.map.FunctionMode = DotSpatial.Controls.FunctionMode.None;
            this.map.IsBusy = false;
            this.map.IsZoomedToMaxExtent = false;
            this.map.Location = new System.Drawing.Point(12, 12);
            this.map.Name = "map";
            this.map.ProgressHandler = null;
            this.map.ProjectionModeDefine = DotSpatial.Controls.ActionMode.Prompt;
            this.map.ProjectionModeReproject = DotSpatial.Controls.ActionMode.Prompt;
            this.map.RedrawLayersWhileResizing = false;
            this.map.SelectionEnabled = true;
            this.map.Size = new System.Drawing.Size(1080, 718);
            this.map.TabIndex = 0;
            this.map.ZoomOutFartherThanMaxExtent = false;
            // 
            // FMap
            // 
            this.ClientSize = new System.Drawing.Size(1104, 742);
            this.Controls.Add(this.map);
            this.Name = "FMap";
            this.Load += new System.EventHandler(this.FMap_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private DotSpatial.Controls.Map map;
    }
}
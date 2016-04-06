﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Euro2016.VisualComponents
{
    public class SmoothLabel : Label
    {
        public SmoothLabel()
            : base()
        {
            this.BackColor = Color.Transparent;
            this.Font = StaticData.PVC != null ? new Font(StaticData.PVC.Families[StaticData.FontExo_Index], 16, FontStyle.Regular) : new Font("Arial", 16, FontStyle.Regular);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
            base.OnPaint(e);
        }
    }
}

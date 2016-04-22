using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Euro2016.VisualComponents
{
    public class FlagView : MyEuroBaseControl
    {
        private Country country;
        public Country Country
        {
            get { return this.country; }
            set { this.country = value; this.Invalidate(); }
        }

        public FlagView()
            : base()
        {
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.BackColor = Color.Transparent;
            this.Cursor = Cursors.Hand;
            this.Size = new Size(32, 20);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.CompositingQuality = CompositingQuality.AssumeLinear;

            float padX = this.Width / 2f - this.country.Flag20px.Width / 2f, padY = this.Height / 2f - this.country.Flag20px.Height / 2f;

            if (this.country != null)
                e.Graphics.DrawImage(this.country.Flag20px, padX, padY);
        }
    }
}

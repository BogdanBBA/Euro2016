using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Euro2016.VisualComponents
{
    public class CountryView : MyEuroBaseControl
    {
        internal const int DefaultHeight = 28;

        private Country country;
        public Country Country
        {
            get { return this.country; }
            set { this.country = value; this.Invalidate(); }
        }

        public Team Team
        {
            set { this.country = value.Country; this.Invalidate(); }
        }

        private Settings settings;
        public Settings Settings
        {
            get { return this.settings; }
            set { this.settings = value; this.Invalidate(); }
        }

        public CountryView()
            : base()
        {
            this.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            this.Size = new Size(200, CountryView.DefaultHeight);
            this.Cursor = Cursors.Hand;
            this.settings = null;
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.AssumeLinear;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            if (this.country != null)
                e.Graphics.DrawImage(this.country.Flag20px, new Point(16 - this.country.Flag20px.Width / 2, this.Height / 2 - this.country.Flag20px.Height / 2));

            string text = this.country != null ? this.country.Names[this.settings != null ? this.settings.ShowCountryNamesInNativeLanguage : false] : typeof(CountryView).Name;
            SizeF size = e.Graphics.MeasureString(text, this.Font);
            e.Graphics.DrawString(text, this.Font, this.isChecked ? MyGUIs.Accent.Highlighted.Brush : MyGUIs.Text[this.mouseIsOver].Brush, new PointF(32 + 8, this.Height / 2 - size.Height / 2));
        }
    }
}

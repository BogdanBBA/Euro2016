using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Euro2016.VisualComponents
{
    public class TitleLabel : MyEuroBaseControl
    {
        public static readonly Pair<int> BarHeight = new Pair<int>(2, 4);
        public const int TitleLabelHeight = 78;

        public TitleLabel()
            : base()
        {
            this.TitleFormatting = new Tuple<Font, Brush, string>(
                StaticData.PVC != null ? new Font(StaticData.PVC.Families[StaticData.FontExo_Index], 27, FontStyle.Bold) : new Font("Arial", 32, FontStyle.Bold),
                MyGUIs.Text.Normal.Brush, "[Title text]");
            this.SubtitleFormatting = new Tuple<Font, Brush, string>(
                StaticData.PVC != null ? new Font(StaticData.PVC.Families[StaticData.FontExo_Index], 14, FontStyle.Regular) : new Font("Arial", 15, FontStyle.Bold),
                MyGUIs.Text.Highlighted.Brush, "[Subtitle text]");
            this.Size = new Size(400, TitleLabel.TitleLabelHeight);
            this.Cursor = Cursors.SizeAll;
        }

        private bool drawBar = true;
        public bool DrawBar
        {
            get { return this.drawBar; }
            set { this.drawBar = value; this.Invalidate(); }
        }

        private bool bigBar = false;
        public bool BigBar
        {
            get { return this.bigBar; }
            set { this.bigBar = value; this.Invalidate(); }
        }

        public Tuple<Font, Brush, string> TitleFormatting { get; internal set; }
        public Tuple<Font, Brush, string> SubtitleFormatting { get; internal set; }

        public string TextTitle
        {
            get { return this.TitleFormatting.Item3; }
            set { this.TitleFormatting = new Tuple<Font, Brush, string>(this.TitleFormatting.Item1, this.TitleFormatting.Item2, value); this.Invalidate(); }
        }

        public string TextSubtitle
        {
            get { return this.SubtitleFormatting.Item3; }
            set { this.SubtitleFormatting = new Tuple<Font, Brush, string>(this.SubtitleFormatting.Item1, this.SubtitleFormatting.Item2, value); this.Invalidate(); }
        }

        private HorizontalAlignment textAlign;
        public HorizontalAlignment TextAlign
        {
            get { return this.textAlign; }
            set { this.textAlign = value; this.Invalidate(); }
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            e.Graphics.Clear(MyGUIs.Background.Normal.Color);
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            SizeF size = e.Graphics.MeasureString(this.TitleFormatting.Item3, this.TitleFormatting.Item1);
            PointF location = new PointF(this.textAlign == HorizontalAlignment.Left
                ? 0 : (this.textAlign == HorizontalAlignment.Center ? this.Width / 2 - size.Width / 2 : this.Width - size.Width), 0);
            e.Graphics.DrawString(this.TitleFormatting.Item3, this.TitleFormatting.Item1, this.TitleFormatting.Item2, location);

            float lastBottom = location.Y + size.Height;
            size = e.Graphics.MeasureString(this.SubtitleFormatting.Item3, this.SubtitleFormatting.Item1);
            location = new PointF(this.textAlign == HorizontalAlignment.Left
                ? 4 : (this.textAlign == HorizontalAlignment.Center ? this.Width / 2 - size.Width / 2 : this.Width - size.Width - 4), lastBottom - 8);
            e.Graphics.DrawString(this.SubtitleFormatting.Item3, this.SubtitleFormatting.Item1, this.SubtitleFormatting.Item2, location);

            if (this.drawBar)
                e.Graphics.FillRectangle(MyGUIs.Accent.Normal.Brush, 1, this.Height - BarHeight.GetValue(this.bigBar), this.Width - 2, BarHeight.GetValue(this.bigBar));
        }
    }
}

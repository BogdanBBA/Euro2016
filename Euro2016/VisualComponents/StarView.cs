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
    public class StarView : MyEuroBaseControl
    {
        protected static readonly PointF[] StarPoints = new PointF[] { 
            new PointF(0.5f, 0f), 
            new PointF(0.3477f, 0.3234f), new PointF(0f, 0.383f), new PointF(0.2461f, 0.6343f), new PointF(0.1924f, 1f), 
            new PointF(0.5f, 0.8318f),
            new PointF(0.8076f, 1f), new PointF(0.7539f, 0.6343f), new PointF(1f, 0.383f), new PointF(0.6523f, 0.3234f) 
        };

        protected GraphicsPath path;

        public StarView()
            : base()
        {
            this.path = new GraphicsPath();
            this.Size = new Size(50, 50);
            this.Cursor = Cursors.Hand;
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            this.Checked = !this.Checked;
        }

        protected override void OnResize(EventArgs e)
        {
            this.path.Reset();
            for (int iP = 0; iP < StarView.StarPoints.Length - 1; iP++)
            {
                PointF a = StarView.StarPoints[iP], b = StarView.StarPoints[iP + 1];
                this.path.AddLine(new PointF(a.X * this.Width, a.Y * this.Height), new PointF(b.X * this.Width, b.Y * this.Height));
            }
            this.path.AddLine(new PointF(StarView.StarPoints.Last().X * this.Width, StarView.StarPoints.Last().Y * this.Height), new PointF(StarView.StarPoints[0].X * this.Width, StarView.StarPoints[0].Y * this.Height));

            base.OnResize(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(MyGUIs.Background[this.mouseIsOver].Color);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            e.Graphics.FillPath(this.mouseIsClicked ? MyGUIs.Accent.Highlighted.Brush : MyGUIs.Star[this.isChecked].Brush, path);
        }
    }
}

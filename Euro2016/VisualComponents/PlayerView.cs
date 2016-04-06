using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Euro2016.VisualComponents
{
    public class PlayerViewBase : MyEuroBaseControl
    {
        protected static readonly double[] ColumnWidths = { 0.05, 0.05, 0.25, 0.2, 0.1, 0.1, 0.05, 0.2 };
        protected static readonly string[] ColumnCaptions = { "No.", "", "Name", "Born", "Caps", "Goals", "", "Club" };
    }

    public class PlayerViewHeader : PlayerViewBase
    {
        public PlayerViewHeader()
        {
            this.Font = StaticData.PVC != null ? new Font(StaticData.PVC.Families[StaticData.FontExo_Index], 16, FontStyle.Regular) : new Font("Arial", 12, FontStyle.Regular);
            this.Size = new Size(500, 24);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(MyGUIs.Background.Normal.Color);

            double lastLeft = 0;
            this.DrawTextCell(e.Graphics, this.Font, PlayerViewBase.ColumnCaptions[0], HorizontalAlignment.Center, PlayerViewBase.ColumnWidths[0], lastLeft);
            lastLeft += GroupView.ColumnWidthPercentages[0];
            this.DrawTextCell(e.Graphics, this.Font, PlayerViewBase.ColumnCaptions[1], HorizontalAlignment.Center, PlayerViewBase.ColumnWidths[1], lastLeft);
            lastLeft += GroupView.ColumnWidthPercentages[1];
            this.DrawTextCell(e.Graphics, this.Font, PlayerViewBase.ColumnCaptions[2], HorizontalAlignment.Center, PlayerViewBase.ColumnWidths[2], lastLeft);
            lastLeft += GroupView.ColumnWidthPercentages[2];
            this.DrawTextCell(e.Graphics, this.Font, PlayerViewBase.ColumnCaptions[3], HorizontalAlignment.Center, PlayerViewBase.ColumnWidths[3], lastLeft);
            lastLeft += GroupView.ColumnWidthPercentages[3];
            this.DrawTextCell(e.Graphics, this.Font, PlayerViewBase.ColumnCaptions[4], HorizontalAlignment.Center, PlayerViewBase.ColumnWidths[4], lastLeft);
            lastLeft += GroupView.ColumnWidthPercentages[4];
            this.DrawTextCell(e.Graphics, this.Font, PlayerViewBase.ColumnCaptions[5], HorizontalAlignment.Center, PlayerViewBase.ColumnWidths[5], lastLeft);
            lastLeft += GroupView.ColumnWidthPercentages[5];
            this.DrawTextCell(e.Graphics, this.Font, PlayerViewBase.ColumnCaptions[6], HorizontalAlignment.Center, PlayerViewBase.ColumnWidths[6], lastLeft);
            lastLeft += GroupView.ColumnWidthPercentages[6];
            this.DrawTextCell(e.Graphics, this.Font, PlayerViewBase.ColumnCaptions[7], HorizontalAlignment.Center, PlayerViewBase.ColumnWidths[7], lastLeft);
        }
    }

    public class PlayerView : PlayerViewBase
    {
        private Player player = null;
        public Player Player
        {
            get { return this.player; }
            set { this.player = value; this.Invalidate(); }
        }

        public PlayerView()
        {
            this.Size = new Size(500, 24);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Euro2016.VisualComponents
{
    public class PlayersView
    {
        private EventHandler onPlayerViewMouseEnterDelegate;
        private EventHandler onPlayerViewClickDelegate;

        private PlayerViewHeader header;
        private PlayerViewRow[] rows;

        internal MyScrollPanel myScrollPanel;
        private Settings settings;

        public PlayersView(Panel parent, EventHandler onPlayerViewMouseEnterDelegate, EventHandler onPlayerViewClickDelegate, Settings settings)
        {
            this.onPlayerViewMouseEnterDelegate = onPlayerViewMouseEnterDelegate;
            this.onPlayerViewClickDelegate = onPlayerViewClickDelegate;

            this.myScrollPanel = new MyScrollPanel(parent, MyScrollBar.ScrollBarPosition.Right, 2, 80);
            this.myScrollPanel.UpdatePanelSize();

            this.header = new PlayerViewHeader();
            this.header.Size = new Size(this.myScrollPanel.VisibleSize.Width, PlayerViewBase.DefaultHeight);
            this.myScrollPanel.AddControl(this.header, Point.Empty, false);
            this.rows = new PlayerViewRow[0];

            this.settings = settings;
        }

        public void SetPlayers(ListOfIDObjects<Player> players)
        {
            if (this.rows.Length > players.Count)
                for (int i = players.Count; i < this.rows.Length; i++)
                    this.rows[i].Hide();
            else if (this.rows.Length < players.Count)
            {
                PlayerViewRow[] temp = new PlayerViewRow[players.Count];
                Array.Copy(this.rows, temp, this.rows.Length);
                for (int i = this.rows.Length; i < players.Count; i++)
                {
                    temp[i] = new PlayerViewRow();
                    temp[i].Size = new Size(this.myScrollPanel.VisibleSize.Width, PlayerViewBase.DefaultHeight);
                    temp[i].MouseEnter += this.onPlayerViewMouseEnterDelegate;
                    temp[i].Click += this.onPlayerViewClickDelegate;
                    this.myScrollPanel.AddControl(temp[i], new Point(0, (i + 1) * PlayerViewBase.DefaultHeight), false);
                }
                this.rows = temp;
            }

            this.myScrollPanel.UpdatePanelSize();
            for (int i = 0; i < this.rows.Length; i++)
                this.rows[i].Player = players[i];
        }
    }

    public class PlayerViewBase : MyEuroBaseControl
    {
        protected static readonly double[] ColumnWidths = { 0.07, 0.04, 0.26, 0.15, 0.07, 0.07, 0.07, 0.04, 0.22 };
        protected static readonly string[] ColumnCaptions = { "No.", "", "Name", "Born", "Age", "Caps", "Goals", "", "Club" };
        public const int DefaultHeight = 24;

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.CompositingQuality = CompositingQuality.AssumeLinear;
            e.Graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
        }
    }

    public class PlayerViewRow : PlayerViewBase
    {
        private Player player = null;
        public Player Player
        {
            get { return this.player; }
            set { this.player = value; this.Invalidate(); }
        }

        public PlayerViewRow()
        {
            this.Font = StaticData.PVC != null ? new Font(StaticData.PVC.Families[StaticData.FontExo_Index], 13, FontStyle.Regular) : new Font("Arial", 11, FontStyle.Regular);
            this.Size = new Size(500, PlayerViewBase.DefaultHeight);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (this.player == null)
                return;

            double lastLeft = 0.0;
            this.DrawTextCell(e.Graphics, this.Font, "#" + this.player.Number, HorizontalAlignment.Center, lastLeft, PlayerViewBase.ColumnWidths[0]);
            lastLeft += PlayerViewBase.ColumnWidths[0];
            this.DrawImageCell(e.Graphics, this.player.Nationality.Country.Flag20px, HorizontalAlignment.Center, lastLeft, lastLeft + PlayerViewBase.ColumnWidths[1]);
            lastLeft += PlayerViewBase.ColumnWidths[1];
            this.DrawTextCell(e.Graphics, this.Font, this.player.Name, HorizontalAlignment.Left, lastLeft, lastLeft + PlayerViewBase.ColumnWidths[2]);
            lastLeft += PlayerViewBase.ColumnWidths[2];
            this.DrawTextCell(e.Graphics, this.Font, this.player.BirthDate.ToString("d MMM yyyy"), HorizontalAlignment.Center, lastLeft, lastLeft + PlayerViewBase.ColumnWidths[3]);
            lastLeft += PlayerViewBase.ColumnWidths[3];
            this.DrawTextCell(e.Graphics, this.Font, "0", HorizontalAlignment.Center, lastLeft, lastLeft + PlayerViewBase.ColumnWidths[4]);
            lastLeft += PlayerViewBase.ColumnWidths[4];
            this.DrawTextCell(e.Graphics, this.Font, this.player.Caps.ToString(), HorizontalAlignment.Center, lastLeft, lastLeft + PlayerViewBase.ColumnWidths[5]);
            lastLeft += PlayerViewBase.ColumnWidths[5];
            this.DrawTextCell(e.Graphics, this.Font, this.player.Goals.ToString(), HorizontalAlignment.Center, lastLeft, lastLeft + PlayerViewBase.ColumnWidths[6]);
            lastLeft += PlayerViewBase.ColumnWidths[6];
            this.DrawImageCell(e.Graphics, this.player.Club.Country.Flag20px, HorizontalAlignment.Right, lastLeft, lastLeft + PlayerViewBase.ColumnWidths[7]);
            lastLeft += PlayerViewBase.ColumnWidths[7];
            this.DrawTextCell(e.Graphics, this.Font, this.player.Club.Name, HorizontalAlignment.Left, lastLeft, lastLeft + PlayerViewBase.ColumnWidths[8]);
        }
    }

    public class PlayerViewHeader : PlayerViewBase
    {
        public PlayerViewHeader()
        {
            this.Font = StaticData.PVC != null ? new Font(StaticData.PVC.Families[StaticData.FontExo_Index], 16, FontStyle.Regular) : new Font("Arial", 12, FontStyle.Regular);
            this.Size = new Size(500, PlayerViewBase.DefaultHeight);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.Clear(MyGUIs.Background.Normal.Color);

            double lastLeft = 0;
            this.DrawTextCell(e.Graphics, this.Font, PlayerViewBase.ColumnCaptions[0], HorizontalAlignment.Center, lastLeft, lastLeft + PlayerViewBase.ColumnWidths[0]);
            lastLeft += PlayerViewBase.ColumnWidths[0];
            this.DrawTextCell(e.Graphics, this.Font, PlayerViewBase.ColumnCaptions[2], HorizontalAlignment.Center, lastLeft, lastLeft + PlayerViewBase.ColumnWidths[1] + PlayerViewBase.ColumnWidths[2]);
            lastLeft += PlayerViewBase.ColumnWidths[1] + PlayerViewBase.ColumnWidths[2];
            this.DrawTextCell(e.Graphics, this.Font, PlayerViewBase.ColumnCaptions[3], HorizontalAlignment.Center, lastLeft, lastLeft + PlayerViewBase.ColumnWidths[3]);
            lastLeft += PlayerViewBase.ColumnWidths[3];
            this.DrawTextCell(e.Graphics, this.Font, PlayerViewBase.ColumnCaptions[4], HorizontalAlignment.Center, lastLeft, lastLeft + PlayerViewBase.ColumnWidths[4]);
            lastLeft += PlayerViewBase.ColumnWidths[4];
            this.DrawTextCell(e.Graphics, this.Font, PlayerViewBase.ColumnCaptions[5], HorizontalAlignment.Center, lastLeft, lastLeft + PlayerViewBase.ColumnWidths[5]);
            lastLeft += PlayerViewBase.ColumnWidths[5];
            this.DrawTextCell(e.Graphics, this.Font, PlayerViewBase.ColumnCaptions[6], HorizontalAlignment.Center, lastLeft, lastLeft + PlayerViewBase.ColumnWidths[6]);
            lastLeft += PlayerViewBase.ColumnWidths[6];
            this.DrawTextCell(e.Graphics, this.Font, PlayerViewBase.ColumnCaptions[8], HorizontalAlignment.Center, lastLeft, lastLeft + PlayerViewBase.ColumnWidths[7] + PlayerViewBase.ColumnWidths[8]);
        }
    }
}

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
    /// <summary>
    /// 
    /// </summary>
    public class PlayersView
    {
        protected EventHandler onPlayerViewMouseEnterDelegate;
        protected EventHandler onPlayerViewClickDelegate;

        internal PlayerViewHeader header;
        protected PlayerViewRow[] rows;

        internal MyScrollPanel myScrollPanel;
        protected Settings settings;

        public PlayersView(Panel parent, FPlayers playersForm, EventHandler onPlayerViewMouseEnterDelegate, EventHandler onPlayerViewClickDelegate, Settings settings)
        {
            this.onPlayerViewMouseEnterDelegate = onPlayerViewMouseEnterDelegate;
            this.onPlayerViewClickDelegate = onPlayerViewClickDelegate;

            this.myScrollPanel = new MyScrollPanel(parent, MyScrollBar.ScrollBarPosition.Right, 2, 80);
            this.myScrollPanel.UpdatePanelSize();

            this.header = new PlayerViewHeader(playersForm);
            this.header.Size = new Size(this.myScrollPanel.VisibleSize.Width, PlayerViewBase.DefaultHeight);
            this.myScrollPanel.AddControl(this.header, Point.Empty, false);
            this.rows = new PlayerViewRow[0];

            this.settings = settings;
        }

        public void SetPlayers(ListOfIDObjects<Player> players, int sortByColumn, bool descending)
        {
            players.SortPlayers(sortByColumn, descending);

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

            for (int i = 0; i < players.Count; i++)
            {
                this.rows[i].Show();
                this.rows[i].PlayerProperty = players[i];
            }
            this.myScrollPanel.UpdatePanelSize();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class PlayerViewBase : MyEuroBaseControl
    {
        protected static readonly double[] ColumnWidths = { 0.06, 0.04, 0.24, 0.05, 0.09, 0.13, 0.07, 0.07, 0.04, 0.2 };
        protected static readonly string[] ColumnCaptions = { "No.", "", "Name", "Position", "Age", "Born", "Caps", "Goals", "", "Club" };
        public const int DefaultHeight = 24;

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.CompositingQuality = CompositingQuality.AssumeLinear;
            e.Graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class PlayerViewHeader : PlayerViewBase
    {
        public int SortByColumn { get; protected set; }
        public bool Descending { get; protected set; }
        protected Point cursorLocation = Point.Empty;
        protected FPlayers playersForm;

        public PlayerViewHeader(FPlayers playersForm)
            : base()
        {
            this.Font = StaticData.PVC != null ? new Font(StaticData.PVC.Families[StaticData.FontExoBold_Index], 16, FontStyle.Bold) : new Font("Arial", 12, FontStyle.Regular);
            this.Size = new Size(500, PlayerViewBase.DefaultHeight);
            this.Cursor = Cursors.Hand;
            this.playersForm = playersForm;
            this.SortByColumn = 0;
            this.Descending = false;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            this.cursorLocation = e.Location;
            base.OnMouseMove(e);
        }

        protected override void OnClick(EventArgs e)
        {
            double p = (double) this.cursorLocation.X / this.Width, totalWidth;
            int column;
            for (column = 0, totalWidth = 0.0; column < PlayerViewBase.ColumnWidths.Length; column++)
            {
                totalWidth += PlayerViewBase.ColumnWidths[column];
                if (p < totalWidth && !PlayerViewBase.ColumnCaptions[column].Equals(""))
                    break;
            }
            if (column == this.SortByColumn)
                this.Descending = !this.Descending;
            this.SortByColumn = column;
            this.Invalidate();
            this.playersForm.RefreshInformation(this.playersForm.countryViews.First(cv => cv.Checked).Country);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.Clear(MyGUIs.Background.Normal.Color);

            double lastLeft = 0.0;
            for (int iCol = 0; iCol < PlayerViewBase.ColumnWidths.Length; iCol++)
            {
                double width = iCol == 2 || iCol == 9 ? PlayerViewBase.ColumnWidths[iCol] + PlayerViewBase.ColumnWidths[iCol - 1] : PlayerViewBase.ColumnWidths[iCol];
                if (iCol != 1 && iCol != 8)
                {
                    string text = PlayerViewBase.ColumnCaptions[iCol] + (this.SortByColumn == iCol ? (this.Descending ? "▼" : "▲") : "");
                    this.DrawTextCell(e.Graphics, this.Font, text, HorizontalAlignment.Center, lastLeft, lastLeft + width);
                    lastLeft += width;
                }
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class PlayerViewRow : PlayerViewBase
    {
        private Font playerPositionFont;

        private Player player = null;
        public Player PlayerProperty
        {
            get { return this.player; }
            set { this.player = value; this.Invalidate(); }
        }

        public PlayerViewRow()
        {
            this.Font = StaticData.PVC != null ? new Font(StaticData.PVC.Families[StaticData.FontExo_Index], 13, FontStyle.Regular) : new Font("Arial", 11, FontStyle.Regular);
            this.playerPositionFont = StaticData.PVC != null ? new Font(StaticData.PVC.Families[StaticData.FontExoBold_Index], 10, FontStyle.Bold) : new Font("Arial", 11, FontStyle.Regular);
            this.Size = new Size(500, PlayerViewBase.DefaultHeight);
        }

        protected void DrawPlayerPosition(Graphics g, double left, double width, Player player)
        {
            Brush brush = new SolidBrush(Player.PlayingPositionColors[(int) this.player.PlayerPosition]);
            RectangleF colorRect = new RectangleF((float) (left * this.Width + 1), 1, (float) (width * this.Width - 2 * 1), this.Height - 2 * 1);
            g.FillRectangle(brush, colorRect);
            SizeF size = g.MeasureString(this.player.PlayerPosition.ToString(), this.playerPositionFont);
            g.DrawString(this.player.PlayerPosition.ToString(), this.playerPositionFont, Brushes.WhiteSmoke,
                new PointF(colorRect.Left + colorRect.Width / 2 - size.Width / 2, colorRect.Top + colorRect.Height / 2 - size.Height / 2));
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
            this.DrawPlayerPosition(e.Graphics, lastLeft, PlayerViewBase.ColumnWidths[3], this.player);
            lastLeft += PlayerViewBase.ColumnWidths[3];
            this.DrawTextCell(e.Graphics, this.Font, this.player.Age.ToString(), HorizontalAlignment.Center, lastLeft, lastLeft + PlayerViewBase.ColumnWidths[4]);
            lastLeft += PlayerViewBase.ColumnWidths[4];
            this.DrawTextCell(e.Graphics, this.Font, this.player.BirthDate.ToString("d MMM yyyy"), HorizontalAlignment.Center, lastLeft, lastLeft + PlayerViewBase.ColumnWidths[5]);
            lastLeft += PlayerViewBase.ColumnWidths[5];
            this.DrawTextCell(e.Graphics, this.Font, this.player.Caps.ToString(), HorizontalAlignment.Center, lastLeft, lastLeft + PlayerViewBase.ColumnWidths[6]);
            lastLeft += PlayerViewBase.ColumnWidths[6];
            this.DrawTextCell(e.Graphics, this.Font, this.player.Goals.ToString(), HorizontalAlignment.Center, lastLeft, lastLeft + PlayerViewBase.ColumnWidths[7]);
            lastLeft += PlayerViewBase.ColumnWidths[7];
            this.DrawImageCell(e.Graphics, this.player.Club.Country.Flag20px, HorizontalAlignment.Right, lastLeft, lastLeft + PlayerViewBase.ColumnWidths[8]);
            lastLeft += PlayerViewBase.ColumnWidths[8];
            this.DrawTextCell(e.Graphics, this.Font, this.player.Club.Name, HorizontalAlignment.Left, lastLeft, lastLeft + PlayerViewBase.ColumnWidths[9]);
        }
    }
}

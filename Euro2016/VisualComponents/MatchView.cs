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
    /// <summary>
    /// 
    /// </summary>
    public class MatchesView
    {
        private EventHandler onMatchHeaderClickDelegate;
        private EventHandler onMatchRowClickDelegate;

        private List<MatchHeader> headers;
        private List<MatchRow> rows;

        internal MyScrollPanel myScrollPanel;
        private Settings settings;

        public MatchesView(Panel parent, EventHandler onMatchHeaderClickDelegate, EventHandler onMatchRowClickDelegate, Settings settings)
        {
            this.onMatchHeaderClickDelegate = onMatchHeaderClickDelegate;
            this.onMatchRowClickDelegate = onMatchRowClickDelegate;

            this.headers = new List<MatchHeader>();
            this.rows = new List<MatchRow>();

            this.myScrollPanel = new MyScrollPanel(parent, MyScrollBar.ScrollBarPosition.Right, 2, 80);
            this.myScrollPanel.UpdatePanelSize();

            this.settings = settings;
        }

        public void SetMatches(ListOfIDObjects<Match> matches)
        {
            List<DateTime> dates = new List<DateTime>();
            matches.SortMatchesChronologically();
            foreach (Match match in matches)
                if (dates.IndexOf(match.WhenOffset.Date) == -1)
                    dates.Add(match.WhenOffset.Date);

            Size headerSize = new Size(this.myScrollPanel.VisibleSize.Width, MatchHeader.HeaderHeight);
            if (this.headers.Count > dates.Count)
                for (int index = dates.Count; index < this.headers.Count; index++)
                    this.headers[index].Hide();
            else if (this.headers.Count < dates.Count)
                for (int index = this.headers.Count; index < dates.Count; index++)
                    this.headers.Add(new MatchHeader(DateTime.Now, this.onMatchHeaderClickDelegate) { Size = headerSize });

            Size rowSize = new Size(this.myScrollPanel.VisibleSize.Width, MatchRow.RowHeight);
            if (this.rows.Count > matches.Count)
                for (int index = matches.Count; index < this.rows.Count; index++)
                    this.rows[index].Hide();
            else if (this.rows.Count < matches.Count)
                for (int index = this.rows.Count; index < matches.Count; index++)
                    this.rows.Add(new MatchRow(null, this.onMatchRowClickDelegate, this.settings) { Size = rowSize });

            DateTime lastDate = new DateTime(2000, 1, 1);
            Point location;
            for (int iM = 0, iD = -1, lastTop = 0; iM < matches.Count; iM++)
            {
                if (lastDate.Date.CompareTo(matches[iM].WhenOffset.Date) != 0)
                {
                    lastDate = dates[++iD];
                    location = new Point(0, lastTop);
                    lastTop += MatchHeader.HeaderHeight;
                    if (!this.myScrollPanel.ContainsControl(this.headers[iD]) || !this.headers[iD].Location.Equals(location))
                        this.myScrollPanel.AddControl(this.headers[iD], location, false);
                    this.headers[iD].Show();
                    this.headers[iD].Date = dates[iD];
                }

                location = new Point(0, lastTop);
                lastTop += MatchRow.RowHeight;
                if (!this.myScrollPanel.ContainsControl(this.rows[iM]) || !this.rows[iM].Location.Equals(location))
                    this.myScrollPanel.AddControl(this.rows[iM], location, false);
                this.rows[iM].Show();
                this.rows[iM].Match = matches[iM];
            }

            this.myScrollPanel.UpdatePanelSize();
        }

        public MatchRow GetRowByMatch(Match match)
        {
            foreach (MatchRow row in this.rows)
                if (row.Match.Equals(match))
                    return row;
            return null;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class MatchHeader : MyEuroBaseControl
    {
        public static string DateFormat = "dddd, d MMMM yyyy";
        public static int HeaderHeight = 40;

        private DateTime date;
        public DateTime Date
        {
            get { return this.date; }
            set { this.date = value; this.Invalidate(); }
        }

        public MatchHeader(DateTime date, EventHandler onClickDelegate)
            : base()
        {
            this.date = date;
            this.Font = new Font(StaticData.PVC.Families[StaticData.FontExo_Index], 13, FontStyle.Bold);
            this.Cursor = Cursors.Hand;
            this.Click += onClickDelegate;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            SizeF size = e.Graphics.MeasureString(this.date.ToString(DateFormat), this.Font);
            e.Graphics.DrawString(this.date.ToString(DateFormat), this.Font, MyGUIs.Accent[this.mouseIsOver].Brush, new PointF(this.Width / 2 - size.Width / 2, this.Height / 2 - size.Height / 2));
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class MatchRow : MyEuroBaseControl
    {
        public static int RowHeight = 28;
        private static readonly Font CategoryFont = new Font(StaticData.PVC.Families[StaticData.FontExoLight_Index], 9, FontStyle.Bold);
        private static readonly Font TeamFont = new Font(StaticData.PVC.Families[StaticData.FontExo_Index], 10, FontStyle.Regular);
        private static readonly Font ScoreFont = new Font(StaticData.PVC.Families[StaticData.FontExoBold_Index], 12, FontStyle.Bold);

        private Match match;
        public Match Match
        {
            get { return this.match; }
            set { this.match = value; this.Invalidate(); }
        }
        private Settings settings;

        public MatchRow(Match match, EventHandler onClickDelegate, Settings settings)
            : base()
        {
            this.match = match;
            this.settings = settings;
            this.Cursor = Cursors.Hand;
            this.Click += onClickDelegate;
        }

        private void DrawText(Graphics g, Font font, Brush brush, string text, HorizontalAlignment alignment, float percentageStart, float percentageEnd)
        {
            RectangleF rect = new RectangleF(this.Width * percentageStart, 0, this.Width * (percentageEnd - percentageStart), this.Height);
            SizeF size = g.MeasureString(text, font);
            PointF location = new PointF(alignment == HorizontalAlignment.Left
                ? rect.Left : (alignment == HorizontalAlignment.Center ? rect.Left + rect.Width / 2 - size.Width / 2 : rect.Right - size.Width),
                rect.Height / 2 - size.Height / 2);
            g.DrawString(text, font, brush, location);
        }

        private void DrawImage(Graphics g, Bitmap image, HorizontalAlignment alignment, float percentageStart, float percentageEnd)
        {
            RectangleF rect = new RectangleF(this.Width * percentageStart, 0, this.Width * (percentageEnd - percentageStart), this.Height);
            PointF location = new PointF(alignment == HorizontalAlignment.Left
                ? rect.Left : (alignment == HorizontalAlignment.Center ? rect.Left + rect.Width / 2 - image.Size.Width / 2 : rect.Right - image.Size.Width),
                rect.Height / 2 - image.Size.Height / 2);
            g.DrawImage(image, location);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.AssumeLinear;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            if (this.match == null)
            {
                e.Graphics.DrawString("this.Match == null", this.Font, MyGUIs.Text.Normal.Brush, Point.Empty);
                return;
            }

            string text = this.match.FormatCategory;
            this.DrawText(e.Graphics, MatchRow.CategoryFont, MyGUIs.Category[this.mouseIsClicked].Brush, text, HorizontalAlignment.Center, 0f, 0.16f);

            text = this.match.Teams.Home != null ? this.match.Teams.Home.Country.Names[this.settings.ShowCountryNamesInNativeLanguage] : "TBD";
            this.DrawText(e.Graphics, MatchRow.TeamFont, MyGUIs.Text[this.mouseIsOver].Brush, text, HorizontalAlignment.Right, 0.16f, 0.43f);

            Bitmap image = this.match.Teams.Home != null ? this.match.Teams.Home.Country.Flag20px : (Bitmap) Utils.ScaleImage(StaticData.Images[Paths.UnknownTeamImageFile], 32, 20, InterpolationMode.NearestNeighbor, false);
            this.DrawImage(e.Graphics, image, HorizontalAlignment.Center, 0.43f, 0.53f);

            if (this.match.Scoreboard.Played)
                this.DrawText(e.Graphics, MatchRow.ScoreFont, MyGUIs.Text[this.mouseIsOver].Brush, this.match.Scoreboard.FormatScore(false), HorizontalAlignment.Center, 0.53f, 0.63f);
            else
                this.DrawText(e.Graphics, MatchRow.CategoryFont, MyGUIs.Category[this.mouseIsOver].Brush, this.match.WhenOffset.ToString("HH:mm"), HorizontalAlignment.Center, 0.53f, 0.63f);

            image = this.match.Teams.Away != null ? this.match.Teams.Away.Country.Flag20px : (Bitmap) Utils.ScaleImage(StaticData.Images[Paths.UnknownTeamImageFile], 32, 20, InterpolationMode.NearestNeighbor, false);
            this.DrawImage(e.Graphics, image, HorizontalAlignment.Center, 0.63f, 0.73f);

            text = this.match.Teams.Away != null ? this.match.Teams.Away.Country.Names[this.settings.ShowCountryNamesInNativeLanguage] : "TBD";
            this.DrawText(e.Graphics, MatchRow.TeamFont, MyGUIs.Text[this.mouseIsOver].Brush, text, HorizontalAlignment.Left, 0.73f, 1f);
        }
    }
}

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
    public class MatchesView
    {
        private EventHandler onMatchHeaderClickDelegate;
        private EventHandler onMatchRowClickDelegate;

        private MatchHeader[] headers;
        private MatchRow[] rows;

        internal MyScrollPanel myScrollPanel;
        private Settings settings;

        public MatchesView(Panel parent, EventHandler onMatchHeaderClickDelegate, EventHandler onMatchRowClickDelegate, Settings settings)
        {
            this.onMatchHeaderClickDelegate = onMatchHeaderClickDelegate;
            this.onMatchRowClickDelegate = onMatchRowClickDelegate;

            this.headers = new MatchHeader[0];
            this.rows = new MatchRow[0];

            this.myScrollPanel = new MyScrollPanel(parent, MyScrollBar.ScrollBarPosition.Right, 2, 80);
            this.myScrollPanel.UpdatePanelSize();

            this.settings = settings;
        }

        public void SetMatches(ListOfIDObjects<Match> matches)
        {
            List<DateTime> dates = new List<DateTime>();
            matches.SortMatchesChronologically();
            foreach (Match match in matches)
                if (dates.IndexOf(match.When.Date) == -1)
                    dates.Add(match.When.Date);

            foreach (MatchHeader header in this.headers)
            {
                header.Hide();
                header.Dispose();
            }
            foreach (MatchRow row in this.rows)
            {
                row.Hide();
                row.Dispose();
            }
            this.headers = new MatchHeader[dates.Count];
            this.rows = new MatchRow[matches.Count];

            DateTime lastDate = new DateTime(2000, 1, 1);
            for (int iM = 0, iD = -1, lastTop = 0; iM < matches.Count; iM++)
            {
                if (lastDate.Date.CompareTo(matches[iM].When.Date) != 0)
                {
                    lastDate = dates[++iD];
                    this.headers[iD] = new MatchHeader(lastDate, this.onMatchHeaderClickDelegate);
                    this.headers[iD].Size = new Size(this.myScrollPanel.VisibleSize.Width, MatchHeader.HeaderHeight);
                    this.myScrollPanel.AddControl(this.headers[iD], new Point(0, lastTop), false);
                    lastTop += MatchHeader.HeaderHeight;
                }
                this.rows[iM] = new MatchRow(matches[iM], this.onMatchRowClickDelegate, this.settings);
                this.rows[iM].Size = new Size(this.myScrollPanel.VisibleSize.Width, MatchRow.RowHeight);
                this.myScrollPanel.AddControl(this.rows[iM], new Point(0, lastTop), false);
                lastTop += MatchRow.RowHeight;
            }

            this.myScrollPanel.UpdatePanelSize();
        }
    }

    public class MatchHeader : MyEuroBaseControl
    {
        public static string DateFormat = "dddd, d MMMM yyyy";
        public static int HeaderHeight = 40;

        private DateTime date;

        public MatchHeader(DateTime date, EventHandler onClickDelegate)
            : base()
        {
            this.date = date;
            this.Font = new Font(StaticData.PVC.Families[StaticData.FontExo_Index], 13, FontStyle.Bold);
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

    public class MatchRow : MyEuroBaseControl
    {
        public static int RowHeight = 28;
        private static readonly Font CategoryFont = new Font(StaticData.PVC.Families[StaticData.FontExoLight_Index], 9, FontStyle.Bold);
        private static readonly Font TeamFont = new Font(StaticData.PVC.Families[StaticData.FontExo_Index], 10, FontStyle.Regular);
        private static readonly Font ScoreFont = new Font(StaticData.PVC.Families[StaticData.FontExoBold_Index], 12, FontStyle.Bold);

        public Match Match { get; private set; }
        private Settings settings;

        public MatchRow(Match match, EventHandler onClickDelegate, Settings settings)
            : base()
        {
            this.Match = match;
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

            string text = this.Match.ID + ". " + this.Match.Category; //this.Match.FormatCategory;
            this.DrawText(e.Graphics, MatchRow.CategoryFont, MyGUIs.Category[this.mouseIsClicked].Brush, text, HorizontalAlignment.Center, 0f, 0.16f);

            text = this.Match.Teams.Home != null ? this.Match.Teams.Home.Country.Names[this.settings.ShowCountryNamesInNativeLanguage] : "TBD";
            this.DrawText(e.Graphics, MatchRow.TeamFont, MyGUIs.Text[this.mouseIsOver].Brush, this.Match.TeamReferences.Home + ". " + text, HorizontalAlignment.Right, 0.16f, 0.43f);

            Bitmap image = this.Match.Teams.Home != null ? this.Match.Teams.Home.Country.Flag20px : (Bitmap) Utils.ScaleImage(StaticData.Images[Paths.UnknownTeamImageFile], 32, 20, InterpolationMode.NearestNeighbor, false);
            this.DrawImage(e.Graphics, image, HorizontalAlignment.Center, 0.43f, 0.53f);

            if (this.Match.Scoreboard.Played)
                this.DrawText(e.Graphics, MatchRow.ScoreFont, MyGUIs.Text[this.mouseIsOver].Brush, this.Match.Scoreboard.FormatScore, HorizontalAlignment.Center, 0.53f, 0.63f);
            else
                this.DrawText(e.Graphics, MatchRow.CategoryFont, MyGUIs.Category[this.mouseIsOver].Brush, this.Match.When.ToString("HH:mm"), HorizontalAlignment.Center, 0.53f, 0.63f);

            image = this.Match.Teams.Away != null ? this.Match.Teams.Away.Country.Flag20px : (Bitmap) Utils.ScaleImage(StaticData.Images[Paths.UnknownTeamImageFile], 32, 20, InterpolationMode.NearestNeighbor, false);
            this.DrawImage(e.Graphics, image, HorizontalAlignment.Center, 0.63f, 0.73f);

            text = this.Match.Teams.Away != null ? this.Match.Teams.Away.Country.Names[this.settings.ShowCountryNamesInNativeLanguage] : "TBD";
            this.DrawText(e.Graphics, MatchRow.TeamFont, MyGUIs.Text[this.mouseIsOver].Brush, this.Match.TeamReferences.Away + ". " + text, HorizontalAlignment.Left, 0.73f, 1f);
        }
    }
}

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
    public class MatchDaysView
    {
        public List<MatchDayView> MatchDayViews { get; private set; }

        public MatchDaysView(Panel parent, ListOfIDObjects<Match> matches, EventHandler matchDayClick_EventHandler, Settings settings)
        {
            this.MatchDayViews = new List<MatchDayView>();
            DateTime firstMatchWeekMonday = new DateTime(2016, 6, 6);

            List<MyButton> dows = MyEuroBaseControl.CreateControlCollection<MyButton>(new string[] { "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun" }, null, "dow",
                new List<Tuple<string, object>>() { new Tuple<string, object>("Cursor", Cursors.Default), 
                    new Tuple<string, object>("Font", new Font(StaticData.PVC.Families[StaticData.FontExoBold_Index], 24, FontStyle.Bold)) });

            for (int iCol = 0, width = parent.Width / 7, height = parent.Height / 6; iCol < 7; iCol++)
            {
                dows[iCol].Parent = parent;
                dows[iCol].SetBounds(iCol * width, 0, width, height);

                for (int iRow = 0; iRow < 5; iRow++)
                {
                    MatchDayView mdv = new MatchDayView();
                    mdv.Parent = parent;
                    mdv.Settings = settings;
                    mdv.Date = firstMatchWeekMonday.AddDays(iRow * 7 + iCol).Date;
                    mdv.Matches = matches.GetMatchesBy(mdv.Date);
                    mdv.SetBounds(iCol * width, (iRow + 1) * height, width, height);
                    mdv.Click += matchDayClick_EventHandler;
                    this.MatchDayViews.Add(mdv);
                }
            }
        }
    }

    public class MatchDayView : MyEuroBaseControl
    {
        public const int DefaultHeight = 90;

        private DateTime date;
        public DateTime Date
        {
            get { return this.date; }
            set { this.date = value; this.Invalidate(); }
        }

        private ListOfIDObjects<Match> matches;
        public ListOfIDObjects<Match> Matches
        {
            get { return this.matches; }
            set { this.matches = value; this.Invalidate(); }
        }

        private Settings settings;
        public Settings Settings
        {
            get { return this.settings; }
            set { this.settings = value; this.Invalidate(); }
        }

        private Font matchCountFont;

        public MatchDayView()
            : base()
        {
            this.Size = new Size(120, MatchDayView.DefaultHeight);
            this.Font = StaticData.PVC != null ? new Font(StaticData.PVC.Families[StaticData.FontExoBold_Index], 24, FontStyle.Bold) : new Font("Arial", 24, FontStyle.Bold);
            this.matchCountFont = StaticData.PVC != null ? new Font(StaticData.PVC.Families[StaticData.FontExo_Index], 9, FontStyle.Regular) : new Font("Arial", 9, FontStyle.Regular);
            this.Cursor = Cursors.Hand;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.CompositingQuality = CompositingQuality.AssumeLinear;
            e.Graphics.TextRenderingHint = TextRenderingHint.AntiAlias;

            if (this.date == null || this.matches == null)
                return;

            if (this.date.Equals(/*new DateTime(2016, 06, 29)*/ DateTime.Now.Date))
                e.Graphics.DrawRectangle(MyGUIs.Accent.Highlighted.Pen, 2, 2, this.Width - 5, this.Height - 5);

            this.Text = this.date.Day.ToString();
            SizeF size = e.Graphics.MeasureString(this.Text, this.Font);
            e.Graphics.DrawString(this.Text, this.Font, this.isChecked ? MyGUIs.Accent.Highlighted.Brush : MyGUIs.Text.GetValue(this.mouseIsClicked).Brush,
                new PointF(this.Width / 2 - size.Width / 2, this.Height / 2 - size.Height / 2));

            if (this.matches.Count > 0)
            {
                string text = this.matches.Count + (this.matches.Count > 1 ? " matches" : " match");
                size = e.Graphics.MeasureString(text, this.matchCountFont);
                float left = this.Width / 2f - size.Width / 2f;
                if (this.matches.FirstOrDefault(m => m.Teams.Home.Equals(this.settings.FavoriteTeam) || m.Teams.Away.Equals(this.settings.FavoriteTeam)) != null)
                {
                    left -= this.settings.FavoriteTeam.Country.Flag20px.Width / 2f;
                    e.Graphics.DrawImage(this.settings.FavoriteTeam.Country.Flag20px, left, this.Height - this.settings.FavoriteTeam.Country.Flag20px.Height - 8);
                    left += this.settings.FavoriteTeam.Country.Flag20px.Width + 4f;
                }
                e.Graphics.DrawString(text, this.matchCountFont, MyGUIs.Text.Normal.Brush, left, this.Height - size.Height - 8);
            }
        }
    }
}

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
    public class TeamStatsViews
    {
        protected EventHandler onTeamStatsViewMouseEnterEH;
        protected EventHandler onTeamStatsClickEH;

        internal TeamStatsViewHeader header;
        protected TeamStatsViewRow[] rows;

        internal MyScrollPanel myScrollPanel;
        protected Database database;

        public TeamStatsViews(Panel parent, FTeamStats teamStatsForm, EventHandler onTeamStatsMouseEnterEH, EventHandler onTeamStatsClickEH, Database database)
        {
            this.onTeamStatsViewMouseEnterEH = onTeamStatsMouseEnterEH;
            this.onTeamStatsClickEH = onTeamStatsClickEH;

            this.myScrollPanel = new MyScrollPanel(parent, MyScrollBar.ScrollBarPosition.Right, 2, 80);
            this.myScrollPanel.UpdatePanelSize();

            this.header = new TeamStatsViewHeader(teamStatsForm);
            this.header.Size = new Size(this.myScrollPanel.VisibleSize.Width, TeamStatsViewBase.DefaultHeight + 12);
            this.myScrollPanel.AddControl(this.header, Point.Empty, false);
            this.rows = new TeamStatsViewRow[0];

            this.database = database;
        }

        public void SetTeams(List<Team> teams, int sortByColumn, bool descending)
        {
            teams.SortTeams(this.database, sortByColumn, descending);

            if (this.rows.Length > teams.Count)
                for (int i = teams.Count; i < this.rows.Length; i++)
                    this.rows[i].Hide();
            else if (this.rows.Length < teams.Count)
            {
                TeamStatsViewRow[] temp = new TeamStatsViewRow[teams.Count];
                Array.Copy(this.rows, temp, this.rows.Length);
                for (int i = this.rows.Length; i < teams.Count; i++)
                {
                    temp[i] = new TeamStatsViewRow(i + 1, this.database);
                    temp[i].Size = new Size(this.myScrollPanel.VisibleSize.Width, TeamStatsViewBase.DefaultHeight);
                    temp[i].MouseEnter += this.onTeamStatsViewMouseEnterEH;
                    temp[i].Click += this.onTeamStatsClickEH;
                    this.myScrollPanel.AddControl(temp[i], new Point(0, (i + 1) * TeamStatsViewBase.DefaultHeight + 12), false);
                }
                this.rows = temp;
                temp = null;
            }

            for (int i = 0; i < teams.Count; i++)
            {
                this.rows[i].Show();
                this.rows[i].TeamProperty = teams[i];
            }
            this.myScrollPanel.UpdatePanelSize();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class TeamStatsViewBase : MyEuroBaseControl
    {
        protected static readonly double[] ColumnWidths = { 0.05, 0.04, 0.13, 0.12, 0.07, 0.07, 0.1, 0.09, 0.09, 0.07, 0.07, 0.1 };
        protected static readonly string[] ColumnCaptions = { "No.", "", "Team", "Age", "Time", "Duration", "Goals", "Attendance", "Phase", "Clubs", "Leagues", "Home play" };
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
    public class TeamStatsViewHeader : TeamStatsViewBase
    {
        public int SortByColumn { get; protected set; }
        public bool Descending { get; protected set; }
        protected Point cursorLocation = Point.Empty;
        protected FTeamStats teamStatsForm;

        public TeamStatsViewHeader(FTeamStats teamStatsForm)
            : base()
        {
            this.Font = StaticData.PVC != null ? new Font(StaticData.PVC.Families[StaticData.FontExoBold_Index], 16, FontStyle.Bold) : new Font("Arial", 12, FontStyle.Regular);
            this.Size = new Size(500, TeamStatsViewBase.DefaultHeight);
            this.Cursor = Cursors.Hand;
            this.teamStatsForm = teamStatsForm;
            this.SortByColumn = 2;
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
            for (column = 0, totalWidth = 0.0; column < TeamStatsViewBase.ColumnWidths.Length; column++)
            {
                totalWidth += TeamStatsViewBase.ColumnWidths[column];
                if (p < totalWidth && !TeamStatsViewBase.ColumnCaptions[column].Equals(""))
                    break;
            }
            if (column == 0)
                return;
            if (column == this.SortByColumn)
                this.Descending = !this.Descending;
            this.SortByColumn = column;
            this.Invalidate();
            this.teamStatsForm.RefreshInformation(null);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.Clear(MyGUIs.Background.Normal.Color);

            double lastLeft = 0.0;
            for (int iCol = 0; iCol < TeamStatsViewBase.ColumnWidths.Length; iCol++)
            {
                double width = iCol > 1 && TeamStatsViewBase.ColumnCaptions[iCol - 1].Equals("") ? TeamStatsViewBase.ColumnWidths[iCol] + TeamStatsViewBase.ColumnWidths[iCol - 1] : TeamStatsViewBase.ColumnWidths[iCol];
                if (!TeamStatsViewBase.ColumnCaptions[iCol].Equals(""))
                {
                    string text = TeamStatsViewBase.ColumnCaptions[iCol] + (this.SortByColumn == iCol ? (this.Descending ? "▼" : "▲") : "");
                    this.DrawTextCell(e.Graphics, this.Font, text, HorizontalAlignment.Center, lastLeft, lastLeft + width);
                    lastLeft += width;
                }
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class TeamStatsViewRow : TeamStatsViewBase
    {
        protected Font FontBold;

        private Team team = null;
        public Team TeamProperty
        {
            get { return this.team; }
            set { this.team = value; this.Invalidate(); }
        }

        private int position;
        private Database database;

        public TeamStatsViewRow(int position, Database database)
        {
            this.position = position;
            this.database = database;
            this.Font = StaticData.PVC != null ? new Font(StaticData.PVC.Families[StaticData.FontExo_Index], 13, FontStyle.Regular) : new Font("Arial", 11, FontStyle.Regular);
            this.FontBold = StaticData.PVC != null ? new Font(StaticData.PVC.Families[StaticData.FontExoBold_Index], 13, FontStyle.Bold) : new Font("Arial", 11, FontStyle.Regular);
            this.Cursor = Cursors.Hand;
            this.Size = new Size(500, TeamStatsViewBase.DefaultHeight);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (this.team == null)
                return;

            ListOfIDObjects<Match> teamMatches = this.database.Matches.GetMatchesBy(this.team);
            ListOfIDObjects<Match> playedTeamMatches = teamMatches.GetMatchesBy(true);
            MatchScoreboard matchesScoreboard = playedTeamMatches.GetAllGoals(this.team);
            TimeSpan averageStartTime = playedTeamMatches.GetAverageStartTime(), averageDuration = playedTeamMatches.GetAverageDuration();
            Tuple<int, int> clubsAndLeagues = this.team.GetClubAndLeagueCount();

            double lastLeft = 0.0;
            this.DrawTextCell(e.Graphics, this.FontBold, this.position + ".", HorizontalAlignment.Center, lastLeft, TeamStatsViewBase.ColumnWidths[0]);
            lastLeft += TeamStatsViewBase.ColumnWidths[0];
            this.DrawImageCell(e.Graphics, this.team.Country.Flag20px, HorizontalAlignment.Center, lastLeft, lastLeft + TeamStatsViewBase.ColumnWidths[1]);
            lastLeft += TeamStatsViewBase.ColumnWidths[1];
            this.DrawTextCell(e.Graphics, this.FontBold, this.team.Country.Names[this.database.Settings.ShowCountryNamesInNativeLanguage], HorizontalAlignment.Left, lastLeft, lastLeft + TeamStatsViewBase.ColumnWidths[2]);
            lastLeft += TeamStatsViewBase.ColumnWidths[2];
            this.DrawTextCell(e.Graphics, this.Font, this.team.Players.GetAverageAge().FormatAge(true), HorizontalAlignment.Center, lastLeft, lastLeft + TeamStatsViewBase.ColumnWidths[3]);
            lastLeft += TeamStatsViewBase.ColumnWidths[3];
            this.DrawTextCell(e.Graphics, this.Font, averageStartTime.Ticks == 0 ? "-" : averageStartTime.ToString("h\\:mm"), HorizontalAlignment.Center, lastLeft, lastLeft + TeamStatsViewBase.ColumnWidths[4]);
            lastLeft += TeamStatsViewBase.ColumnWidths[4];
            this.DrawTextCell(e.Graphics, this.Font, averageDuration.Ticks == 0 ? "-" : averageDuration.TotalMinutes.ToString("N0") + " min", HorizontalAlignment.Center, lastLeft, lastLeft + TeamStatsViewBase.ColumnWidths[5]);
            lastLeft += TeamStatsViewBase.ColumnWidths[5];
            this.DrawTextCell(e.Graphics, this.FontBold, string.Format("{0}-{1} ({2}{3})", matchesScoreboard.FinalScoreWithoutPenalties.Home, matchesScoreboard.FinalScoreWithoutPenalties.Away, matchesScoreboard.FinalScoreWithoutPenalties.GoalDifference > 0 ? "+" : "", matchesScoreboard.FinalScoreWithoutPenalties.GoalDifference), HorizontalAlignment.Center, lastLeft, lastLeft + TeamStatsViewBase.ColumnWidths[6]);
            lastLeft += TeamStatsViewBase.ColumnWidths[6];
            this.DrawTextCell(e.Graphics, this.Font, "~" + Utils.FormatNumber(playedTeamMatches.GetAttendance()), HorizontalAlignment.Center, lastLeft, lastLeft + TeamStatsViewBase.ColumnWidths[7]);
            lastLeft += TeamStatsViewBase.ColumnWidths[7];
            this.DrawTextCell(e.Graphics, this.FontBold, Utils.FormatMatchCategory(this.database.TournamentResultOfTeam(this.team)), HorizontalAlignment.Center, lastLeft, lastLeft + TeamStatsViewBase.ColumnWidths[8]);
            lastLeft += TeamStatsViewBase.ColumnWidths[8];
            this.DrawTextCell(e.Graphics, this.Font, clubsAndLeagues.Item1.ToString(), HorizontalAlignment.Center, lastLeft, lastLeft + TeamStatsViewBase.ColumnWidths[9]);
            lastLeft += TeamStatsViewBase.ColumnWidths[9];
            this.DrawTextCell(e.Graphics, this.Font, clubsAndLeagues.Item2.ToString(), HorizontalAlignment.Center, lastLeft, lastLeft + TeamStatsViewBase.ColumnWidths[10]);
            lastLeft += TeamStatsViewBase.ColumnWidths[10];
            this.DrawTextCell(e.Graphics, this.Font, this.team.Players.Count(p => p.Club.Country.Equals(p.Nationality.Country)) + " / " + this.team.Players.Count, HorizontalAlignment.Center, lastLeft, lastLeft + TeamStatsViewBase.ColumnWidths[11]);
        }
    }
}

using Euro2016.VisualComponents;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Euro2016
{
    public partial class FMatch : MyForm
    {
        internal FMain mainForm;
        internal MatchesView matchesView;
        private Match lastMatch;

        public FMatch(FMain mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            this.Owner = mainForm;
        }

        private void FMatch_Load(object sender, EventArgs e)
        {
            this.matchesView = new MatchesView(matchesP, this.mainForm.MatchHeader_Click, this.MatchRow_Click, this.mainForm.Database.Settings);
            this.MouseWheel += this.matchesView.myScrollPanel.MouseWheelScroll_EventHandler;
            this.matchesView.SetMatches(this.mainForm.Database.Matches);

            phaseL.ForeColor = MyGUIs.Text.Highlighted.Color;

            phaseL.Font = new Font(StaticData.PVC.Families[StaticData.FontExoBold_Index], 18, FontStyle.Bold);
            whereL.Font = new Font(StaticData.PVC.Families[StaticData.FontExo_Index], 13, FontStyle.Regular);
            whenL.Font = new Font(StaticData.PVC.Families[StaticData.FontExoBold_Index], 13, FontStyle.Bold);
            homeTeamL.Font = new Font(StaticData.PVC.Families[StaticData.FontExoBold_Index], 23, FontStyle.Bold);
            awayTeamL.Font = new Font(StaticData.PVC.Families[StaticData.FontExoBold_Index], 23, FontStyle.Bold);
            homeNicknameL.Font = new Font(StaticData.PVC.Families[StaticData.FontExo_Index], 12, FontStyle.Regular);
            awayNicknameL.Font = new Font(StaticData.PVC.Families[StaticData.FontExo_Index], 12, FontStyle.Regular);
            scoreL.Font = new Font(StaticData.PVC.Families[StaticData.FontExoBold_Index], 35, FontStyle.Bold);
            halvesL.Font = new Font(StaticData.PVC.Families[StaticData.FontExo_Index], 10, FontStyle.Regular);

            this.RegisterControlsToMoveForm(this.titleLabel1);
        }

        private void RefreshTeamInfo(string teamReference, Team team, PictureBox flagPB, Label nameL, Label nicknameL)
        {
            if (team != null)
            {
                flagPB.Image = team.Country.Flag100px;
                nameL.Text = team.Country.Names[this.mainForm.Database.Settings.ShowCountryNamesInNativeLanguage];
                nicknameL.Text = team.Nicknames[Utils.Random.Next(team.Nicknames.Count)];
            }
            else
            {
                flagPB.Image = Utils.ScaleImage(StaticData.Images[Paths.UnknownTeamImageFile], flagPB.Width, flagPB.Height, System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic, false);
                nameL.Text = "TBD";
                nicknameL.Text = teamReference;
            }
        }

        private void MatchRow_Click(object sender, EventArgs e)
        {
            Match match = sender is MatchRow ? (sender as MatchRow).Match : sender as Match;
            this.lastMatch = match;
            phaseL.Text = match.FormatCategory;
            whenL.Text = match.When.ToString("dddd, d MMMM yyyy, 'at' HH:mm");
            whereL.Text = match.Where.Name + ", " + match.Where.City;
            this.RefreshTeamInfo(match.TeamReferences.Home, match.Teams.Home, homeFlagPB, homeTeamL, homeNicknameL);
            this.RefreshTeamInfo(match.TeamReferences.Away, match.Teams.Away, awayFlagPB, awayTeamL, awayNicknameL);
            scoreL.Text = match.Scoreboard.FormatScore(false);
            halvesL.Text = match.Scoreboard.ScoreDescription(false);

            MatchRow row = sender is MatchRow ? sender as MatchRow : this.matchesView.GetRowByMatch(match);
            this.matchesView.myScrollPanel.ScrollToViewControl(row);
        }
        
        /// <summary>Refreshes the information for the given Match object.</summary>
        /// <param name="item">the Match object to display information for</param>
        public override void RefreshInformation(object item)
        {
            this.MatchRow_Click(item, null);
        }

        private void phaseL_Click(object sender, EventArgs e)
        {
            if (this.lastMatch.IsGroupMatch)
                this.mainForm.ShowForm<FGroup, Group>(this.mainForm.Database.Groups.GetItemByID(this.lastMatch.Category.Substring(this.lastMatch.Category.IndexOf(':') + 1)));
            else
                this.mainForm.ShowForm<FKnockOut, ListOfIDObjects<Match>>(this.mainForm.Database.Matches.GetMatchesBy("KO"));
        }

        private void whereL_Click(object sender, EventArgs e)
        {
            this.mainForm.ShowForm<FVenue, Venue>(this.lastMatch.Where);
        }

        private void whenL_Click(object sender, EventArgs e)
        {
            this.mainForm.ShowForm<FMatchDays, DateTime>(this.lastMatch.When.Date);
        }

        private void homeFlagPB_Click(object sender, EventArgs e)
        {
            if (this.lastMatch.Teams.Home != null)
                this.mainForm.ShowForm<FTeam, Team>(this.lastMatch.Teams.Home);
        }

        private void awayFlagPB_Click(object sender, EventArgs e)
        {
            if (this.lastMatch.Teams.Away != null)
                this.mainForm.ShowForm<FTeam, Team>(this.lastMatch.Teams.Away);
        }

        private void editB_Click(object sender, EventArgs e)
        {
            if (this.lastMatch.Teams.Home == null || this.lastMatch.Teams.Away == null)
            {
                MessageBox.Show("Both teams of a match must be known in order to edit the match.", "Match editor warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            new FMatchEditor(this.lastMatch).ShowDialog(this);
        }
    }
}

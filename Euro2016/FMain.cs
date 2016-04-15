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
    public partial class FMain : MyForm
    {
        internal const int ControlPadding = 16, MatchesPanelWidth = 480, MenuPanelHeight = 60, GroupPanelWidth = 520;

        public Database Database { get; private set; }
        protected GroupView[] GroupViews { get; private set; }
        protected MatchesView MatchesView { get; private set; }
        internal KeyValuePair<MyForm, object> OpenFormAndItem { get; set; }

        public FMain(Database database)
        {
            InitializeComponent();
            this.Database = database;
        }

        private void FMain_Resize(object sender, EventArgs e)
        {
            matchesP.SetBounds(ControlPadding, ControlPadding, MatchesPanelWidth, this.Height - 3 * ControlPadding - MenuPanelHeight);
            logoPB.SetBounds(matchesP.Right + ControlPadding, matchesP.Top, this.Width - 5 * ControlPadding - MatchesPanelWidth - 1 * GroupPanelWidth, matchesP.Height);
            for (int index = 0, panelHeight = (this.Height - menuP.Height - 7 * ControlPadding) / 6; index < 6; index++)
                (this.Controls.Find("group" + ((char) (65 + index)) + "P", true)[0] as Panel).SetBounds(logoPB.Right + ControlPadding, index * (ControlPadding + panelHeight), GroupPanelWidth, panelHeight);
            menuP.SetBounds(matchesP.Left, matchesP.Bottom + ControlPadding, this.Width - 2 * ControlPadding, MenuPanelHeight);
        }

        private void FMain_Load(object sender, EventArgs e)
        {
            this.MatchesView = new MatchesView(matchesP, this.MatchHeader_Click, this.MatchRow_Click, this.Database.Settings);
            this.MouseWheel += this.MatchesView.myScrollPanel.MouseWheelScroll_EventHandler;
            this.MatchesView.SetMatches(this.Database.Matches);
            logoPB.Image = Utils.ScaleImage(StaticData.Images[Paths.LogoImageFile], logoPB.Width, logoPB.Height, System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic, false);
            this.GroupViews = new GroupView[6];
            for (int index = 0; index < 6; index++)
            {
                this.GroupViews[index] = new GroupView(this.Controls.Find("group" + ((char) (65 + index)) + "P", true)[0] as Panel, false, this.GroupHeader_Click, this.GroupRow_Click, this.Database.Settings);
                this.GroupViews[index].SetGroup(this.Database.Groups.GetItemByID(((char) (65 + index)).ToString()));
            }
            Utils.SizeAndPositionControlsInPanel(menuP, new Control[] { venuesB, teamsB, groupsB, matchesB, knockoutB, settingsB, moreB, exitB }, true, 0);

            /*this.Database.Clubs.Add(new Club("XXX", "DummyClub", this.Database.Countries.GetItemByID("XXX")));
            for (int iT = 0; iT < this.Database.Teams.Count; iT++)
                for (int iP = 0; iP < 23; iP++)
                    this.Database.Players.Add(new Player((23 * iT + iP + 1).ToString(), "Dummy Player", iP + 1, (Player.PlayingPosition) (iP / 6), DateTime.Now,
                        Utils.Random.Next(100), Utils.Random.Next(20), this.Database.Teams[iT].Country, this.Database.Clubs.GetItemByID("XXX")));*/

            logoPB_Click(null, null);
        }

        private void FMain_Shown(object sender, EventArgs e)
        {
            if (this.Database.Settings.ShowKnockoutStageOnStartup && this.Database.Matches.Count(m => m.IsGroupMatch && !m.Scoreboard.Played) == 0)
                this.knockoutB_Click(sender, e);
        }

        public override void RefreshInformation(object item)
        {
            this.MatchesView.SetMatches(this.Database.Matches);
            foreach (GroupView view in this.GroupViews)
                view.SetGroup(view.Group);
        }

        public void ShowForm<FORM_TYPE, OBJECT_TYPE>(OBJECT_TYPE forItem, bool closeIfFormTypeIsTheSame = false) where FORM_TYPE : MyForm
        {
            // note: remember to set this.OpenFormAndItem to null from FSettings, FMore itself and anything created directly or indirectly from FMore

            // if the form exists and it should be closed if its type is the same as the given type (that is, if the command is passed from the main form menu buttons), close it and be done with it
            if (closeIfFormTypeIsTheSame && this.OpenFormAndItem.Key != null && this.OpenFormAndItem.Key is FORM_TYPE)
                if (this.OpenFormAndItem.Key != null)
                {
                    this.OpenFormAndItem.Key.Close();
                    this.OpenFormAndItem = new KeyValuePair<MyForm, object>(null, null);
                    return;
                }

            // if a form different from what we want exists, close it
            if (this.OpenFormAndItem.Key != null && !(this.OpenFormAndItem.Key is FORM_TYPE))
            {
                this.OpenFormAndItem.Key.Close();
                this.OpenFormAndItem = new KeyValuePair<MyForm, object>(null, null);
            }

            // if we don't have it open, we should create and open the form
            if (this.OpenFormAndItem.Key == null)
            {
                MyForm form = (FORM_TYPE) typeof(FORM_TYPE).GetConstructor(new Type[] { typeof(FMain) }).Invoke(new object[] { this });
                form.Owner = this;
                form.Show();
                form.Focus();
                this.OpenFormAndItem = new KeyValuePair<MyForm, object>(form, forItem);
            }

            // refresh the form with the given item
            this.OpenFormAndItem.Key.RefreshInformation(forItem);
        }

        public void venuesB_Click(object sender, EventArgs e)
        {
            this.ShowForm<FVenue, Venue>(this.Database.Venues.GetItemByID(this.Database.Matches.GetMatchesBy("KO:1")[0].Where.ID), true);
        }

        public void teamsB_Click(object sender, EventArgs e)
        {
            this.ShowForm<FTeam, Team>(this.Database.Settings.FavoriteTeam, true);
        }

        private void groupsB_Click(object sender, EventArgs e)
        {
            this.ShowForm<FGroup, Group>(this.Database.Groups.First(group => group.TableLines.FirstOrDefault(tableLine => tableLine.Team.Equals(this.Database.Settings.FavoriteTeam)) != null), true);
        }

        private void matchesB_Click(object sender, EventArgs e)
        {
            Match firstUnplayedMatch = this.Database.Matches.FirstOrDefault(match => !match.Scoreboard.Played);
            this.ShowForm<FMatch, Match>(firstUnplayedMatch != null ? firstUnplayedMatch : this.Database.Matches.GetMatchesBy("KO:1")[0], true);
        }

        private void knockoutB_Click(object sender, EventArgs e)
        {
            this.ShowForm<FKnockOut, ListOfIDObjects<Match>>(this.Database.Matches.GetMatchesBy("KO"), true);
        }

        private void settingsB_Click(object sender, EventArgs e)
        {
            this.ShowForm<FSettings, Settings>(this.Database.Settings, true);
        }

        private void moreB_Click(object sender, EventArgs e)
        {
            this.ShowForm<FMore, MyButton>(null, true);
        }

        public void MatchHeader_Click(object sender, EventArgs e)
        {
            this.ShowForm<FMatchDays, DateTime>((sender as MatchHeader).Date);
        }

        public void MatchRow_Click(object sender, EventArgs e)
        {
            this.ShowForm<FMatch, Match>((sender as MatchRow).Match);
        }

        public void GroupHeader_Click(object sender, EventArgs e)
        {
            this.ShowForm<FGroup, Group>((sender as GroupButton).Group);
        }

        public void GroupRow_Click(object sender, EventArgs e)
        {
            this.ShowForm<FTeam, Team>((sender as GroupRow).TableLine.Team);
        }

        private void logoPB_Click(object sender, EventArgs e)
        {
            this.ShowForm<FMap, object>(null, true);
        }

        private void exitB_Click(object sender, EventArgs e)
        {
            if (this.OpenFormAndItem.Key != null)
                this.OpenFormAndItem.Key.Close();
            StaticData.PVC.Dispose();
            string saveResult = this.Database.SaveDatabase(Paths.DatabaseFile, Paths.DatabasePlayersFile);
            if (!saveResult.Equals(""))
                MessageBox.Show(saveResult, "Database save ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Application.Exit();
        }
    }
}

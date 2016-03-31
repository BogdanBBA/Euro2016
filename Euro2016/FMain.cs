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
        internal MyForm OpenForm { get; set; }

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

        public void ShowForm<FORM_TYPE, OBJECT_TYPE>(object forItem) where FORM_TYPE : MyForm
        {
            if (forItem is OBJECT_TYPE)
            {
                if (this.OpenForm != null)
                {
                    this.OpenForm.Close();
                    if (this.OpenForm is FORM_TYPE)
                    {
                        this.OpenForm = null;
                        return;
                    }
                }
                this.OpenForm = (FORM_TYPE) typeof(FORM_TYPE).GetConstructor(new Type[] { typeof(FMain) }).Invoke(new object[] { this });
            }
            this.OpenForm.Show();
            this.OpenForm.Focus();
            this.OpenForm.RefreshInformation(forItem);
        }

        public void venuesB_Click(object sender, EventArgs e)
        {
            this.ShowForm<FVenue, Venue>(this.Database.Venues.GetItemByID(this.Database.Matches.GetMatchesBy("KO:1")[0].Where.ID));
        }

        public void teamsB_Click(object sender, EventArgs e)
        {
            this.ShowForm<FTeam, Team>(this.Database.Settings.FavoriteTeam);
        }

        private void groupsB_Click(object sender, EventArgs e)
        {
            this.ShowForm<FGroup, Group>(this.Database.Groups.First(group => group.TableLines.FirstOrDefault(tableLine => tableLine.Team.Equals(this.Database.Settings.FavoriteTeam)) != null));
        }

        private void matchesB_Click(object sender, EventArgs e)
        {
            Match firstUnplayedMatch = this.Database.Matches.FirstOrDefault(match => !match.Scoreboard.Played);
            this.ShowForm<FMatch, Match>(firstUnplayedMatch != null ? firstUnplayedMatch : this.Database.Matches.GetMatchesBy("KO:1")[0]);
        }

        private void knockoutB_Click(object sender, EventArgs e)
        {
            this.ShowForm<FKnockOut, ListOfIDObjects<Match>>(this.Database.Matches.GetMatchesBy("KO"));
        }

        private void settingsB_Click(object sender, EventArgs e)
        {
            this.ShowForm<FSettings, Settings>(this.Database.Settings);
        }

        private void moreB_Click(object sender, EventArgs e)
        {
            this.ShowForm<FMore, MyButton>(sender);
        }

        public void MatchHeader_Click(object sender, EventArgs e)
        {
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

        private void exitB_Click(object sender, EventArgs e)
        {
            if (this.OpenForm != null)
                this.OpenForm.Close();
            StaticData.PVC.Dispose();
            string saveResult = this.Database.SaveDatabase(Paths.DatabaseFile);
            if (!saveResult.Equals(""))
                MessageBox.Show(saveResult, "Database save ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Application.Exit();
        }
    }
}

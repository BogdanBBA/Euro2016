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
    public partial class FTeam : MyForm
    {
        private const string TeamButtonNamePrefix = "teamCV";

        private FMain mainForm;
        private List<CountryView> countryButtons;
        private MatchesView matchesView;
        private Team team;

        public FTeam(FMain mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            this.Owner = mainForm;
        }

        private void FTeam_Load(object sender, EventArgs e)
        {
            string[] teamNames = new string[this.mainForm.Database.Teams.Count];
            for (int index = 0; index < this.mainForm.Database.Teams.Count; index++)
                teamNames[index] = this.mainForm.Database.Teams[index].Country.Names[this.mainForm.Database.Settings.ShowCountryNamesInNativeLanguage];
            this.countryButtons = MyEuroBaseControl.CreateControlCollection<CountryView>(teamNames, this.VenueButton_Click, TeamButtonNamePrefix,
                new Tuple<string, object>[] { new Tuple<string, object>("Settings", this.mainForm.Database.Settings) });
            for (int index = 0; index < this.countryButtons.Count; index++)
                this.countryButtons[index].Team = this.mainForm.Database.Teams[index];
            Utils.SizeAndPositionControlsInPanel(teamButtonP, this.countryButtons, false, 0);
            this.matchesView = new MatchesView(this.matchesP, this.mainForm.MatchHeader_Click, this.mainForm.MatchRow_Click, this.mainForm.Database.Settings);
            this.MouseWheel += this.matchesView.myScrollPanel.MouseWheelScroll_EventHandler;
            this.RegisterControlsToMoveForm(this.titleLabel1);
        }

        private void infoViewDetail3_Click(object sender, EventArgs e)
        {
            this.mainForm.ShowForm<FPlayers, Country>(this.team.Country);
        }

        private void VenueButton_Click(object sender, EventArgs e)
        {
            this.RefreshInformation(this.mainForm.Database.Teams.First(t => t.Country.Equals(sender is CountryView ? (sender as CountryView).Country : (sender as Team).Country)));
        }

        public override void RefreshInformation(object item)
        {
            Team team = item as Team;
            this.team = team;
            this.countryButtons.CheckItemAndUncheckAllOthers<CountryView>(this.countryButtons.First(cb => cb.Country.Equals(team.Country)));
            this.flagPB.Image = team.Country.Flag100px;
            teamNameHomeIV.TextText = team.Country.Names.Home;
            teamNameAwayIV.TextText = team.Country.Names.Away;
            ListOfIDObjects<Match> matches = this.mainForm.Database.Matches.GetMatchesBy(team);
            MatchScoreboard matchesScoreboard = matches.GetAllGoals(team);
            infoViewDetail1.TextText = string.Format("{0}-{1}", matchesScoreboard.FinalScoreWithoutPenalties.Home, matchesScoreboard.FinalScoreWithoutPenalties.Away);
            infoViewDetail2.TextText = string.Format("{0:N2}-{1:N2}", (double) matchesScoreboard.FinalScoreWithoutPenalties.Home / matches.Count, (double) matchesScoreboard.FinalScoreWithoutPenalties.Away / matches.Count);
            infoViewDetail3.TextText = team.Players.Count + " players";
            this.matchesView.SetMatches(matches);
        }
    }
}

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

        public FTeam(FMain mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            this.Owner = mainForm;

            this.countryNameNativeL.ForeColor = MyGUIs.Text.Highlighted.Color;
            this.countryNameEnglishL.ForeColor = MyGUIs.Text.Highlighted.Color;
        }

        private void FTeam_Load(object sender, EventArgs e)
        {
            string[] teamNames = new string[this.mainForm.Database.Teams.Count];
            for (int index = 0; index < this.mainForm.Database.Teams.Count; index++)
                teamNames[index] = this.mainForm.Database.Teams[index].Country.Names[this.mainForm.Database.Settings.ShowCountryNamesInNativeLanguage];
            this.countryButtons = MyEuroBaseControl.CreateControlCollection<CountryView>(teamNames, this.VenueButton_Click, TeamButtonNamePrefix, null);
            for (int index = 0; index < this.countryButtons.Count; index++)
                this.countryButtons[index].Team = this.mainForm.Database.Teams[index];
            Utils.SizeAndPositionControlsInPanel(teamButtonP, this.countryButtons, false, 0);
            this.matchesView = new MatchesView(this.matchesP, this.mainForm.MatchHeader_Click, this.mainForm.MatchRow_Click, this.mainForm.Database.Settings);
            this.MouseWheel += this.matchesView.myScrollPanel.MouseWheelScroll_EventHandler;
        }

        private void VenueButton_Click(object sender, EventArgs e)
        {
            this.RefreshInformation(this.mainForm.Database.Teams.First(t => t.Country.Equals(sender is CountryView ? (sender as CountryView).Country : (sender as Team).Country)));
        }

        public override void RefreshInformation(object item)
        {
            Team team = item as Team;
            this.countryButtons.CheckItemAndUncheckAllOthers<CountryView>(this.countryButtons.First(cb => cb.Country.Equals(team.Country)));
            this.flagPB.Image = team.Country.Flag100px;
            countryNameNativeL.Text = team.Country.Names.Home;
            countryNameEnglishL.Text = team.Country.Names.Away;
            goalsL.Text = goalsAverageL.Text = label6.Text = "n/a";
            this.matchesView.SetMatches(this.mainForm.Database.Matches.GetMatchesBy(team));
        }
    }
}

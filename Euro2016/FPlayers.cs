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
    public partial class FPlayers : MyForm
    {
        private const string CountryViewControlPrefix = "countryCV_";

        private FMain mainForm;
        private MyScrollPanel countryMSP;
        private List<CountryView> countryViews;
        private PlayersView playersView;

        public FPlayers(FMain mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void FPlayers_Load(object sender, EventArgs e)
        {
            this.countryMSP = new MyScrollPanel(this.countriesP, MyScrollBar.ScrollBarPosition.Right, 2, 80);
            this.MouseWheel += this.countryMSP.MouseWheelScroll_EventHandler;
            string[] countryNames = new string[this.mainForm.Database.Countries.Count];
            for (int index = 0; index < this.mainForm.Database.Countries.Count; index++)
                countryNames[index] = this.mainForm.Database.Countries[index].Names[this.mainForm.Database.Settings.ShowCountryNamesInNativeLanguage];
            this.countryViews = MyEuroBaseControl.CreateControlCollection<CountryView>(countryNames, this.CountryView_Click, CountryViewControlPrefix,
                new List<Tuple<string, object>>() { new Tuple<string, object>("Size", new Size(this.countryMSP.VisibleSize.Width, CountryView.DefaultHeight)), 
                    new Tuple<string, object>("Settings", this.mainForm.Database.Settings) });
            for (int index = 0; index < this.mainForm.Database.Countries.Count; index++)
            {
                this.countryViews[index].Country = this.mainForm.Database.Countries[index];
                this.countryViews[index].MouseEnter += this.CountryView_MouseEnter;
                this.countryMSP.AddControl(this.countryViews[index], new Point(0, index * CountryView.DefaultHeight), false);
            }
            this.countryMSP.UpdatePanelSize();
            this.playersView = new PlayersView(playerP, this.PlayerViewRow_MouseEnter, this.PlayerView_Click, this.mainForm.Database.Settings);
            this.RegisterControlsToMoveForm(this.titleLabel1);
        }

        private void CountryView_MouseEnter(object sender, EventArgs e)
        {
            this.RemoveAllClickEvents();
            this.MouseWheel += this.countryMSP.MouseWheelScroll_EventHandler;
        }

        private void PlayerViewRow_MouseEnter(object sender, EventArgs e)
        {
            this.RemoveAllClickEvents();
            this.MouseWheel += this.playersView.myScrollPanel.MouseWheelScroll_EventHandler;
        }

        private void CountryView_Click(object sender, EventArgs e)
        {
            this.RefreshInformation((sender as CountryView).Country);
        }

        private void PlayerView_Click(object sender, EventArgs e)
        {
            //
        }

        public override void RefreshInformation(object item)
        {
            Country country = item as Country;
            if (country == null)
                return;
            this.countryViews.CheckItemAndUncheckAllOthers<CountryView>(this.countryViews.First(cv => cv.Country.Equals(country)));

            flagPB.Image = country.Flag100px;
            countryIV.TextText = country.Names[this.mainForm.Database.Settings.ShowCountryNamesInNativeLanguage];

            Team team = this.mainForm.Database.Teams.First(t => t.Country.Equals(country));
            coachFlagPB.Image = team.Coach.Key.Flag40px;
            coachIVD.TextText = team.Coach.Value;
            this.playersView.SetPlayers(team == null ? new ListOfIDObjects<Player>() : team.Players);
        }
    }
}

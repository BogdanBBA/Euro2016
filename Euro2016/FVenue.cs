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
    public partial class FVenue : MyForm
    {
        private const string VenueButtonNamePrefix = "venueB";

        private FMain mainForm;
        private List<MyButton> venueButtons;
        private MatchesView matchesView;

        public FVenue(FMain mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            this.Owner = mainForm;
        }

        private void FVenues_Load(object sender, EventArgs e)
        {
            string[] venueNames = new string[this.mainForm.Database.Venues.Count];
            for (int index = 0; index < this.mainForm.Database.Venues.Count; index++)
                venueNames[index] = this.mainForm.Database.Venues[index].City;
            this.venueButtons = MyEuroBaseControl.CreateControlCollection<MyButton>(venueNames, this.VenueButton_Click, VenueButtonNamePrefix, new Tuple<string, object>[] { new Tuple<string, object>("DrawBar", false) });
            Utils.SizeAndPositionControlsInPanel(venueButtonP, this.venueButtons, false, 0);
            this.matchesView = new MatchesView(this.matchesP, this.mainForm.MatchHeader_Click, this.mainForm.MatchRow_Click, this.mainForm.Database.Settings);
            this.MouseWheel += this.matchesView.myScrollPanel.MouseWheelScroll_EventHandler;
        }

        public override void RefreshInformation(object item)
        {
            Venue venue = item as Venue;
            this.venueButtons.CheckItemAndUncheckAllOthers<MyButton>(this.venueButtons.First(vb => vb.Text.Equals(venue.City)));
            titleL.Text = venue.Name;
            subtitleL.Text = venue.City + ", France";
            locationPB.Load(Paths.StadiumLocationsFolder + venue.ID + ".png");
            cityPB.Load(Paths.CitiesFolder + venue.ID + ".jpg");
            stadiumOutsidePB.Load(Paths.StadiumOutsidesFolder + venue.ID + ".jpg");
            stadiumInsidePB.Load(Paths.StadiumInsidesFolder + venue.ID + ".jpg");
            this.matchesView.SetMatches(this.mainForm.Database.Matches.GetMatchesBy(venue));
        }

        private void VenueButton_Click(object sender, EventArgs e)
        {
            this.RefreshInformation(this.mainForm.Database.Venues.First(v => v.City.Equals((sender as Control).Text)));
        }
    }
}

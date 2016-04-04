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
        private Venue lastVenue;

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
            this.RegisterControlsToMoveForm(this.titleLabel1);
        }

        public override void RefreshInformation(object item)
        {
            Venue venue = item as Venue;
            this.lastVenue = venue;
            this.venueButtons.CheckItemAndUncheckAllOthers<MyButton>(this.venueButtons.First(vb => vb.Text.Equals(venue.City)));
            venueNameIV.TextText = venue.Name;
            venueCityIV.TextText = venue.City + ", France";
            yearOpenedIVD.TextText = venue.YearOpened.ToString();
            capacityIVD.TextText = Utils.FormatNumber(venue.Capacity);
            geoCoordinatesIVD.TextText = string.Format("{0:N5}, {1:N5}", venue.Location.X, venue.Location.Y);
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

        private void venueNameIV_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"https://en.wikipedia.org/w/index.php?search=" + (sender as InfoView).TextText);
        }

        private void locationPB_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(string.Format(@"https://www.google.de/maps/@{0},{1},3000a,20y/data=!3m1!1e3?hl=en", this.lastVenue.Location.X, this.lastVenue.Location.Y));
        }
    }
}

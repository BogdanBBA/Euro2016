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
        private List<CountryView> countryViews;

        public FPlayers(FMain mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void FPlayers_Load(object sender, EventArgs e)
        {
            string[] countryNames = new string[this.mainForm.Database.Countries.Count];
            for (int index = 0; index < this.mainForm.Database.Countries.Count; index++)
                countryNames[index] = this.mainForm.Database.Countries[index].Names[this.mainForm.Database.Settings.ShowCountryNamesInNativeLanguage];
            this.countryViews = MyEuroBaseControl.CreateControlCollection<CountryView>(countryNames, this.CountryView_Click, CountryViewControlPrefix,
                new List<Tuple<string, object>>() { new Tuple<string, object>("Settings", this.mainForm.Database.Settings) });
            Utils.SizeAndPositionControlsInPanel<CountryView>(countriesP, this.countryViews, false, 0);
            for (int index = 0; index < this.mainForm.Database.Countries.Count; index++)
                this.countryViews[index].Country = this.mainForm.Database.Countries[index];
        }

        private void CountryView_Click(object sender, EventArgs e)
        {
            this.RefreshInformation((sender as CountryView).Country);
        }

        public override void RefreshInformation(object item)
        {
            Country country = item as Country;
            if (country == null)
                return;
            this.countryViews.CheckItemAndUncheckAllOthers<CountryView>(this.countryViews.First(cv => cv.Country.Equals(country)));

            flagPB.Image = country.Flag100px;
            countryIV.TextText = country.Names[this.mainForm.Database.Settings.ShowCountryNamesInNativeLanguage];
        }
    }
}

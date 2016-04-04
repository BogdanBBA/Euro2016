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
    public partial class FAbout : MyForm
    {
        private FMain mainForm;

        public FAbout(FMain mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void FAbout_Load(object sender, EventArgs e)
        {
            this.goTeamIV.TextText = this.mainForm.Database.Settings.FavoriteTeam.Country.Names[this.mainForm.Database.Settings.ShowCountryNamesInNativeLanguage];
            flagPB.Image = this.mainForm.Database.Settings.FavoriteTeam.Country.Flag100px;
            this.RegisterControlsToMoveForm(this.titleLabel1);
        }

        private void FAbout_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

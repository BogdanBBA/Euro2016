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
    public partial class FSettings : MyForm
    {
        private FMain mainForm;

        public FSettings(FMain mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            this.Owner = mainForm;
        }

        private string FormatTeamForCombobox(Team team)
        {
            return team.Country.ID + ". " + team.Country.Names.Away;
        }

        private Team ParseTeamFromCombobox(string text)
        {
            return this.mainForm.Database.Teams.First(t => t.Country.ID.Equals(text.Substring(0, text.IndexOf('.'))));
        }

        private void FSettings_Load(object sender, EventArgs e)
        {
            foreach (Team team in this.mainForm.Database.Teams)
                favoriteTeamCB.Items.Add(this.FormatTeamForCombobox(team));
            this.RegisterControlsToMoveForm(this.titleLabel1);
        }

        /// <summary>Refreshes the information for the main form database settings.</summary>
        /// <param name="item">no item needed, pass null</param>
        public override void RefreshInformation(object item)
        {
            Settings sett = this.mainForm.Database.Settings;
            favoriteTeamCB.SelectedIndex = favoriteTeamCB.Items.IndexOf(this.FormatTeamForCombobox(sett.FavoriteTeam));
            showCountryNamesInNativeLanguageChB.Checked = sett.ShowCountryNamesInNativeLanguage;
            showKnockoutPhaseOnStartupChB.Checked = sett.ShowKnockoutStageOnStartup;
            spamWithWinnerOnStartupChB.Checked = sett.SpamWithWinnerOnStartup;
            showFlagsOnMapChB.Checked = sett.ShowFlagsOnMap;
            timeOffsetNUD.Value = (decimal) sett.TimeOffset;
        }

        private void okB_Click(object sender, EventArgs e)
        {
            Settings sett = this.mainForm.Database.Settings;
            sett.FavoriteTeam = this.ParseTeamFromCombobox(favoriteTeamCB.Items[favoriteTeamCB.SelectedIndex] as string);
            sett.ShowCountryNamesInNativeLanguage = showCountryNamesInNativeLanguageChB.Checked;
            sett.ShowKnockoutStageOnStartup = showKnockoutPhaseOnStartupChB.Checked;
            sett.SpamWithWinnerOnStartup = spamWithWinnerOnStartupChB.Checked;
            sett.ShowFlagsOnMap = showFlagsOnMapChB.Checked;
            sett.TimeOffset = (double) timeOffsetNUD.Value;

            string saveResult = this.mainForm.Database.SaveDatabase(Paths.DatabaseFile);
            if (!saveResult.Equals(""))
                MessageBox.Show(saveResult, "Database save ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);

            this.mainForm.Database.Matches.SetTimeOffset(sett.TimeOffset);
            this.mainForm.RefreshInformation(null);
            this.mainForm.ShowForm<FSettings, object>(null, true);
        }
    }
}

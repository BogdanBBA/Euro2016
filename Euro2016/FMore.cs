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
    public partial class FMore : MyForm
    {
        private const string MenuButtonPrefix = "button";
        private const string CloseButtonLabel = "CLOSE";
        private static readonly string[] ButtonCaptions = { "Euro 2016 map", "Match days", "Players", "Reset matches", "Simulate results", "Open workspace", "About the app", CloseButtonLabel };

        private FMain mainForm;
        private List<MyButton> menuButtons;

        public FMore(FMain mainForm)
        {
            InitializeComponent();
            this.Size = new Size(424, 24 + FMore.ButtonCaptions.Length * 50);
            this.mainForm = mainForm;
            this.Owner = mainForm;
        }

        private void FMore_Load(object sender, EventArgs e)
        {
            buttonsP.SetBounds(12, 12, 400, FMore.ButtonCaptions.Length * 50);
            this.menuButtons = MyEuroBaseControl.CreateControlCollection<MyButton>(ButtonCaptions, this.MenuButton_Click, MenuButtonPrefix, null);
            Utils.SizeAndPositionControlsInPanel(buttonsP, this.menuButtons, false, 0);
        }

        private void MenuButton_Click(object sender, EventArgs e)
        {
            Database db = this.mainForm.Database;
            MyButton button = sender as MyButton;
            ListOfIDObjects<Match> playedMatches, unplayedMatches;
            string saveResult;
            switch (button.Text)
            {
                case "Euro 2016 map":
                    this.MenuButton_Click(this.menuButtons.First(mb => mb.Text.Equals(CloseButtonLabel)), null);
                    this.mainForm.ShowForm<FMap, object>(null);
                    break;

                case "Match days":
                    this.MenuButton_Click(this.menuButtons.First(mb => mb.Text.Equals(CloseButtonLabel)), null);
                    this.mainForm.ShowForm<FMatchDays, DateTime>(DateTime.Now.Date);
                    break;

                case "Players":
                    this.MenuButton_Click(this.menuButtons.First(mb => mb.Text.Equals(CloseButtonLabel)), null);
                    this.mainForm.ShowForm<FPlayers, Country>(this.mainForm.Database.Settings.FavoriteTeam.Country);
                    break;

                case "Reset matches":
                    this.MenuButton_Click(this.menuButtons.First(mb => mb.Text.Equals(CloseButtonLabel)), null);
                    playedMatches = db.Matches.GetMatchesBy(true);
                    if (playedMatches.Count == 0)
                        MessageBox.Show("The database matches are already reset (set as not played)!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        if (MessageBox.Show("Are you sure you want to reset the database matches? You'll then have to re-enter any real results manually or randomly simulate them again.",
                            "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        {
                            foreach (Match match in playedMatches)
                                match.Scoreboard.SetHalves(new List<HalfScoreboard>());
                            MessageBox.Show("Done!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            this.mainForm.Database.Calculate(true, true, true);
                            saveResult = this.mainForm.Database.SaveDatabase(Paths.DatabaseFile);
                            if (!saveResult.Equals(""))
                                MessageBox.Show(saveResult, "Database save ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.mainForm.RefreshInformation(null);
                        }
                    break;

                case "Simulate results":
                    this.MenuButton_Click(this.menuButtons.First(mb => mb.Text.Equals(CloseButtonLabel)), null);
                    unplayedMatches = db.Matches.GetMatchesBy(false);
                    if (unplayedMatches.Count == 0)
                        MessageBox.Show("There aren't any matches that have not been played! All good then.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        if (MessageBox.Show("This will simulate any played match randomly. Are you sure you want to do this?",
                            "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        {
                            foreach (Match match in unplayedMatches)
                                match.Scoreboard = Utils.GetRandomResult(match.IsGroupMatch);
                            MessageBox.Show("Done!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            this.mainForm.Database.Calculate(true, true, true);
                            saveResult = this.mainForm.Database.SaveDatabase(Paths.DatabaseFile);
                            if (!saveResult.Equals(""))
                                MessageBox.Show(saveResult, "Database save ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.mainForm.RefreshInformation(null);
                        }
                    break;

                case "Open workspace":
                    this.MenuButton_Click(this.menuButtons.First(mb => mb.Text.Equals(CloseButtonLabel)), null);
                    System.Diagnostics.Process.Start(Paths.ProgramFilesFolder);
                    break;

                case "About the app":
                    this.MenuButton_Click(this.menuButtons.First(mb => mb.Text.Equals(CloseButtonLabel)), null);
                    this.mainForm.ShowForm<FAbout, Country>(this.mainForm.Database.Settings.FavoriteTeam.Country);
                    break;

                case CloseButtonLabel:
                    this.mainForm.OpenFormAndItem = new KeyValuePair<MyForm, object>(null, null);
                    this.Close();
                    break;
            }
        }
    }
}

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
    public partial class FMatch : MyForm
    {
        private FMain mainForm;
        private MatchesView matchesView;

        public FMatch(FMain mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            this.Owner = mainForm;
        }

        private void FMatch_Load(object sender, EventArgs e)
        {
            this.matchesView = new MatchesView(matchesP, this.mainForm.MatchHeader_Click, this.MatchRow_Click, this.mainForm.Database.Settings);
            this.MouseWheel += this.matchesView.myScrollPanel.MouseWheelScroll_EventHandler;
            this.matchesView.SetMatches(this.mainForm.Database.Matches);
        }

        private void RefreshTeamInfo(Team team, PictureBox flagPB, Label nameL, Label nicknameL)
        {
            if (team != null)
            {
                flagPB.Image = team.Country.Flag100px;
                nameL.Text = team.Country.Names[this.mainForm.Database.Settings.ShowCountryNamesInNativeLanguage];
                nicknameL.Text = team.Nicknames[Utils.Random.Next(team.Nicknames.Count)];
            }
            else
            {
                flagPB.Image = Utils.ScaleImage(StaticData.Images[Paths.UnknownTeamImageFile], flagPB.Width, flagPB.Height, System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic, false);
                nameL.Text = "TBD";
                nicknameL.Text = "";
            }
        }

        private void MatchRow_Click(object sender, EventArgs e)
        {
            Match match = sender is MatchRow ? (sender as MatchRow).Match : sender as Match;
            phaseL.Text = match.FormatCategory;
            whenL.Text = match.When.ToString("dddd, d MMMM yyyy, 'at' HH:mm");
            whereL.Text = match.Where.Name + ", " + match.Where.City;
            this.RefreshTeamInfo(match.Teams.Home, homeFlagPB, homeTeamL, homeNicknameL);
            this.RefreshTeamInfo(match.Teams.Away, awayFlagPB, awayTeamL, awayNicknameL);
            scoreL.Text = match.Scoreboard.FormatScore;
        }

        public override void RefreshInformation(object item)
        {
            this.MatchRow_Click(item, null);
        }
    }
}

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
    public partial class FMatchEditor : MyForm
    {
        private enum MatchPhase { None, RegularTime, ExtraTime, Penalties };
        private Match match;

        public FMatchEditor(Match match)
        {
            InitializeComponent();
            regularFirstHalfTB.BackColor = MyGUIs.Background.Highlighted.Color;
            regularSecondHalfTB.BackColor = MyGUIs.Background.Highlighted.Color;
            extraFirstHalfTB.BackColor = MyGUIs.Background.Highlighted.Color;
            extraSecondHalfTB.BackColor = MyGUIs.Background.Highlighted.Color;
            penaltiesTB.BackColor = MyGUIs.Background.Highlighted.Color;

            if (match.Teams.Home == null || match.Teams.Away == null)
                throw new ArgumentException("Cannot edit match for which either team is not known!");
            this.match = match;

            titleIV.TextDescription = string.Format("Match {0} ({1})", match.ID, match.Category);
            homeTeamCV.Team = match.Teams.Home;
            awayTeamCV.Team = match.Teams.Away;
        }

        private void FMatchEditor_Load(object sender, EventArgs e)
        {
            MatchPhase phase = !this.match.Scoreboard.Played ? MatchPhase.None
                : (this.match.Scoreboard.FinishedInRegularTime ? MatchPhase.RegularTime
                    : (this.match.Scoreboard.FinishedInExtraTime ? MatchPhase.ExtraTime
                        : MatchPhase.Penalties));
            this.SetMatchPhase(phase);

            if (this.match.Scoreboard.Played)
            {
                regularFirstHalfTB.Text = this.match.Scoreboard.Halves[0].FormatHalfScore;
                regularSecondHalfTB.Text = this.match.Scoreboard.Halves[1].FormatHalfScore;
                if (this.match.Scoreboard.FinishedInExtraTime)
                {
                    extraFirstHalfTB.Text = this.match.Scoreboard.Halves[2].FormatHalfScore;
                    extraSecondHalfTB.Text = this.match.Scoreboard.Halves[3].FormatHalfScore;
                    if (this.match.Scoreboard.FinishedAtPenalties)
                        penaltiesTB.Text = this.match.Scoreboard.Halves[4].FormatHalfScore;
                }
            }
        }

        private void SetMatchPhase(MatchPhase phase)
        {
            switch (phase)
            {
                case MatchPhase.None:
                    matchPenaltiesChB.Checked = false;
                    matchExtraTimeChB.Checked = false;
                    matchPlayedChB.Checked = false;
                    break;
                case MatchPhase.RegularTime:
                    matchPenaltiesChB.Checked = false;
                    matchExtraTimeChB.Checked = false;
                    matchPlayedChB.Checked = true;
                    break;
                case MatchPhase.ExtraTime:
                    matchPlayedChB.Checked = true;
                    matchExtraTimeChB.Checked = true;
                    matchPenaltiesChB.Checked = false;
                    break;
                case MatchPhase.Penalties:
                    matchPlayedChB.Checked = true;
                    matchExtraTimeChB.Checked = true;
                    matchPenaltiesChB.Checked = true;
                    break;
            }
            matchPlayedChB_CheckedChanged(null, null);
        }

        private void SetScoreBox(TextBox box, bool enabled)
        {
            box.Enabled = enabled;
            box.BackColor = MyGUIs.Background[enabled].Color;
            box.Text = enabled ? "0-0" : "";
        }

        // played
        private void matchPlayedChB_CheckedChanged(object sender, EventArgs e)
        {
            this.SetScoreBox(regularFirstHalfTB, matchPlayedChB.Checked);
            this.SetScoreBox(regularSecondHalfTB, matchPlayedChB.Checked);

            matchExtraTimeChB.Visible = matchPlayedChB.Checked;
            matchExtraTimeChB_CheckedChanged(sender, e);
        }

        // extra time
        private void matchExtraTimeChB_CheckedChanged(object sender, EventArgs e)
        {
            this.SetScoreBox(extraFirstHalfTB, matchExtraTimeChB.Checked);
            this.SetScoreBox(extraSecondHalfTB, matchExtraTimeChB.Checked);

            matchPenaltiesChB.Visible = matchExtraTimeChB.Checked;
            matchPenaltiesChB_CheckedChanged(sender, e);
        }

        // penalties
        private void matchPenaltiesChB_CheckedChanged(object sender, EventArgs e)
        {
            this.SetScoreBox(penaltiesTB, matchPenaltiesChB.Checked);
        }

        private void okB_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

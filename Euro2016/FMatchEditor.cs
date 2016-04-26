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
        private HalfScoreboard score;

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
                if (this.match.Scoreboard.Halves.Count > 2)
                {
                    extraFirstHalfTB.Text = this.match.Scoreboard.Halves[2].FormatHalfScore;
                    extraSecondHalfTB.Text = this.match.Scoreboard.Halves[3].FormatHalfScore;
                    if (this.match.Scoreboard.Halves.Count > 4)
                        penaltiesTB.Text = this.match.Scoreboard.Halves[4].FormatHalfScore;
                }
            }

            this.RegisterControlsToMoveForm(this.titleIV);
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
            box.Visible = enabled;
            box.Text = enabled ? "0-0" : "";
        }

        // played
        private void matchPlayedChB_CheckedChanged(object sender, EventArgs e)
        {
            this.SetScoreBox(regularFirstHalfTB, matchPlayedChB.Checked);
            this.SetScoreBox(regularSecondHalfTB, matchPlayedChB.Checked);

            matchExtraTimeChB.Visible = matchPlayedChB.Checked;
            if (!matchExtraTimeChB.Visible)
                matchExtraTimeChB.Checked = false;
            matchExtraTimeChB_CheckedChanged(sender, e);

            Checks_Event(sender, e);
        }

        // extra time
        private void matchExtraTimeChB_CheckedChanged(object sender, EventArgs e)
        {
            this.SetScoreBox(extraFirstHalfTB, matchExtraTimeChB.Checked);
            this.SetScoreBox(extraSecondHalfTB, matchExtraTimeChB.Checked);

            matchPenaltiesChB.Visible = matchExtraTimeChB.Checked;
            if (!matchPenaltiesChB.Visible)
                matchPenaltiesChB.Checked = false;
            matchPenaltiesChB_CheckedChanged(sender, e);

            Checks_Event(sender, e);
        }

        // penalties
        private void matchPenaltiesChB_CheckedChanged(object sender, EventArgs e)
        {
            this.SetScoreBox(penaltiesTB, matchPenaltiesChB.Checked);

            Checks_Event(sender, e);
        }

        private bool CheckScoreboxFormat(TextBox box, out string error)
        {
            try
            {
                string[] parts = box.Text.Split('-');
                if (parts.Length != 2)
                    throw new ApplicationException("Incorrect number of parts separated by a dash");
                int a = Int32.Parse(parts[0]), b = Int32.Parse(parts[1]);
                if (a < 0 || b < 0)
                    throw new ApplicationException("Incorrect number of goals scored");
                error = "";
                return true;
            }
            catch (Exception E)
            {
                error = box.Name.Replace("TB", "") + ": " + E.Message;
                return false;
            }
        }

        private string ChecksResult()
        {
            string error;
            if (matchPlayedChB.Checked && !this.CheckScoreboxFormat(regularFirstHalfTB, out error))
                return error;
            if (matchPlayedChB.Checked && !this.CheckScoreboxFormat(regularSecondHalfTB, out error))
                return error;
            if (matchExtraTimeChB.Checked && !this.CheckScoreboxFormat(extraFirstHalfTB, out error))
                return error;
            if (matchExtraTimeChB.Checked && !this.CheckScoreboxFormat(extraSecondHalfTB, out error))
                return error;
            if (matchPenaltiesChB.Checked && !this.CheckScoreboxFormat(penaltiesTB, out error))
                return error;

            if (matchPlayedChB.Checked)
            {
                score = HalfScoreboard.Parse(regularFirstHalfTB.Text);
                score.AddHalfScoreboard(HalfScoreboard.Parse(regularSecondHalfTB.Text));
                if (!this.match.IsGroupMatch && score.Tie && !matchExtraTimeChB.Checked)
                    return "Knock-out match can not end in a tie";
                if (this.match.IsGroupMatch && matchExtraTimeChB.Checked)
                    return "A group match can not have extra time"; //meh
                if (!score.Tie && matchExtraTimeChB.Checked)
                    return "A match can not have extra time if the score is not a tie";
                if (matchExtraTimeChB.Checked)
                {
                    score.AddHalfScoreboard(HalfScoreboard.Parse(extraFirstHalfTB.Text));
                    score.AddHalfScoreboard(HalfScoreboard.Parse(extraSecondHalfTB.Text));
                    if (score.Tie && !matchPenaltiesChB.Checked)
                        return "Knock-out match can not end in a tie";
                    if (!score.Tie && matchPenaltiesChB.Checked)
                        return "A match can not have penalties if the score is not a tie";
                    if (matchPenaltiesChB.Checked)
                    {
                        score.AddHalfScoreboard(HalfScoreboard.Parse(penaltiesTB.Text));
                        if (score.Tie)
                            return "Knock-out match can not end in a tie";
                    }
                }
            }

            return "";
        }

        private void Checks_Event(object sender, EventArgs e)
        {
            errorL.Text = this.ChecksResult();
            okB.Visible = errorL.Text.Equals("");
            scoreL.Text = okB.Visible ? (this.matchPlayedChB.Checked ? this.score.FormatHalfScore : "-") : "-";
        }

        private void okB_Click(object sender, EventArgs e)
        {
            List<HalfScoreboard> halves = new List<HalfScoreboard>();
            if (matchPlayedChB.Checked)
            {
                halves.Add(HalfScoreboard.Parse(regularFirstHalfTB.Text));
                halves.Add(HalfScoreboard.Parse(regularSecondHalfTB.Text));
                if (matchExtraTimeChB.Checked)
                {
                    halves.Add(HalfScoreboard.Parse(extraFirstHalfTB.Text));
                    halves.Add(HalfScoreboard.Parse(extraSecondHalfTB.Text));
                    if (matchPenaltiesChB.Checked)
                        halves.Add(HalfScoreboard.Parse(penaltiesTB.Text));
                }
            }
            this.match.Scoreboard.SetHalves(halves);

            FMatch matchForm = this.Owner as FMatch;
            matchForm.mainForm.Database.Calculate(false, true, true);
            matchForm.RefreshInformation(this.match);
            matchForm.matchesView.GetRowByMatch(this.match).Invalidate();
            matchForm.mainForm.RefreshInformation(null);
            this.Close();
        }

        private void cancelB_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

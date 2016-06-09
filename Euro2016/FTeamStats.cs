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
    public partial class FTeamStats : MyForm
    {
        private FMain mainForm;

        private TeamStatsViews teamStatsViews;

        public FTeamStats(FMain mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void FTeamStats_Load(object sender, EventArgs e)
        {
            this.teamStatsViews = new TeamStatsViews(teamP, this, null, this.teamStatsViewRow_Click, this.mainForm.Database);
            this.RegisterControlsToMoveForm(this.titleLabel1);
        }

        private void teamStatsViewRow_Click(object sender, EventArgs e)
        {
            this.mainForm.ShowForm<FTeam, Team>((sender as TeamStatsViewRow).TeamProperty);
        }

        public override void RefreshInformation(object item)
        {
            this.teamStatsViews.SetTeams(this.mainForm.Database.Teams, this.teamStatsViews.header.SortByColumn, this.teamStatsViews.header.Descending);
        }
    }
}

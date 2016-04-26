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
    public partial class FMatchDays : MyForm
    {
        private FMain mainForm;
        private MatchDaysView matchDaysView;
        private MatchesView matchesView;

        public FMatchDays(FMain mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void FMatchDays_Load(object sender, EventArgs e)
        {
            this.matchDaysView = new MatchDaysView(matchDaysP, this.mainForm.Database.Matches, this.MatchDay_Click, this.mainForm.Database.Settings);
            this.matchesView = new MatchesView(matchesP, this.mainForm.MatchHeader_Click, this.mainForm.MatchRow_Click, this.mainForm.Database.Settings);
            this.MouseWheel += this.matchesView.myScrollPanel.MouseWheelScroll_EventHandler;
            this.RegisterControlsToMoveForm(this.titleLabel1);
        }

        private void MatchDay_Click(object sender, EventArgs e)
        {
            this.RefreshInformation((sender as MatchDayView).Date);
        }
        
        /// <summary>Refreshes the information for the given DateTime object.</summary>
        /// <param name="item">the DateTime object to display information for</param>
        public override void RefreshInformation(object item)
        {
            DateTime date = ((DateTime) item).Date;
            this.matchDaysView.MatchDayViews.CheckItemAndUncheckAllOthers<MatchDayView>(this.matchDaysView.MatchDayViews.FirstOrDefault(mdv => mdv.Date.Equals(date)));
            selectedDateIV.TextText = date.ToString("dddd, d MMMM yyyy");
            ListOfIDObjects<Match> matches = this.mainForm.Database.Matches.GetMatchesBy(date);
            matchDayMatchCountIVD.TextText = matches.Count + (matches.Count == 1 ? " match" : " matches");
            this.matchesView.SetMatches(matches);
        }
    }
}

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
using System.Windows.Forms.DataVisualization.Charting;

namespace Euro2016
{
    public partial class FStats : MyForm
    {
        private FMain mainForm;

        public FStats(FMain mainForm)
        {
            InitializeComponent();

            for (int i = 1; i <= 6; i++)
            {
                Chart chart = this.Controls.Find("chart" + i, true)[0] as Chart;
                chart.Series[0].Font = new Font(StaticData.PVC.Families[StaticData.FontExo_Index], 10, FontStyle.Regular);
                chart.Legends[0].Font = new Font(StaticData.PVC.Families[StaticData.FontExo_Index], 10, FontStyle.Regular);
            }

            this.mainForm = mainForm;
        }

        private void FStats_Load(object sender, EventArgs e)
        {
            this.RegisterControlsToMoveForm(this.titleLabel1);
        }
        
        /// <summary>Refreshes the statistical information for the main form database.</summary>
        /// <param name="item">no item needed, pass null</param>
        public override void RefreshInformation(object item)
        {
            Database db = this.mainForm.Database;
            ListOfIDObjects<Match> groupMatches = db.Matches.GetMatchesBy("G:"), knockoutMatches = db.Matches.GetMatchesBy("KO:");
            ListOfIDObjects<Match> matchesPlayed = db.Matches.GetMatchesBy(true), knockoutMatchesPlayed = knockoutMatches.GetMatchesBy(true);
            MatchScoreboard goals = matchesPlayed.GetAllGoals(null);
            Tuple<int, int, int> groupResults = groupMatches.GetResults(), knockoutResults = knockoutMatches.GetResults();

            chart1.Series[0].Points.Clear();
            chart1.Series[0].Points.AddXY("15:00", db.Matches.Count(m => m.When.TimeOfDay.Hours == 15));
            chart1.Series[0].Points.Last().Color = ColorTranslator.FromHtml("#0BA7DB");
            chart1.Series[0].Points.AddXY("18:00", db.Matches.Count(m => m.When.TimeOfDay.Hours == 18));
            chart1.Series[0].Points.Last().Color = ColorTranslator.FromHtml("#174396");
            chart1.Series[0].Points.AddXY("21:00", db.Matches.Count(m => m.When.TimeOfDay.Hours == 21));
            chart1.Series[0].Points.Last().Color = ColorTranslator.FromHtml("#6652A1");

            chart4.Series[0].Points.Clear();
            chart4.Series[0].Points.AddXY("played", matchesPlayed.Count);
            chart4.Series[0].Points.Last().Color = ColorTranslator.FromHtml("#9E1E3C");
            chart4.Series[0].Points.AddXY("remaining", db.Matches.Count - matchesPlayed.Count);
            chart4.Series[0].Points.Last().Color = ColorTranslator.FromHtml("#1E9E7A");

            chart3.Series[0].Points.Clear();
            chart3.Series[0].Points.AddXY("90 mins", knockoutMatchesPlayed.GetMatchesBy(2).Count);
            chart3.Series[0].Points.Last().Color = ColorTranslator.FromHtml("#4794BA");
            chart3.Series[0].Points.AddXY("120 mins", knockoutMatchesPlayed.GetMatchesBy(4).Count);
            chart3.Series[0].Points.Last().Color = ColorTranslator.FromHtml("#BA47B1");
            chart3.Series[0].Points.AddXY("120+ mins", knockoutMatchesPlayed.GetMatchesBy(5).Count);
            chart3.Series[0].Points.Last().Color = ColorTranslator.FromHtml("#FA0707");

            infoViewDetail14.TextText = "Goals (" + (goals.FullScore.Home + goals.FullScore.Away) + " in total)";
            chart2.Series[0].Points.Clear();
            if (goals.Halves.Count > 0)
            {
                chart2.Series[0].Points.AddXY("1st half", goals.Halves[0].Home + goals.Halves[0].Away);
                chart2.Series[0].Points.Last().Color = ColorTranslator.FromHtml("#63C215");
                chart2.Series[0].Points.AddXY("2nd half", goals.Halves[1].Home + goals.Halves[1].Away);
                chart2.Series[0].Points.Last().Color = ColorTranslator.FromHtml("#CFED0C");
                if (goals.Halves.Count > 2)
                {
                    chart2.Series[0].Points.AddXY("1st extra half", goals.Halves[2].Home + goals.Halves[2].Away);
                    chart2.Series[0].Points.Last().Color = ColorTranslator.FromHtml("#A0D674");
                    chart2.Series[0].Points.AddXY("2nd extra half", goals.Halves[3].Home + goals.Halves[3].Away);
                    chart2.Series[0].Points.Last().Color = ColorTranslator.FromHtml("#D9E396");
                }
                if (goals.Halves.Count > 4)
                {
                    chart2.Series[0].Points.AddXY("shootout", goals.Halves[4].Home + goals.Halves[4].Away);
                    chart2.Series[0].Points.Last().Color = ColorTranslator.FromHtml("#E87C00");
                }
            }

            chart5.Series[0].Points.Clear();
            chart5.Series[0].Points.AddXY("home wins", groupResults.Item1);
            chart5.Series[0].Points.Last().Color = ColorTranslator.FromHtml("#03A7FF");
            chart5.Series[0].Points.AddXY("ties", groupResults.Item2);
            chart5.Series[0].Points.Last().Color = ColorTranslator.FromHtml("#BBBF47");
            chart5.Series[0].Points.AddXY("away wins", groupResults.Item3);
            chart5.Series[0].Points.Last().Color = ColorTranslator.FromHtml("#FF5703");

            chart6.Series[0].Points.Clear();
            chart6.Series[0].Points.AddXY("home wins", knockoutResults.Item1);
            chart6.Series[0].Points.Last().Color = ColorTranslator.FromHtml("#03A7FF");
            chart6.Series[0].Points.AddXY("ties", knockoutResults.Item2);
            chart6.Series[0].Points.Last().Color = ColorTranslator.FromHtml("#BBBF47");
            chart6.Series[0].Points.AddXY("away wins", knockoutResults.Item3);
            chart6.Series[0].Points.Last().Color = ColorTranslator.FromHtml("#FF5703");

            myProgressBar11.SetValues(0, db.Countries.Count(c => c.UefaCountry), db.Teams.Count);
            myProgressBar12.SetValues(0, db.Players.Count, db.Players.Count(p => p.Nationality.Country.Equals(p.Club.Country)));
            myProgressBar1.SetValues(0, db.Matches.Sum(m => m.Where.Capacity), db.Matches.Sum(m => m.Where.Capacity));
        }
    }
}

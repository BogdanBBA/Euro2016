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
    public partial class FKnockOut : MyForm
    {
        private FMain mainForm;
        private List<MatchRow> matchRows;

        public FKnockOut(FMain mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            this.Owner = mainForm;
        }

        private void FKnockOut_Load(object sender, EventArgs e)
        {
            this.knockoutPB.Image = StaticData.Images[Paths.KnockoutImageFile];
            this.matchRows = new List<MatchRow>();
            ListOfIDObjects<Match> matches = this.mainForm.Database.Matches.GetMatchesBy("KO");
            for (int index = 0; index < matches.Count; index++)
            {
                MatchRow row = new MatchRow(matches[index], this.mainForm.MatchRow_Click, this.mainForm.Database.Settings);
                row.Parent = this.Controls.Find("myPanel" + (index + 1), true)[0] as MyPanel;
                row.SetBounds(0, 0, 480, MatchRow.RowHeight);
                this.matchRows.Add(row);
            }
        }
    }
}

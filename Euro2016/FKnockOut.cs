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

        private void CreateNewMatchRow(Match match, MyPanel panel)
        {
            MatchRow row = new MatchRow(match, this.mainForm.MatchRow_Click, this.mainForm.Database.Settings);
            row.Parent = panel;
            row.SetBounds(0, 0, 480, MatchRow.RowHeight);
            this.matchRows.Add(row);
        }

        private void FKnockOut_Load(object sender, EventArgs e)
        {
            this.knockoutPB.Image = StaticData.Images[Paths.KnockoutImageFile];
            this.matchRows = new List<MatchRow>();
            
            this.CreateNewMatchRow(this.mainForm.Database.Matches.GetItemByID("37"), myPanel1);
            this.CreateNewMatchRow(this.mainForm.Database.Matches.GetItemByID("39"), myPanel2);
            this.CreateNewMatchRow(this.mainForm.Database.Matches.GetItemByID("38"), myPanel3);
            this.CreateNewMatchRow(this.mainForm.Database.Matches.GetItemByID("42"), myPanel4);
            this.CreateNewMatchRow(this.mainForm.Database.Matches.GetItemByID("41"), myPanel5);
            this.CreateNewMatchRow(this.mainForm.Database.Matches.GetItemByID("43"), myPanel6);
            this.CreateNewMatchRow(this.mainForm.Database.Matches.GetItemByID("40"), myPanel7);
            this.CreateNewMatchRow(this.mainForm.Database.Matches.GetItemByID("44"), myPanel8);
            
            this.CreateNewMatchRow(this.mainForm.Database.Matches.GetItemByID("45"), myPanel9);
            this.CreateNewMatchRow(this.mainForm.Database.Matches.GetItemByID("46"), myPanel10);
            this.CreateNewMatchRow(this.mainForm.Database.Matches.GetItemByID("47"), myPanel11);
            this.CreateNewMatchRow(this.mainForm.Database.Matches.GetItemByID("48"), myPanel12);
            
            this.CreateNewMatchRow(this.mainForm.Database.Matches.GetItemByID("49"), myPanel13);
            this.CreateNewMatchRow(this.mainForm.Database.Matches.GetItemByID("50"), myPanel14);
            
            this.CreateNewMatchRow(this.mainForm.Database.Matches.GetItemByID("51"), myPanel15);
        }
    }
}

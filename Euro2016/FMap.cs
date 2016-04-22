using DotSpatial.Data;
using DotSpatial.Projections;
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
using Svg;
using System.IO;
using System.Drawing.Imaging;

namespace Euro2016
{
    public partial class FMap : MyForm
    {
        private const string FlagViewPrefix = "flag_";

        private FMain mainForm;

        private List<FlagView> flags;

        public FMap(FMain mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            this.mapMSM.OnDrawFinishCallback = this.SetFlagsVisible;
            this.flags = new List<FlagView>();
        }

        private void FMap_Load(object sender, EventArgs e)
        {
            this.mapMSM.LoadSvg(this.mainForm.Database, Paths.SvgMapFile);
            this.mapMSM.RefreshSvgMap();

            if (this.mainForm.Database.Settings.ShowFlagsOnMap)
            {
                foreach (Team team in this.mainForm.Database.Teams)
                {
                    this.flags.Add(new FlagView()
                       {
                           Parent = mapMSM,
                           Name = FMap.FlagViewPrefix + team.Country.ID,
                           Location = new Point(team.MapCoords.X, mapMSM.Height - team.MapCoords.Y - team.Country.Flag20px.Height),
                           Country = team.Country,
                           Visible = false                           
                       });
                    this.flags.Last().Click += this.FlagView_Click;
                }
                mapMSM.SendToBack();
            }

            this.RegisterControlsToMoveForm(this.titleLabel1);
        }

        private void SetFlagsVisible()
        {
            foreach (FlagView flag in this.flags)
                flag.Visible = true;
        }

        private void FlagView_Click(object sender, EventArgs e)
        {
            Team team = this.mainForm.Database.Teams.First(t => t.Country.ID.Equals((sender as FlagView).Name.Substring(FMap.FlagViewPrefix.Length, 3)));
            this.mainForm.ShowForm<FTeam, Team>(team);
        }

        public override void RefreshInformation(object item)
        {
            //
        }
    }
}

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

namespace Euro2016
{
    public partial class FMap : MyForm
    {
        private FMain mainForm;

        public FMap(FMain mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            this.map.BackColor = MyGUIs.Background.Normal.Color;
        }

        private void FMap_Load(object sender, EventArgs e)
        {
            FeatureSet fs = (FeatureSet) FeatureSet.Open(Paths.MapShapefile);
            fs.Reproject(KnownCoordinateSystems.Projected.Europe.EuropeLambertConformalConic);
            fs.FillAttributes();
            map.Layers.Add(fs);
            map.FunctionMode = DotSpatial.Controls.FunctionMode.Select;

            map.ViewExtents = new Extent(new double[] { -1750871.40255991, 459306.659425525, 3353464.2366365, 4450918.39684444 });
        }

        public override void RefreshInformation(object item)
        {

        }
    }
}

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
        private FMain mainForm;

        public FMap(FMain mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void FMap_Load(object sender, EventArgs e)
        {
            this.mySvgMap1.LoadSvg(this.mainForm.Database, Paths.SvgMapFile);
            this.mySvgMap1.RefreshSvgMap();

            this.RegisterControlsToMoveForm(this.titleLabel1);
        }

        public override void RefreshInformation(object item)
        {
            //
        }
    }
}

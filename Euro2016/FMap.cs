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
            try
            {
                using (FileStream fileStream = File.OpenRead(Paths.SvgMapFile))
                {
                    MemoryStream memoryStream = new MemoryStream();
                    memoryStream.SetLength(fileStream.Length);
                    fileStream.Read(memoryStream.GetBuffer(), 0, (int) fileStream.Length);

                    SvgDocument doc = SvgDocument.Open<SvgDocument>(memoryStream);

                    foreach (SvgElement element in doc.Children)
                    {
                        string idValue;
                        if (element.TryGetAttribute("id", out idValue))
                        {
                            Country country = this.mainForm.Database.Countries.GetItemByID(idValue);
                            Team team = this.mainForm.Database.Teams.FirstOrDefault(t => t.Country.Equals(country));
                            SetPathAttributes(element, country, team);
                        }
                    }

                    mapPB.Image = doc.Draw(mapPB.Width, mapPB.Height);
                }
            }
            catch (Exception E)
            {
                MessageBox.Show(E.ToString(), "Draw error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void SetPathAttributes(SvgElement element, Country country, Team team)
        {
            if (element is SvgPath || element is SvgEllipse)
                element.Fill = new SvgColourServer(country == null ? Color.Black : (team == null ? Color.Pink : Color.DarkRed));
            else if (element is SvgGroup)
                foreach (SvgElement subElement in element.Children)
                    SetPathAttributes(subElement, country, team);
        }

        public override void RefreshInformation(object item)
        {
            //
        }
    }
}

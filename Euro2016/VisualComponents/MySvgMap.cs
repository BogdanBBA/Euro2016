using Svg;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Euro2016.VisualComponents
{
    public class MySvgMap : Control
    {
        protected static readonly Color unknown = ColorTranslator.FromHtml("#000000");
        protected static readonly Color notUefa = ColorTranslator.FromHtml("#C4C4C4");
        protected static readonly Color notQualified = ColorTranslator.FromHtml("#707070");

        public enum MySvgMapStatus { NotInitialized, Working, Done };

        public MySvgMapStatus Status { get; private set; }
        public Action OnDrawFinishCallback { get; set; }
        private Database database;
        private SvgDocument svgDoc;
        private BackgroundWorker bgWorker;
        private Image image;

        public MySvgMap()
            : base()
        {
            this.Status = MySvgMapStatus.NotInitialized;
            this.Font = StaticData.PVC != null ? new Font(StaticData.PVC.Families[StaticData.FontExo_Index], 17, FontStyle.Regular) : new Font("Arial", 19, FontStyle.Regular);
            this.Size = new Size(200, 200);
            this.bgWorker = new BackgroundWorker();
            this.bgWorker.WorkerSupportsCancellation = false;
            this.bgWorker.WorkerReportsProgress = false;
            this.bgWorker.DoWork += bgWorker_DoWork;
            this.bgWorker.RunWorkerCompleted += bgWorker_RunWorkerCompleted;
        }

        public void LoadSvg(Database database, string svgPath)
        {
            this.database = database;

            using (FileStream fileStream = File.OpenRead(svgPath))
            {
                MemoryStream memoryStream = new MemoryStream();
                memoryStream.SetLength(fileStream.Length);
                fileStream.Read(memoryStream.GetBuffer(), 0, (int) fileStream.Length);

                this.svgDoc = SvgDocument.Open<SvgDocument>(memoryStream);

                foreach (SvgElement element in this.svgDoc.Children)
                {
                    string idValue;
                    if (element.TryGetAttribute("id", out idValue))
                    {
                        Country country = this.database.Countries.GetItemByID(idValue);
                        Team team = this.database.Teams.FirstOrDefault(t => t.Country.Equals(country));
                        SetSvgElementAttributes(element, country, team);
                    }
                }
            }
        }

        private void SetSvgElementAttributes(SvgElement element, Country country, Team team)
        {
            if (element is SvgPath || element is SvgEllipse)
                element.Fill = new SvgColourServer(country == null ? unknown : (country.UefaCountry ? (team != null ? Utils.TournamentResultColorForTeam(this.database.TournamentResultOfTeam(team)) : notQualified) : notUefa));
            else if (element is SvgGroup)
                foreach (SvgElement subElement in element.Children)
                    SetSvgElementAttributes(subElement, country, team);
        }

        public void RefreshSvgMap()
        {
            this.Status = MySvgMapStatus.Working;
            this.Invalidate();
            this.bgWorker.RunWorkerAsync(new KeyValuePair<SvgDocument, Size>(this.svgDoc, this.Size));
        }

        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            KeyValuePair<SvgDocument, Size> argument = (KeyValuePair<SvgDocument, Size>) e.Argument;
            Image image = argument.Key.Draw(argument.Value.Width, argument.Value.Height);
            e.Result = image;
        }

        private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.image = (Image) e.Result;
            this.Status = MySvgMapStatus.Done;
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(MyGUIs.Background.Normal.Color);
            e.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.AssumeLinear;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            if (this.Status == MySvgMapStatus.NotInitialized || this.Status == MySvgMapStatus.Working)
            {
                string text = this.Status == MySvgMapStatus.NotInitialized ? "Not initialized" : "Working, please wait...";
                SizeF size = e.Graphics.MeasureString(text, this.Font);
                e.Graphics.DrawString(text, this.Font, MyGUIs.Text.Highlighted.Brush, this.Width / 2 - size.Width / 2, this.Height / 2 - size.Height / 2);
            }
            else if (this.Status == MySvgMapStatus.Done)
            {
                e.Graphics.DrawImage(this.image, Point.Empty);
                if (this.OnDrawFinishCallback != null)
                    this.OnDrawFinishCallback();
            }
        }
    }
}

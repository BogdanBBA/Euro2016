using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Euro2016.VisualComponents
{
    public class GroupView
    {
        internal static readonly double[] ColumnWidthPercentages = new double[] { 0.06, 0.11, 0.31, 0.08, 0.04, 0.04, 0.04, 2 * 0.07, 1.5 * 0.06, 1.5 * 0.06 }; // Pos Flag TEAM MP W D L GF-GA +/-GD Pts
        internal static readonly string[] ColumnWidthCaptions = new string[] { "Pos", "", "Team", "MP", "W", "D", "L", "Goals", "Diff", "Pts" };

        public Group Group { get; private set; }

        private GroupButton header = null;
        private GroupHeader headerRow = null;
        private GroupRow[] rows;
        private bool detailedLayout;
        private Settings settings;

        public GroupView(Panel parent, bool detailedLayout, EventHandler onGroupHeaderClickDelegate, EventHandler onGroupRowClickDelegate, Settings settings)
        {
            this.detailedLayout = detailedLayout;
            this.settings = settings;

            if (!detailedLayout)
            {
                this.header = new GroupButton(parent, null, onGroupHeaderClickDelegate);
                this.header.SetBounds(0, 0, parent.Width / 4, parent.Height);
            }
            if (detailedLayout)
            {
                this.headerRow = new GroupHeader();
                this.headerRow.Parent = parent;
                this.headerRow.SetBounds(0, 0, parent.Width, GroupHeader.RowHeight);
            }
            this.rows = new GroupRow[detailedLayout ? 6 : 4];
            for (int iRow = 0; iRow < this.rows.Length; iRow++)
            {
                GroupRow row = new GroupRow(parent, null, onGroupRowClickDelegate, detailedLayout, this.settings);
                if (!detailedLayout)
                    row.SetBounds(parent.Width / 4, parent.Height / 2 + (iRow - 2) * GroupRow.RowHeight, 3 * parent.Width / 4, GroupRow.RowHeight);
                else
                    row.SetBounds(0, (int) (GroupHeader.RowHeight + iRow * (parent.Height / 5.0)), parent.Width, (int) (parent.Height / 5.0));
                this.rows[iRow] = row;
            }
        }

        public void SetGroup(Group group)
        {
            this.Group = group;
            if (!detailedLayout)
                this.header.Group = group;
            for (int index = 0; index < (detailedLayout ? 6 : 4); index++)
            {
                this.rows[index].Visible = group.TableLines.Count > index;
                if (this.rows[index].Visible)
                    this.rows[index].TableLine = group.TableLines[index];
            }
        }
    }

    public class GroupButton : MyEuroBaseControl
    {
        private Group group;
        public Group Group
        {
            get { return this.group; }
            set { this.group = value; this.Invalidate(); }
        }

        public GroupButton()
            : this(null, null, null)
        {
        }

        public GroupButton(Panel parent, Group group, EventHandler onClickDelegate)
            : base()
        {
            this.Parent = parent;
            this.Font = new Font(StaticData.PVC.Families[StaticData.FontExo_Index], 16, FontStyle.Bold);
            this.Cursor = Cursors.Hand;
            this.group = group;
            this.Click += onClickDelegate;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            string text = this.group != null ? this.group.Name : "GROUP?";
            SizeF size = e.Graphics.MeasureString(text, this.Font);
            e.Graphics.DrawString(text, this.Font, this.isChecked ? MyGUIs.Accent.Highlighted.Brush : MyGUIs.Accent[this.mouseIsOver].Brush,
                new PointF(this.Width / 2 - size.Width / 2, this.Height / 2 - size.Height / 2));
        }
    }

    public class GroupHeader : MyEuroBaseControl
    {
        internal const int RowHeight = 40;

        public GroupHeader()
            : base()
        {
            this.Font = new Font(StaticData.PVC.Families[StaticData.FontExoBold_Index], 17, FontStyle.Bold);
            this.Cursor = Cursors.Default;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(MyGUIs.Background.Normal.Color);
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            double lastLeft = 0.0;
            for (int iCol = 0; iCol < GroupView.ColumnWidthPercentages.Length; lastLeft += GroupView.ColumnWidthPercentages[iCol], iCol++)
                this.DrawTextCell(e.Graphics, this.Font, GroupView.ColumnWidthCaptions[iCol], HorizontalAlignment.Center, lastLeft, lastLeft + GroupView.ColumnWidthPercentages[iCol]);
        }
    }

    public class GroupRow : MyEuroBaseControl
    {
        internal const int RowHeight = 32;

        private TableLine tableLine;
        public TableLine TableLine
        {
            get { return this.tableLine; }
            set { this.tableLine = value; this.Invalidate(); }
        }
        private Font fontNormal;
        private Font fontEmphasized;
        private bool detailedLayout;
        private Settings settings;

        public GroupRow(Panel parent, TableLine tableLine, EventHandler onClickDelegate, bool detailedLayout, Settings settings)
            : base()
        {
            this.Parent = parent;
            this.Cursor = Cursors.Hand;
            this.tableLine = tableLine;
            this.fontNormal = detailedLayout ? new Font(StaticData.PVC.Families[StaticData.FontExo_Index], 16, FontStyle.Regular) : new Font(StaticData.PVC.Families[StaticData.FontExo_Index], 12, FontStyle.Regular);
            this.fontEmphasized = detailedLayout ? new Font(StaticData.PVC.Families[StaticData.FontExoBold_Index], 16, FontStyle.Bold) : new Font(StaticData.PVC.Families[StaticData.FontExoBold_Index], 12, FontStyle.Bold);
            this.settings = settings;
            this.Click += onClickDelegate;
            this.detailedLayout = detailedLayout;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.AssumeLinear;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            if (this.tableLine == null)
                return;
            if (this.detailedLayout)
            {
                double lastLeft = 0.0;
                this.DrawTextCell(e.Graphics, this.fontNormal, this.tableLine.Position + ".", HorizontalAlignment.Center, lastLeft, lastLeft + GroupView.ColumnWidthPercentages[0]);
                lastLeft += GroupView.ColumnWidthPercentages[0];
                this.DrawImageCell(e.Graphics, this.tableLine.Team.Country.Flag40px, HorizontalAlignment.Center, lastLeft, lastLeft + GroupView.ColumnWidthPercentages[1]);
                lastLeft += GroupView.ColumnWidthPercentages[1];
                this.DrawTextCell(e.Graphics, this.fontEmphasized, this.tableLine.Team.Country.Names[this.settings.ShowCountryNamesInNativeLanguage], HorizontalAlignment.Left, lastLeft, lastLeft + GroupView.ColumnWidthPercentages[2]);
                lastLeft += GroupView.ColumnWidthPercentages[2];
                this.DrawTextCell(e.Graphics, this.fontNormal, this.tableLine.MatchesPlayed.ToString(), HorizontalAlignment.Center, lastLeft, lastLeft + GroupView.ColumnWidthPercentages[3]);
                lastLeft += GroupView.ColumnWidthPercentages[3];
                this.DrawTextCell(e.Graphics, this.fontNormal, this.tableLine.Won.ToString(), HorizontalAlignment.Center, lastLeft, lastLeft + GroupView.ColumnWidthPercentages[4]);
                lastLeft += GroupView.ColumnWidthPercentages[4];
                this.DrawTextCell(e.Graphics, this.fontNormal, this.tableLine.Drawn.ToString(), HorizontalAlignment.Center, lastLeft, lastLeft + GroupView.ColumnWidthPercentages[5]);
                lastLeft += GroupView.ColumnWidthPercentages[5];
                this.DrawTextCell(e.Graphics, this.fontNormal, this.tableLine.Lost.ToString(), HorizontalAlignment.Center, lastLeft, lastLeft + GroupView.ColumnWidthPercentages[6]);
                lastLeft += GroupView.ColumnWidthPercentages[6];
                this.DrawTextCell(e.Graphics, this.fontNormal, this.tableLine.GoalsFor + "-" + this.tableLine.GoalsAgainst, HorizontalAlignment.Center, lastLeft, lastLeft + GroupView.ColumnWidthPercentages[7]);
                lastLeft += GroupView.ColumnWidthPercentages[7];
                this.DrawTextCell(e.Graphics, this.fontNormal, this.tableLine.FormatGoalDifference, HorizontalAlignment.Center, lastLeft, lastLeft + GroupView.ColumnWidthPercentages[8]);
                lastLeft += GroupView.ColumnWidthPercentages[8];
                this.DrawTextCell(e.Graphics, this.fontEmphasized, this.tableLine.Points.ToString(), HorizontalAlignment.Center, lastLeft, lastLeft + GroupView.ColumnWidthPercentages[9]);
                lastLeft += GroupView.ColumnWidthPercentages[9];
            }
            else
            {
                this.DrawTextCell(e.Graphics, this.fontEmphasized, this.tableLine.Position + ".", HorizontalAlignment.Center, 0f, 0.08f);
                this.DrawImageCell(e.Graphics, this.tableLine.Team.Country.Flag20px, HorizontalAlignment.Center, 0.08f, 0.15f);
                this.DrawTextCell(e.Graphics, this.fontEmphasized, this.tableLine.Team.Country.Names[this.settings.ShowCountryNamesInNativeLanguage], HorizontalAlignment.Left, 0.17f, 0.65f);
                this.DrawTextCell(e.Graphics, this.fontNormal, this.tableLine.MatchesPlayed + "m", HorizontalAlignment.Center, 0.65f, 0.75f);
                this.DrawTextCell(e.Graphics, this.fontNormal, this.tableLine.FormatGoalDifference, HorizontalAlignment.Center, 0.75f, 0.9f);
                this.DrawTextCell(e.Graphics, this.fontEmphasized, this.tableLine.Points + "p", HorizontalAlignment.Center, 0.9f, 1f);
            }
        }
    }
}

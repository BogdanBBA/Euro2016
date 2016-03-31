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
    public partial class FGroup : MyForm
    {
        private const string GroupButtonNamePrefix = "groupGB_";

        private FMain mainForm;
        private List<GroupButton> groupButtons;
        private GroupView groupView;
        private MatchesView matchesView;

        public FGroup(FMain mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            this.Owner = mainForm;
        }

        private void FGroup_Load(object sender, EventArgs e)
        {
            string[] groupNames = new string[this.mainForm.Database.Groups.Count];
            for (int index = 0; index < this.mainForm.Database.Groups.Count; index++)
                groupNames[index] = this.mainForm.Database.Groups[index].Name;
            this.groupButtons = MyEuroBaseControl.CreateControlCollection<GroupButton>(groupNames, this.GroupButton_Click, GroupButtonNamePrefix, null);
            for (int index = 0; index < this.mainForm.Database.Groups.Count; index++)
            {
                this.groupButtons[index].Group = this.mainForm.Database.Groups[index];
                this.groupButtons[index].Click += this.GroupButton_Click;
            }
            Utils.SizeAndPositionControlsInPanel(groupButtonP, this.groupButtons, true, 0);
            this.groupView = new GroupView(groupP, true, this.GroupButton_Click, this.mainForm.GroupRow_Click, this.mainForm.Database.Settings);
            this.matchesView = new MatchesView(this.matchesP, this.mainForm.MatchHeader_Click, this.mainForm.MatchRow_Click, this.mainForm.Database.Settings);
            this.MouseWheel += this.matchesView.myScrollPanel.MouseWheelScroll_EventHandler;
        }

        private void GroupButton_Click(object sender, EventArgs e)
        {
            this.RefreshInformation(sender is GroupButton ? (sender as GroupButton).Group : sender as Group);
        }

        public override void RefreshInformation(object item)
        {
            Group group = item as Group;
            this.groupButtons.CheckItemAndUncheckAllOthers<GroupButton>(this.groupButtons.First(gh => gh.Group.Equals(group)));
            this.groupView.SetGroup(group);

            ListOfIDObjects<Match> matches = new ListOfIDObjects<Match>();
            foreach (TableLine tableLine in group.TableLines)
                matches.AddRange(this.mainForm.Database.Matches.GetMatchesBy(tableLine.Team).GetMatchesBy("G:"));
            this.matchesView.SetMatches(matches);
        }
    }
}

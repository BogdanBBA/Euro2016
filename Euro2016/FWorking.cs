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
    public partial class FWorking : MyForm
    {
        public enum WorkToDo { Initialize };

        private WorkToDo workToDo;
        private Database database;

        public FWorking(WorkToDo workToDo, Database database)
        {
            InitializeComponent();
            this.workToDo = workToDo;
            this.database = database;
        }

        private void FWorking_Load(object sender, EventArgs e)
        {
            startT.Enabled = true;
        }

        private void startT_Tick(object sender, EventArgs e)
        {
            startT.Enabled = false;

            BackgroundWorker bgW = new BackgroundWorker();
            bgW.WorkerReportsProgress = true;
            bgW.WorkerSupportsCancellation = false;

            switch (this.workToDo)
            {
                case WorkToDo.Initialize:
                    bgW.DoWork += this.bgW_Initialize_DoWork;
                    bgW.ProgressChanged += this.bgW_Initialize_ProgressChanged;
                    bgW.RunWorkerCompleted += this.bgW_Initialize_RunWorkerCompleted;
                    break;
            }

            bgW.RunWorkerAsync(this.database);
        }

        #region initialize methods
        private void bgW_Initialize_DoWork(object sender, DoWorkEventArgs e)
        {
            (sender as BackgroundWorker).ReportProgress(0, "Checking folders and files...");

            string checkResult = Paths.CheckPaths(true);
            if (!checkResult.Equals(""))
                throw new ApplicationException("Failed to initialize because there were errors with the folder and file check.");
            (sender as BackgroundWorker).ReportProgress(0, "Reading database (hold on, it takes a while)...");

            checkResult = StaticData.LoadData();
            if (!checkResult.Equals(""))
                throw new ApplicationException(checkResult);

            Database database = new Database();
            checkResult = database.LoadDatabase(Paths.DatabaseFile, Paths.DatabasePlayersFile);
            if (!checkResult.Equals(""))
                throw new ApplicationException(checkResult);

            e.Result = database;

            (sender as BackgroundWorker).ReportProgress(0, "Finished!");
        }

        private void bgW_Initialize_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            titleLabel1.TextSubtitle = e.UserState as string;
        }

        private void bgW_Initialize_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                FMain mainForm = new FMain(e.Result as Database);
                mainForm.Show();
                mainForm.Focus();
                closeT.Enabled = true;
            }
            else
            {
                MessageBox.Show(e.Error.ToString(), "Initialization ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }
        #endregion

        private void closeT_Tick(object sender, EventArgs e)
        {
            closeT.Enabled = false;
            switch (this.workToDo)
            {
                case WorkToDo.Initialize:
                    this.Hide();
                    break;
                default:
                    this.Close();
                    break;
            }
        }
    }
}

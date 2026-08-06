using DVLDBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD_Project.ApplicationType.Forms;
using DVLD_Project.DetainedLicense.Forms;
using DVLD_Project.Drivers.Forms;
using DVLD_Project.InternationalLicense.Forms;
using DVLD_Project.License.Forms;
using DVLD_Project.LocalDrivingLicenseApplication.Forms;
using DVLD_Project.People.Forms;
using DVLD_Project.TestType.Forms;
using DVLD_Project.User.Forms;

namespace DVLD_Project.Global.Forms
{
    public partial class MainMenu : Form
    {

        private Form ActiveForm=null;

        LogInScreen frm = null;
        public MainMenu(int i , int j , LogInScreen fr_m  )
        {
            frm = fr_m;
            
            this.Size = new Size(i, j);
            InitializeComponent();

            
        }

        private void OpenChildForm(Form ChildForm)
        {
            if (ActiveForm != null)
            {
                ActiveForm.Close();
            }

            ActiveForm = ChildForm;
            ChildForm.TopLevel = false;
            ChildForm.FormBorderStyle = FormBorderStyle.None; //delete non important thing like close/minimize/... buttons 
            ChildForm.Dock = DockStyle.Fill;
            this.pnlMenu.Controls.Add(ChildForm);
            this.pnlMenu.Tag = ChildForm;
            ChildForm.BringToFront();
            ChildForm.Show();


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lbltime.Text = DateTime.Now.ToString();
            pictureBox1.ImageLocation = clsCurrentUser.CurrentUser.Person.ImagePath;
            //MessageBox.Show(clsCurrentUser.CurrentUser.UserName, clsCurrentUser.CurrentUser.Person.ImagePath);
            lblUserName.Text=clsCurrentUser.CurrentUser.UserName;
        }

        private void btn_Application_Click(object sender, EventArgs e)
        {
            contextMenuStrip2.Show();
        }

        private void pnlMenus_Paint(object sender, PaintEventArgs e)
        {

        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            //Form form = new LogInScreen(this);
            Size size = new Size();
            size.Width = 755;
            size.Height = 580;
            frm.Size = size;
            frm.frm = this;
            frm.ShowDialog();
            //this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lbltime.Text= DateTime.Now.ToString();
            lbltime.Refresh();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //OpenChildForm(new UsersManagementForm());
            Form frm = new UsersManagementForm();
            OpenChildForm(frm);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Show();
        }

        private void currentUserInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Size size = new Size(975, 700);
            ShowUserInfo frm = new ShowUserInfo(clsCurrentUser.CurrentUser.UserID);
            frm.Size = size;
            frm.ShowDialog();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Size size = new Size(975, 700);
            UpdateUserPasswordForm frm = new UpdateUserPasswordForm(clsCurrentUser.CurrentUser.UserID);
            frm.Size = size;
            frm.ShowDialog();
        }

        private void btn_People_Click(object sender, EventArgs e)
        {
            OpenChildForm(new PeopleForm());
        }

        private void manageApplicationTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationTypes frm = new ApplicationTypes();
            Size size = new Size(760, 700);
            frm.Size = size;
            frm.ShowDialog();
        }

        private void manageTestTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListTestType frm = new ListTestType();
            Size size = new Size(760, 700);
            frm.Size = size;
            frm.ShowDialog();
        }

        private void localLicenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewLocalDrivingLicenseApplication frm = new NewLocalDrivingLicenseApplication();
            frm.ShowDialog();
        }

        private void localDrivingLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LocalDrivingLicenseApplicationManagement frm = new LocalDrivingLicenseApplicationManagement();
            frm.Size = new Size(1100, 700);
            frm.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenChildForm(new ManageDrivers());
            //ManageDrivers frm = new ManageDrivers();
            //frm.Size = new Size(1100, 700);
            //frm.ShowDialog();
        }

        private void internationalLicenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InternationalLicenseAppForm frm = new InternationalLicenseAppForm();

            frm.Size = new Size(1000, 950);
            frm.ShowDialog();
        }

        private void localDrivingLicenseApplicationsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ManageInlernationalApplicationForm frm = new ManageInlernationalApplicationForm();
            frm.Size = new Size(1100, 700);
            frm.ShowDialog();
        }

        private void renewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RenewDrivingLicenseForm frm = new RenewDrivingLicenseForm();

            frm.Size = new Size(1000, 950);
            frm.ShowDialog();
        }

        private void remplacementForLostOrDamagedLicenceToolStripMenuItem_Click(object sender, EventArgs e)
        {

            NewLicenseForDamagedOrLostForm frm = new NewLicenseForDamagedOrLostForm();

            frm.Size = new Size(1000, 950);
            frm.ShowDialog();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {

        }

        private void detainLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {

            DetainLicenseForm frm = new DetainLicenseForm();

            frm.Size = new Size(1000, 950);
            frm.ShowDialog();
        }

        private void releasDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReleaseDetainedLicenseForm frm = new ReleaseDetainedLicenseForm();

            frm.Size = new Size(898, 800);
            frm.ShowDialog();
        }

        private void manageDetainedLicensesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageDetainedLicensesForm frm = new ManageDetainedLicensesForm();
            frm.Size = new Size(1100, 750);
            frm.ShowDialog();
        }

        private void releaseDetainedDrivingLicenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReleaseDetainedLicenseForm frm = new ReleaseDetainedLicenseForm();

            frm.Size = new Size(898, 800);
            frm.ShowDialog();
        }

        private void retakeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LocalDrivingLicenseApplicationManagement frm = new LocalDrivingLicenseApplicationManagement();
            frm.Size = new Size(1100, 700);
            frm.ShowDialog();
        }

        private void MainMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            frm.Close();
        }
    }
}

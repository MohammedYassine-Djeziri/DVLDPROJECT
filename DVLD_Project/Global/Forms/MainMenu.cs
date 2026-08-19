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

        /// <summary>
        /// Set to true when the user chooses "Sign out". Read by the login
        /// screen after this dialog closes to decide whether to re-show the
        /// login form (sign-out) or exit the application (window closed).
        /// </summary>
        public bool SignOutRequested = false;

        public MainMenu(int i , int j)
        {
            
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
            // Just close this dialog. The login screen (which is blocked inside
            // ShowDialog) regains control and re-shows itself for a new session.
            // We must NOT call ShowDialog() on the login form from here while we
            // are ourselves displayed modally, and we must NOT Hide() ourselves
            // and stay alive (that left MainMenu in a modal state and caused
            // "Form that is already displayed modally cannot be displayed as a
            // modal dialog box" on the next login).
            SignOutRequested = true;
            this.Close();
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
            frm.Size = new Size(1100, 850);
            frm.ShowDialog();
        }

        private void localDrivingLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LocalDrivingLicenseApplicationManagement frm = new LocalDrivingLicenseApplicationManagement();
            frm.Size = new Size(1100, 850);
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

            frm.Size = new Size(1000, 850);
            frm.ShowDialog();
        }

        private void localDrivingLicenseApplicationsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ManageInlernationalApplicationForm frm = new ManageInlernationalApplicationForm();
            frm.Size = new Size(1100, 800);
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
            frm.Size = new Size(1100, 850);
            frm.ShowDialog();
        }

        private void MainMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            // The login form's lifecycle is now managed by LogInScreen after
            // this dialog returns (see btn_LOGIN_Click). Previously this handler
            // called frm.Close() while we were still inside our own modal
            // ShowDialog, which caused re-entrancy issues; closing the login
            // form is now done by the login screen itself when SignOutRequested
            // is false.
        }
    }
}

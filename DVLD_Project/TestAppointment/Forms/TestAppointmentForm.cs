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
using DVLD_Project.Test.Forms;
using DVLD_Project.Properties;

namespace DVLD_Project.TestAppointment.Forms
{
    public partial class TestAppointmentForm : Form
    {
        int LDLID = -1;
        int TestTypeID=-1;

        //clsTestAppointment TestAppointment = clsTestAppointment.GetEmptyTestAppointment();
        clsApplication MyApplication = clsApplication.GetEmptyApplication();
        clsLocalDrivingLicenseApp LDLApp = clsLocalDrivingLicenseApp.GetEmptyLocalDrivingLicenseApplication();
        public TestAppointmentForm(int ldl_id, int test_type_id)
        {
            InitializeComponent();
            LDLID = ldl_id;
            TestTypeID = test_type_id;
            LDLApp = clsLocalDrivingLicenseApp.FindLDLAppByLDLAppID(ldl_id);
            MyApplication = clsApplication.FindApplicationByLDLID(ldl_id);
        }

        private void TestForm_Load(object sender, EventArgs e)

        {
            dataGridView1.DataSource = clsTestAppointment.ListTestsAppointment(LDLID, TestTypeID);
            
            for(int i = 0; i < dataGridView1.Rows.Count;i++)
            {
                clsTestAppointment A = clsTestAppointment.FindTestAppointmentByAppointmentID(
                    Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value));
                if (A.AppointmentDate<DateTime.Now)
                {
                    A.IsLocked=true;
                    A.Save();   
                }
            }
            dataGridView1.DataSource = clsTestAppointment.ListTestsAppointment(LDLID, TestTypeID);
            showApplicationDetails1.ID= LDLID;
            showApplicationDetails1.PassedTest = TestTypeID - 1;
            showApplicationDetails1.RefreshInfo();


            lbl_TestType.Text = clsTestTypes.GetTestNameFromTestTypeID(TestTypeID)+"\nAppointment";
            if (TestTypeID == 1)
            {
                pictureBox1.Image = Resources.Vision_512;
            }

            else if (TestTypeID == 2)
            {
                pictureBox1.Image = Resources.Written_Test_512;
            }

            else if (TestTypeID == 3)
            {
                pictureBox1.Image = Resources.driving_test_512;
            }
        }

        private void btn_AddAppointment_Click(object sender, EventArgs e)
        {
            if(clsTestAppointment.HasAppointment(LDLID, TestTypeID))
            {
                MessageBox.Show("You already have a scheduled appointment for this test type");
            }

            else if (clsTestAppointment.IsAlreadyWinInTestType(LDLID , TestTypeID))
            {
                MessageBox.Show("You already get the test");
            }

            else
            {
                TakeTestAppointment frm = new TakeTestAppointment(LDLApp.LocalDrivingLicenseAppID, LDLApp.ApplicationID
                    , -1, (dataGridView1.Rows.Count)  , TestTypeID);
                frm.Size = new Size(500, 600);
                frm.ShowDialog();
                dataGridView1.DataSource = null;
               dataGridView1.DataSource = clsTestAppointment.ListTestsAppointment(LDLID, TestTypeID);
                
                dataGridView1.RefreshEdit();
                dataGridView1.Refresh();
            }
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();   
        }

        private void editApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TakeTestAppointment frm = new TakeTestAppointment(LDLApp.LocalDrivingLicenseAppID, LDLApp.ApplicationID
                    , Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value), (dataGridView1.Rows.Count)  , TestTypeID);
            frm.Size = new Size(500, 600);
            frm.ShowDialog();
            dataGridView1.DataSource = clsTestAppointment.ListTestsAppointment(LDLID, TestTypeID);
        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TakeTestForm frm = new TakeTestForm(LDLApp.LocalDrivingLicenseAppID, LDLApp.ApplicationID
                    , Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value), (dataGridView1.Rows.Count)  , TestTypeID);
            frm.Size = new Size(500, 600);
            frm.ShowDialog();
            dataGridView1.DataSource = clsTestAppointment.ListTestsAppointment(LDLID, TestTypeID);
            

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if((bool)dataGridView1.CurrentRow.Cells[3].Value)
            {
                takeTestToolStripMenuItem.Enabled = false;
            }
            else
            {
                takeTestToolStripMenuItem.Enabled = ! false;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}

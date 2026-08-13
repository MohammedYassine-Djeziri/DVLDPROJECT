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
using DVLD_Project.Global;
using DVLD_Project.Properties;

namespace DVLD_Project.Test.Forms
{
    public partial class TakeTestForm : Form
    {
        clsApplication MyApplication = clsApplication.GetEmptyApplication();
        clsTestAppointment MyAppointment = clsTestAppointment.GetEmptyTestAppointment();
        clsLocalDrivingLicenseApp LDLApp = clsLocalDrivingLicenseApp.GetEmptyLocalDrivingLicenseApplication();
        clsTest MyTest=clsTest.GetEmptyTest();
        int TestType_ID = -1;
        public TakeTestForm(int ldl_ID, int app_ID, int appointment_ID, int Trial , int test_type)
        {
            InitializeComponent();
            MessageBox.Show("TakeTestForm constructor called ");
            TestType_ID = test_type;
            MyApplication = clsApplication.FindApplicationByAppID(app_ID);
            LDLApp = clsLocalDrivingLicenseApp.FindLDLAppByLDLAppID(ldl_ID);
            if (appointment_ID != -1)
            {
                MyAppointment = clsTestAppointment.FindTestAppointmentByAppointmentID(appointment_ID);
            }

            
            lbl_LDLID.Text = LDLApp.LocalDrivingLicenseAppID.ToString();
            lbl_Class.Text = clsLicenseClasses.GetLicenseClassNameFromClassID(LDLApp.LicenseClassID);
            lbl_Name.Text = clsPeoples.FindByPersonalID(MyApplication.PersonID).FullName();
            lbl_Trial.Text = Trial.ToString();
            lbl_Fees.Text = clsTestTypes.GetTestFeesFromTestTypeID(TestType_ID).ToString();
            label5.Text = "No Token Yet";

            MyTest.TestAppointmentID = MyAppointment.TestAppointmentID;
            MyTest.UserID=clsCurrentUser.CurrentUser.UserID;
            

        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TakeTestForm_Load(object sender, EventArgs e)
        {
            lbl_TestType.Text = clsTestTypes.GetTestNameFromTestTypeID(TestType_ID);
            if (TestType_ID == 1)
            {
                pictureBox1.Image = Resources.Vision_512;
            }

            else if (TestType_ID == 2)
            {
                pictureBox1.Image = Resources.Written_Test_512;
            }

            else if (TestType_ID == 3)
            {
                pictureBox1.Image = Resources.driving_test_512;
            }
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            if( (!Pass_RB.Checked) && (!Fail_RB.Checked))
            {
                MessageBox.Show("Enter Result!");
            }


            else
            {
                MyTest.TestResult = false;
                if (Pass_RB.Checked)
                {
                    MyTest.TestResult = true;
                    
                }

               


                MyTest.AddNewTest();
                MyAppointment.IsLocked = true;
                MyAppointment.Save();
                btn_Save.Enabled = !true;

            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            MyTest.Notes = textBox1.Text;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Pass_RB_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}

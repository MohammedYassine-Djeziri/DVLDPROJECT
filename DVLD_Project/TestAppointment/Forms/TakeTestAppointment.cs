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

namespace DVLD_Project.TestAppointment.Forms
{
    public partial class TakeTestAppointment : Form
    {

        clsApplication MyApplication = clsApplication.GetEmptyApplication();
        clsTestAppointment MyAppointment = clsTestAppointment.GetEmptyTestAppointment();
        clsLocalDrivingLicenseApp LDLApp = clsLocalDrivingLicenseApp.GetEmptyLocalDrivingLicenseApplication();
        int TestType_ID = -1;

        public TakeTestAppointment(int ldl_ID ,int app_ID , int appointment_ID , int Trial , int test_type)
        {
            TestType_ID=test_type;
            //MessageBox.Show(ldl_ID.ToString()+"  "+app_ID.ToString() + "  " + appointment_ID.ToString() + "  " + Trial.ToString());
            InitializeComponent();
            MyApplication = clsApplication.FindApplicationByAppID(app_ID);
            LDLApp=clsLocalDrivingLicenseApp.FindLDLAppByLDLAppID(ldl_ID);

            if (appointment_ID != -1)
            {
                MyAppointment = clsTestAppointment.FindTestAppointmentByAppointmentID(appointment_ID);
            }

            else
            {
                MyAppointment.TestTypeID = TestType_ID;
                MyAppointment.LocalDrivingLicenseAppID = ldl_ID;
                MyAppointment.Fees = clsTestTypes.GetTestFeesFromTestTypeID(TestType_ID);
                MyAppointment.IsLocked = false;
                MyAppointment.UserID=clsCurrentUser.CurrentUser.UserID; 

            }
            // bug in the update process he create retake_test_app fix it using  MyAppointment.Mode
            //Add new case
            if (MyAppointment.TestAppointmentID==-1)
            {
                if (Trial >= 1)
                {
                        clsApplication RetakeTestApp = clsApplication.GetEmptyApplication();
                        RetakeTestApp.ApplicationStatus = 1;
                        RetakeTestApp.ApplicationFees = clsApplicationTypes.FindAppFeesByAppTypeID(7);
                        RetakeTestApp.ApplicationType = 7;
                        RetakeTestApp.PersonID = MyApplication.PersonID;
                        RetakeTestApp.UserId = clsCurrentUser.CurrentUser.UserID;
                        RetakeTestApp.Save();
                        MyAppointment.RetakeTestApplicationID = RetakeTestApp.ApplicationID;
                        retakeTestInfo1.RefreshInfo();
                }
            }

            //update case just refresh retake test info
            if (MyAppointment.TestAppointmentID != -1)
                {
                    if (Trial > 1)
                    {
                        clsApplication RetakeTestApp = clsApplication.GetEmptyApplication();
                        RetakeTestApp = clsApplication.FindApplicationByAppID(MyAppointment.RetakeTestApplicationID);
                        retakeTestInfo1.Enabled = true;
                        retakeTestInfo1.RetakeTestAppID = RetakeTestApp.ApplicationID;
                        retakeTestInfo1.Fees = RetakeTestApp.ApplicationFees;
                        retakeTestInfo1.TotalFees = RetakeTestApp.ApplicationFees + MyAppointment.Fees;

                    }
                }

            dateTimePicker1.MinDate = DateTime.Now;
            dateTimePicker1.Value = DateTime.Now.AddDays(1) ;
            lbl_LDLID.Text=LDLApp.LocalDrivingLicenseAppID.ToString();
            lbl_Class.Text = clsLicenseClasses.GetLicenseClassNameFromClassID(LDLApp.LicenseClassID);
            lbl_Name.Text = clsPeoples.FindByPersonalID(MyApplication.PersonID).FullName();
            lbl_Trial.Text = Trial.ToString();
            lbl_Fees.Text = clsTestTypes.GetTestFeesFromTestTypeID(1).ToString();
            if (MyAppointment.IsLocked == true)
            {
                dateTimePicker1.Enabled = false;
                dateTimePicker1.MinDate = new DateTime(1753, 1, 1, 0, 0, 0);
                Masked_lbl.Visible = true;
                btn_Save.Enabled = false;
            }
            if (MyAppointment.TestAppointmentID!=-1)
            {
                dateTimePicker1.Value = MyAppointment.AppointmentDate;
            }

            


        }

        private void TakeVisionTestAppointment_Load(object sender, EventArgs e)
        {
            lbl_TestType.Text = clsTestTypes.GetTestNameFromTestTypeID(TestType_ID);
            if (TestType_ID==1)
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

            this.Size = new Size(500, 650);
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            MyAppointment.AppointmentDate = dateTimePicker1.Value;
            MyAppointment.Save();
            this.Close();
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            //MyAppointment.AppointmentDate = dateTimePicker1.Value;

        }

       
    }
}

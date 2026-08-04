using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DVLDBusinessLayer
{
    public class clsTestAppointment
    {
        public enum enMode { New=1 , Update=2}

        public enMode Mode = enMode.New;
        public int TestAppointmentID {  get; set; }
        public int TestTypeID { get; set; }
        public int LocalDrivingLicenseAppID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public float Fees {  get; set; }
        public int UserID { get; set; }
        public bool IsLocked { get; set; }
        public int RetakeTestApplicationID {  get; set; }

        clsTestAppointment(int testAppointmentID, int testTypeID, int LDLID, DateTime date,
            float fees, int user_id, bool is_locked, int retake_app_id)
        {
            Mode= enMode.Update;
            TestAppointmentID = testAppointmentID;
            TestTypeID = testTypeID;
            LocalDrivingLicenseAppID = LDLID;
            Fees = fees;
            UserID = user_id;
            IsLocked = is_locked;
            AppointmentDate = date;
            RetakeTestApplicationID = retake_app_id;
        }

        clsTestAppointment( int testTypeID, int LDLID, DateTime date,
            float fees, int user_id, bool is_locked, int retake_app_id)
        {
            Mode= enMode.New;
            TestTypeID = testTypeID;
            LocalDrivingLicenseAppID = LDLID;
            Fees = fees;
            UserID = user_id;
            IsLocked = is_locked;
            AppointmentDate = date;
            RetakeTestApplicationID = retake_app_id;

        }


        public static clsTestAppointment GetEmptyTestAppointment()
        {
            clsTestAppointment TestAppointment = new clsTestAppointment(-1,-1, -1, DateTime.Now, 0, -1, false, -1);
            TestAppointment.Mode = enMode.New;
            return TestAppointment;
        }

        public void Save()
        {
            switch(Mode)
            {
                case enMode.New:
                    this.TestAppointmentID = clsSqlTestAppointment.AddNewTestAppointment(TestTypeID, LocalDrivingLicenseAppID,
                        AppointmentDate, Fees, UserID, IsLocked , RetakeTestApplicationID);
                    break;
                case enMode.Update:
                    clsSqlTestAppointment.UpdateTestAppointment(TestAppointmentID,TestTypeID, LocalDrivingLicenseAppID,
                        AppointmentDate, Fees, UserID, IsLocked, RetakeTestApplicationID);
                    break;
                default: break;

            }
        }

        public static DataTable ListTestsAppointment(int LDLID, int TestTypeID)
        {
            return clsSqlTestAppointment.ListTestsAppointment(LDLID, TestTypeID);
        }

        public static bool HasAppointment(int LDLID, int testTypeID)
        {
            return clsSqlTestAppointment.HasAppointment(LDLID, testTypeID);
        }

        public static clsTestAppointment FindTestAppointmentByAppointmentID(int Appointment_ID)
        {
            int TestType_ID = -1;  int LDL_ID = -1;
            DateTime Appointment_Date=DateTime.Now;  int RetakeApplication_ID=-1;  bool is__Locked=false;  float Fees=0;
            int userId=-1;
            if (clsSqlTestAppointment.FindTestAppointmentByAppointmentID(Appointment_ID, ref  TestType_ID, ref  LDL_ID,
            ref  Appointment_Date, ref  RetakeApplication_ID, ref  is__Locked, ref  Fees,
            ref  userId))
            {
                return new clsTestAppointment(Appointment_ID, TestType_ID, LDL_ID, Appointment_Date, Fees, userId,
                    is__Locked, RetakeApplication_ID);
            }
            else
            {
                return GetEmptyTestAppointment();
            }
        }

        public bool HaveRetakeTestApplicationForTestAppointmentID()
        {
            return clsSqlTestAppointment.HaveRetakeTestApplicationForTestAppointmentID(this.TestAppointmentID);
        }

        public static bool IsAlreadyWinInTestType(int LdlID , int Test___Type)
        {
            return clsSqlTestAppointment.IsAlreadyWinInTestType(LdlID ,Test___Type);
        }

    }
}

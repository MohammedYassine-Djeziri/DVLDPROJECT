using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDBusinessLayer
{
    public class clsTest
    {
        public enum enMode { New=1 , Update=2};
        public enMode Mode { get; set; }
        public int TestID {  get; set; }
        public int TestAppointmentID {  get; set; }
        public int UserID {  get; set; }
        public bool TestResult {  get; set; }
        public string Notes {  get; set; }

        public clsTest(int test_id , int Appointment_id , int user_id , bool result , string note  ) 
        {
            this.Mode = enMode.Update;
            this.TestID = test_id;
            this.TestAppointmentID=Appointment_id;
            this.UserID = user_id;
            this.TestResult = result;
            this.Notes = note;
        }

        public clsTest( int Appointment_id, int user_id, bool result, string note)
        {
            Mode= enMode.New;
            this.TestAppointmentID = Appointment_id;
            this.UserID = user_id;
            this.TestResult = result;
            this.Notes = note;
            this.TestID = -1;
        }

        public void AddNewTest()
        {
            this.TestID=clsSqlTest.AddNewTest(this.TestAppointmentID , this.UserID , this.TestResult ,this.Notes);
        }

        public static clsTest GetEmptyTest()
        {
            clsTest Test = new clsTest(-1, -1, false, "");
            Test.Mode= enMode.New;
            return Test;
        }

    }
}

using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDBusinessLayer
{
    public class clsLicenses
    {
        

        public enum enMode { New = 1, Update = 2 }

        public enMode Mode = enMode.New;
        public int LicenseID { get; set; }
        public int LicenseClassID { get; set; }
        public int ApplicationID { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public float PaidFees { get; set; }
        public int UserID { get; set; }
        public bool IsActive { get; set; }
        public short IssueReason { get; set; }
        public int DriverID {  get; set; }
        public string Notes { get; set; }


        clsLicenses(int license_ID, int license_class_ID, int app_ID, DateTime dateI, DateTime dateE  ,
            float fees, int user_id, bool is_active, short isssue_reason , int driver_id , string notes )
        {
            Mode = enMode.Update;
            LicenseID = license_ID;
            LicenseClassID = license_class_ID;
            ApplicationID = app_ID;
            IssueDate = dateI;
            ExpirationDate = dateE;
            DriverID = driver_id;
            Notes = notes;
            UserID = user_id;
            IsActive = is_active;
            IssueReason = isssue_reason;
            PaidFees = fees;

           
        }


        clsLicenses(int license_class_ID, int app_ID, DateTime dateI, DateTime dateE,
            float fees, int user_id, bool is_active, short isssue_reason, int driver_id, string notes)
        {
            Mode = enMode.New;
            LicenseClassID = license_class_ID;
            ApplicationID = app_ID;
            IssueDate = dateI;
            ExpirationDate = dateE;
            DriverID = driver_id;
            Notes = notes;
            UserID = user_id;
            IsActive = is_active;
            IssueReason = isssue_reason;
            PaidFees = fees;
            this.LicenseID = -1;

        }


        public static clsLicenses GetEmptyLicense()
        {
            clsLicenses license = new clsLicenses( -1,-1 , DateTime.Now, DateTime.Now,0, -1,  false, -1, -1, "");
            license.Mode = enMode.New;
            return license;
        }


        public void Save()
        {
            switch (Mode)
            {
                case enMode.New:
                    this.LicenseID = clsSqlLicenses.AddNewLicense(LicenseClassID,
                       ApplicationID, IssueDate, ExpirationDate, PaidFees,
                        UserID, IsActive, IssueReason, DriverID, Notes);
                    break;
                case enMode.Update:
                    clsSqlLicenses.UpdateLicense(LicenseID, LicenseClassID,
                      ApplicationID, IssueDate, ExpirationDate, PaidFees,
                       UserID, IsActive, IssueReason, DriverID, Notes);
                    break;
                default: break;

            }
        }

        public static string GetIssueReasonByCode(int code)
        {
            if (code == 1) return "First Time";
            else if (code == 2) return "Renew";
            else if (code == 3) return "Replacement for Lost";
            else return "Replacement for damaged";
        }
        public static DataTable ListLicenses()
        {
            return null;
        }


        public static clsLicenses FindLicenseByLicenseID(int license_ID)
        {
            short license_class_ID=-1; int app_ID = -1; DateTime dateI=DateTime.Now; DateTime dateE=DateTime.Now; float fees = 0; 
            int user_id = -1; bool is_active=false;  short isssue_reason = -1; int driver_id = -1; string notes="";

            
            if(clsSqlLicenses.FindLicenseByLicenseID(license_ID, ref license_class_ID, ref app_ID,
               ref dateI, ref dateE, ref fees, ref user_id , ref is_active,
                ref isssue_reason , ref driver_id , ref notes))
            {
                return new clsLicenses(license_ID, license_class_ID, app_ID, dateI, dateE, fees, user_id, is_active, isssue_reason
                    , driver_id, notes);
            }

            return GetEmptyLicense();

        }


        public static clsLicenses FindLicenseByDriverID(int driver_ID)
        {
            int license_ID = -1;
            short license_class_ID = -1; int app_ID = -1; DateTime dateI = DateTime.Now; DateTime dateE = DateTime.Now; float fees = 0;
            int user_id = -1; bool is_active = false; short isssue_reason = -1;  string notes = "";


            if (clsSqlLicenses.FindLicenseByDriverID(ref license_ID, ref license_class_ID, ref app_ID,
               ref dateI, ref dateE, ref fees, ref user_id, ref is_active,
                ref isssue_reason,  driver_ID, ref notes))
            {
                return new clsLicenses(license_ID, license_class_ID, app_ID, dateI, dateE, fees, user_id, is_active, isssue_reason
                    , driver_ID, notes);
            }

            return GetEmptyLicense();

        }

        public static bool IsLicenseHasCreatedFirstTime(int AppID)
        {
            return clsSqlLicenses.IsLicenseHasCreatedFirstTime(AppID);
        }

        public static int GetLicenseIDByAppID(int appID)
        {
            return clsSqlLicenses.GetLicenseIDByAppID(appID);
        }


        public static DataTable ListLicensesByDriverID(int driverID)
        {
            return clsSqlLicenses.ListLicensesByDriverID(driverID);
        }


        public static DataTable ListLicensesByLicenseID(int licenseID)
        {
            return clsSqlLicenses.ListLicensesByDriverID(licenseID);
        }


        public static bool IsLicenseDetained(int licenseID)
        {
            return clsSqlLicenses.IsLicenseDetained(licenseID);
        }


    }
}

using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DVLDBusinessLayer
{
    public class clsInternationalLicense
    {
        public enum enMode { New = 1, Update = 2 }

        public enMode Mode = enMode.New;

        public int InternationalLicenseID { get; set; }
        public int ApplicationID { get; set; }
        public int DriverID { get; set; }
        public int LicenseID { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int UserID { get; set; }
        public bool IsActive { get; set; }


        clsInternationalLicense(int Inter_license_ID, int driver_id,int license_id ,  int app_ID, DateTime dateI, DateTime dateE,
                         int user_id, bool is_active )
        {
            this.Mode = enMode.Update;
            this.InternationalLicenseID = Inter_license_ID;  
            this.ApplicationID = app_ID;
            this.LicenseID = license_id;
            this.IssueDate = dateI;
            this.ExpirationDate = dateE;
            this.DriverID = driver_id;
            this.UserID = user_id;
            this.IsActive = is_active;

        }


        clsInternationalLicense(  int driver_id, int license_id , int app_ID, DateTime dateI, 
            DateTime dateE, int user_id, bool is_active)
        {
            Mode = enMode.New;
            ApplicationID = app_ID;
            IssueDate = dateI;
            ExpirationDate = dateE;
            DriverID = driver_id;
            UserID = user_id;
            IsActive = is_active;

        }


        public static clsInternationalLicense GetEmptyInternationalLicense()
        {
            clsInternationalLicense license = new clsInternationalLicense(-1 ,-1, -1, -1, DateTime.Now, DateTime.Now, -1, false);
            license.Mode = enMode.New;
            return license;
        }


        public void Save()
        {
            switch (Mode)
            {
                case enMode.New:
                    this.InternationalLicenseID = clsSqlInternationalLicense.AddNewLicense(DriverID,
                    LicenseID,ApplicationID , IssueDate, ExpirationDate, UserID, IsActive);
                    break;
                case enMode.Update:
                    clsSqlInternationalLicense.UpdateLicense(InternationalLicenseID,
                    DriverID, LicenseID, ApplicationID, IssueDate, ExpirationDate,
                    UserID, IsActive);
                    break;
                default: break;

            }
        }

        
        public static DataTable ListInternationalLicensesByDriverID(int driver_id)
        {
            return clsSqlInternationalLicense.ListInternationalLicensesByDriverID(driver_id);
        }


        public static clsInternationalLicense FindLicenseByLicenseID(int licence_id)
        {


             int app_ID = -1; DateTime dateI = DateTime.Now; DateTime dateE = DateTime.Now; 
            int user_id = -1; bool is_active = false; int driver_id = -1; int Inter_license_ID = -1;


            if (clsSqlInternationalLicense.FindLicenseByLicenseID(ref Inter_license_ID, ref driver_id,  licence_id,
             ref app_ID , ref dateI, ref dateE,  ref user_id, ref is_active))
            {
                return new clsInternationalLicense(Inter_license_ID ,driver_id , licence_id 
                    , app_ID, dateI, dateE,  user_id, is_active);
            }

            return GetEmptyInternationalLicense();

        }


        public static clsInternationalLicense FindLicenseByInterLicenseID(int inter_licence_id)
        {


            int app_ID = -1; DateTime dateI = DateTime.Now; DateTime dateE = DateTime.Now;
            int user_id = -1; bool is_active = false; int driver_id = -1; int  license_ID = -1;


            if (clsSqlInternationalLicense.FindLicenseByInternationalLicenseID(inter_licence_id, ref driver_id, ref license_ID,
             ref app_ID, ref dateI, ref dateE, ref user_id, ref is_active))
            {
                //File.AppendAllText("output.txt" , inter_licence_id.ToString() + "|" + driver_id.ToString() + "|" + license_ID.ToString() + "|" + app_ID.ToString() + "|" + dateI.ToString() + "|" + dateE.ToString() + "|" + user_id.ToString() + "|" + is_active.ToString() + "\n");

                return new clsInternationalLicense(inter_licence_id, driver_id, license_ID
                    , app_ID, dateI, dateE, user_id, is_active);
            }

            return GetEmptyInternationalLicense();

        }

        public static clsInternationalLicense FindLicenseByDriverID(int driver_id )
        {


            int app_ID = -1; DateTime dateI = DateTime.Now; DateTime dateE = DateTime.Now;
            int user_id = -1; bool is_active = false; int licence_id = -1; int Inter_license_ID = -1;


            if (clsSqlInternationalLicense.FindLicenseByDriverID(ref Inter_license_ID,  driver_id, ref licence_id,
             ref app_ID, ref dateI, ref dateE, ref user_id, ref is_active))
            {
                return new clsInternationalLicense(Inter_license_ID, driver_id, licence_id
                    , app_ID, dateI, dateE, user_id, is_active);
            }

            return GetEmptyInternationalLicense();

        }

        public static bool IsDriverAlreadyHaveInternationalLicense(int Driver_ID)
        {
            return clsSqlInternationalLicense.IsDriverAlreadyHaveInternationalLicense(Driver_ID);
        }

        public static DataTable ListInternationalLicenses()
        {
            return clsSqlInternationalLicense.ListInternationalLicenses();
        }

    }
}

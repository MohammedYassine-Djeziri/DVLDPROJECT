using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDBusinessLayer
{
    public class clsDetainedLicense
    {
        public enum enMode { New = 1, Update = 2 }

        public enMode Mode = enMode.New;
        public int LicenseID { get; set; }
        public int DetainedID { get; set; }
        public int ApplicationID { get; set; }
        public DateTime DetainDate { get; set; }
        public DateTime ReleaseDate { get; set; }
        public float FineFees { get; set; }
        public int DetainedUserID { get; set; }
        public int ReleasedUserID { get; set; }
        public bool IsReleased { get; set; }


        clsDetainedLicense(int detained_ID , int license_ID, int app_ID, DateTime dateDet, DateTime dateRel,
            float fees, int det_user_id, int rel_user_id,  bool is_released)
        {
            Mode = enMode.Update;
            LicenseID = license_ID;
            DetainedID = detained_ID;
            ApplicationID = app_ID;
            DetainDate = dateDet;
            ReleaseDate = dateRel;
            ReleasedUserID = rel_user_id;
            DetainedUserID = det_user_id;
            FineFees = fees;
            IsReleased = is_released;

        }


        clsDetainedLicense(int license_ID, int app_ID, DateTime dateDet, DateTime dateRel,
            float fees, int det_user_id, int rel_user_id, bool is_released) // add new bcz i don't give you pk id if i have the pk id why i did not give it to you
        {
            Mode = enMode.New;
            LicenseID = license_ID;
            ApplicationID = app_ID;
            DetainDate = dateDet;
            ReleaseDate = dateRel;
            ReleasedUserID = rel_user_id;
            DetainedUserID = det_user_id;
            FineFees = fees;
            IsReleased = is_released;

        }

        public static clsDetainedLicense GetEmptyLicense()
        {
            clsDetainedLicense detained_license = new clsDetainedLicense(-1, -1, DateTime.Now, DateTime.Now, 0,-1 , -1, false);
            detained_license.Mode = enMode.New;
            return detained_license;
        }


        public void Save()
        {
            switch (Mode)
            {
                case enMode.New:
                    this.DetainedID = clsSqlDetainedLicense.AddNewDetainedLicense(LicenseID,
                       ApplicationID, DetainDate, ReleaseDate, FineFees,
                        ReleasedUserID , DetainedUserID, IsReleased);
                    break;
                case enMode.Update:
                    clsSqlDetainedLicense.UpdateDetainedLicense(DetainedID, LicenseID,
                       ApplicationID, DetainDate, ReleaseDate, FineFees,
                        ReleasedUserID, DetainedUserID, IsReleased);
                    break;
                default: break;

            }
        }

      


        public static clsDetainedLicense FindDetainedLicenseByLicenseID(int license_ID)
        {
            int detained_license_id = -1; int app_ID = -1; DateTime date_det = DateTime.Now; DateTime date_rel = DateTime.Now; float fees = 0;
            int det_user_id = -1; int rel_user_id = -1; bool is_released = false;

            if (clsSqlDetainedLicense.FindDetainedLicenseByLicenseID(ref detained_license_id , license_ID, ref app_ID, ref date_det,
               ref date_rel, ref fees, ref rel_user_id, ref det_user_id,
                ref is_released))
            {
                return new clsDetainedLicense(detained_license_id ,  license_ID, app_ID, date_det, date_rel, fees, det_user_id, rel_user_id, is_released
                    );
            }

            return GetEmptyLicense();

        }
        


        public static clsDetainedLicense FindDetainedLicenseByDetainedID(int detain_ID)
        {
            int license_id = -1; int app_ID = -1; DateTime date_det = DateTime.Now; DateTime date_rel = DateTime.Now; float fees = 0;
            int det_user_id = -1; int rel_user_id = -1; bool is_released = false;

            if (clsSqlDetainedLicense.FindDetainedLicenseByDetainedLicenseID(detain_ID, ref license_id, 
                ref app_ID, ref date_det, ref date_rel, ref fees, ref rel_user_id, ref det_user_id,
                ref is_released))
            {
                return new clsDetainedLicense(detain_ID, license_id, app_ID, date_det, date_rel, fees,
                    det_user_id, rel_user_id, is_released );
            }

            return GetEmptyLicense();

        }


        public static DataTable ListDetainedLicenses()
        {
            return clsSqlDetainedLicense.ListDetainedLicenses();
        }




        //public static bool IsLicenseHasCreatedFirstTime(int AppID)
        //{
        //    return clsSqlLicenses.IsLicenseHasCreatedFirstTime(AppID);
        //}

        //public static int GetLicenseIDByAppID(int appID)
        //{
        //    return clsSqlLicenses.GetLicenseIDByAppID(appID);
        //}


        //public static DataTable ListLicensesByDriverID(int driverID)
        //{
        //    return clsSqlLicenses.ListLicensesByDriverID(driverID);
        //}


        //public static DataTable ListLicensesByLicenseID(int licenseID)
        //{
        //    return clsSqlLicenses.ListLicensesByDriverID(licenseID);
        //}


        //public static bool IsLicenseDetained(int licenseID)
        //{
        //    return clsSqlLicenses.IsLicenseDetained(licenseID);
        //}
    }
}

using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DVLDBusinessLayer
{
    public class clsLocalDrivingLicenseApp
    {
        enum EnMode { New = 0, Update = 1 }
        EnMode Mode { get; set; }
        public int LocalDrivingLicenseAppID { get; set; }
        public int ApplicationID { get; set; }
        public int LicenseClassID { get; set; }

        public clsLocalDrivingLicenseApp(int localDrivingLicenseAppID, int applicationID, int licenseClassID)
        {
            this.Mode = EnMode.Update;
            this.LocalDrivingLicenseAppID = localDrivingLicenseAppID;
            this.ApplicationID = applicationID;
            this.LicenseClassID = licenseClassID;
        }

        clsLocalDrivingLicenseApp(int applicationID, int licenseClassID)
        {
            this.Mode = EnMode.New;
            this.ApplicationID = applicationID;
            this.LicenseClassID = licenseClassID;
            this.LocalDrivingLicenseAppID = -1;
        }

        public static clsLocalDrivingLicenseApp GetEmptyLocalDrivingLicenseApplication()
        {
            clsLocalDrivingLicenseApp Local = new clsLocalDrivingLicenseApp( -1, -1);
            Local.Mode = EnMode.New;
            return Local;
        }

        public static clsLocalDrivingLicenseApp FindLDLAppByLDLAppID(int LDLID)
        {
            int AppID = -1; int licenseClassID = -1;
            if (clsSqlLocalDrivingLicenseApp.FindLDLAppByLDLAppID(LDLID, ref AppID, ref licenseClassID))
            {
                return new clsLocalDrivingLicenseApp(LDLID, AppID, licenseClassID);
            }
            else
            {
                return GetEmptyLocalDrivingLicenseApplication();
            }
        }

        public static int GetClassNameByAppID(int app_id)
        {
            return clsSqlLocalDrivingLicenseApp.GetClassNameByAppID(app_id);
        }
        public void Save()
        {
            switch(Mode)
            {
                case EnMode.New:
                    this.LocalDrivingLicenseAppID = clsSqlLocalDrivingLicenseApp.AddNewLocalDrivingLicenseApplication(
                        ApplicationID, LicenseClassID);
                    this.Mode = EnMode.Update;
                    break; 
                case EnMode.Update:        
                    clsSqlLocalDrivingLicenseApp.UpdateLocalDrivingLicenseApplication(LocalDrivingLicenseAppID,
                        ApplicationID , LicenseClassID);
                    break;
            }
        }

    }
}
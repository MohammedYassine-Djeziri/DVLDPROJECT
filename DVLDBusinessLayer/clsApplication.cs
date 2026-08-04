using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace DVLDBusinessLayer
{
    public class clsApplication
    {
        public enum EnMode { New = 0, Update = 1 }
        public int ApplicationID { get; set; }
        public int PersonID { get; set; }
        public DateTime ApplicationDate { get; set; }
        public int ApplicationType { get; set; }
        public int ApplicationStatus { get; set; }
        public float ApplicationFees { get; set; }
        public int UserId { get; set; }
        public DateTime LastStatusDate { get; set; }
        public EnMode Mode { get; set; }


        public clsApplication(int applicationID, int personID, DateTime applicationDate, int applicationType, int applicationStatus
            , float applicationFees, int userId, DateTime lastStatusDate)
        {
            Mode = EnMode.Update;
            ApplicationID = applicationID;
            PersonID = personID;
            ApplicationDate = applicationDate;
            ApplicationType = applicationType;
            ApplicationStatus = applicationStatus;
            ApplicationFees = applicationFees;
            UserId = userId;
            LastStatusDate = lastStatusDate;
        }

        clsApplication( int personID, DateTime applicationDate, int applicationType, int applicationStatus
             , float applicationFees, int userId, DateTime lastStatusDate)
        {
            Mode = EnMode.New;
            PersonID = personID;
            ApplicationDate = applicationDate;
            ApplicationType = applicationType;
            ApplicationStatus = applicationStatus;
            ApplicationFees = applicationFees;
            UserId = userId;
            LastStatusDate = lastStatusDate;
        }

        public static clsApplication GetEmptyApplication()
        {
            clsApplication App = new clsApplication(-1, -1, DateTime.Now, -1, -1, -1, -1, DateTime.Now);
            App.Mode=EnMode.New;
            return App;
        }
        
        public void Save()
        {
           switch (Mode)
           {
                case EnMode.New:
                     this.ApplicationID =clsSqlApplications.AddNewApplication(PersonID, ApplicationDate, ApplicationType, 
                        ApplicationStatus, LastStatusDate, ApplicationFees, UserId);
                    this.Mode = EnMode.Update;
                    break;
                case EnMode.Update:
                    clsSqlApplications.UpdateApplication(ApplicationID, PersonID, ApplicationDate, ApplicationType,
                        ApplicationStatus, LastStatusDate, ApplicationFees, UserId);
                    break;
           }
        }
        

        public  bool IsLicenseClassAlreadyUsed(int LicenseClass)
        {
            return clsSqlApplications.IsLicenseClassAlreadyUsed(this.PersonID, LicenseClass);
        }


        public static DataTable ListLDLApplication()
        {
            return clsSqlApplications.ListLDLApplication();
        }

        public static clsApplication FindApplicationByLDLID(int LDLID)
        {
            int AppID=-1;  int personID=-1;  DateTime applicationDate = DateTime.Now;
            int applicationType=-1; int applicationStatus=-1; float applicationFees=-1;
            int userId=-1; DateTime lastStatusDate= DateTime.Now;
            if (clsSqlApplications.FindApplicationByLDLID(LDLID , ref  AppID , ref  personID, ref  applicationDate, 
            ref  applicationType,ref  applicationStatus , ref  applicationFees, 
            ref   userId, ref  lastStatusDate))
            {
                return new clsApplication(AppID, personID, applicationDate, applicationType, applicationStatus, applicationFees,
                        userId, lastStatusDate);
            }
            else
            {
                return GetEmptyApplication();
            }
        }

        public static clsApplication FindApplicationByAppID(int AppID)
        {
            int personID = -1; DateTime applicationDate = DateTime.Now;
            int applicationType = -1; int applicationStatus = -1; float applicationFees = -1;
            int userId = -1; DateTime lastStatusDate = DateTime.Now;
            if (clsSqlApplications.FindApplicationByAppID( AppID, ref personID, ref applicationDate,
            ref applicationType, ref applicationStatus, ref applicationFees,
            ref userId, ref lastStatusDate))
            {
                return new clsApplication(AppID, personID, applicationDate, applicationType, applicationStatus, applicationFees,
                        userId, lastStatusDate);
            }
            else
            {
                return GetEmptyApplication();
            }
        }

        public bool ChangeStatus(int NewStatus)
        {
            return clsSqlApplications.ChangeStatus(this.ApplicationID, NewStatus);
        }


       public static bool DeleteApplicationByLDLID(int LDLAppID)
        {
            return clsSqlApplications.DeleteApplicationByLDLID(LDLAppID);
        }
    }
}
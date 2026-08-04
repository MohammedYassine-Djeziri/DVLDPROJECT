using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DVLDBusinessLayer
{
    public class clsDriver
    {

        public enum enMode { New = 1, Update = 2 }

        public enMode Mode = enMode.New;
        
        public int DriverID { get; set; }
        public int PersonID { get; set; }
        public int UserID { get; set; }
        public DateTime CreatedDate { get; set; }


        clsDriver(int driver_ID, int person_ID, int user_ID, DateTime date)
        {
            Mode = enMode.Update;
            PersonID = person_ID;
            DriverID = driver_ID;
            UserID = user_ID;
            CreatedDate = date;


        }


        clsDriver( int person_ID, int user_ID, DateTime date)
        {
            Mode = enMode.New;
            PersonID = person_ID;
            UserID = user_ID;
            CreatedDate = date;

        }


        public static clsDriver GetEmptyDriver()
        {
            clsDriver driver = new clsDriver(-1, -1,-1, DateTime.Now);
            driver.Mode = enMode.New;
            return driver;
        }


        public void Save()
        {
            switch (Mode)
            {
                case enMode.New:
                    this.DriverID = clsSqlDriver.AddNewDriver(PersonID, UserID, CreatedDate);
                    break;
                case enMode.Update:
                   clsSqlDriver.UpdateDriver(DriverID , PersonID, UserID, CreatedDate);
                    break;
                default: break;

            }
        }


        public static DataTable ListDrivers() { 
            return clsSqlDriver.ListDrivers();
        }


        public static clsDriver FindDriverExistByPersonID(int PerID)
        {
            int driver_ID = -1; int user_ID = -1; DateTime date = DateTime.Now;

            if( clsSqlDriver.IsDriverExistByPersonID(PerID ,ref driver_ID , ref user_ID , ref date))
            {
                return new clsDriver(driver_ID, PerID, user_ID, date);
            }
            else
            {
                return GetEmptyDriver();
            }
        }


        public static clsDriver FindDriverByDriverID(int driver_id)
        {
            int person_ID = -1; int user_ID = -1; DateTime date = DateTime.Now;

            if (clsSqlDriver.FindDriverByDriverID(driver_id, ref person_ID, ref user_ID, ref date))
            {
                return new clsDriver(driver_id, person_ID, user_ID, date);
            }
            else
            {
                return GetEmptyDriver();
            }
        }

    }
}

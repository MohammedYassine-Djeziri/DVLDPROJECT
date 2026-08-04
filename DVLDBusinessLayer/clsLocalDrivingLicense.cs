using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDBusinessLayer
{
    public class clsLocalDrivingLicense
    {
        int LicenseID {  get; set; }
        int PersonID { get; set; }



        public static bool IsLicenseExistsByLDLAppID(int License_id)
        {
            return clsSqlLocalDrivingLicense.IsLicenseExistsByLDLAppID(License_id);
        }

    }
}

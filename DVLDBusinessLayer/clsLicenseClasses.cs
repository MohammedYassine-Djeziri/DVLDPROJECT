using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDBusinessLayer
{
    public class clsLicenseClasses
    {
        public static DataTable ListLicenseClasses()
        {
            return clsSqlLicenseClasses.ListLicenseClasses();
        }

        public static string GetLicenseClassNameFromClassID(int class_id)
        {
            return clsSqlLicenseClasses.GetLicenseClassNameFromClassID(class_id);
        }

        public static int GetLicenseValidityLengthFromClassID(string class_name)
        {
            return clsSqlLicenseClasses.GetLicenseValidityLengthFromClassID(class_name);
        }



        public static float FindLicenseFeesByLicenseClassID(int Type)
        {
            return clsSqlLicenseClasses.FindLicenseFeesByLicenseClassID(Type);
        }

        public static float FindLicenseFeesByLicenseClassName(string  Type)
        {
            return clsSqlLicenseClasses.FindLicenseFeesByLicenseClassName(Type);
        }
    }
    }

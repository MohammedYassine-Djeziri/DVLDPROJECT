using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDBusinessLayer
{
    public class clsApplicationTypes
    {

        public static DataTable ListApplicationTypes()
        {
            return clsSqlApplicationTypes.ListApplicationTypes();
        }

        public static void UpdateApplicationTypes(int id, string title, float fees)
        {
            clsSqlApplicationTypes.UpdateApplicationTypes(id, title, fees);
        }

        public static float FindAppFeesByAppTitle(string Type)
        {
            return clsSqlApplicationTypes.FindAppFeesByAppTitle(Type);
        }

        public static float FindAppFeesByAppTypeID(int Type)
        {
            return clsSqlApplicationTypes.FindAppFeesByAppTypeID(Type);
        }

        public static string GetApplicationTypeNameByAppTypeID(int AppType_id)
        {
            return clsSqlApplicationTypes.GetApplicationTypeNameByAppTypeID(AppType_id);
        }

    }
}

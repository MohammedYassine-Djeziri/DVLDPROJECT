using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDBusinessLayer
{
    public class clsTestTypes
    {
        public static DataTable ListTestTypes()
        {
            return clsSqlTestTypes.ListTestTypes();
        }

        public static void UpdateTestTypes(int id, string title, string des, float fees)
        {
            clsSqlTestTypes.UpdateTestTypes(id, title, des, fees);
        }

        public static float GetTestFeesFromTestTypeID(int id)
        {
            return clsSqlTestTypes.GetTestFeesFromTestTypeID(id);
        }


        public static string GetTestNameFromTestTypeID(int id)
        {
            return clsSqlTestTypes.GetTestNameFromTestTypeID(id);
        }
    }
}

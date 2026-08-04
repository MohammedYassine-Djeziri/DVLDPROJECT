using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class clsSqlLicenseClasses
    {
        public static string DVLD_Connection_Info => clsConnectionSettings.ConnectionString;

        public static DataTable ListLicenseClasses()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(DVLD_Connection_Info);
            string q = "select * from LicenseClasses;";
            SqlCommand command = new SqlCommand(q, connection);
            connection.Open();
            try
            {
                SqlDataReader Result = command.ExecuteReader();
                if (Result.HasRows)
                {
                    dt.Load(Result);

                }
            }
            catch { }
            finally { connection.Close(); }
            return dt;

        }

        public static string GetLicenseClassNameFromClassID(int class_id)
        {
            string Class_Name ="";
            SqlConnection connection = new SqlConnection(DVLD_Connection_Info);
            string q = "select LicenseClasses.ClassName from LicenseClasses where LicenseClassID=@class_id;";
            SqlCommand command = new SqlCommand(q, connection);
            command.Parameters.AddWithValue("@class_id", class_id);

            connection.Open();
            try
            {
                SqlDataReader Result = command.ExecuteReader();
                if (Result.Read())
                {
                    Class_Name=Result[0].ToString();
                }
            }
            catch { }
            finally { connection.Close(); }
            return Class_Name;

        }


        public static int GetLicenseValidityLengthFromClassID(string class_id)
        {
            int Class_Validity = 0;
            SqlConnection connection = new SqlConnection(DVLD_Connection_Info);
            string q = "select LicenseClasses.DefaultValidityLength from LicenseClasses where ClassName=@class_id;";
            SqlCommand command = new SqlCommand(q, connection);
            command.Parameters.AddWithValue("@class_id", class_id);

            connection.Open();
            try
            {
                SqlDataReader Result = command.ExecuteReader();
                if (Result.Read())
                {
                    Class_Validity = Convert.ToInt32(Result[0]);
                }
            }
            catch { }
            finally { connection.Close(); }
            return Class_Validity;

        }



        public static float FindLicenseFeesByLicenseClassID(int Type)
        {
            float Fees = 0;
            SqlConnection connection = new SqlConnection(DVLD_Connection_Info);
            string q = "select LicenseClasses.ClassFees from LicenseClasses where LicenseClasses.LicenseClassID = @type;";
            SqlCommand command = new SqlCommand(q, connection);
            command.Parameters.AddWithValue("@type", Type);
            connection.Open();
            try
            {
                SqlDataReader Result = command.ExecuteReader();
                if (Result.Read())
                {
                    Fees = Convert.ToInt32(Result[0]);

                }
            }
            catch
            {
            }
            finally { connection.Close(); }
            return Fees;

        }


        public static float FindLicenseFeesByLicenseClassName(string Type)
        {
            float Fees = 0;
            SqlConnection connection = new SqlConnection(DVLD_Connection_Info);
            string q = "select LicenseClasses.ClassFees from LicenseClasses where ClassName = @type;";
            SqlCommand command = new SqlCommand(q, connection);
            command.Parameters.AddWithValue("@type", Type);
            connection.Open();
            try
            {
                SqlDataReader Result = command.ExecuteReader();
                if (Result.Read())
                {
                    Fees = Convert.ToInt32(Result[0]);

                }
            }
            catch
            {
            }
            finally { connection.Close(); }
            return Fees;

        }

    }
}

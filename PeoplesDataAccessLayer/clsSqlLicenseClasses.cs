using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class clsSqlLicenseClasses
    {
        public static DataTable ListLicenseClasses()
        {
            DataTable dt = new DataTable();
            var connection = clsDatabaseFactory.CreateConnection();
            // Simple SELECT – auto-convert
            string q = clsDatabaseFactory.GetQuery("select * from LicenseClasses;");
            var command = clsDatabaseFactory.CreateCommand(q, connection);
            connection.Open();
            try
            {
                IDataReader Result = command.ExecuteReader();
                dt.Load(Result);
            }
            catch { }
            finally { connection.Close(); }
            return dt;
        }

        public static string GetLicenseClassNameFromClassID(int class_id)
        {
            string Class_Name = "";
            var connection = clsDatabaseFactory.CreateConnection();
            // Simple SELECT – auto-convert
            string q = clsDatabaseFactory.GetQuery("select LicenseClasses.ClassName from LicenseClasses where LicenseClassID=@class_id;");
            var command = clsDatabaseFactory.CreateCommand(q, connection);
            clsDatabaseFactory.AddParam(command, "@class_id", class_id);

            connection.Open();
            try
            {
                IDataReader Result = command.ExecuteReader();
                if (Result.Read())
                {
                    Class_Name = Result[0].ToString();
                }
            }
            catch { }
            finally { connection.Close(); }
            return Class_Name;
        }


        public static int GetLicenseValidityLengthFromClassID(string class_id)
        {
            int Class_Validity = 0;
            var connection = clsDatabaseFactory.CreateConnection();
            // Simple SELECT – auto-convert
            string q = clsDatabaseFactory.GetQuery("select LicenseClasses.DefaultValidityLength from LicenseClasses where ClassName=@class_id;");
            var command = clsDatabaseFactory.CreateCommand(q, connection);
            clsDatabaseFactory.AddParam(command, "@class_id", class_id);

            connection.Open();
            try
            {
                IDataReader Result = command.ExecuteReader();
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
            var connection = clsDatabaseFactory.CreateConnection();
            // Simple SELECT – auto-convert
            string q = clsDatabaseFactory.GetQuery("select LicenseClasses.ClassFees from LicenseClasses where LicenseClasses.LicenseClassID = @type;");
            var command = clsDatabaseFactory.CreateCommand(q, connection);
            clsDatabaseFactory.AddParam(command, "@type", Type);
            connection.Open();
            try
            {
                IDataReader Result = command.ExecuteReader();
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
            var connection = clsDatabaseFactory.CreateConnection();
            // Simple SELECT – auto-convert
            string q = clsDatabaseFactory.GetQuery("select LicenseClasses.ClassFees from LicenseClasses where ClassName = @type;");
            var command = clsDatabaseFactory.CreateCommand(q, connection);
            clsDatabaseFactory.AddParam(command, "@type", Type);
            connection.Open();
            try
            {
                IDataReader Result = command.ExecuteReader();
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
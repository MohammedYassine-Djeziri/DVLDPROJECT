using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class clsSqlLocalDrivingLicenseApp
    {
        public static string DVLD_Connection_Info => clsConnectionSettings.ConnectionString;
        public static int AddNewLocalDrivingLicenseApplication(int AppID, int LicenseClassID)
        {
            int ID = -1;
            SqlConnection connection = new SqlConnection(DVLD_Connection_Info);
            string q = " INSERT INTO [dbo].[LocalDrivingLicenseApplications] ([ApplicationID] ,[LicenseClassID])" +
                "VALUES (@AppID, @LicenseClassID ) ; SELECT SCOPE_IDENTITY() ;";
            connection.Open();
            SqlCommand command = new SqlCommand(q, connection);
            command.Parameters.AddWithValue("@AppID", AppID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

            try
            {

                object result = command.ExecuteScalar();
                if (result != null)
                {
                    ID = Convert.ToInt32(result);
                }

            }

            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();

            }
            return ID;
        }

        public static bool UpdateLocalDrivingLicenseApplication(int LDLAppID , int AppID, int LicenseClassID)
        {
            bool IsUpdated=false;
            SqlConnection connection = new SqlConnection(DVLD_Connection_Info);
            string q = "UPDATE [dbo].[LocalDrivingLicenseApplications] SET [ApplicationID] = @AppID " +
                ",[LicenseClassID] = @LicenseClassID WHERE LocalDrivingLicenseApplicationID =@LDLAppID ";
            connection.Open();
            SqlCommand command = new SqlCommand(q, connection);
            command.Parameters.AddWithValue("@LDLAppID", LDLAppID);
            command.Parameters.AddWithValue("@AppID", AppID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

            try
            {
                int result = command.ExecuteNonQuery();
                if (result >= 1) 
                {
                    IsUpdated = true;
                }

            }

            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();

            }
            return IsUpdated;
        }


        public static bool FindLDLAppByLDLAppID(int LDLID, ref int AppID, ref int LicenseClassID)
        {
            bool IsExist = false;
            SqlConnection connection = new SqlConnection(DVLD_Connection_Info);
            string q = "select * from LocalDrivingLicenseApplications where " +
                "LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID =@LDLID;";
            connection.Open();
            SqlCommand cmd = new SqlCommand(q, connection);
            cmd.Parameters.AddWithValue("@LDLID", LDLID);

            try
            {
                SqlDataReader r = cmd.ExecuteReader();
                if (r.Read())
                {
                    AppID = Convert.ToInt32(r[1]);
                    LicenseClassID = Convert.ToInt32(r[2]);
                    IsExist = true;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }


            return IsExist;
        }



        public static int GetAppIDByLDLID(int LDLID )
        {
            int AppID = 0;
            SqlConnection connection = new SqlConnection(DVLD_Connection_Info);
            string q = "select LocalDrivingLicenseApplications.ApplicationID from LocalDrivingLicenseApplications " +
                "where LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID  =@LDLID;";
            connection.Open();
            SqlCommand cmd = new SqlCommand(q, connection);
            cmd.Parameters.AddWithValue("@LDLID", LDLID);

            try
            {
                SqlDataReader r = cmd.ExecuteReader();
                if (r.Read())
                {
                    AppID = Convert.ToInt32(r[0]);
                    
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }


            return AppID;
        }


        public static int GetClassNameByAppID(int AppID)
        {
            int ClassID = -1;
            SqlConnection connection = new SqlConnection(DVLD_Connection_Info);
            string q = "select LocalDrivingLicenseApplications.LicenseClassID from " +
                "LocalDrivingLicenseApplications " +
                "where LocalDrivingLicenseApplications.ApplicationID  =@AppID;";
            connection.Open();
            SqlCommand cmd = new SqlCommand(q, connection);
            cmd.Parameters.AddWithValue("@AppID", AppID);

            try
            {
                SqlDataReader r = cmd.ExecuteReader();
                if (r.Read())
                {
                    ClassID = Convert.ToInt32(r[0]);

                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }


            return ClassID;
        }

    }
}
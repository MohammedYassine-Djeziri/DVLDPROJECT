using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class clsSqlLocalDrivingLicenseApp
    {
        public static int AddNewLocalDrivingLicenseApplication(int AppID, int LicenseClassID)
        {
            int ID = -1;
            var connection = clsDatabaseFactory.CreateConnection();

            // INSERT with SCOPE_IDENTITY → explicit PG RETURNING version
            string q = clsDatabaseFactory.GetQuery(
                " INSERT INTO [dbo].[LocalDrivingLicenseApplications] ([ApplicationID] ,[LicenseClassID])" +
                "VALUES (@AppID, @LicenseClassID ) ; SELECT SCOPE_IDENTITY() ;",

                " INSERT INTO \"LocalDrivingLicenseApplications\" (\"ApplicationID\" ,\"LicenseClassID\")" +
                "VALUES (@AppID, @LicenseClassID ) RETURNING \"LocalDrivingLicenseApplicationID\" ;");

            connection.Open();
            var command = clsDatabaseFactory.CreateCommand(q, connection);
            clsDatabaseFactory.AddParam(command, "@AppID", AppID);
            clsDatabaseFactory.AddParam(command, "@LicenseClassID", LicenseClassID);

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

        public static bool UpdateLocalDrivingLicenseApplication(int LDLAppID, int AppID, int LicenseClassID)
        {
            bool IsUpdated = false;
            var connection = clsDatabaseFactory.CreateConnection();

            // Simple UPDATE – auto-convert
            string q = clsDatabaseFactory.GetQuery(
                "UPDATE [dbo].[LocalDrivingLicenseApplications] SET [ApplicationID] = @AppID " +
                ",[LicenseClassID] = @LicenseClassID WHERE LocalDrivingLicenseApplicationID =@LDLAppID ");

            connection.Open();
            var command = clsDatabaseFactory.CreateCommand(q, connection);
            clsDatabaseFactory.AddParam(command, "@LDLAppID", LDLAppID);
            clsDatabaseFactory.AddParam(command, "@AppID", AppID);
            clsDatabaseFactory.AddParam(command, "@LicenseClassID", LicenseClassID);

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
            var connection = clsDatabaseFactory.CreateConnection();

            // Simple SELECT – auto-convert
            string q = clsDatabaseFactory.GetQuery(
                "select * from LocalDrivingLicenseApplications where " +
                "LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID =@LDLID;");

            connection.Open();
            var cmd = clsDatabaseFactory.CreateCommand(q, connection);
            clsDatabaseFactory.AddParam(cmd, "@LDLID", LDLID);

            try
            {
                IDataReader r = cmd.ExecuteReader();
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


        public static int GetAppIDByLDLID(int LDLID)
        {
            int AppID = 0;
            var connection = clsDatabaseFactory.CreateConnection();

            // Simple SELECT – auto-convert
            string q = clsDatabaseFactory.GetQuery(
                "select LocalDrivingLicenseApplications.ApplicationID from LocalDrivingLicenseApplications " +
                "where LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID  =@LDLID;");

            connection.Open();
            var cmd = clsDatabaseFactory.CreateCommand(q, connection);
            clsDatabaseFactory.AddParam(cmd, "@LDLID", LDLID);

            try
            {
                IDataReader r = cmd.ExecuteReader();
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
            var connection = clsDatabaseFactory.CreateConnection();

            // Simple SELECT – auto-convert
            string q = clsDatabaseFactory.GetQuery(
                "select LocalDrivingLicenseApplications.LicenseClassID from " +
                "LocalDrivingLicenseApplications " +
                "where LocalDrivingLicenseApplications.ApplicationID  =@AppID;");

            connection.Open();
            var cmd = clsDatabaseFactory.CreateCommand(q, connection);
            clsDatabaseFactory.AddParam(cmd, "@AppID", AppID);

            try
            {
                IDataReader r = cmd.ExecuteReader();
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
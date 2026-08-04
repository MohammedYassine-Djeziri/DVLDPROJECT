using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using System.Security.AccessControl;
using System.ComponentModel;
using System.Windows.Input;

namespace DataAccessLayer
{
    public class clsSqlLicenses
    {
        public static int AddNewLicense(int license_class_ID, int app_ID, DateTime dateI, DateTime dateE,
            float fees, int user_id, bool is_active, short isssue_reason, int driver_id, string notes)
        {
            int ID = -1;
            var connection = clsDatabaseFactory.CreateConnection();

            // INSERT with SCOPE_IDENTITY → explicit PG RETURNING version
            string q = clsDatabaseFactory.GetQuery(
                " INSERT INTO [dbo].[Licenses] ([ApplicationID] ,[DriverID] ,[LicenseClass] ,[IssueDate] ,[ExpirationDate] " +
                ",[Notes] ,[PaidFees] ,[IsActive] ,[IssueReason]  ,[CreatedByUserID]) VALUES ( @app_ID, @driver_id," +
                " @license_class_ID, @dateI, @dateE, @notes,  @fees, @is_active, @isssue_reason, @user_id ) ; SELECT SCOPE_IDENTITY() ; ",

                " INSERT INTO \"Licenses\" (\"ApplicationID\" ,\"DriverID\" ,\"LicenseClass\" ,\"IssueDate\" ,\"ExpirationDate\" " +
                ",\"Notes\" ,\"PaidFees\" ,\"IsActive\" ,\"IssueReason\"  ,\"CreatedByUserID\") VALUES ( @app_ID, @driver_id," +
                " @license_class_ID, @dateI, @dateE, @notes,  @fees, @is_active, @isssue_reason, @user_id ) RETURNING \"LicenseID\" ; ");

            connection.Open();
            var command = clsDatabaseFactory.CreateCommand(q, connection);
            clsDatabaseFactory.AddParam(command, "@license_class_ID", license_class_ID);
            clsDatabaseFactory.AddParam(command, "@app_ID", app_ID);
            clsDatabaseFactory.AddParam(command, "@dateI", dateI);
            clsDatabaseFactory.AddParam(command, "@dateE", dateE);
            clsDatabaseFactory.AddParam(command, "@fees", fees);
            clsDatabaseFactory.AddParam(command, "@user_id", user_id);
            clsDatabaseFactory.AddParam(command, "@is_active", is_active);
            clsDatabaseFactory.AddParam(command, "@isssue_reason", isssue_reason);
            clsDatabaseFactory.AddParam(command, "@driver_id", driver_id);
            clsDatabaseFactory.AddParam(command, "@notes", notes);

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

        public static bool UpdateLicense(int license_ID, int license_class_ID, int app_ID, DateTime dateI, DateTime dateE,
            float fees, int user_id, bool is_active, short isssue_reason, int driver_id, string notes)
        {
            int r = 0;
            var connection = clsDatabaseFactory.CreateConnection();

            // Simple UPDATE – auto-convert
            string q = clsDatabaseFactory.GetQuery(
                "UPDATE [dbo].[Licenses] SET [ApplicationID] = @app_ID, [DriverID] = @driver_id, " +
                "[LicenseClass] = @license_class_ID, [IssueDate] = @dateI, [ExpirationDate] = @dateE, [Notes] = " +
                "@notes, [PaidFees] = @fees, [IsActive] = @is_active, [IssueReason] = @isssue_reason,[CreatedByUserID] " +
                "= @user_id WHERE Licenses.LicenseID = @license_ID;");

            connection.Open();
            var command = clsDatabaseFactory.CreateCommand(q, connection);
            clsDatabaseFactory.AddParam(command, "@license_class_ID", license_class_ID);
            clsDatabaseFactory.AddParam(command, "@app_ID", app_ID);
            clsDatabaseFactory.AddParam(command, "@dateI", dateI);
            clsDatabaseFactory.AddParam(command, "@dateE", dateE);
            clsDatabaseFactory.AddParam(command, "@fees", fees);
            clsDatabaseFactory.AddParam(command, "@user_id", user_id);
            clsDatabaseFactory.AddParam(command, "@is_active", is_active);
            clsDatabaseFactory.AddParam(command, "@isssue_reason", isssue_reason);
            clsDatabaseFactory.AddParam(command, "@driver_id", driver_id);
            clsDatabaseFactory.AddParam(command, "@notes", notes);
            clsDatabaseFactory.AddParam(command, "@license_ID", license_ID);

            try
            {
                int result = command.ExecuteNonQuery();
                r = result;
            }
            catch (Exception ex)
            {
            }
            finally
            {
                connection.Close();
            }
            return r >= 1;
        }

        public static bool IsLicenseActive(int licenseID)
        {
            bool IsActive = false;
            var connection = clsDatabaseFactory.CreateConnection();

            // Simple SELECT – auto-convert
            string q = clsDatabaseFactory.GetQuery("select 1 from Licenses where LicenseID=@licenseID  and IsActive=1 ;");

            connection.Open();
            var command = clsDatabaseFactory.CreateCommand(q, connection);
            clsDatabaseFactory.AddParam(command, "@licenseID", licenseID);
            try
            {
                IDataReader result = command.ExecuteReader();
                if (result.Read())
                {
                    IsActive = true;
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                connection.Close();
            }

            return IsActive;
        }

        public static DataTable ListLicensesByDriverID(int driver_id)
        {
            DataTable Table = new DataTable();
            var connection = clsDatabaseFactory.CreateConnection();

            // C# string concatenation only, no SQL '+' → auto-convert
            string q = clsDatabaseFactory.GetQuery(
                "select 'Lic.ID'=Licenses.LicenseID  ,  'App.ID'=Licenses.ApplicationID , " +
                "LicenseClasses.ClassName , IssueDate , ExpirationDate ,IsActive from Licenses " +
                "join LicenseClasses on Licenses.LicenseClass = LicenseClasses.LicenseClassID " +
                "where DriverID =@driver_id");

            connection.Open();
            var command = clsDatabaseFactory.CreateCommand(q, connection);
            clsDatabaseFactory.AddParam(command, "@driver_id", driver_id);
            try
            {
                IDataReader result = command.ExecuteReader();
                Table.Load(result);
            }
            catch (Exception ex)
            {
            }
            finally
            {
                connection.Close();
            }

            return Table;
        }


        public static bool FindLicenseByLicenseID(int license_ID, ref short license_class_ID, ref int app_ID,
            ref DateTime dateI, ref DateTime dateE, ref float fees, ref int user_id, ref bool is_active, ref short isssue_reason,
            ref int driver_id, ref string notes)
        {
            bool IsExist = false;
            var connection = clsDatabaseFactory.CreateConnection();

            // Simple SELECT – auto-convert
            string q = clsDatabaseFactory.GetQuery("select * from Licenses where Licenses.LicenseID=@license_ID;");

            connection.Open();
            var cmd = clsDatabaseFactory.CreateCommand(q, connection);
            clsDatabaseFactory.AddParam(cmd, "@license_ID", license_ID);

            try
            {
                IDataReader r = cmd.ExecuteReader();
                if (r.Read())
                {
                    app_ID = Convert.ToInt32(r[1]);
                    driver_id = Convert.ToInt32(r[2]);
                    license_class_ID = Convert.ToInt16(r[3]);
                    dateI = Convert.ToDateTime(r[4]);
                    dateE = Convert.ToDateTime(r[5]);
                    notes = Convert.ToString(r[6]);
                    fees = Convert.ToSingle(r[7]);
                    if (Convert.ToInt32(r[8]) == 1)
                    {
                        is_active = true;
                    }
                    else
                    {
                        is_active = false;
                    }
                    isssue_reason = Convert.ToInt16(r[9]);
                    user_id = Convert.ToInt32(r[10]);

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

        public static bool FindLicenseByDriverID(ref int license_ID, ref short license_class_ID, ref int app_ID,
            ref DateTime dateI, ref DateTime dateE, ref float fees, ref int user_id, ref bool is_active, ref short isssue_reason,
             int driver_id, ref string notes)
        {
            bool IsExist = false;
            var connection = clsDatabaseFactory.CreateConnection();

            // Simple SELECT – auto-convert
            string q = clsDatabaseFactory.GetQuery("select * from Licenses where Licenses.DriverID=@driver_id;");

            connection.Open();
            var cmd = clsDatabaseFactory.CreateCommand(q, connection);
            clsDatabaseFactory.AddParam(cmd, "@driver_id", driver_id);

            try
            {
                IDataReader r = cmd.ExecuteReader();
                if (r.Read())
                {
                    license_ID = Convert.ToInt32(r[0]);
                    app_ID = Convert.ToInt32(r[1]);
                    driver_id = Convert.ToInt32(r[2]);
                    license_class_ID = Convert.ToInt16(r[3]);
                    dateI = Convert.ToDateTime(r[4]);
                    dateE = Convert.ToDateTime(r[5]);
                    notes = Convert.ToString(r[6]);
                    fees = Convert.ToSingle(r[7]);
                    if (Convert.ToInt32(r[8]) == 1)
                    {
                        is_active = true;
                    }
                    else
                    {
                        is_active = false;
                    }
                    isssue_reason = Convert.ToInt16(r[9]);
                    user_id = Convert.ToInt32(r[10]);

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


        public static bool IsLicenseHasCreatedFirstTime(int AppID)
        {
            bool IsExist = false;
            var connection = clsDatabaseFactory.CreateConnection();

            // Simple SELECT – auto-convert
            string q = clsDatabaseFactory.GetQuery(" select 1 from Licenses where Licenses.ApplicationID=@AppID; ");

            connection.Open();
            var cmd = clsDatabaseFactory.CreateCommand(q, connection);
            clsDatabaseFactory.AddParam(cmd, "@AppID", AppID);

            try
            {
                IDataReader r = cmd.ExecuteReader();
                if (r.Read())
                {
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


        public static int GetLicenseIDByAppID(int AppID)
        {
            int LicenseID = -1;
            var connection = clsDatabaseFactory.CreateConnection();

            // Simple SELECT – auto-convert
            string q = clsDatabaseFactory.GetQuery("select LicenseID from Licenses where Licenses.ApplicationID=@AppID;");

            connection.Open();
            var cmd = clsDatabaseFactory.CreateCommand(q, connection);
            clsDatabaseFactory.AddParam(cmd, "@AppID", AppID);

            try
            {
                IDataReader r = cmd.ExecuteReader();
                if (r.Read())
                {
                    LicenseID = Convert.ToInt32(r[0]);
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                connection.Close();
            }

            return LicenseID;
        }

        public static bool IsLicenseDetained(int LicID)
        {
            bool IsExist = false;
            var connection = clsDatabaseFactory.CreateConnection();

            // Simple SELECT – auto-convert
            string q = clsDatabaseFactory.GetQuery(" select 1 from DetainedLicenses where DetainedLicenses.LicenseID = @LicID  and DetainedLicenses.IsReleased = 0 ; ");

            connection.Open();
            var cmd = clsDatabaseFactory.CreateCommand(q, connection);
            clsDatabaseFactory.AddParam(cmd, "@LicID", LicID);

            try
            {
                IDataReader r = cmd.ExecuteReader();
                if (r.Read())
                {
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
    }
}
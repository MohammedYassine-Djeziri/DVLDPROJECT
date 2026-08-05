using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class clsSqlInternationalLicense
    {
        public static int AddNewLicense(int driver_id, int license_id, int app_ID, DateTime dateI, DateTime dateE,
                         int user_id, bool is_active)
        {
            int ID = -1;
            var connection = clsDatabaseFactory.CreateConnection();

            // INSERT with SCOPE_IDENTITY → explicit PG RETURNING version
            string q = clsDatabaseFactory.GetQuery(
                " INSERT INTO [dbo].[InternationalLicenses] ([ApplicationID] ,[DriverID] ,[IssuedUsingLocalLicenseID] ,[IssueDate] ,[ExpirationDate] " +
                "  ,[IsActive] , [CreatedByUserID]) VALUES ( @app_ID, @driver_id, @license_id, " +
                "   @dateI, @dateE,  @is_active,   @user_id ) ; SELECT SCOPE_IDENTITY() ; ",

                " INSERT INTO internallicenses (applicationid ,driverid ,issuedusinglocallicenseid ,issuedate ,expirationdate " +
                "  ,isactive , createdbyuserid) VALUES ( @app_ID, @driver_id, @license_id, " +
                "   @dateI, @dateE,  @is_active,   @user_id ) RETURNING internallicenseid ; ");

            connection.Open();
            var command = clsDatabaseFactory.CreateCommand(q, connection);
            clsDatabaseFactory.AddParam(command, "@app_ID", app_ID);
            clsDatabaseFactory.AddParam(command, "@dateI", dateI);
            clsDatabaseFactory.AddParam(command, "@dateE", dateE);
            clsDatabaseFactory.AddParam(command, "@user_id", user_id);
            clsDatabaseFactory.AddParam(command, "@is_active", is_active);
            clsDatabaseFactory.AddParam(command, "@driver_id", driver_id);
            clsDatabaseFactory.AddParam(command, "@license_id", license_id);

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

        public static bool UpdateLicense(int Inter_license_ID, int driver_id, int license_id, int app_ID, DateTime dateI, DateTime dateE,
                         int user_id, bool is_active)
        {
            int r = 0;
            var connection = clsDatabaseFactory.CreateConnection();

            // Simple UPDATE – auto-convert
            string q = clsDatabaseFactory.GetQuery(
                "UPDATE [dbo].[InternationalLicenses] SET [ApplicationID] = @app_ID, [DriverID] = @driver_id, " +
                " [IssueDate] = @dateI, [ExpirationDate] = @dateE,   [IsActive] = @is_active, [CreatedByUserID] " +
                "= @user_id  , [IssuedUsingLocalLicenseID] = @license_id WHERE InternationalLicenses.InternationalLicenseID = @Inter_license_ID;");

            connection.Open();
            var command = clsDatabaseFactory.CreateCommand(q, connection);
            clsDatabaseFactory.AddParam(command, "@app_ID", app_ID);
            clsDatabaseFactory.AddParam(command, "@Inter_license_ID", Inter_license_ID);
            clsDatabaseFactory.AddParam(command, "@dateI", dateI);
            clsDatabaseFactory.AddParam(command, "@dateE", dateE);
            clsDatabaseFactory.AddParam(command, "@user_id", user_id);
            clsDatabaseFactory.AddParam(command, "@is_active", is_active);
            clsDatabaseFactory.AddParam(command, "@driver_id", driver_id);
            clsDatabaseFactory.AddParam(command, "@license_id", license_id);

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

        public static bool IsDriverAlreadyHaveInternationalLicense(int driver_id)
        {
            bool IsActive = false;
            var connection = clsDatabaseFactory.CreateConnection();

            // Simple SELECT – auto-convert
            string q = clsDatabaseFactory.GetQuery("select 1 from InternationalLicenses where InternationalLicenses.DriverID =@driver_id;");

            connection.Open();
            var command = clsDatabaseFactory.CreateCommand(q, connection);
            clsDatabaseFactory.AddParam(command, "@driver_id", driver_id);
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


        public static DataTable ListInternationalLicensesByDriverID(int driver_id)
        {
            DataTable Table = new DataTable();
            var connection = clsDatabaseFactory.CreateConnection();

            // C# string concatenation only, no SQL '+' → auto-convert
            string q = clsDatabaseFactory.GetQuery(
                "select 'Int.Lic ID'=InternationalLicenses.InternationalLicenseID  ,  " +
                "'App.ID'=InternationalLicenses.ApplicationID , 'L.License ID'" +
                "=InternationalLicenses.IssuedUsingLocalLicenseID, IssueDate , " +
                "ExpirationDate ,IsActive from InternationalLicenses where DriverID =@driver_id");

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


        public static DataTable ListInternationalLicenses()
        {
            DataTable Table = new DataTable();
            var connection = clsDatabaseFactory.CreateConnection();

            // C# string concatenation only, no SQL '+' → auto-convert
            string q = clsDatabaseFactory.GetQuery(
                "select 'Int.Lic ID'=InternationalLicenses.InternationalLicenseID  ,  'ApplicationID'" +
                "=InternationalLicenses.ApplicationID ,'L.License ID'=InternationalLicenses.IssuedUsingLocalLicenseID," +
                " DriverID, IssueDate , ExpirationDate ,IsActive from InternationalLicenses ;");

            connection.Open();
            var command = clsDatabaseFactory.CreateCommand(q, connection);
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

        public static bool FindLicenseByInternationalLicenseID(int Inter_license_ID, ref int driver_id,
            ref int license_id, ref int app_ID, ref DateTime dateI, ref DateTime dateE, ref int user_id, ref bool is_active)
        {
            bool IsExist = false;
            var connection = clsDatabaseFactory.CreateConnection();

            // Simple SELECT – auto-convert
            string q = clsDatabaseFactory.GetQuery("select * from InternationalLicenses where  " +
                "InternationalLicenses.InternationalLicenseID = @Inter_license_ID;");

            connection.Open();
            var cmd = clsDatabaseFactory.CreateCommand(q, connection);
            clsDatabaseFactory.AddParam(cmd, "@Inter_license_ID", Inter_license_ID);

            try
            {
                IDataReader r = cmd.ExecuteReader();
                if (r.Read())
                {
                    app_ID = Convert.ToInt32(r[1]);
                    driver_id = Convert.ToInt32(r[2]);
                    license_id = Convert.ToInt32(r[3]);
                    dateI = Convert.ToDateTime(r[4]);
                    dateE = Convert.ToDateTime(r[5]);
                    if (Convert.ToInt32(r[6]) == 1)
                    {
                        is_active = true;
                    }
                    else
                    {
                        is_active = false;
                    }
                    user_id = Convert.ToInt32(r[7]);

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


        public static bool FindLicenseByLicenseID(ref int Inter_license_ID, ref int driver_id,
            int license_id, ref int app_ID, ref DateTime dateI, ref DateTime dateE, ref int user_id, ref bool is_active)
        {
            bool IsExist = false;
            var connection = clsDatabaseFactory.CreateConnection();

            // Simple SELECT – auto-convert
            string q = clsDatabaseFactory.GetQuery("select * from InternationalLicenses where  " +
                "InternationalLicenses.IssuedUsingLocalLicenseID = @license_id;");

            connection.Open();
            var cmd = clsDatabaseFactory.CreateCommand(q, connection);
            clsDatabaseFactory.AddParam(cmd, "@license_id", license_id);

            try
            {
                IDataReader r = cmd.ExecuteReader();
                if (r.Read())
                {
                    Inter_license_ID = Convert.ToInt32(r[0]);
                    app_ID = Convert.ToInt32(r[1]);
                    driver_id = Convert.ToInt32(r[2]);
                    dateI = Convert.ToDateTime(r[4]);
                    dateE = Convert.ToDateTime(r[5]);
                    if (Convert.ToInt32(r[6]) == 1)
                    {
                        is_active = true;
                    }
                    else
                    {
                        is_active = false;
                    }
                    user_id = Convert.ToInt32(r[7]);

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


        public static bool FindLicenseByDriverID(ref int Inter_license_ID, int driver_id,
           ref int license_id, ref int app_ID, ref DateTime dateI, ref DateTime dateE, ref int user_id, ref bool is_active)
        {
            bool IsExist = false;
            var connection = clsDatabaseFactory.CreateConnection();

            // Simple SELECT – auto-convert
            string q = clsDatabaseFactory.GetQuery("select * from InternationalLicenses where  " +
                "InternationalLicenses.DriverID = @driver_id;");

            connection.Open();
            var cmd = clsDatabaseFactory.CreateCommand(q, connection);
            clsDatabaseFactory.AddParam(cmd, "@driver_id", driver_id);

            try
            {
                IDataReader r = cmd.ExecuteReader();
                if (r.Read())
                {
                    Inter_license_ID = Convert.ToInt32(r[0]);
                    app_ID = Convert.ToInt32(r[1]);
                    license_id = Convert.ToInt32(r[3]);
                    dateI = Convert.ToDateTime(r[4]);
                    dateE = Convert.ToDateTime(r[5]);
                    if (Convert.ToInt32(r[6]) == 1)
                    {
                        is_active = true;
                    }
                    else
                    {
                        is_active = false;
                    }
                    user_id = Convert.ToInt32(r[7]);

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
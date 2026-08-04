using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
        public static string DVLD_Connection_Info => clsConnectionSettings.ConnectionString;

        public static int AddNewLicense(int license_class_ID, int app_ID, DateTime dateI, DateTime dateE,
            float fees, int user_id, bool is_active, short isssue_reason, int driver_id, string notes)
        {
            int ID = -1;
            SqlConnection connection = new SqlConnection(DVLD_Connection_Info);
            string q = " INSERT INTO [dbo].[Licenses] ([ApplicationID] ,[DriverID] ,[LicenseClass] ,[IssueDate] ,[ExpirationDate] " +
                ",[Notes] ,[PaidFees] ,[IsActive] ,[IssueReason]  ,[CreatedByUserID]) VALUES ( @app_ID, @driver_id," +
                " @license_class_ID, @dateI, @dateE, @notes,  @fees, @is_active, @isssue_reason, @user_id ) ; SELECT SCOPE_IDENTITY() ; ";
            connection.Open();
            SqlCommand command = new SqlCommand(q, connection);
            command.Parameters.AddWithValue("@license_class_ID", license_class_ID);
            command.Parameters.AddWithValue("@app_ID", app_ID);
            command.Parameters.AddWithValue("@dateI", dateI);
            command.Parameters.AddWithValue("@dateE", dateE);
            command.Parameters.AddWithValue("@fees", fees);
            command.Parameters.AddWithValue("@user_id", user_id);
            command.Parameters.AddWithValue("@is_active", is_active);
            command.Parameters.AddWithValue("@isssue_reason", isssue_reason);
            command.Parameters.AddWithValue("@driver_id", driver_id);
            command.Parameters.AddWithValue("@notes", notes);


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
            SqlConnection connection = new SqlConnection(DVLD_Connection_Info);
            string q = "UPDATE [dbo].[Licenses] SET [ApplicationID] = @app_ID, [DriverID] = @driver_id, " +
                "[LicenseClass] = @license_class_ID, [IssueDate] = @dateI, [ExpirationDate] = @dateE, [Notes] = " +
                "@notes, [PaidFees] = @fees, [IsActive] = @is_active, [IssueReason] = @isssue_reason,[CreatedByUserID] " +
                "= @user_id WHERE Licenses.LicenseID = @license_ID;";
            connection.Open();
            SqlCommand command = new SqlCommand(q, connection);
            command.Parameters.AddWithValue("@license_class_ID", license_class_ID);
            command.Parameters.AddWithValue("@app_ID", app_ID);
            command.Parameters.AddWithValue("@dateI", dateI);
            command.Parameters.AddWithValue("@dateE", dateE);
            command.Parameters.AddWithValue("@fees", fees);
            command.Parameters.AddWithValue("@user_id", user_id);
            command.Parameters.AddWithValue("@is_active", is_active);
            command.Parameters.AddWithValue("@isssue_reason", isssue_reason);
            command.Parameters.AddWithValue("@driver_id", driver_id);
            command.Parameters.AddWithValue("@notes", notes);
            command.Parameters.AddWithValue("@license_ID", license_ID);

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
            SqlConnection connection = new SqlConnection(DVLD_Connection_Info);
            string q = "select 1 from Licenses where LicenseID=@licenseID  and IsActive=1 ;";
            connection.Open();
            SqlCommand command = new SqlCommand(q, connection);
           
            command.Parameters.AddWithValue("@licenseID", licenseID);
            try
            {

                SqlDataReader result = command.ExecuteReader();
                if (result.HasRows)
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

            SqlConnection connection = new SqlConnection(DVLD_Connection_Info);
            string q = "select 'Lic.ID'=Licenses.LicenseID  ,  'App.ID'=Licenses.ApplicationID , " +
                "LicenseClasses.ClassName , IssueDate , ExpirationDate ,IsActive from Licenses " +
                "join LicenseClasses on Licenses.LicenseClass = LicenseClasses.LicenseClassID " +
                "where DriverID =@driver_id";
            connection.Open();
            SqlCommand command = new SqlCommand(q, connection);
            command.Parameters.AddWithValue("@driver_id", driver_id);
            try
            {

                SqlDataReader result = command.ExecuteReader();
                if (result.HasRows)
                {
                    Table.Load(result);
                }

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
            SqlConnection connection = new SqlConnection(DVLD_Connection_Info);
            string q = "select * from Licenses where Licenses.LicenseID=@license_ID;";
            connection.Open();
            SqlCommand cmd = new SqlCommand(q, connection);
            cmd.Parameters.AddWithValue("@license_ID", license_ID);

            try
            {
                SqlDataReader r = cmd.ExecuteReader();
                if (r.Read())
                {
                    
                    app_ID = Convert.ToInt32(r[1]);
                    driver_id = Convert.ToInt32(r[2]);
                    license_class_ID = Convert.ToInt16(r[3]);
                    dateI = Convert.ToDateTime(r[4]);
                    dateE = Convert.ToDateTime(r[5]);
                    notes = Convert.ToString(r[6]);
                    fees = Convert.ToSingle(r[7]);
                    if(Convert.ToInt32(r[8])==1)
                    {
                        is_active = true;
                    }
                    else
                    {
                        is_active=false;
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
            SqlConnection connection = new SqlConnection(DVLD_Connection_Info);
            string q = "select * from Licenses where Licenses.DriverID=@driver_id;";
            connection.Open();
            SqlCommand cmd = new SqlCommand(q, connection);
            cmd.Parameters.AddWithValue("@driver_id", driver_id);

            try
            {
                SqlDataReader r = cmd.ExecuteReader();
                if (r.Read())
                {
                    license_ID= Convert.ToInt32(r[0]);
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
            SqlConnection connection = new SqlConnection(DVLD_Connection_Info);
            string q = " select 1 from Licenses where Licenses.ApplicationID=@AppID; ";
            connection.Open();
            SqlCommand cmd = new SqlCommand(q, connection);
            cmd.Parameters.AddWithValue("@AppID", AppID);

            try
            {
                SqlDataReader r = cmd.ExecuteReader();
                if (r.HasRows)
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
            SqlConnection connection = new SqlConnection(DVLD_Connection_Info);
            string q = "select LicenseID from Licenses where Licenses.ApplicationID=@AppID;";
            connection.Open();
            SqlCommand cmd = new SqlCommand(q, connection);
            cmd.Parameters.AddWithValue("@AppID", AppID);

            try
            {
                SqlDataReader r = cmd.ExecuteReader();
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
            SqlConnection connection = new SqlConnection(DVLD_Connection_Info);
            string q = " select 1 from DetainedLicenses where DetainedLicenses.LicenseID = @LicID  and DetainedLicenses.IsReleased = 0 ; ";
            connection.Open();
            SqlCommand cmd = new SqlCommand(q, connection);
            cmd.Parameters.AddWithValue("@LicID", LicID);

            try
            {
                SqlDataReader r = cmd.ExecuteReader();
                if (r.HasRows)
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

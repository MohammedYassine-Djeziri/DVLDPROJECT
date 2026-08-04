using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class clsSqlInternationalLicense
    {

        public static string DVLD_Connection_Info => clsConnectionSettings.ConnectionString;

        public static int AddNewLicense(int driver_id, int license_id, int app_ID, DateTime dateI, DateTime dateE,
                         int user_id, bool is_active)
        {
            int ID = -1;
            SqlConnection connection = new SqlConnection(DVLD_Connection_Info);
            string q = " INSERT INTO [dbo].[InternationalLicenses] ([ApplicationID] ,[DriverID] ,[IssuedUsingLocalLicenseID] ,[IssueDate] ,[ExpirationDate] " +
                "  ,[IsActive] , [CreatedByUserID]) VALUES ( @app_ID, @driver_id, @license_id, " +
                "   @dateI, @dateE,  @is_active,   @user_id ) ; SELECT SCOPE_IDENTITY() ; ";
            connection.Open();
            SqlCommand command = new SqlCommand(q, connection);
            command.Parameters.AddWithValue("@app_ID", app_ID);
            command.Parameters.AddWithValue("@dateI", dateI);
            command.Parameters.AddWithValue("@dateE", dateE);
            command.Parameters.AddWithValue("@user_id", user_id);
            command.Parameters.AddWithValue("@is_active", is_active);
            command.Parameters.AddWithValue("@driver_id", driver_id);
            command.Parameters.AddWithValue("@license_id", license_id);

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
            SqlConnection connection = new SqlConnection(DVLD_Connection_Info);
            string q = "UPDATE [dbo].[InternationalLicenses] SET [ApplicationID] = @app_ID, [DriverID] = @driver_id, " +
                " [IssueDate] = @dateI, [ExpirationDate] = @dateE,   [IsActive] = @is_active, [CreatedByUserID] " +
                "= @user_id  , [IssuedUsingLocalLicenseID] = @license_id WHERE InternationalLicenses.InternationalLicenseID = @Inter_license_ID;";
            connection.Open();
            SqlCommand command = new SqlCommand(q, connection);
            command.Parameters.AddWithValue("@app_ID", app_ID);
            command.Parameters.AddWithValue("@Inter_license_ID", Inter_license_ID);
            command.Parameters.AddWithValue("@dateI", dateI);
            command.Parameters.AddWithValue("@dateE", dateE);
            command.Parameters.AddWithValue("@user_id", user_id);
            command.Parameters.AddWithValue("@is_active", is_active);
            command.Parameters.AddWithValue("@driver_id", driver_id);
            command.Parameters.AddWithValue("@license_id", license_id);

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
            SqlConnection connection = new SqlConnection(DVLD_Connection_Info);
            string q = "select 1 from InternationalLicenses where InternationalLicenses.DriverID =@driver_id;";
            connection.Open();
            SqlCommand command = new SqlCommand(q, connection);

            command.Parameters.AddWithValue("@driver_id", driver_id);
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


        //public static DataTable ListLicenses()
        //{
        //    DataTable Table = new DataTable();

        //    SqlConnection connection = new SqlConnection(DVLD_Connection_Info);
        //    string q = "select * from Licenses inner join Drivers on Licenses.DriverID = Drivers.DriverID inner join People on Drivers.PersonID = People.PersonID;";
        //    connection.Open();
        //    SqlCommand command = new SqlCommand(q, connection);

        //    try
        //    {

        //        SqlDataReader result = command.ExecuteReader();
        //        if (result.HasRows)
        //        {
        //            Table.Load(result);
        //        }

        //    }

        //    catch (Exception ex)
        //    {

        //    }
        //    finally
        //    {
        //        connection.Close();

        //    }


        //    return Table;
        //}


        public static DataTable ListInternationalLicensesByDriverID(int driver_id)
        {
            DataTable Table = new DataTable();

            SqlConnection connection = new SqlConnection(DVLD_Connection_Info);
            string q = "select 'Int.Lic ID'=InternationalLicenses.InternationalLicenseID  ,  " +
                "'App.ID'=InternationalLicenses.ApplicationID , 'L.License ID'" +
                "=InternationalLicenses.IssuedUsingLocalLicenseID, IssueDate , " +
                "ExpirationDate ,IsActive from InternationalLicenses where DriverID =@driver_id";
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


        public static DataTable ListInternationalLicenses()
        {
            DataTable Table = new DataTable();

            SqlConnection connection = new SqlConnection(DVLD_Connection_Info);
            string q = "select 'Int.Lic ID'=InternationalLicenses.InternationalLicenseID  ,  'ApplicationID'" +
            "=InternationalLicenses.ApplicationID ,'L.License ID'=InternationalLicenses.IssuedUsingLocalLicenseID," +
            " DriverID, IssueDate , ExpirationDate ,IsActive from InternationalLicenses ;";
            connection.Open();
            SqlCommand command = new SqlCommand(q, connection);
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

        public static bool FindLicenseByInternationalLicenseID(int Inter_license_ID, ref int driver_id, 
            ref int license_id, ref int app_ID, ref DateTime dateI, ref DateTime dateE, ref int user_id, ref bool is_active)
        {
            bool IsExist = false;
            SqlConnection connection = new SqlConnection(DVLD_Connection_Info);
            string q = "select * from InternationalLicenses where  " +
                "InternationalLicenses.InternationalLicenseID = @Inter_license_ID;";
            connection.Open();
            SqlCommand cmd = new SqlCommand(q, connection);
            cmd.Parameters.AddWithValue("@Inter_license_ID", Inter_license_ID);

            try
            {
                SqlDataReader r = cmd.ExecuteReader();
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
            SqlConnection connection = new SqlConnection(DVLD_Connection_Info);
            string q = "select * from InternationalLicenses where  " +
                "InternationalLicenses.IssuedUsingLocalLicenseID = @license_id;";
            connection.Open();
            SqlCommand cmd = new SqlCommand(q, connection);
            cmd.Parameters.AddWithValue("@license_id", license_id);

            try
            {
                SqlDataReader r = cmd.ExecuteReader();
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


        public static bool FindLicenseByDriverID(ref int Inter_license_ID,  int driver_id,
           ref int license_id, ref int app_ID, ref DateTime dateI, ref DateTime dateE, ref int user_id, ref bool is_active)
        {
            bool IsExist = false;
            SqlConnection connection = new SqlConnection(DVLD_Connection_Info);
            string q = "select * from InternationalLicenses where  " +
                "InternationalLicenses.DriverID = @driver_id;";
            connection.Open();
            SqlCommand cmd = new SqlCommand(q, connection);
            cmd.Parameters.AddWithValue("@driver_id", driver_id);

            try
            {
                SqlDataReader r = cmd.ExecuteReader();
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




        //public static bool IsLicenseHasCreatedFirstTime(int AppID)
        //{
        //    bool IsExist = false;
        //    SqlConnection connection = new SqlConnection(DVLD_Connection_Info);
        //    string q = " select 1 from Licenses where Licenses.ApplicationID=@AppID; ";
        //    connection.Open();
        //    SqlCommand cmd = new SqlCommand(q, connection);
        //    cmd.Parameters.AddWithValue("@AppID", AppID);

        //    try
        //    {
        //        SqlDataReader r = cmd.ExecuteReader();
        //        if (r.HasRows)
        //        {
        //            IsExist = true;
        //        }
        //    }

        //    catch (Exception ex)
        //    {

        //    }

        //    finally
        //    {
        //        connection.Close();
        //    }

        //    return IsExist;
        //}






    }
}

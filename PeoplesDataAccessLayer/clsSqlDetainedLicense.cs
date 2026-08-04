using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class clsSqlDetainedLicense
    {
        public static string DVLD_Connection_Info => clsConnectionSettings.ConnectionString;

        public static int AddNewDetainedLicense(int license_ID, int app_ID, DateTime date_det, DateTime date_rel,
            float fees, int rel_user_id, int det_user_id, bool is_released)
        {
            int ID = -1;
            SqlConnection connection = new SqlConnection(DVLD_Connection_Info);
            string q = " INSERT INTO [dbo].[DetainedLicenses] ([LicenseID],[DetainDate],[FineFees],[CreatedByUserID]," +
                "[IsReleased],[ReleaseDate],[ReleasedByUserID],[ReleaseApplicationID]) VALUES (@license_ID , @date_det, @fees," +
                " @det_user_id, @is_released, @date_rel,@rel_user_id , @app_ID ) ; SELECT SCOPE_IDENTITY() ; ";
            connection.Open();
            SqlCommand command = new SqlCommand(q, connection);

            command.Parameters.AddWithValue("@license_ID", license_ID);
            command.Parameters.AddWithValue("@date_det", date_det);
            command.Parameters.AddWithValue("@fees", fees);
            command.Parameters.AddWithValue("@det_user_id", det_user_id);
            command.Parameters.AddWithValue("@is_released", is_released);
            
            if (date_rel < date_det)
            {
                command.Parameters.AddWithValue("@date_rel", DBNull.Value);
            }

            else
            {
                command.Parameters.AddWithValue("@date_rel", date_rel);
            }

           
            if(rel_user_id == -1)
            {
                command.Parameters.AddWithValue("@rel_user_id", DBNull.Value);
            }

            else
            {
                command.Parameters.AddWithValue("@rel_user_id", rel_user_id);
            }


            
            if (app_ID == -1)
            {
                command.Parameters.AddWithValue("@app_ID", DBNull.Value);
            }

            else
            {
                command.Parameters.AddWithValue("@app_ID", app_ID);
            }


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

        public static bool UpdateDetainedLicense(int detain_ID, int license_ID, int app_ID, DateTime date_det, DateTime date_rel,
            float fees, int rel_user_id, int det_user_id, bool is_released)
        {
            int r = 0;
            SqlConnection connection = new SqlConnection(DVLD_Connection_Info);
            string q = "UPDATE [dbo].[DetainedLicenses] SET [LicenseID] = @license_ID,[DetainDate] = " +
                "@date_det,[FineFees] = @fees,[CreatedByUserID] = @det_user_id,[IsReleased] =" +
                " @is_released,[ReleaseDate] = @date_rel,[ReleasedByUserID] = @rel_user_id," +
                "[ReleaseApplicationID] = @app_ID WHERE  DetainID =@detain_ID";
            connection.Open();
            SqlCommand command = new SqlCommand(q, connection);
            command.Parameters.AddWithValue("@license_ID", license_ID);
            command.Parameters.AddWithValue("@date_det", date_det);
            command.Parameters.AddWithValue("@fees", fees);
            command.Parameters.AddWithValue("@det_user_id", det_user_id);
            command.Parameters.AddWithValue("@is_released", is_released);
            command.Parameters.AddWithValue("@date_rel", date_rel);
            command.Parameters.AddWithValue("@rel_user_id", rel_user_id);
            command.Parameters.AddWithValue("@app_ID", app_ID);
            command.Parameters.AddWithValue("@detain_ID", detain_ID);

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

        
        public static bool FindDetainedLicenseByLicenseID(ref int detain_ID,  int license_ID,ref  int app_ID,
            ref DateTime date_det, ref DateTime date_rel, ref float fees, ref int rel_user_id,
            ref int det_user_id, ref bool is_released)
        {
            bool IsExist = false;
            SqlConnection connection = new SqlConnection(DVLD_Connection_Info);
            string q = "select * from DetainedLicenses where LicenseID =@license_ID;";
            connection.Open();
            SqlCommand cmd = new SqlCommand(q, connection);
            cmd.Parameters.AddWithValue("@license_ID", license_ID);

            try
            {
                SqlDataReader r = cmd.ExecuteReader();
                if (r.Read())
                {
                    detain_ID = Convert.ToInt32(r[0]);
                    if (r[8] != DBNull.Value)
                    {
                        app_ID = Convert.ToInt32(r[8]);
                    }
                    else { app_ID = -1; }
                    if (r[8] != DBNull.Value)
                    {
                        rel_user_id = Convert.ToInt32(r[7]);
                    }
                    else { rel_user_id = -1; }
                    det_user_id = Convert.ToInt32(r[4]);
                    date_det = Convert.ToDateTime(r[2]);
                    if (r[8] != DBNull.Value)
                    {
                        date_rel = Convert.ToDateTime(r[6]);
                    }
                    else
                    { date_rel  = DateTime.MinValue; }
                    fees = Convert.ToSingle(r[3]);
                    if (Convert.ToInt32(r[5]) == 1)
                    {
                        is_released = true;
                    }
                    else
                    {
                        is_released = false;
                    }
                    
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



        public static bool FindDetainedLicenseByDetainedLicenseID( int detain_ID, ref int license_ID, ref int app_ID,
            ref DateTime date_det, ref DateTime date_rel, ref float fees, ref int rel_user_id,
            ref int det_user_id, ref bool is_released)
        {
            bool IsExist = false;
            SqlConnection connection = new SqlConnection(DVLD_Connection_Info);
            string q = "select * from DetainedLicenses where LicenseID =@license_ID;";
            connection.Open();
            SqlCommand cmd = new SqlCommand(q, connection);
            cmd.Parameters.AddWithValue("@license_ID", license_ID);

            try
            {
                SqlDataReader r = cmd.ExecuteReader();
                if (r.Read())
                {
                    license_ID = Convert.ToInt32(r[1]);
                    app_ID = Convert.ToInt32(r[8]);
                    rel_user_id = Convert.ToInt32(r[7]);
                    det_user_id = Convert.ToInt16(r[4]);
                    date_det = Convert.ToDateTime(r[2]);
                    date_rel = Convert.ToDateTime(r[6]);
                    fees = Convert.ToSingle(r[3]);
                    if (Convert.ToInt32(r[5]) == 1)
                    {
                        is_released = true;
                    }
                    else
                    {
                        is_released = false;
                    }

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


        public static DataTable ListDetainedLicenses()
        {
            DataTable Table = new DataTable();

            SqlConnection connection = new SqlConnection(DVLD_Connection_Info);
            string q = "select 'D.ID' = DetainedLicenses.DetainID , 'L.ID' =DetainedLicenses.LicenseID , " +
                "'D.Date' =DetainedLicenses.DetainDate , DetainedLicenses.IsReleased , DetainedLicenses.FineFees " +
                ", DetainedLicenses.ReleaseDate  , 'N.No' =People.NationalNo  ,  'Full Name'=  case WHEN " +
                "People.FirstName  IS NULL THEN '' else People.FirstName+' ' END +CASE  WHEN People.SecondName  " +
                "IS NULL THEN '' else People.SecondName+' '  END +CASE  WHEN People.ThirdName IS NULL THEN ''  " +
                "else People.ThirdName+' 'END  +CASE  WHEN People.LastName  IS NULL THEN '' else People.LastName " +
                " END ,'Release App.ID' = DetainedLicenses.ReleaseApplicationID   from DetainedLicenses inner join" +
                " Licenses on DetainedLicenses.LicenseID = Licenses.LicenseID inner join Drivers on " +
                "Licenses.DriverID = Drivers.DriverID inner join People on Drivers.PersonID = People.PersonID;";
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



        //public static int GetLicenseIDByAppID(int AppID)
        //{
        //    int LicenseID = -1;
        //    SqlConnection connection = new SqlConnection(DVLD_Connection_Info);
        //    string q = "select LicenseID from Licenses where Licenses.ApplicationID=@AppID;";
        //    connection.Open();
        //    SqlCommand cmd = new SqlCommand(q, connection);
        //    cmd.Parameters.AddWithValue("@AppID", AppID);

        //    try
        //    {
        //        SqlDataReader r = cmd.ExecuteReader();
        //        if (r.Read())
        //        {

        //            LicenseID = Convert.ToInt32(r[0]);
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    finally
        //    {
        //        connection.Close();
        //    }


        //    return LicenseID;
        //}

        //public static bool IsLicenseDetained(int LicID)
        //{
        //    bool IsExist = false;
        //    SqlConnection connection = new SqlConnection(DVLD_Connection_Info);
        //    string q = " select 1 from DetainedLicenses where DetainedLicenses.LicenseID = @LicID  and DetainedLicenses.IsReleased = 0 ; ";
        //    connection.Open();
        //    SqlCommand cmd = new SqlCommand(q, connection);
        //    cmd.Parameters.AddWithValue("@LicID", LicID);

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

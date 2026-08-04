using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class clsSqlTestAppointment
    {
        public static string DVLD_Connection_Info => clsConnectionSettings.ConnectionString;

        public static DataTable ListTestsAppointment(int LDLID, int TestTypeID)
        {
            DataTable List = new DataTable();
            SqlConnection connection = new SqlConnection(DVLD_Connection_Info);
            string q = "select 'Appointment ID'=TestAppointmentID , 'Appointment Date'=AppointmentDate, 'Paid Fees'=PaidFees," +
                " 'Is Locked'=IsLocked from TestAppointments where TestAppointments.LocalDrivingLicenseApplicationID=" +
                "@LDLID and TestTypeID=@TestTypeID ; ";
            connection.Open();
            SqlCommand command = new SqlCommand(q, connection);
            command.Parameters.AddWithValue("@LDLID", LDLID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {

                SqlDataReader result = command.ExecuteReader();
                if (result.HasRows)
                {
                    List.Load(result);
                }

            }

            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();

            }
            return List;
        }

        public static int AddNewTestAppointment( int testTypeID, int LDLID, DateTime date,
            float fees, int user_id, bool is_locked, int retake_app_id)
        {
            byte Locked = 0;
            if (is_locked)
            {
                Locked = 1;
            }
            int ID = -1;
            SqlConnection connection = new SqlConnection(DVLD_Connection_Info);
            string q = " INSERT INTO [dbo].[TestAppointments] ([TestTypeID] ,[LocalDrivingLicenseApplicationID] ,[AppointmentDate] "
                + ",[PaidFees] ,[CreatedByUserID] ,[IsLocked] ,[RetakeTestApplicationID]) VALUES (@testTypeID " +
                ",@LDLID, @date, @fees ,@user_id ,@Locked  , @retake_app_id) ; SELECT SCOPE_IDENTITY() ;";
            connection.Open();
            SqlCommand command = new SqlCommand(q, connection);
            command.Parameters.AddWithValue("@testTypeID", testTypeID);
            command.Parameters.AddWithValue("@LDLID", LDLID);
            command.Parameters.AddWithValue("@date", date);
            command.Parameters.AddWithValue("@fees", fees);
            command.Parameters.AddWithValue("@user_id", user_id);
            command.Parameters.AddWithValue("@Locked", Locked);
            if (retake_app_id == -1)
            {
                command.Parameters.AddWithValue("@retake_app_id", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@retake_app_id", retake_app_id);
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

        public static bool UpdateTestAppointment( int AppointmentID, int testTypeID, int LDLID, DateTime date,
            float fees, int user_id, bool is_locked, int retake_app_id)
        {
            byte Locked = 0;
            if(is_locked)
            {
                Locked = 1;
            }
            int r = 0;
            SqlConnection connection = new SqlConnection(DVLD_Connection_Info);
            string q = "UPDATE [dbo].[TestAppointments]  SET [TestTypeID] = @testTypeID ,[LocalDrivingLicenseApplicationID] = @LDLID " +
                ",[AppointmentDate] =@date ,[PaidFees] =@fees ,[CreatedByUserID] = @user_id ,[IsLocked] =  @Locked" +
                ",[RetakeTestApplicationID] =@retake_app_id WHERE TestAppointmentID =@AppointmentID ; ";
            connection.Open();
            SqlCommand command = new SqlCommand(q, connection);
            command.Parameters.AddWithValue("@AppointmentID", AppointmentID);
            command.Parameters.AddWithValue("@testTypeID", testTypeID);
            command.Parameters.AddWithValue("@LDLID", LDLID);
            command.Parameters.AddWithValue("@date", date);
            command.Parameters.AddWithValue("@fees", fees);
            command.Parameters.AddWithValue("@user_id", user_id);
            command.Parameters.AddWithValue("@Locked", Locked);
            if (retake_app_id == -1)
            {
                command.Parameters.AddWithValue("@retake_app_id", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@retake_app_id", retake_app_id);
            }
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

        public static bool HasAppointment(int LDLID, int testTypeID)
        {
            bool IsActive = false;
            SqlConnection connection = new SqlConnection(DVLD_Connection_Info);
            string q = "select 1 from TestAppointments where TestAppointments.LocalDrivingLicenseApplicationID="+
                "@LDLID and TestTypeID=@testTypeID and IsLocked = 0 ; \r\n";
            connection.Open();
            SqlCommand command = new SqlCommand(q, connection);
            command.Parameters.AddWithValue("@LDLID", LDLID);
            command.Parameters.AddWithValue("@testTypeID", testTypeID);
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

        public static bool FindTestAppointmentByAppointmentID(int AppointmentID, ref int TestTypeID, ref int LDLID, 
            ref DateTime AppointmentDate, ref int RetakeApplicationID, ref bool is_Locked, ref float Fees,
            ref int userId)
        {
            bool IsExist = false;
            SqlConnection connection = new SqlConnection(DVLD_Connection_Info);
            string q = "select * from TestAppointments where TestAppointmentID=@AppointmentID;";
            connection.Open();
            SqlCommand cmd = new SqlCommand(q, connection);
            cmd.Parameters.AddWithValue("@AppointmentID", AppointmentID);

            try
            {
                SqlDataReader r = cmd.ExecuteReader();
                if (r.Read())
                {
                    AppointmentID = Convert.ToInt32(r[0]);
                    TestTypeID = Convert.ToInt32(r[1]);
                    LDLID = Convert.ToInt32(r[2]);
                    AppointmentDate = Convert.ToDateTime(r[3]);
                    if (r[7] != DBNull.Value)
                    {
                        RetakeApplicationID = Convert.ToInt32(r[7]);
                    }
                    else
                    {
                        RetakeApplicationID = -1;
                    }
                    is_Locked = Convert.ToBoolean(r[6]);
                    Fees = Convert.ToSingle(r[4]);
                    userId = Convert.ToInt32(r[5]);

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

       public static bool HaveRetakeTestApplicationForTestAppointmentID(int TAppoint_ID)
       {
            bool IsExist = false;
            SqlConnection connection = new SqlConnection(DVLD_Connection_Info);
            string q = "select 1 from TestAppointments where TestAppointmentID=@TAppoint_ID and RetakeTestApplicationID !=null";
            connection.Open();
            SqlCommand cmd = new SqlCommand(q, connection);
            cmd.Parameters.AddWithValue("@TAppoint_ID", TAppoint_ID);

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



        public static bool IsAlreadyWinInTestType(int LdlID, int Test___Type)
        {
            bool IsExist = false;
            SqlConnection connection = new SqlConnection(DVLD_Connection_Info);
            string q = "select 1 from TestAppointments join Tests on TestAppointments.TestAppointmentID= Tests.TestAppointmentID " +
                "where TestAppointments.LocalDrivingLicenseApplicationID=@LdlID and TestTypeID=@Test___Type and TestResult=1;";
            connection.Open();
            SqlCommand cmd = new SqlCommand(q, connection);
            cmd.Parameters.AddWithValue("@LdlID", LdlID);
            cmd.Parameters.AddWithValue("@Test___Type", Test___Type);

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

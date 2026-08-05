using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class clsSqlTestAppointment
    {
        public static DataTable ListTestsAppointment(int LDLID, int TestTypeID)
        {
            DataTable List = new DataTable();
            var connection = clsDatabaseFactory.CreateConnection();

            // C# string concatenation only, no SQL '+' → auto-convert
            string q = clsDatabaseFactory.GetQuery(
                "select 'Appointment ID'=TestAppointmentID , 'Appointment Date'=AppointmentDate, 'Paid Fees'=PaidFees," +
                " 'Is Locked'=IsLocked from TestAppointments where TestAppointments.LocalDrivingLicenseApplicationID=" +
                "@LDLID and TestTypeID=@TestTypeID ; ");

            connection.Open();
            var command = clsDatabaseFactory.CreateCommand(q, connection);
            clsDatabaseFactory.AddParam(command, "@LDLID", LDLID);
            clsDatabaseFactory.AddParam(command, "@TestTypeID", TestTypeID);

            try
            {
                IDataReader result = command.ExecuteReader();
                List.Load(result);
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

        public static int AddNewTestAppointment(int testTypeID, int LDLID, DateTime date,
            float fees, int user_id, bool is_locked, int retake_app_id)
        {
            byte Locked = 0;
            if (is_locked)
            {
                Locked = 1;
            }
            int ID = -1;
            var connection = clsDatabaseFactory.CreateConnection();

            // INSERT with SCOPE_IDENTITY → explicit PG RETURNING version
            string q = clsDatabaseFactory.GetQuery(
                " INSERT INTO [dbo].[TestAppointments] ([TestTypeID] ,[LocalDrivingLicenseApplicationID] ,[AppointmentDate] "
                + ",[PaidFees] ,[CreatedByUserID] ,[IsLocked] ,[RetakeTestApplicationID]) VALUES (@testTypeID "
                + ",@LDLID, @date, @fees ,@user_id ,@Locked  , @retake_app_id) ; SELECT SCOPE_IDENTITY() ;",

                " INSERT INTO testappointments (testtypeid ,localdrivinglicenseapplicationid ,appointmentdate "
                + ",paidfees ,createdbyuserid ,islocked ,retaketestapplicationid) VALUES (@testTypeID "
                + ",@LDLID, @date, @fees ,@user_id ,@Locked  , @retake_app_id) RETURNING testappointmentid ;");

            connection.Open();
            var command = clsDatabaseFactory.CreateCommand(q, connection);
            clsDatabaseFactory.AddParam(command, "@testTypeID", testTypeID);
            clsDatabaseFactory.AddParam(command, "@LDLID", LDLID);
            clsDatabaseFactory.AddParam(command, "@date", date);
            clsDatabaseFactory.AddParam(command, "@fees", fees);
            clsDatabaseFactory.AddParam(command, "@user_id", user_id);
            clsDatabaseFactory.AddParam(command, "@Locked", Locked);
            if (retake_app_id == -1)
            {
                clsDatabaseFactory.AddParam(command, "@retake_app_id", DBNull.Value);
            }
            else
            {
                clsDatabaseFactory.AddParam(command, "@retake_app_id", retake_app_id);
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

        public static bool UpdateTestAppointment(int AppointmentID, int testTypeID, int LDLID, DateTime date,
            float fees, int user_id, bool is_locked, int retake_app_id)
        {
            byte Locked = 0;
            if (is_locked)
            {
                Locked = 1;
            }
            int r = 0;
            var connection = clsDatabaseFactory.CreateConnection();

            // Simple UPDATE – auto-convert
            string q = clsDatabaseFactory.GetQuery(
                "UPDATE [dbo].[TestAppointments]  SET [TestTypeID] = @testTypeID ,[LocalDrivingLicenseApplicationID] = @LDLID " +
                ",[AppointmentDate] =@date ,[PaidFees] =@fees ,[CreatedByUserID] = @user_id ,[IsLocked] =  @Locked" +
                ",[RetakeTestApplicationID] =@retake_app_id WHERE TestAppointmentID =@AppointmentID ; ");

            connection.Open();
            var command = clsDatabaseFactory.CreateCommand(q, connection);
            clsDatabaseFactory.AddParam(command, "@AppointmentID", AppointmentID);
            clsDatabaseFactory.AddParam(command, "@testTypeID", testTypeID);
            clsDatabaseFactory.AddParam(command, "@LDLID", LDLID);
            clsDatabaseFactory.AddParam(command, "@date", date);
            clsDatabaseFactory.AddParam(command, "@fees", fees);
            clsDatabaseFactory.AddParam(command, "@user_id", user_id);
            clsDatabaseFactory.AddParam(command, "@Locked", Locked);
            if (retake_app_id == -1)
            {
                clsDatabaseFactory.AddParam(command, "@retake_app_id", DBNull.Value);
            }
            else
            {
                clsDatabaseFactory.AddParam(command, "@retake_app_id", retake_app_id);
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
            var connection = clsDatabaseFactory.CreateConnection();

            // Simple SELECT – auto-convert
            string q = clsDatabaseFactory.GetQuery(
                "select 1 from TestAppointments where TestAppointments.LocalDrivingLicenseApplicationID=" +
                "@LDLID and TestTypeID=@testTypeID and IsLocked = 0 ; \r\n");

            connection.Open();
            var command = clsDatabaseFactory.CreateCommand(q, connection);
            clsDatabaseFactory.AddParam(command, "@LDLID", LDLID);
            clsDatabaseFactory.AddParam(command, "@testTypeID", testTypeID);
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

        public static bool FindTestAppointmentByAppointmentID(int AppointmentID, ref int TestTypeID, ref int LDLID,
            ref DateTime AppointmentDate, ref int RetakeApplicationID, ref bool is_Locked, ref float Fees,
            ref int userId)
        {
            bool IsExist = false;
            var connection = clsDatabaseFactory.CreateConnection();

            // Simple SELECT – auto-convert
            string q = clsDatabaseFactory.GetQuery("select * from TestAppointments where TestAppointmentID=@AppointmentID;");

            connection.Open();
            var cmd = clsDatabaseFactory.CreateCommand(q, connection);
            clsDatabaseFactory.AddParam(cmd, "@AppointmentID", AppointmentID);

            try
            {
                IDataReader r = cmd.ExecuteReader();
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
            var connection = clsDatabaseFactory.CreateConnection();

            // !=null is SQL Server syntax; PG requires IS NOT NULL
            string q = clsDatabaseFactory.GetQuery(
                "select 1 from TestAppointments where TestAppointmentID=@TAppoint_ID and RetakeTestApplicationID IS NOT NULL",

                "select 1 from testappointments where testappointmentid=@TAppoint_ID and retaketestapplicationid IS NOT NULL");

            connection.Open();
            var cmd = clsDatabaseFactory.CreateCommand(q, connection);
            clsDatabaseFactory.AddParam(cmd, "@TAppoint_ID", TAppoint_ID);

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


        public static bool IsAlreadyWinInTestType(int LdlID, int Test___Type)
        {
            bool IsExist = false;
            var connection = clsDatabaseFactory.CreateConnection();

            // Simple SELECT – auto-convert
            string q = clsDatabaseFactory.GetQuery(
                "select 1 from TestAppointments join Tests on TestAppointments.TestAppointmentID= Tests.TestAppointmentID " +
                "where TestAppointments.LocalDrivingLicenseApplicationID=@LdlID and TestTypeID=@Test___Type and TestResult=1;");

            connection.Open();
            var cmd = clsDatabaseFactory.CreateCommand(q, connection);
            clsDatabaseFactory.AddParam(cmd, "@LdlID", LdlID);
            clsDatabaseFactory.AddParam(cmd, "@Test___Type", Test___Type);

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
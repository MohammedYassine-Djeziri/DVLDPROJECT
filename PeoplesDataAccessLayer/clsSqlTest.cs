using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class clsSqlTest
    {
        public static int AddNewTest(int Appointment_id, int user_id, bool Result, string note)
        {
            byte IsSuccesses = 0;
            if (Result)
            {
                IsSuccesses = 1;
            }

            int ID = -1;
            var connection = clsDatabaseFactory.CreateConnection();

            // INSERT with SCOPE_IDENTITY → explicit PG RETURNING version
            string q = clsDatabaseFactory.GetQuery(
                "INSERT INTO [dbo].[Tests] ([TestAppointmentID] ,[TestResult] ,[Notes] ,[CreatedByUserID]) VALUES " +
                "(@Appointment_id, @IsSuccesses, @note, @user_id) ; SELECT SCOPE_IDENTITY() ;",

                "INSERT INTO \"Tests\" (\"TestAppointmentID\" ,\"TestResult\" ,\"Notes\" ,\"CreatedByUserID\") VALUES " +
                "(@Appointment_id, @IsSuccesses, @note, @user_id) RETURNING \"TestID\" ;");

            connection.Open();
            var command = clsDatabaseFactory.CreateCommand(q, connection);
            clsDatabaseFactory.AddParam(command, "@Appointment_id", Appointment_id);
            clsDatabaseFactory.AddParam(command, "@user_id", user_id);
            clsDatabaseFactory.AddParam(command, "@note", note);
            clsDatabaseFactory.AddParam(command, "@IsSuccesses", IsSuccesses);

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
                //hadle logging here
            }
            finally
            {
                connection.Close();
            }
            return ID;
        }
    }
}
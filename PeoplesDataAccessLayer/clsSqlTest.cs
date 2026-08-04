using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class clsSqlTest
    {
        public static string DVLD_Connection_Info => clsConnectionSettings.ConnectionString;

        public static int AddNewTest(int Appointment_id, int user_id, bool Result, string note)
        {
            byte IsSuccesses = 0;
            if (Result)
            {
                 IsSuccesses = 1;
            }

            int ID = -1;
            SqlConnection connection = new SqlConnection(DVLD_Connection_Info);
            string q = "INSERT INTO [dbo].[Tests] ([TestAppointmentID] ,[TestResult] ,[Notes] ,[CreatedByUserID]) VALUES " +
                "(@Appointment_id, @IsSuccesses, @note, @user_id) ; SELECT SCOPE_IDENTITY() ;";
            connection.Open();
            SqlCommand command = new SqlCommand(q, connection);
            command.Parameters.AddWithValue("@Appointment_id", Appointment_id);
            command.Parameters.AddWithValue("@user_id", user_id);
            command.Parameters.AddWithValue("@note", note);
            command.Parameters.AddWithValue("@IsSuccesses", IsSuccesses);


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
                File.WriteAllText(@"C:\Users\mohya\source\repos\ProjectBackUp\DVLD_Project\Users\Error.txt", ex.ToString());
            }
            finally
            {
                connection.Close();

            }
            return ID;
        }
    }
}

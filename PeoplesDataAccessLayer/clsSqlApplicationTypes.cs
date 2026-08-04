using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class clsSqlApplicationTypes
    {
        public static string DVLD_Connection_Info => clsConnectionSettings.ConnectionString;

        public static DataTable ListApplicationTypes()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(DVLD_Connection_Info);
            string q = "select * from ApplicationTypes;";
            SqlCommand command = new SqlCommand(q, connection);
            connection.Open();
            try
            {
                SqlDataReader Result = command.ExecuteReader();
                if (Result.HasRows)
                {
                    dt.Load(Result);

                }
            }
            catch { }
            finally { connection.Close(); }
            return dt;

        }

        public static void UpdateApplicationTypes(int id, string title, float fees)
        {

            SqlConnection connection = new SqlConnection(DVLD_Connection_Info);
            string q = "UPDATE [dbo].[ApplicationTypes] SET [ApplicationTypeTitle] = @title,[ApplicationFees]=@fees" +
                "  where ApplicationTypeID = @id";
            SqlCommand command = new SqlCommand(q, connection);
            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@title", title);
            command.Parameters.AddWithValue("@fees", fees);
            connection.Open();
            try
            {
                command.ExecuteNonQuery();
            }
            catch { }
            finally
            {
                connection.Close();
            }

        }

        public static float FindAppFeesByAppTitle(string Type)
        {
            int Fees = 0;
            SqlConnection connection = new SqlConnection(DVLD_Connection_Info);
            string q = "select ApplicationFees from ApplicationTypes where ApplicationTypeTitle=@type;";
            SqlCommand command = new SqlCommand(q, connection);
            command.Parameters.AddWithValue("@type", Type);
            connection.Open();
            try
            {
                SqlDataReader Result = command.ExecuteReader();
                if (Result.Read())
                {
                    Fees = Convert.ToInt32(Result[0]);

                }
            }
            catch
            {
            }
            finally { connection.Close(); }
            return Fees;

        }


        public static float FindAppFeesByAppTypeID(int Type)
        {
            int Fees = 0;
            SqlConnection connection = new SqlConnection(DVLD_Connection_Info);
            string q = "select ApplicationFees from ApplicationTypes where ApplicationTypeID=@type;";
            SqlCommand command = new SqlCommand(q, connection);
            command.Parameters.AddWithValue("@type", Type);
            connection.Open();
            try
            {
                SqlDataReader Result = command.ExecuteReader();
                if (Result.Read())
                {
                    Fees = Convert.ToInt32(Result[0]);

                }
            }
            catch
            {
            }
            finally { connection.Close(); }
            return Fees;

        }


        public static string GetApplicationTypeNameByAppTypeID(int AppType_id)
        {
            string Class_Name = "";
            SqlConnection connection = new SqlConnection(DVLD_Connection_Info);
            string q = "select ApplicationTypeTitle from ApplicationTypes where ApplicationTypeID=@tAppType_id;";
            SqlCommand command = new SqlCommand(q, connection);
            command.Parameters.AddWithValue("@AppType_id", AppType_id);
            connection.Open();
            try
            {
                SqlDataReader Result = command.ExecuteReader();
                if (Result.Read())
                {
                    Class_Name = Result[0].ToString();
                }
            }
            catch { }
            finally { connection.Close(); }
            return Class_Name;

        }
    }
}

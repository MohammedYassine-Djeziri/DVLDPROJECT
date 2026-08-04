using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class clsSqlTestTypes
    {
        public static string DVLD_Connection_Info => clsConnectionSettings.ConnectionString;

        public static DataTable ListTestTypes()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(DVLD_Connection_Info);
            string q = "select * from TestTypes;";
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

        public static void UpdateTestTypes(int id, string title, string des, float fees)
        {

            SqlConnection connection = new SqlConnection(DVLD_Connection_Info);
            string q = "UPDATE [dbo].[TestTypes] SET [TestTypeTitle] = @title,[TestTypeFees]=@fees , " +
                "[TestTypeDescription]= @des  where TestTypeID = @id";
            SqlCommand command = new SqlCommand(q, connection);
            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@title", title);
            command.Parameters.AddWithValue("@fees", fees);
            command.Parameters.AddWithValue("@des", des);
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


        public static float GetTestFeesFromTestTypeID(int id)
        {
            float Fees = 0;
            SqlConnection connection = new SqlConnection(DVLD_Connection_Info);
            string q = "select TestTypes.TestTypeFees from TestTypes where TestTypeID=@id;";
            SqlCommand command = new SqlCommand(q, connection);
            command.Parameters.AddWithValue("@id", id);
            connection.Open();
            try
            {
            SqlDataReader r = command.ExecuteReader();
                    if(r.Read())
                    {
                        Fees = Convert.ToSingle(r[0]);                
                    }
            }
            catch { }
            finally
            {
                connection.Close();
            }
        
        return Fees;
        }



        public static string GetTestNameFromTestTypeID(int id)
        {
            string Name = "";
            SqlConnection connection = new SqlConnection(DVLD_Connection_Info);
            string q = "select TestTypes.TestTypeTitle from TestTypes where TestTypeID=@id;";
            SqlCommand command = new SqlCommand(q, connection);
            command.Parameters.AddWithValue("@id", id);
            connection.Open();
            try
            {
                SqlDataReader r = command.ExecuteReader();
                if (r.Read())
                {
                    Name = Convert.ToString(r[0]);
                }
            }
            catch { }
            finally
            {
                connection.Close();
            }

            return Name;
        }
    }
}

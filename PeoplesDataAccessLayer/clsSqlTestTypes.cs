using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class clsSqlTestTypes
    {
        public static DataTable ListTestTypes()
        {
            DataTable dt = new DataTable();
            var connection = clsDatabaseFactory.CreateConnection();
            string q = clsDatabaseFactory.GetQuery("select * from TestTypes;");
            var command = clsDatabaseFactory.CreateCommand(q, connection);
            connection.Open();
            try
            {
                IDataReader Result = command.ExecuteReader();
                dt.Load(Result);
            }
            catch { }
            finally { connection.Close(); }
            return dt;
        }

        public static void UpdateTestTypes(int id, string title, string des, float fees)
        {
            var connection = clsDatabaseFactory.CreateConnection();
            string q = clsDatabaseFactory.GetQuery(
                "UPDATE [dbo].[TestTypes] SET [TestTypeTitle] = @title,[TestTypeFees]=@fees , " +
                "[TestTypeDescription]= @des  where TestTypeID = @id");
            var command = clsDatabaseFactory.CreateCommand(q, connection);
            clsDatabaseFactory.AddParam(command, "@id", id);
            clsDatabaseFactory.AddParam(command, "@title", title);
            clsDatabaseFactory.AddParam(command, "@fees", fees);
            clsDatabaseFactory.AddParam(command, "@des", des);
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
            var connection = clsDatabaseFactory.CreateConnection();
            string q = clsDatabaseFactory.GetQuery("select TestTypes.TestTypeFees from TestTypes where TestTypeID=@id;");
            var command = clsDatabaseFactory.CreateCommand(q, connection);
            clsDatabaseFactory.AddParam(command, "@id", id);
            connection.Open();
            try
            {
                IDataReader r = command.ExecuteReader();
                if (r.Read())
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
            var connection = clsDatabaseFactory.CreateConnection();
            string q = clsDatabaseFactory.GetQuery("select TestTypes.TestTypeTitle from TestTypes where TestTypeID=@id;");
            var command = clsDatabaseFactory.CreateCommand(q, connection);
            clsDatabaseFactory.AddParam(command, "@id", id);
            connection.Open();
            try
            {
                IDataReader r = command.ExecuteReader();
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
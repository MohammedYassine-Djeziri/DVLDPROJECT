using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class clsSqlApplicationTypes
    {
        public static DataTable ListApplicationTypes()
        {
            DataTable dt = new DataTable();
            var connection = clsDatabaseFactory.CreateConnection();
            string q = clsDatabaseFactory.GetQuery("select * from ApplicationTypes;");
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

        public static void UpdateApplicationTypes(int id, string title, float fees)
        {
            var connection = clsDatabaseFactory.CreateConnection();
            string q = clsDatabaseFactory.GetQuery(
                "UPDATE [dbo].[ApplicationTypes] SET [ApplicationTypeTitle] = @title,[ApplicationFees]=@fees" +
                "  where ApplicationTypeID = @id");
            var command = clsDatabaseFactory.CreateCommand(q, connection);
            clsDatabaseFactory.AddParam(command, "@id", id);
            clsDatabaseFactory.AddParam(command, "@title", title);
            clsDatabaseFactory.AddParam(command, "@fees", fees);
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
            var connection = clsDatabaseFactory.CreateConnection();
            string q = clsDatabaseFactory.GetQuery("select ApplicationFees from ApplicationTypes where ApplicationTypeTitle=@type;");
            var command = clsDatabaseFactory.CreateCommand(q, connection);
            clsDatabaseFactory.AddParam(command, "@type", Type);
            connection.Open();
            try
            {
                IDataReader Result = command.ExecuteReader();
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
            var connection = clsDatabaseFactory.CreateConnection();
            string q = clsDatabaseFactory.GetQuery("select ApplicationFees from ApplicationTypes where ApplicationTypeID=@type;");
            var command = clsDatabaseFactory.CreateCommand(q, connection);
            clsDatabaseFactory.AddParam(command, "@type", Type);
            connection.Open();
            try
            {
                IDataReader Result = command.ExecuteReader();
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
            var connection = clsDatabaseFactory.CreateConnection();
            string q = clsDatabaseFactory.GetQuery("select ApplicationTypeTitle from ApplicationTypes where ApplicationTypeID=@tAppType_id;");
            var command = clsDatabaseFactory.CreateCommand(q, connection);
            clsDatabaseFactory.AddParam(command, "@AppType_id", AppType_id);
            connection.Open();
            try
            {
                IDataReader Result = command.ExecuteReader();
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
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class clsSqlDriver
    {
        public static int AddNewDriver(int person_ID, int user_ID, DateTime date)
        {
            int ID = -1;
            var connection = clsDatabaseFactory.CreateConnection();

            // INSERT with SCOPE_IDENTITY → explicit PG RETURNING version
            string q = clsDatabaseFactory.GetQuery(
                " INSERT INTO [dbo].[Drivers] ([PersonID] ,[CreatedByUserID] ,[CreatedDate] ) " +
                "VALUES ( @person_ID, @user_ID, @date ) ; SELECT SCOPE_IDENTITY() ; ",

                " INSERT INTO \"Drivers\" (\"PersonID\" ,\"CreatedByUserID\" ,\"CreatedDate\" ) " +
                "VALUES ( @person_ID, @user_ID, @date ) RETURNING \"DriverID\" ; ");

            connection.Open();
            var command = clsDatabaseFactory.CreateCommand(q, connection);
            clsDatabaseFactory.AddParam(command, "@person_ID", person_ID);
            clsDatabaseFactory.AddParam(command, "@user_ID", user_ID);
            clsDatabaseFactory.AddParam(command, "@date", date);

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

        public static bool UpdateDriver(int driver_ID, int person_ID, int user_ID, DateTime date)
        {
            int r = 0;
            var connection = clsDatabaseFactory.CreateConnection();

            // Simple UPDATE – auto-convert
            string q = clsDatabaseFactory.GetQuery(
                "UPDATE [dbo].[Drivers] SET  [PersonID] =@person_ID ,[CreatedByUserID] =@user_ID  " +
                ",[CreatedDate] = @date WHERE  Drivers.DriverID=  @driver_ID");

            connection.Open();
            var command = clsDatabaseFactory.CreateCommand(q, connection);
            clsDatabaseFactory.AddParam(command, "@driver_ID", driver_ID);
            clsDatabaseFactory.AddParam(command, "@person_ID", person_ID);
            clsDatabaseFactory.AddParam(command, "@user_ID", user_ID);
            clsDatabaseFactory.AddParam(command, "@date", date);

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


        public static DataTable ListDrivers()
        {
            DataTable Table = new DataTable();
            var connection = clsDatabaseFactory.CreateConnection();

            // SQL-level '+' concatenation → explicit PG version with ||
            string q = clsDatabaseFactory.GetQuery(
                "select distinct Drivers.DriverID , People.PersonID , People.NationalNo , 'FullName'= CASE  WHEN People.FirstName  " +
                "IS NULL THEN '' else People.FirstName+' ' END +CASE  WHEN People.SecondName  IS NULL THEN '' else People.SecondName+" +
                "' ' END +CASE  WHEN People.ThirdName  IS NULL THEN ''else People.ThirdName+' 'END  +CASE  WHEN People.LastName  " +
                "IS NULL THEN '' else People.LastName  END \r\n, Drivers.CreatedDate , Licenses.IsActive from Drivers inner join " +
                "People on Drivers.PersonID = People.PersonID  join Licenses on Drivers.DriverID = Licenses.DriverID ; ",

                "select distinct \"Drivers\".\"DriverID\" , \"People\".\"PersonID\" , \"People\".\"NationalNo\" , CASE  WHEN \"People\".\"FirstName\"  " +
                "IS NULL THEN '' else \"People\".\"FirstName\"||' ' END ||CASE  WHEN \"People\".\"SecondName\"  IS NULL THEN '' else \"People\".\"SecondName\"||" +
                "' ' END ||CASE  WHEN \"People\".\"ThirdName\"  IS NULL THEN ''else \"People\".\"ThirdName\"||' 'END  ||CASE  WHEN \"People\".\"LastName\"  " +
                "IS NULL THEN '' else \"People\".\"LastName\"  END AS \"FullName\"\r\n, \"Drivers\".\"CreatedDate\" , \"Licenses\".\"IsActive\" from \"Drivers\" inner join " +
                "\"People\" on \"Drivers\".\"PersonID\" = \"People\".\"PersonID\"  join \"Licenses\" on \"Drivers\".\"DriverID\" = \"Licenses\".\"DriverID\" ; ");

            connection.Open();
            var command = clsDatabaseFactory.CreateCommand(q, connection);

            try
            {
                IDataReader result = command.ExecuteReader();
                Table.Load(result);
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


        public static bool FindDriverByDriverID(int driver_ID, ref int person_ID, ref int user_ID, ref DateTime date)
        {
            bool IsExist = false;
            var connection = clsDatabaseFactory.CreateConnection();

            // Simple SELECT – auto-convert
            string q = clsDatabaseFactory.GetQuery("select * from Drivers where Drivers.DriverID=  @driver_ID ;");

            connection.Open();
            var cmd = clsDatabaseFactory.CreateCommand(q, connection);
            clsDatabaseFactory.AddParam(cmd, "@driver_ID", driver_ID);

            try
            {
                IDataReader r = cmd.ExecuteReader();
                if (r.Read())
                {
                    person_ID = Convert.ToInt32(r[1]);
                    user_ID = Convert.ToInt32(r[2]);
                    date = Convert.ToDateTime(r[3]);

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


        public static bool IsDriverExistByPersonID(int person_ID, ref int driver_ID, ref int user_ID, ref DateTime date)
        {
            bool IsExist = false;
            var connection = clsDatabaseFactory.CreateConnection();

            // Simple SELECT – auto-convert
            string q = clsDatabaseFactory.GetQuery("select * from Drivers where Drivers.PersonID = @person_ID ;");

            connection.Open();
            var cmd = clsDatabaseFactory.CreateCommand(q, connection);
            clsDatabaseFactory.AddParam(cmd, "@person_ID", person_ID);

            try
            {
                IDataReader r = cmd.ExecuteReader();
                if (r.Read())
                {
                    driver_ID = Convert.ToInt32(r[0]);
                    user_ID = Convert.ToInt32(r[2]);
                    date = Convert.ToDateTime(r[3]);
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
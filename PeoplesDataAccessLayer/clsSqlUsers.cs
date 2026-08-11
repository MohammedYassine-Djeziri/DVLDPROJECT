using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class clsSqlUsers
    {
        public static bool IsUserExists(string username, string pass)
        {
            bool IsExist = false;
            var connection = clsDatabaseFactory.CreateConnection();
            // Dual version: SQL Server bit needs = 1; PostgreSQL boolean needs = TRUE.
            // (T-SQL has no 'true' literal -> 'Users.IsActive = true' fails on SQL Server;
            //  PostgreSQL does not support 'boolean = integer' -> '= 1' fails on PG.)
            string q = clsDatabaseFactory.GetQuery(
                "select * from Users where Users.IsActive = 1 and Users.Password=@pass  and Users.UserName=@UserName;",
                "select * from users where users.isactive = TRUE and users.password=@pass  and users.username=@UserName;");
            connection.Open();
            var cmd = clsDatabaseFactory.CreateCommand(q, connection);
            clsDatabaseFactory.AddParam(cmd, "@UserName", username);
            clsDatabaseFactory.AddParam(cmd, "@pass", pass);
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
            finally { connection.Close(); }
            return IsExist;
        }


        public static bool FindUserByUserNameAndPassword(string username, string pass, ref int UserID, ref int PerID, ref bool IsActive)
        {
            bool IsExist = false;
            var connection = clsDatabaseFactory.CreateConnection();
            // Dual version: SQL Server bit needs = 1; PostgreSQL boolean needs = TRUE.
            string q = clsDatabaseFactory.GetQuery(
                "select * from Users where Users.IsActive = 1 and Users.Password=@pass  and Users.UserName=@UserName;",
                "select * from users where users.isactive = TRUE and users.password=@pass  and users.username=@UserName;");
            connection.Open();
            var cmd = clsDatabaseFactory.CreateCommand(q, connection);
            clsDatabaseFactory.AddParam(cmd, "@UserName", username);
            clsDatabaseFactory.AddParam(cmd, "@pass", pass);
            try
            {
                IDataReader r = cmd.ExecuteReader();
                if (r.Read())
                {
                    UserID = Convert.ToInt32(r[0]);
                    PerID = Convert.ToInt32(r[1]);
                    IsActive = Convert.ToBoolean(r[4]);
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

        public static bool FindUserByUserID(ref string username, ref string pass, int UserID, ref int PerID, ref bool IsActive)
        {
            bool IsExist = false;
            var connection = clsDatabaseFactory.CreateConnection();
            string q = clsDatabaseFactory.GetQuery("select * from Users where UserID = @UserID");
            connection.Open();
            var cmd = clsDatabaseFactory.CreateCommand(q, connection);
            clsDatabaseFactory.AddParam(cmd, "@UserID", UserID);
            try
            {
                IDataReader r = cmd.ExecuteReader();
                if (r.Read())
                {
                    PerID = Convert.ToInt32(r[1]);
                    pass = r[3].ToString();
                    username = r[2].ToString();
                    IsActive = Convert.ToBoolean(r[4]);
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


        public static DataTable ListUsers()
        {
            DataTable dt = new DataTable();
            var connection = clsDatabaseFactory.CreateConnection();
            string q = clsDatabaseFactory.GetQuery("select * from Users;");
            connection.Open();
            var cmd = clsDatabaseFactory.CreateCommand(q, connection);
            try
            {
                IDataReader r = cmd.ExecuteReader();
                dt.Load(r);
                dt.Columns.Remove("Password");
            }
            catch (Exception ex)
            {
            }
            finally
            {
                connection.Close();
            }
            return dt;
        }

        public static bool IsUserExistByPersonID(int perID)
        {
            bool IsExist = false;
            var connection = clsDatabaseFactory.CreateConnection();
            string q = clsDatabaseFactory.GetQuery("select * from Users where PersonID=@perID");
            connection.Open();
            var cmd = clsDatabaseFactory.CreateCommand(q, connection);
            clsDatabaseFactory.AddParam(cmd, "@perID", perID);
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
            finally { connection.Close(); }
            return IsExist;
        }


        public static bool AddNewUser(ref int UserID, int perID, string UserName, string Pass, bool IsActive)
        {
            bool IsAdded = false;
            var connection = clsDatabaseFactory.CreateConnection();

            // INSERT with SCOPE_IDENTITY → explicit PG RETURNING version
            string q = clsDatabaseFactory.GetQuery(
                "INSERT INTO [dbo].[Users] ([PersonID],[UserName],[Password],[IsActive])VALUES(@perID,@UserName,@Pass,@Active)"
                + ";  SELECT SCOPE_IDENTITY() ;",

                "INSERT INTO users (personid,username,password,isactive)VALUES(@perID,@UserName,@Pass,@Active)"
                + " RETURNING userid ;");

            connection.Open();
            var cmd = clsDatabaseFactory.CreateCommand(q, connection);
            clsDatabaseFactory.AddParam(cmd, "@perID", perID);
            clsDatabaseFactory.AddParam(cmd, "@UserName", UserName);
            clsDatabaseFactory.AddParam(cmd, "@Pass", Pass);
            clsDatabaseFactory.AddParam(cmd, "@Active", IsActive);

            try
            {
                object Result = cmd.ExecuteScalar();
                if (Result != null)
                {
                    IsAdded = true;
                    UserID = Convert.ToInt32(Result);
                }
            }
            catch (Exception ex)
            {
            }
            finally { connection.Close(); }
            return IsAdded;
        }


        public static bool UpdateUser(int UserID, int perID, string UserName, string Pass, bool IsActive)
        {
            bool IsUpdated = false;
            var connection = clsDatabaseFactory.CreateConnection();
            string q = clsDatabaseFactory.GetQuery(
                @"UPDATE [dbo].[Users] SET [PersonID] = @perID ,[UserName] = @UserName ,[Password] = @Pass
                ,[IsActive] = @Active  WHERE Users.UserID= @UserID;");
            connection.Open();
            var cmd = clsDatabaseFactory.CreateCommand(q, connection);
            clsDatabaseFactory.AddParam(cmd, "@UserID", UserID);
            clsDatabaseFactory.AddParam(cmd, "@perID", perID);
            clsDatabaseFactory.AddParam(cmd, "@UserName", UserName);
            clsDatabaseFactory.AddParam(cmd, "@Pass", Pass);
            clsDatabaseFactory.AddParam(cmd, "@Active", IsActive);

            try
            {
                int Result = cmd.ExecuteNonQuery();
                if (Result >= 1)
                {
                    IsUpdated = true;
                }
            }
            catch (Exception ex)
            {
            }
            finally { connection.Close(); }
            return IsUpdated;
        }


        public static bool DeleteUser(int UserID)
        {
            bool IsDeleted = false;
            var connection = clsDatabaseFactory.CreateConnection();
            string q = clsDatabaseFactory.GetQuery("delete from Users where UserID=@UserID");
            connection.Open();
            var cmd = clsDatabaseFactory.CreateCommand(q, connection);
            clsDatabaseFactory.AddParam(cmd, "@UserID", UserID);
            try
            {
                int Result = cmd.ExecuteNonQuery();
                if (Result >= 1)
                {
                    IsDeleted = true;
                }
            }
            catch (Exception ex)
            {
            }
            finally { connection.Close(); }
            return IsDeleted;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class clsSqlUsers
    {

        public static string DVLD_Connection_Info => clsConnectionSettings.ConnectionString;

        public static bool IsUserExists(string username, string pass)
        {
            bool IsExist = false;
            SqlConnection connection = new SqlConnection(DVLD_Connection_Info);
            string q = "select * from Users where Users.IsActive = 1 and Users.Password=@pass  and Users.UserName=@UserName;";
            connection.Open();
            SqlCommand cmd = new SqlCommand(q, connection);
            cmd.Parameters.AddWithValue("username", username);
            cmd.Parameters.AddWithValue("pass", pass);
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
            finally { connection.Close(); }


            return IsExist;
        }


        public static bool FindUserByUserNameAndPassword(string username, string pass, ref int UserID, ref int PerID, ref bool IsActive)
        {
            bool IsExist = false;
            SqlConnection connection = new SqlConnection(DVLD_Connection_Info);
            string q = "select * from Users where Users.IsActive = 1 and Users.Password=@pass  and Users.UserName=@UserName;";
            connection.Open();
            SqlCommand cmd = new SqlCommand(q, connection);
            cmd.Parameters.AddWithValue("Username", username);
            cmd.Parameters.AddWithValue("pass", pass);
            try
            {
                SqlDataReader r = cmd.ExecuteReader();
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
                //logger.LogError(ex, "clsSqlUsers.FindUserByUserNameAndPassword");
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
            SqlConnection connection = new SqlConnection(DVLD_Connection_Info);
            string q = "select * from Users where UserID = @UserID";
            connection.Open();
            SqlCommand cmd = new SqlCommand(q, connection);
            cmd.Parameters.AddWithValue("@UserID", UserID);
            try
            {
                SqlDataReader r = cmd.ExecuteReader();
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
                //logger.LogError(ex, "clsSqlUsers.FindUserByUserID");
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
            SqlConnection connection = new SqlConnection(DVLD_Connection_Info);
            string q = "select * from Users;";
            connection.Open();
            SqlCommand cmd = new SqlCommand(q, connection);

            try
            {
                SqlDataReader r = cmd.ExecuteReader();
                if (r.HasRows)
                {
                    dt.Load(r);
                    dt.Columns.Remove("Password");
                }
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
            SqlConnection connection = new SqlConnection(DVLD_Connection_Info);
            string q = "select * from Users where PersonID=@perID";
            connection.Open();
            SqlCommand cmd = new SqlCommand(q, connection);
            cmd.Parameters.AddWithValue("perID", perID);
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
            finally { connection.Close(); }


            return IsExist;
        }


        public static bool AddNewUser(ref int UserID ,int perID , string UserName , string Pass , bool IsActive)
        {
            bool IsAdded = false;
            Byte f = 0;
            if(IsActive) { f= 1; }
            SqlConnection connection = new SqlConnection(DVLD_Connection_Info);
            string q = "INSERT INTO [dbo].[Users] ([PersonID],[UserName],[Password],[IsActive])VALUES(@perID,@UserName,@Pass,@Active)"
                +";  SELECT SCOPE_IDENTITY() ;";
            connection.Open();
            SqlCommand cmd = new SqlCommand(q, connection);
            cmd.Parameters.AddWithValue("@perID", perID);
            cmd.Parameters.AddWithValue("@UserName", UserName);
            cmd.Parameters.AddWithValue("@Pass", Pass);
            cmd.Parameters.AddWithValue("@Active", f);

            try
            {
                object  Result = cmd.ExecuteScalar();
                if(Result!= DBNull.Value) {  
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


        //delete from Users where UserID=17;
        public static bool UpdateUser(int UserID, int perID, string UserName, string Pass, bool IsActive)
        {
            bool IsUpdated = false;
            Byte f = 0;
            if (IsActive) { f = 1; }
            SqlConnection connection = new SqlConnection(DVLD_Connection_Info);
            string q = @"UPDATE [dbo].[Users] SET [PersonID] = @perID ,[UserName] = @UserName ,[Password] = @Pass
                ,[IsActive] = @Active  WHERE Users.UserID= @UserID;";
            connection.Open();
            SqlCommand cmd = new SqlCommand(q, connection);
            cmd.Parameters.AddWithValue("@UserID", UserID);
            cmd.Parameters.AddWithValue("@perID", perID);
            cmd.Parameters.AddWithValue("@UserName", UserName);
            cmd.Parameters.AddWithValue("@Pass", Pass);
            cmd.Parameters.AddWithValue("@Active", f);

            try
            {
                int Result = cmd.ExecuteNonQuery();
                if (Result >=1)
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

            SqlConnection connection = new SqlConnection(DVLD_Connection_Info);
            string q = "delete from Users where UserID=@UserID";
            connection.Open();
            SqlCommand cmd = new SqlCommand(q, connection);
            cmd.Parameters.AddWithValue("@UserID", UserID);
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
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class clsSqlDriver
    {
        public static string DVLD_Connection_Info => clsConnectionSettings.ConnectionString;

        public static int AddNewDriver( int person_ID, int user_ID, DateTime date)
        {
            int ID = -1;
            SqlConnection connection = new SqlConnection(DVLD_Connection_Info);
            string q = " INSERT INTO [dbo].[Drivers] ([PersonID] ,[CreatedByUserID] ,[CreatedDate] ) " +
                "VALUES ( @person_ID, @user_ID, @date ) ; SELECT SCOPE_IDENTITY() ; ";
            connection.Open();
            SqlCommand command = new SqlCommand(q, connection);
            command.Parameters.AddWithValue("@person_ID", person_ID);
            command.Parameters.AddWithValue("@user_ID", user_ID);
            command.Parameters.AddWithValue("@date", date);



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
            SqlConnection connection = new SqlConnection(DVLD_Connection_Info);
            string q = "UPDATE [dbo].[Drivers] SET  [PersonID] =@person_ID ,[CreatedByUserID] =@user_ID  " +
                ",[CreatedDate] = @date WHERE  Drivers.DriverID=  @driver_ID";
            connection.Open();
            SqlCommand command = new SqlCommand(q, connection);
            command.Parameters.AddWithValue("@driver_ID", driver_ID);
            command.Parameters.AddWithValue("@person_ID", person_ID);
            command.Parameters.AddWithValue("@user_ID", user_ID);
            command.Parameters.AddWithValue("@date", date);

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

            SqlConnection connection = new SqlConnection(DVLD_Connection_Info);
            string q = "select distinct Drivers.DriverID , People.PersonID , People.NationalNo , 'FullName'= CASE  WHEN People.FirstName  " +
                "IS NULL THEN '' else People.FirstName+' ' END +CASE  WHEN People.SecondName  IS NULL THEN '' else People.SecondName+" +
                "' ' END +CASE  WHEN People.ThirdName  IS NULL THEN ''else People.ThirdName+' 'END  +CASE  WHEN People.LastName  " +
                "IS NULL THEN '' else People.LastName  END \r\n, Drivers.CreatedDate , Licenses.IsActive from Drivers inner join " +
                "People on Drivers.PersonID = People.PersonID  join Licenses on Drivers.DriverID = Licenses.DriverID ; ";
            connection.Open();
            SqlCommand command = new SqlCommand(q, connection);

            try
            {

                SqlDataReader result = command.ExecuteReader();
                if (result.HasRows)
                {
                    Table.Load(result);
                }

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
            SqlConnection connection = new SqlConnection(DVLD_Connection_Info);
            string q = "select * from Drivers where Drivers.DriverID=  @driver_ID ;";
            connection.Open();
            SqlCommand cmd = new SqlCommand(q, connection);
            cmd.Parameters.AddWithValue("@driver_ID", driver_ID);

            try
            {
                SqlDataReader r = cmd.ExecuteReader();
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




        public static bool IsDriverExistByPersonID(int person_ID , ref int driver_ID , ref int user_ID , ref DateTime date)
        {
            bool IsExist = false; 
            SqlConnection connection = new SqlConnection(DVLD_Connection_Info);
            string q = "select * from Drivers where Drivers.PersonID = @person_ID ;";
            connection.Open();
            SqlCommand cmd = new SqlCommand(q, connection);
            cmd.Parameters.AddWithValue("@person_ID", person_ID);


                try
                {
                    SqlDataReader r = cmd.ExecuteReader();
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

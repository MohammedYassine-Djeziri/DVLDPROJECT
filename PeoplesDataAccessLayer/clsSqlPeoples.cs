using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace DataAccessLayer
{
    public class clsSqlPeoples
    {
        public static string DVLD_Connection_Info => clsConnectionSettings.ConnectionString;


        public static int AddPerson(string NatNub, string FN, string SN, string TN, string LN, string Phn, string Em
            , int Nat, DateTime date, int gender, string Addr, string Img)
        {
            int ID = -1;
            SqlConnection connection = new SqlConnection(DVLD_Connection_Info);
            if (Img != "")
            {
                Guid guid = Guid.NewGuid();
                string path = @"C:\Users\mohya\source\repos\ProjectBackUp\DVLD_Project\ImageCopy\" + guid.ToString() + ".png";
                File.Copy(Img, path);
                Img = path;
            }
            string q = "INSERT INTO [dbo].[People] ([NationalNo] ,[FirstName] ,[SecondName],[ThirdName]"
               + " ,[LastName] ,[DateOfBirth] ,[Gender] ,[Address] ,[Phone] ,[Email] ,[NationalityCountryID]" +
               " ,[ImagePath]) VALUES(@NatNub,@FN,@SN,@TN,@LN,@date, @gender ,@Addr," +
               " @Phn,@Em , @Nat, @Img) ;  SELECT SCOPE_IDENTITY() ;";
            connection.Open();
            SqlCommand command = new SqlCommand(q, connection);
            command.Parameters.AddWithValue("@NatNub", NatNub);
            command.Parameters.AddWithValue("@FN", FN);
            command.Parameters.AddWithValue("@SN", SN);
            command.Parameters.AddWithValue("@LN", LN);
            command.Parameters.AddWithValue("@date", date);
            command.Parameters.AddWithValue("@gender", gender);
            command.Parameters.AddWithValue("@Addr", Addr);
            command.Parameters.AddWithValue("@Phn", Phn);
            command.Parameters.AddWithValue("@Nat", Nat);

            if (Em != "")
            {
                command.Parameters.AddWithValue("@Em", Em);
            }
            else
            {
                command.Parameters.AddWithValue("@Em", DBNull.Value);
            }
            if (TN == "")
            {
                command.Parameters.AddWithValue("@TN", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@TN", TN);
            }
            if (Img == "")
            {
                command.Parameters.AddWithValue("@Img", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@Img", Img);
            }

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


        public static bool UpdatePerson(int ID, string NatNub, string FN, string SN, string TN, string LN, string Phn, string Em
           , int Nat, DateTime date, int gender, string Addr, ref string Img, ref string LastImg)
        {
            bool IsUpdated = false;
            SqlConnection connection = new SqlConnection(DVLD_Connection_Info);
            Guid guid = Guid.NewGuid();
            string path = "";
            if (Img != "")
            {
                path = @"C:\Users\mohya\source\repos\ProjectBackUp\DVLD_Project\ImageCopy\" + guid.ToString() + ".png";
                File.Copy(Img, path);
            }
            Img = path;

            string q = "update [dbo].[People] set  [NationalNo]=@NatNub ,[FirstName]=@FN ,[SecondName]=@SN,[ThirdName]=@TN"
               + " ,[LastName]=@LN ,[DateOfBirth]=@date ,[Gender]=@gender ,[Address]=@Addr ,[Phone]=@Phn ,[Email]=@Em " +
               ",[NationalityCountryID]=@Nat , [ImagePath]=@Img where PersonID=@ID";
            connection.Open();
            SqlCommand command = new SqlCommand(q, connection);
            command.Parameters.AddWithValue("@ID", ID);
            command.Parameters.AddWithValue("@NatNub", NatNub);
            command.Parameters.AddWithValue("@FN", FN);
            command.Parameters.AddWithValue("@SN", SN);
            command.Parameters.AddWithValue("@LN", LN);
            command.Parameters.AddWithValue("@date", date);
            command.Parameters.AddWithValue("@gender", gender);
            command.Parameters.AddWithValue("@Addr", Addr);
            command.Parameters.AddWithValue("@Phn", Phn);
            command.Parameters.AddWithValue("@Nat", Nat);
            if (Em != "")
            {
                command.Parameters.AddWithValue("@Em", Em);
            }
            else
            {
                command.Parameters.AddWithValue("@Em", DBNull.Value);
            }
            if (TN == "")
            {
                command.Parameters.AddWithValue("@TN", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@TN", TN);
            }
            if (Img == "")
            {
                command.Parameters.AddWithValue("@Img", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@Img", Img);
            }

            try
            {



                int result = command.ExecuteNonQuery();
                if (result >= 1)
                {
                    IsUpdated = true;
                }

            }

            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();

            }
            if (LastImg != "")
            {
                try
                {
                    File.Delete(LastImg);
                }
                catch
                {

                }

            }
            LastImg = Img;
            return IsUpdated;
        }


        public static bool DeletePerson(int ID)
        {
            bool IsDeleted = false;
            SqlConnection connection = new SqlConnection(DVLD_Connection_Info);
            string q = "delete from People where PersonID=@ID";
            connection.Open();
            SqlCommand command = new SqlCommand(q, connection);
            command.Parameters.AddWithValue("@ID", ID);

            try
            {



                int result = command.ExecuteNonQuery();
                if (result >= 1)
                {
                    IsDeleted = true;
                }

            }

            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();

            }
            return IsDeleted;
        }


        public static DataTable ListPeoples()
        {

            Stopwatch stopwatch1 = Stopwatch.StartNew();
           

            DataTable table = new DataTable();
            //table.Columns.Add("ID", typeof(int));
            //table.Columns.Add("Name", typeof(string));
            //table.Rows.Add(1, "One Piece");
            SqlConnection connection = new SqlConnection(DVLD_Connection_Info);
            string q = "select People.PersonID , People.NationalNo , People.FirstName , People.SecondName , People.ThirdName, " +
                "People.LastName , People.DateOfBirth , Gender=\r\ncase \r\nwhen (People.Gender = 0) then 'Male'\r\nwhen " +
                "(People.Gender = 1) then 'Female'\r\nelse 'Croissant'\r\nend\r\n, People.Phone, People.Email , " +
                "Nationality= Countries.CountryName \r\nfrom People  inner join Countries on " +
                "People.NationalityCountryID = Countries.CountryID;";
            connection.Open();
            SqlCommand command = new SqlCommand(q, connection);
            try
            {
                SqlDataReader result = command.ExecuteReader();

                if (result.HasRows)
                {
                    table.Load(result);
                }

            }

            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
                stopwatch1.Stop();
                File.WriteAllText(@"C:\Users\mohya\OneDrive\Bureau\Untitled.txt" , stopwatch1.ElapsedMilliseconds.ToString());

            }
            return table;
        }



        public static DataTable ListCountries()
        {
            DataTable table = new DataTable();
            SqlConnection connection = new SqlConnection(DVLD_Connection_Info);
            string q = "select * from Countries";
            connection.Open();
            SqlCommand command = new SqlCommand(q, connection);
            try
            {
                SqlDataReader result = command.ExecuteReader();

                if (result.HasRows)
                {
                    table.Load(result);
                }

            }

            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();

            }
            return table;
        }


        public static bool FindByPersonalID(int PerID, ref string NatNub, ref string FN, ref string SN, ref string TN,
            ref string LN, ref string Phn, ref string Em, ref int Nat, ref DateTime date, ref int gender,
            ref string Addr, ref string Img)
        {
            bool IsExists = false;
            SqlConnection connection = new SqlConnection(DVLD_Connection_Info);
            string q = "select * from People where PersonID=@PerID";
            connection.Open();
            SqlCommand command = new SqlCommand(q, connection);
            command.Parameters.AddWithValue("@PerID", PerID);
            try
            {
                SqlDataReader result = command.ExecuteReader();

                if (result.Read())
                {
                    IsExists = true;
                    NatNub = result[1].ToString();
                    FN = result[2].ToString();
                    SN = result[3].ToString();
                    TN = result[4].ToString();
                    LN = result[5].ToString();
                    Phn = result[9].ToString();
                    Em = result[10].ToString();
                    Nat = Convert.ToInt32(result[11]);
                    date = Convert.ToDateTime(result[6]);
                    gender = Convert.ToInt32(result[7]);
                    Addr = result[8].ToString();
                    Img = result[12].ToString();
                }

            }

            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();

            }
            return IsExists;
        }


        public static bool IsNationalNumberExists(string NatNub)
        {
            SqlConnection connection = new SqlConnection(DVLD_Connection_Info);
            string q = "select * from People where NationalNo = @NatNub";
            SqlCommand cmd = new SqlCommand(q, connection);
            cmd.Parameters.AddWithValue("@NatNub", NatNub);

            bool ISExists = false;

            connection.Open();
            try
            {
                SqlDataReader Result = cmd.ExecuteReader();
                if (Result.HasRows)
                {
                    ISExists = true;
                }

            }
            catch (Exception ex)
            {

            }
            finally
            { connection.Close(); }


            return ISExists;


        }




        public static string GetPersonFullNameByPersonID(int person_id)
        {
            string Full_Name = "";
            SqlConnection connection = new SqlConnection(DVLD_Connection_Info);
            string q = "select CASE  WHEN People.FirstName  IS NULL THEN '' else People.FirstName+' '  " +
                "END +CASE  WHEN People.SecondName  IS NULL THEN '' else People.SecondName+' ' " +
                "END +CASE  WHEN People.ThirdName IS NULL THEN ''  else People.ThirdName+' 'END  " +
                "+CASE  WHEN People.LastName  IS NULL THEN '' else People.LastName  END from " +
                "People where PersonID =@person_id;";
            SqlCommand command = new SqlCommand(q, connection);
            command.Parameters.AddWithValue("@person_id", person_id);

            connection.Open();
            try
            {
                SqlDataReader Result = command.ExecuteReader();
                if (Result.Read())
                {
                    Full_Name = Result[0].ToString();
                }
            }
            catch { }
            finally { connection.Close(); }
            return Full_Name;

        }



        public static bool FindByNationalNumber(string NatNub , ref int PerID,  ref string FN, ref string SN, ref string TN,
            ref string LN, ref string Phn, ref string Em, ref int Nat, ref DateTime date, ref int gender,
            ref string Addr, ref string Img)
        {
            SqlConnection connection = new SqlConnection(DVLD_Connection_Info);
            string q = "select * from People where NationalNo = @NatNub";
            SqlCommand cmd = new SqlCommand(q, connection);
            cmd.Parameters.AddWithValue("@NatNub", NatNub);
            bool IsExists = false;

            connection.Open();
            try
            {
                SqlDataReader result = cmd.ExecuteReader();
                if (result.Read())
                {
                    IsExists = true;
                    PerID = Convert.ToInt32(result[0]);
                    FN = result[2].ToString();
                    SN = result[3].ToString();
                    TN = result[4].ToString();
                    LN = result[5].ToString();
                    Phn = result[9].ToString();
                    Em = result[10].ToString();
                    Nat = Convert.ToInt32(result[11]);
                    date = Convert.ToDateTime(result[6]);
                    gender = Convert.ToInt32(result[7]);
                    Addr = result[8].ToString();
                    Img = result[12].ToString();
                }

            }
            catch (Exception ex)
            {

            }
            finally
            { 
                connection.Close(); 
            }

            return IsExists;

        }
    }

}
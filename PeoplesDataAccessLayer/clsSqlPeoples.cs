using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace DataAccessLayer
{
    public class clsSqlPeoples
    {
        public static int AddPerson(string NatNub, string FN, string SN, string TN, string LN, string Phn, string Em
            , int Nat, DateTime date, int gender, string Addr, string Img)
        {
            int ID = -1;
            var connection = clsDatabaseFactory.CreateConnection();
            // Image file handling (copy to the ImageCopy folder) is now done in
            // the business layer (clsPeoples). Here we simply persist the path.

            // MSSQL: INSERT … SCOPE_IDENTITY(). PG: INSERT … RETURNING "PersonID"
            string q = clsDatabaseFactory.GetQuery(
                "INSERT INTO [dbo].[People] ([NationalNo] ,[FirstName] ,[SecondName],[ThirdName]"
               + " ,[LastName] ,[DateOfBirth] ,[Gender] ,[Address] ,[Phone] ,[Email] ,[NationalityCountryID]"
               + " ,[ImagePath]) VALUES(@NatNub,@FN,@SN,@TN,@LN,@date, @gender ,@Addr,"
               + " @Phn,@Em , @Nat, @Img) ;  SELECT SCOPE_IDENTITY() ;",

                "INSERT INTO \"People\" (\"NationalNo\" ,\"FirstName\" ,\"SecondName\",\"ThirdName\""
               + " ,\"LastName\" ,\"DateOfBirth\" ,\"Gender\" ,\"Address\" ,\"Phone\" ,\"Email\" ,\"NationalityCountryID\""
               + " ,\"ImagePath\") VALUES(@NatNub,@FN,@SN,@TN,@LN,@date, @gender ,@Addr,"
               + " @Phn,@Em , @Nat, @Img) RETURNING \"PersonID\" ;");

            connection.Open();
            var command = clsDatabaseFactory.CreateCommand(q, connection);
            clsDatabaseFactory.AddParam(command, "@NatNub", NatNub);
            clsDatabaseFactory.AddParam(command, "@FN", FN);
            clsDatabaseFactory.AddParam(command, "@SN", SN);
            clsDatabaseFactory.AddParam(command, "@LN", LN);
            clsDatabaseFactory.AddParam(command, "@date", date);
            clsDatabaseFactory.AddParam(command, "@gender", gender);
            clsDatabaseFactory.AddParam(command, "@Addr", Addr);
            clsDatabaseFactory.AddParam(command, "@Phn", Phn);
            clsDatabaseFactory.AddParam(command, "@Nat", Nat);

            if (Em != "")
            {
                clsDatabaseFactory.AddParam(command, "@Em", Em);
            }
            else
            {
                clsDatabaseFactory.AddParam(command, "@Em", DBNull.Value);
            }
            if (TN == "")
            {
                clsDatabaseFactory.AddParam(command, "@TN", DBNull.Value);
            }
            else
            {
                clsDatabaseFactory.AddParam(command, "@TN", TN);
            }
            if (Img == "")
            {
                clsDatabaseFactory.AddParam(command, "@Img", DBNull.Value);
            }
            else
            {
                clsDatabaseFactory.AddParam(command, "@Img", Img);
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
           , int Nat, DateTime date, int gender, string Addr, string Img)
        {
            bool IsUpdated = false;
            var connection = clsDatabaseFactory.CreateConnection();
            // Image file handling is now done in the business layer (clsPeoples).

            // Simple UPDATE – auto-convert handles brackets
            string q = clsDatabaseFactory.GetQuery(
                "update [dbo].[People] set  [NationalNo]=@NatNub ,[FirstName]=@FN ,[SecondName]=@SN,[ThirdName]=@TN"
               + " ,[LastName]=@LN ,[DateOfBirth]=@date ,[Gender]=@gender ,[Address]=@Addr ,[Phone]=@Phn ,[Email]=@Em "
               + ",[NationalityCountryID]=@Nat , [ImagePath]=@Img where PersonID=@ID");

            connection.Open();
            var command = clsDatabaseFactory.CreateCommand(q, connection);
            clsDatabaseFactory.AddParam(command, "@ID", ID);
            clsDatabaseFactory.AddParam(command, "@NatNub", NatNub);
            clsDatabaseFactory.AddParam(command, "@FN", FN);
            clsDatabaseFactory.AddParam(command, "@SN", SN);
            clsDatabaseFactory.AddParam(command, "@LN", LN);
            clsDatabaseFactory.AddParam(command, "@date", date);
            clsDatabaseFactory.AddParam(command, "@gender", gender);
            clsDatabaseFactory.AddParam(command, "@Addr", Addr);
            clsDatabaseFactory.AddParam(command, "@Phn", Phn);
            clsDatabaseFactory.AddParam(command, "@Nat", Nat);
            if (Em != "")
            {
                clsDatabaseFactory.AddParam(command, "@Em", Em);
            }
            else
            {
                clsDatabaseFactory.AddParam(command, "@Em", DBNull.Value);
            }
            if (TN == "")
            {
                clsDatabaseFactory.AddParam(command, "@TN", DBNull.Value);
            }
            else
            {
                clsDatabaseFactory.AddParam(command, "@TN", TN);
            }
            if (Img == "")
            {
                clsDatabaseFactory.AddParam(command, "@Img", DBNull.Value);
            }
            else
            {
                clsDatabaseFactory.AddParam(command, "@Img", Img);
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
            return IsUpdated;
        }


        public static bool DeletePerson(int ID)
        {
            bool IsDeleted = false;
            var connection = clsDatabaseFactory.CreateConnection();
            // Simple DELETE – auto-convert
            string q = clsDatabaseFactory.GetQuery("delete from People where PersonID=@ID");
            connection.Open();
            var command = clsDatabaseFactory.CreateCommand(q, connection);
            clsDatabaseFactory.AddParam(command, "@ID", ID);

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
  
            DataTable table = new DataTable();
            var connection = clsDatabaseFactory.CreateConnection();

            // SQL-level '+' concatenation → needs explicit PG version with ||
            string q = clsDatabaseFactory.GetQuery(
                "select People.PersonID , People.NationalNo , People.FirstName , People.SecondName , People.ThirdName, " +
                "People.LastName , People.DateOfBirth , Gender=\r\ncase \r\nwhen (People.Gender = 0) then 'Male'\r\nwhen " +
                "(People.Gender = 1) then 'Female'\r\nelse 'Croissant'\r\nend\r\n, People.Phone, People.Email , " +
                "Nationality= Countries.CountryName \r\nfrom People  inner join Countries on " +
                "People.NationalityCountryID = Countries.CountryID;",

                "select \"People\".\"PersonID\" , \"People\".\"NationalNo\" , \"People\".\"FirstName\" , \"People\".\"SecondName\" , \"People\".\"ThirdName\", " +
                "\"People\".\"LastName\" , \"People\".\"DateOfBirth\" , \r\ncase \r\nwhen (\"People\".\"Gender\" = 0) then 'Male'\r\nwhen " +
                "(\"People\".\"Gender\" = 1) then 'Female'\r\nelse 'Croissant'\r\nend AS \"Gender\"\r\n, \"People\".\"Phone\", \"People\".\"Email\" , " +
                "\"Countries\".\"CountryName\" AS \"Nationality\" \r\nfrom \"People\"  inner join \"Countries\" on " +
                "\"People\".\"NationalityCountryID\" = \"Countries\".\"CountryID\";");

            connection.Open();
            var command = clsDatabaseFactory.CreateCommand(q, connection);
            try
            {
                IDataReader result = command.ExecuteReader();
                table.Load(result);
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


        public static DataTable ListCountries()
        {
            DataTable table = new DataTable();
            var connection = clsDatabaseFactory.CreateConnection();
            // Simple SELECT – auto-convert
            string q = clsDatabaseFactory.GetQuery("select * from Countries");
            connection.Open();
            var command = clsDatabaseFactory.CreateCommand(q, connection);
            try
            {
                IDataReader result = command.ExecuteReader();
                table.Load(result);
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
            var connection = clsDatabaseFactory.CreateConnection();
            // Simple SELECT – auto-convert
            string q = clsDatabaseFactory.GetQuery("select * from People where PersonID=@PerID");
            connection.Open();
            var command = clsDatabaseFactory.CreateCommand(q, connection);
            clsDatabaseFactory.AddParam(command, "@PerID", PerID);
            try
            {
                IDataReader result = command.ExecuteReader();
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
            var connection = clsDatabaseFactory.CreateConnection();
            // Simple SELECT – auto-convert
            string q = clsDatabaseFactory.GetQuery("select * from People where NationalNo = @NatNub");
            var command = clsDatabaseFactory.CreateCommand(q, connection);
            clsDatabaseFactory.AddParam(command, "@NatNub", NatNub);

            bool ISExists = false;
            connection.Open();
            try
            {
                IDataReader Result = command.ExecuteReader();
                if (Result.Read())
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
            var connection = clsDatabaseFactory.CreateConnection();

            // SQL-level '+' concatenation → needs explicit PG version with ||
            string q = clsDatabaseFactory.GetQuery(
                "select CASE  WHEN People.FirstName  IS NULL THEN '' else People.FirstName+' '  " +
                "END +CASE  WHEN People.SecondName  IS NULL THEN '' else People.SecondName+' ' " +
                "END +CASE  WHEN People.ThirdName IS NULL THEN ''  else People.ThirdName+' 'END  " +
                "+CASE  WHEN People.LastName  IS NULL THEN '' else People.LastName  END from " +
                "People where PersonID =@person_id;",

                "select CASE  WHEN \"People\".\"FirstName\"  IS NULL THEN '' else \"People\".\"FirstName\"||' '  " +
                "END ||CASE  WHEN \"People\".\"SecondName\"  IS NULL THEN '' else \"People\".\"SecondName\"||' ' " +
                "END ||CASE  WHEN \"People\".\"ThirdName\" IS NULL THEN ''  else \"People\".\"ThirdName\"||' 'END  " +
                "||CASE  WHEN \"People\".\"LastName\"  IS NULL THEN '' else \"People\".\"LastName\"  END from " +
                "\"People\" where \"PersonID\" =@person_id;");

            var command = clsDatabaseFactory.CreateCommand(q, connection);
            clsDatabaseFactory.AddParam(command, "@person_id", person_id);

            connection.Open();
            try
            {
                IDataReader Result = command.ExecuteReader();
                if (Result.Read())
                {
                    Full_Name = Result[0].ToString();
                }
            }
            catch { }
            finally { connection.Close(); }
            return Full_Name;
        }


        public static bool FindByNationalNumber(string NatNub, ref int PerID, ref string FN, ref string SN, ref string TN,
            ref string LN, ref string Phn, ref string Em, ref int Nat, ref DateTime date, ref int gender,
            ref string Addr, ref string Img)
        {
            var connection = clsDatabaseFactory.CreateConnection();
            // Simple SELECT – auto-convert
            string q = clsDatabaseFactory.GetQuery("select * from People where NationalNo = @NatNub");
            var command = clsDatabaseFactory.CreateCommand(q, connection);
            clsDatabaseFactory.AddParam(command, "@NatNub", NatNub);
            bool IsExists = false;

            connection.Open();
            try
            {
                IDataReader result = command.ExecuteReader();
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
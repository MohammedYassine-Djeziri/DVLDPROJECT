using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class clsSqlApplications
    {
        public static string DVLD_Connection_Info => clsConnectionSettings.ConnectionString;

        public static int AddNewApplication(int PerID ,DateTime AppDate , int AppType , int AppStatus , DateTime LastDate , 
            float Fees , int UserID)
        {
            int ID = -1;
            SqlConnection connection = new SqlConnection(DVLD_Connection_Info);
            string q = " INSERT INTO [dbo].[Applications] ([ApplicantPersonID],[ApplicationDate],[ApplicationTypeID]," +
                "[ApplicationStatus],[LastStatusDate],[PaidFees],[CreatedByUserID]) VALUES(@PerID,@AppDate,@AppType,@AppStatus" +
                ",@LastDate,@Fees,@UserID) ; SELECT SCOPE_IDENTITY() ;";
            connection.Open();
            SqlCommand command = new SqlCommand(q, connection);
            command.Parameters.AddWithValue("@PerID", PerID);
            command.Parameters.AddWithValue("@AppDate", AppDate);
            command.Parameters.AddWithValue("@AppType", AppType);
            command.Parameters.AddWithValue("@AppStatus", AppStatus);
            command.Parameters.AddWithValue("@LastDate", LastDate);
            command.Parameters.AddWithValue("@Fees", Fees);
            command.Parameters.AddWithValue("@UserID", UserID);

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

        public static bool UpdateApplication(int AppID , int PerID, DateTime AppDate, int AppType, int AppStatus, DateTime LastDate,
            float Fees, int UserID)
        {
            int r = 0;
            SqlConnection connection = new SqlConnection(DVLD_Connection_Info);
            string q = " Update INTO [dbo].[Applications] set ([ApplicantPersonID],[ApplicationDate],[ApplicationTypeID]," +
                "[ApplicationStatus],[LastStatusDate],[PaidFees],[CreatedByUserID]) VALUES(@PerID,@AppDate,@AppType,@AppStatus" +
                ",@LastDate,@Fees,@UserID ) where ApplicationID = @AppID";
            connection.Open();
            SqlCommand command = new SqlCommand(q, connection);
            command.Parameters.AddWithValue("@AppID", AppID);
            command.Parameters.AddWithValue("@PerID", PerID);
            command.Parameters.AddWithValue("@AppDate", AppDate);
            command.Parameters.AddWithValue("@AppType", AppType);
            command.Parameters.AddWithValue("@AppStatus", AppStatus);
            command.Parameters.AddWithValue("@dLastDate", LastDate);
            command.Parameters.AddWithValue("@Fees", Fees);
            command.Parameters.AddWithValue("@UserID", UserID);

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
            return r>=1;
        }

        public static bool IsLicenseClassAlreadyUsed(int PerID , int LicenseClassID  )
        {
            bool IsActive = false;
            SqlConnection connection = new SqlConnection(DVLD_Connection_Info);
            string q = "select 1 from Applications inner join LocalDrivingLicenseApplications on  " +
            "LocalDrivingLicenseApplications.ApplicationID=Applications.ApplicationID where ApplicantPersonID=@PerID and " +
            "LocalDrivingLicenseApplications.LicenseClassID= @LicenseClassID and ApplicationStatus != 2;";
            connection.Open();
            SqlCommand command = new SqlCommand(q, connection);
            command.Parameters.AddWithValue("@PerID", PerID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            try
            {

                SqlDataReader result = command.ExecuteReader();
                if (result.HasRows)
                {
                    IsActive = true;
                }

            }

            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();

            }

            return IsActive;
        }
        public static DataTable ListLDLApplication()
        {
            DataTable Table = new DataTable();

            SqlConnection connection = new SqlConnection(DVLD_Connection_Info);
            string q = "select 'LDLAppID'=LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID , " +
                "'Driving Class'= ( select LicenseClasses.ClassName from LicenseClasses where" +
                " LicenseClassID= LocalDrivingLicenseApplications.LicenseClassID) , " +
                "People.NationalNo, 'FullName'= CASE  WHEN People.FirstName  IS NULL THEN '' else " +
                "People.FirstName+' '  END +CASE  WHEN People.SecondName  IS NULL THEN '' " +
                "else People.SecondName+' ' END +CASE  WHEN People.ThirdName  IS NULL THEN ''  else " +
                "People.ThirdName+' 'END  +CASE  WHEN People.LastName  IS NULL THEN '' else People.LastName " +
                " END ,ApplicationDate, 'Passed Test'=  (select  count(TestAppointments.TestTypeID ) " +
                "from Tests join TestAppointments  on Tests.TestAppointmentID = TestAppointments.TestAppointmentID where             TestAppointments.LocalDrivingLicenseApplicationID=LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID \r\n            and Tests.TestResult=1) , 'Status'= case  when Applications.ApplicationStatus = 1 then 'New' \r\n            when ApplicationStatus  = 2 then 'Cancelled' else 'Completed' end \r\n             from Applications inner join People on  Applications.ApplicantPersonID=People.PersonID \r\n            inner join LocalDrivingLicenseApplications on LocalDrivingLicenseApplications.ApplicationID= Applications.ApplicationID;";            
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


        public static bool ChangeStatus(int AppID , int NewAppStatus)
        {
            bool IsChanged = false;
            SqlConnection connection = new SqlConnection(DVLD_Connection_Info);
            string q = "UPDATE [dbo].[Applications] SET [ApplicationStatus] = @AppStatus WHERE Applications.ApplicationID=@AppID;";
            connection.Open();
            SqlCommand command = new SqlCommand(q, connection);
            command.Parameters.AddWithValue("@AppID", AppID);
            command.Parameters.AddWithValue("@AppStatus", NewAppStatus);
            try
            {

                int result = command.ExecuteNonQuery();
                if (result >= 1)
                {
                    IsChanged = true;
                }

            }

            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();

            }
            return IsChanged;
        }

        public static bool FindApplicationByLDLID( int LDLID, ref int AppID , ref int personID, ref DateTime applicationDate, 
            ref int applicationType,ref int applicationStatus , ref float applicationFees, 
            ref  int userId, ref DateTime lastStatusDate)
        {
            bool IsExist = false;
            SqlConnection connection = new SqlConnection(DVLD_Connection_Info);
            string q = "select Applications.ApplicationID ,Applications.ApplicantPersonID , Applications.ApplicationDate, " +
                "Applications.ApplicationTypeID , Applications.ApplicationStatus\r\n, Applications.PaidFees , " +
                "Applications.CreatedByUserID, " +
                "Applications.LastStatusDate from Applications join LocalDrivingLicenseApplications on Applications.ApplicationID="
            + "LocalDrivingLicenseApplications.ApplicationID where LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID" +
            "=@LDLID;";
            connection.Open();
            SqlCommand cmd = new SqlCommand(q, connection);
           cmd.Parameters.AddWithValue("@LDLID", LDLID);

            try
            {
                SqlDataReader r = cmd.ExecuteReader();
                if (r.Read())
                {
                    AppID = Convert.ToInt32(r[0]);
                    personID = Convert.ToInt32(r[1]);
                    applicationDate = Convert.ToDateTime(r[2]);
                    applicationType= Convert.ToInt32(r[3]);
                    applicationStatus= Convert.ToInt32(r[4]);
                    applicationFees= Convert.ToSingle(r[5]);
                    userId = Convert.ToInt32(r[6]);
                    lastStatusDate = Convert.ToDateTime(r[7]);

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


        public static bool DeleteApplicationByLDLID(int LDLAppID)
        {
            bool IsDeleted = false;
            int AppID = clsSqlLocalDrivingLicenseApp.GetAppIDByLDLID(LDLAppID);
            SqlConnection connection = new SqlConnection(DVLD_Connection_Info);
            string q = "delete from LocalDrivingLicenseApplications  where LocalDrivingLicenseApplications.ApplicationID=@AppID;" +
                "delete from Applications  where Applications.ApplicationID=@AppID;";
            connection.Open();
            SqlCommand command = new SqlCommand(q, connection);
            command.Parameters.AddWithValue("@AppID", AppID);
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


        public static bool FindApplicationByAppID( int AppID, ref int personID, ref DateTime applicationDate,
           ref int applicationType, ref int applicationStatus, ref float applicationFees,
           ref int userId, ref DateTime lastStatusDate)
        {
            bool IsExist = false;
            SqlConnection connection = new SqlConnection(DVLD_Connection_Info);
            string q = "select * from Applications where Applications.ApplicationID=@AppID";
            connection.Open();
            SqlCommand cmd = new SqlCommand(q, connection);
            cmd.Parameters.AddWithValue("@AppID", AppID);

            try
            {
                SqlDataReader r = cmd.ExecuteReader();
                if (r.Read())
                {
                    AppID = Convert.ToInt32(r[0]);
                    personID = Convert.ToInt32(r[1]);
                    applicationDate = Convert.ToDateTime(r[2]);
                    applicationType = Convert.ToInt32(r[3]);
                    applicationStatus = Convert.ToInt32(r[4]);
                    lastStatusDate = Convert.ToDateTime(r[5]);
                    applicationFees = Convert.ToSingle(r[6]);
                    userId = Convert.ToInt32(r[7]);

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

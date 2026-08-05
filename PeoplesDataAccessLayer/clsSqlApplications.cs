using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class clsSqlApplications
    {
        public static int AddNewApplication(int PerID, DateTime AppDate, int AppType, int AppStatus, DateTime LastDate,
            float Fees, int UserID)
        {
            int ID = -1;
            var connection = clsDatabaseFactory.CreateConnection();

            // INSERT with SCOPE_IDENTITY → provides explicit PG RETURNING version
            string q = clsDatabaseFactory.GetQuery(
                " INSERT INTO [dbo].[Applications] ([ApplicantPersonID],[ApplicationDate],[ApplicationTypeID]," +
                "[ApplicationStatus],[LastStatusDate],[PaidFees],[CreatedByUserID]) VALUES(@PerID,@AppDate,@AppType,@AppStatus" +
                ",@LastDate,@Fees,@UserID) ; SELECT SCOPE_IDENTITY() ;",

                " INSERT INTO applications (applicantpersonid,applicationdate,applicationtypeid," +
                "applicationstatus,laststatusdate,paidfees,createdbyuserid) VALUES(@PerID,@AppDate,@AppType,@AppStatus" +
                ",@LastDate,@Fees,@UserID) RETURNING applicationid ;");

            connection.Open();
            var command = clsDatabaseFactory.CreateCommand(q, connection);
            clsDatabaseFactory.AddParam(command, "@PerID", PerID);
            clsDatabaseFactory.AddParam(command, "@AppDate", AppDate);
            clsDatabaseFactory.AddParam(command, "@AppType", AppType);
            clsDatabaseFactory.AddParam(command, "@AppStatus", AppStatus);
            clsDatabaseFactory.AddParam(command, "@LastDate", LastDate);
            clsDatabaseFactory.AddParam(command, "@Fees", Fees);
            clsDatabaseFactory.AddParam(command, "@UserID", UserID);

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

        public static bool UpdateApplication(int AppID, int PerID, DateTime AppDate, int AppType, int AppStatus, DateTime LastDate,
            float Fees, int UserID)
        {
            int r = 0;
            var connection = clsDatabaseFactory.CreateConnection();

            // Simple UPDATE – auto-convert handles brackets
            string q = clsDatabaseFactory.GetQuery(
                "UPDATE [dbo].[Applications] SET [ApplicantPersonID] = @PerID, [ApplicationDate] = @AppDate, " +
                "[ApplicationTypeID] = @AppType, [ApplicationStatus] = @AppStatus, " +
                "[LastStatusDate] = @LastDate, [PaidFees] = @Fees, [CreatedByUserID] = @UserID " +
                "WHERE ApplicationID = @AppID");

            connection.Open();
            var command = clsDatabaseFactory.CreateCommand(q, connection);
            clsDatabaseFactory.AddParam(command, "@AppID", AppID);
            clsDatabaseFactory.AddParam(command, "@PerID", PerID);
            clsDatabaseFactory.AddParam(command, "@AppDate", AppDate);
            clsDatabaseFactory.AddParam(command, "@AppType", AppType);
            clsDatabaseFactory.AddParam(command, "@AppStatus", AppStatus);
            clsDatabaseFactory.AddParam(command, "@LastDate", LastDate);
            clsDatabaseFactory.AddParam(command, "@Fees", Fees);
            clsDatabaseFactory.AddParam(command, "@UserID", UserID);

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

        public static bool IsLicenseClassAlreadyUsed(int PerID, int LicenseClassID)
        {
            bool IsActive = false;
            var connection = clsDatabaseFactory.CreateConnection();

            // Simple SELECT – auto-convert
            string q = clsDatabaseFactory.GetQuery(
                "select 1 from Applications inner join LocalDrivingLicenseApplications on  " +
                "LocalDrivingLicenseApplications.ApplicationID=Applications.ApplicationID where ApplicantPersonID=@PerID and " +
                "LocalDrivingLicenseApplications.LicenseClassID= @LicenseClassID and ApplicationStatus != 2;");

            connection.Open();
            var command = clsDatabaseFactory.CreateCommand(q, connection);
            clsDatabaseFactory.AddParam(command, "@PerID", PerID);
            clsDatabaseFactory.AddParam(command, "@LicenseClassID", LicenseClassID);
            try
            {
                IDataReader result = command.ExecuteReader();
                if (result.Read())
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
            var connection = clsDatabaseFactory.CreateConnection();

            // SQL-level '+' concatenation → needs explicit PG version with ||
            string q = clsDatabaseFactory.GetQuery(
                "select 'LDLAppID'=LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID , " +
                "'Driving Class'= ( select LicenseClasses.ClassName from LicenseClasses where" +
                " LicenseClassID= LocalDrivingLicenseApplications.LicenseClassID) , " +
                "People.NationalNo, 'FullName'= CASE  WHEN People.FirstName  IS NULL THEN '' else " +
                "People.FirstName+' '  END +CASE  WHEN People.SecondName  IS NULL THEN '' " +
                "else People.SecondName+' ' END +CASE  WHEN People.ThirdName  IS NULL THEN ''  else " +
                "People.ThirdName+' 'END  +CASE  WHEN People.LastName  IS NULL THEN '' else People.LastName " +
                " END ,ApplicationDate, 'Passed Test'=  (select  count(TestAppointments.TestTypeID ) " +
                "from Tests join TestAppointments  on Tests.TestAppointmentID = TestAppointments.TestAppointmentID where " +
                "TestAppointments.LocalDrivingLicenseApplicationID=LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID " +
                "and Tests.TestResult=1) , 'Status'= case  when Applications.ApplicationStatus = 1 then 'New' " +
                "when ApplicationStatus  = 2 then 'Cancelled' else 'Completed' end " +
                " from Applications inner join People on  Applications.ApplicantPersonID=People.PersonID " +
                "inner join LocalDrivingLicenseApplications on LocalDrivingLicenseApplications.ApplicationID= Applications.ApplicationID;",

                "select localdrivinglicenseapplications.localdrivinglicenseapplicationid AS \"LDLAppID\" , " +
                "( select licenseclasses.classname from licenseclasses where" +
                " licenseclassid= localdrivinglicenseapplications.licenseclassid) AS \"Driving Class\" , " +
                "people.nationalno, CASE  WHEN people.firstname  IS NULL THEN '' else " +
                "people.firstname||' '  END ||CASE  WHEN people.secondname  IS NULL THEN '' " +
                "else people.secondname||' ' END ||CASE  WHEN people.thirdname  IS NULL THEN ''  else " +
                "people.thirdname||' 'END  ||CASE  WHEN people.lastname  IS NULL THEN '' else people.lastname " +
                " END AS \"FullName\",applicationdate,  (select  count(testappointments.testtypeid ) " +
                "from tests join testappointments  on tests.testappointmentid = testappointments.testappointmentid where " +
                "testappointments.localdrivinglicenseapplicationid=localdrivinglicenseapplications.localdrivinglicenseapplicationid " +
                "and tests.testresult=1) AS \"Passed Test\" , case  when applications.applicationstatus = 1 then 'New' " +
                "when applicationstatus  = 2 then 'Cancelled' else 'Completed' end AS \"Status\" " +
                " from applications inner join people on  applications.applicantpersonid=people.personid " +
                "inner join localdrivinglicenseapplications on localdrivinglicenseapplications.applicationid= applications.applicationid;");

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


        public static bool ChangeStatus(int AppID, int NewAppStatus)
        {
            bool IsChanged = false;
            var connection = clsDatabaseFactory.CreateConnection();

            // Simple UPDATE – auto-convert
            string q = clsDatabaseFactory.GetQuery(
                "UPDATE [dbo].[Applications] SET [ApplicationStatus] = @AppStatus WHERE Applications.ApplicationID=@AppID;");

            connection.Open();
            var command = clsDatabaseFactory.CreateCommand(q, connection);
            clsDatabaseFactory.AddParam(command, "@AppID", AppID);
            clsDatabaseFactory.AddParam(command, "@AppStatus", NewAppStatus);
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

        public static bool FindApplicationByLDLID(int LDLID, ref int AppID, ref int personID, ref DateTime applicationDate,
            ref int applicationType, ref int applicationStatus, ref float applicationFees,
            ref int userId, ref DateTime lastStatusDate)
        {
            bool IsExist = false;
            var connection = clsDatabaseFactory.CreateConnection();

            // C# string concatenation only, no SQL '+' → auto-convert
            string q = clsDatabaseFactory.GetQuery(
                "select Applications.ApplicationID ,Applications.ApplicantPersonID , Applications.ApplicationDate, " +
                "Applications.ApplicationTypeID , Applications.ApplicationStatus\r\n, Applications.PaidFees , " +
                "Applications.CreatedByUserID, " +
                "Applications.LastStatusDate from Applications join LocalDrivingLicenseApplications on Applications.ApplicationID="
            + "LocalDrivingLicenseApplications.ApplicationID where LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID" +
            "=@LDLID;");

            connection.Open();
            var cmd = clsDatabaseFactory.CreateCommand(q, connection);
            clsDatabaseFactory.AddParam(cmd, "@LDLID", LDLID);

            try
            {
                IDataReader r = cmd.ExecuteReader();
                if (r.Read())
                {
                    AppID = Convert.ToInt32(r[0]);
                    personID = Convert.ToInt32(r[1]);
                    applicationDate = Convert.ToDateTime(r[2]);
                    applicationType = Convert.ToInt32(r[3]);
                    applicationStatus = Convert.ToInt32(r[4]);
                    applicationFees = Convert.ToSingle(r[5]);
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
            var connection = clsDatabaseFactory.CreateConnection();

            // Simple DELETE – auto-convert
            string q = clsDatabaseFactory.GetQuery(
                "delete from LocalDrivingLicenseApplications  where LocalDrivingLicenseApplications.ApplicationID=@AppID;" +
                "delete from Applications  where Applications.ApplicationID=@AppID;");

            connection.Open();
            var command = clsDatabaseFactory.CreateCommand(q, connection);
            clsDatabaseFactory.AddParam(command, "@AppID", AppID);
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


        public static bool FindApplicationByAppID(int AppID, ref int personID, ref DateTime applicationDate,
           ref int applicationType, ref int applicationStatus, ref float applicationFees,
           ref int userId, ref DateTime lastStatusDate)
        {
            bool IsExist = false;
            var connection = clsDatabaseFactory.CreateConnection();

            // Simple SELECT – auto-convert
            string q = clsDatabaseFactory.GetQuery("select * from Applications where Applications.ApplicationID=@AppID");

            connection.Open();
            var cmd = clsDatabaseFactory.CreateCommand(q, connection);
            clsDatabaseFactory.AddParam(cmd, "@AppID", AppID);

            try
            {
                IDataReader r = cmd.ExecuteReader();
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
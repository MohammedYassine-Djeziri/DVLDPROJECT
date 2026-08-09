-- ===========================================================================
-- DVLD schema – PostgreSQL (converted from SQL Server)
-- Idempotent: CREATE TABLE IF NOT EXISTS / CREATE OR REPLACE VIEW
-- ===========================================================================

-- ApplicationTypes
CREATE TABLE IF NOT EXISTS ApplicationTypes (
    ApplicationTypeID int GENERATED ALWAYS AS IDENTITY,
    ApplicationTypeTitle varchar(150) NOT NULL,
    ApplicationFees numeric(10,4) DEFAULT 0 NOT NULL,
    CONSTRAINT PK_ApplicationTypes PRIMARY KEY (ApplicationTypeID)
);

-- Countries
CREATE TABLE IF NOT EXISTS Countries (
    CountryID int GENERATED ALWAYS AS IDENTITY,
    CountryName varchar(50) NOT NULL,
    CONSTRAINT PK_Countries PRIMARY KEY (CountryID)
);

-- LicenseClasses
CREATE TABLE IF NOT EXISTS LicenseClasses (
    LicenseClassID int GENERATED ALWAYS AS IDENTITY,
    ClassName varchar(50) NOT NULL,
    ClassDescription varchar(500) NOT NULL,
    MinimumAllowedAge smallint DEFAULT 18 NOT NULL,
    DefaultValidityLength smallint DEFAULT 1 NOT NULL,
    ClassFees numeric(10,4) DEFAULT 0 NOT NULL,
    CONSTRAINT PK_LicenseClasses PRIMARY KEY (LicenseClassID)
);

-- TestTypes
CREATE TABLE IF NOT EXISTS TestTypes (
    TestTypeID int GENERATED ALWAYS AS IDENTITY,
    TestTypeTitle varchar(100) NOT NULL,
    TestTypeDescription varchar(500) NOT NULL,
    TestTypeFees numeric(10,4) NOT NULL,
    CONSTRAINT PK_TestTypes PRIMARY KEY (TestTypeID)
);

-- sysdiagrams
CREATE TABLE IF NOT EXISTS sysdiagrams (
    name varchar(128) NOT NULL,
    principal_id int NOT NULL,
    diagram_id int GENERATED ALWAYS AS IDENTITY,
    version int NULL,
    definition bytea NULL,
    CONSTRAINT PK_sysdiagrams PRIMARY KEY (diagram_id),
    CONSTRAINT UK_principal_name UNIQUE (principal_id, name)
);

-- People
CREATE TABLE IF NOT EXISTS People (
    PersonID int GENERATED ALWAYS AS IDENTITY,
    NationalNo varchar(20) NOT NULL,
    FirstName varchar(20) NOT NULL,
    SecondName varchar(20) NOT NULL,
    ThirdName varchar(20) NULL,
    LastName varchar(20) NOT NULL,
    DateOfBirth timestamp NOT NULL,
    Gendor smallint DEFAULT 0 NOT NULL,
    Address varchar(500) NOT NULL,
    Phone varchar(20) NOT NULL,
    Email varchar(50) NULL,
    NationalityCountryID int NOT NULL,
    ImagePath varchar(250) NULL,
    CONSTRAINT PK_People PRIMARY KEY (PersonID),
    CONSTRAINT FK_People_Countries1 FOREIGN KEY (NationalityCountryID) REFERENCES Countries(CountryID)
);

-- Users
CREATE TABLE IF NOT EXISTS Users (
    UserID int GENERATED ALWAYS AS IDENTITY,
    PersonID int NOT NULL,
    UserName varchar(20) NOT NULL,
    Password varchar(20) NOT NULL,
    IsActive boolean NOT NULL,
    CONSTRAINT PK_Users PRIMARY KEY (UserID),
    CONSTRAINT FK_Users_People FOREIGN KEY (PersonID) REFERENCES People(PersonID)
);

-- Applications
CREATE TABLE IF NOT EXISTS Applications (
    ApplicationID int GENERATED ALWAYS AS IDENTITY,
    ApplicantPersonID int NOT NULL,
    ApplicationDate timestamp NOT NULL,
    ApplicationTypeID int NOT NULL,
    ApplicationStatus smallint DEFAULT 1 NOT NULL,
    LastStatusDate timestamp NOT NULL,
    PaidFees numeric(10,4) NOT NULL,
    CreatedByUserID int NOT NULL,
    CONSTRAINT PK_Applications PRIMARY KEY (ApplicationID),
    CONSTRAINT FK_Applications_ApplicationTypes FOREIGN KEY (ApplicationTypeID) REFERENCES ApplicationTypes(ApplicationTypeID),
    CONSTRAINT FK_Applications_People FOREIGN KEY (ApplicantPersonID) REFERENCES People(PersonID),
    CONSTRAINT FK_Applications_Users FOREIGN KEY (CreatedByUserID) REFERENCES Users(UserID)
);
-- Drivers
CREATE TABLE IF NOT EXISTS Drivers (
    DriverID int GENERATED ALWAYS AS IDENTITY,
    PersonID int NOT NULL,
    CreatedByUserID int NOT NULL,
    CreatedDate timestamp NOT NULL,
    CONSTRAINT PK_Drivers_1 PRIMARY KEY (DriverID),
    CONSTRAINT FK_Drivers_People FOREIGN KEY (PersonID) REFERENCES People(PersonID),
    CONSTRAINT FK_Drivers_Users FOREIGN KEY (CreatedByUserID) REFERENCES Users(UserID)
);

-- Licenses
CREATE TABLE IF NOT EXISTS Licenses (
    LicenseID int GENERATED ALWAYS AS IDENTITY,
    ApplicationID int NOT NULL,
    DriverID int NOT NULL,
    LicenseClass int NOT NULL,
    IssueDate timestamp NOT NULL,
    ExpirationDate timestamp NOT NULL,
    Notes varchar(500) NULL,
    PaidFees numeric(10,4) NOT NULL,
    IsActive boolean DEFAULT true NOT NULL,
    IssueReason smallint DEFAULT 1 NOT NULL,
    CreatedByUserID int NOT NULL,
    CONSTRAINT PK_Licenses PRIMARY KEY (LicenseID),
    CONSTRAINT FK_Licenses_Applications FOREIGN KEY (ApplicationID) REFERENCES Applications(ApplicationID),
    CONSTRAINT FK_Licenses_Drivers FOREIGN KEY (DriverID) REFERENCES Drivers(DriverID),
    CONSTRAINT FK_Licenses_LicenseClasses FOREIGN KEY (LicenseClass) REFERENCES LicenseClasses(LicenseClassID),
    CONSTRAINT FK_Licenses_Users FOREIGN KEY (CreatedByUserID) REFERENCES Users(UserID)
);

-- LocalDrivingLicenseApplications
CREATE TABLE IF NOT EXISTS LocalDrivingLicenseApplications (
    LocalDrivingLicenseApplicationID int GENERATED ALWAYS AS IDENTITY,
    ApplicationID int NOT NULL,
    LicenseClassID int NOT NULL,
    CONSTRAINT PK_DrivingLicsenseApplications PRIMARY KEY (LocalDrivingLicenseApplicationID),
    CONSTRAINT FK_DrivingLicsenseApplications_Applications FOREIGN KEY (ApplicationID) REFERENCES Applications(ApplicationID),
    CONSTRAINT FK_DrivingLicsenseApplications_LicenseClasses FOREIGN KEY (LicenseClassID) REFERENCES LicenseClasses(LicenseClassID)
);

-- TestAppointments
CREATE TABLE IF NOT EXISTS TestAppointments (
    TestAppointmentID int GENERATED ALWAYS AS IDENTITY,
    TestTypeID int NOT NULL,
    LocalDrivingLicenseApplicationID int NOT NULL,
    AppointmentDate timestamp NOT NULL,
    PaidFees numeric(10,4) NOT NULL,
    CreatedByUserID int NOT NULL,
    IsLocked boolean DEFAULT false NOT NULL,
    CONSTRAINT PK_TestAppointments PRIMARY KEY (TestAppointmentID),
    CONSTRAINT FK_TestAppointments_LocalDrivingLicenseApplications FOREIGN KEY (LocalDrivingLicenseApplicationID) REFERENCES LocalDrivingLicenseApplications(LocalDrivingLicenseApplicationID),
    CONSTRAINT FK_TestAppointments_TestTypes FOREIGN KEY (TestTypeID) REFERENCES TestTypes(TestTypeID),
    CONSTRAINT FK_TestAppointments_Users FOREIGN KEY (CreatedByUserID) REFERENCES Users(UserID)
);

-- Tests
CREATE TABLE IF NOT EXISTS Tests (
    TestID int GENERATED ALWAYS AS IDENTITY,
    TestAppointmentID int NOT NULL,
    TestResult boolean NOT NULL,
    Notes varchar(500) NULL,
    CreatedByUserID int NOT NULL,
    CONSTRAINT PK_Tests PRIMARY KEY (TestID),
    CONSTRAINT FK_Tests_TestAppointments FOREIGN KEY (TestAppointmentID) REFERENCES TestAppointments(TestAppointmentID),
    CONSTRAINT FK_Tests_Users FOREIGN KEY (CreatedByUserID) REFERENCES Users(UserID)
);

-- DetainedLicenses
CREATE TABLE IF NOT EXISTS DetainedLicenses (
    DetainID int GENERATED ALWAYS AS IDENTITY,
    LicenseID int NOT NULL,
    DetainDate timestamp NOT NULL,
    FineFees numeric(10,4) NOT NULL,
    CreatedByUserID int NOT NULL,
    IsReleased boolean DEFAULT false NOT NULL,
    ReleaseDate timestamp NULL,
    ReleasedByUserID int NULL,
    ReleaseApplicationID int NULL,
    CONSTRAINT PK_DetainedLicenses PRIMARY KEY (DetainID),
    CONSTRAINT FK_DetainedLicenses_Applications FOREIGN KEY (ReleaseApplicationID) REFERENCES Applications(ApplicationID),
    CONSTRAINT FK_DetainedLicenses_Licenses FOREIGN KEY (LicenseID) REFERENCES Licenses(LicenseID),
    CONSTRAINT FK_DetainedLicenses_Users FOREIGN KEY (CreatedByUserID) REFERENCES Users(UserID),
    CONSTRAINT FK_DetainedLicenses_Users1 FOREIGN KEY (ReleasedByUserID) REFERENCES Users(UserID)
);

-- InternationalLicenses
CREATE TABLE IF NOT EXISTS InternationalLicenses (
    InternationalLicenseID int GENERATED ALWAYS AS IDENTITY,
    ApplicationID int NOT NULL,
    DriverID int NOT NULL,
    IssuedUsingLocalLicenseID int NOT NULL,
    IssueDate timestamp NOT NULL,
    ExpirationDate timestamp NOT NULL,
    IsActive boolean NOT NULL,
    CreatedByUserID int NOT NULL,
    CONSTRAINT PK_InternationalLicenses PRIMARY KEY (InternationalLicenseID),
    CONSTRAINT FK_InternationalLicenses_Applications FOREIGN KEY (ApplicationID) REFERENCES Applications(ApplicationID),
    CONSTRAINT FK_InternationalLicenses_Drivers FOREIGN KEY (DriverID) REFERENCES Drivers(DriverID),
    CONSTRAINT FK_InternationalLicenses_Licenses FOREIGN KEY (IssuedUsingLocalLicenseID) REFERENCES Licenses(LicenseID),
CONSTRAINT FK_InternationalLicenses_Users FOREIGN KEY (CreatedByUserID) REFERENCES Users(UserID)
);

-- =====================================================================
-- Views
-- =====================================================================

-- Drivers_Views
CREATE OR REPLACE VIEW Drivers_Views AS
SELECT
    d.DriverID,
    d.PersonID,
    p.NationalNo,
    p.FirstName || ' ' || p.SecondName || ' ' || COALESCE(p.ThirdName, '') || ' ' || p.LastName AS FullName,
    d.CreatedDate,
    (SELECT COUNT(l.LicenseID)
     FROM Licenses l
     WHERE l.IsActive = true AND l.DriverID = d.DriverID) AS NumberOfActiveLicenses
FROM Drivers d
INNER JOIN People p ON d.PersonID = p.PersonID;

-- ListLocalDrivingLicenseApplications_View
CREATE OR REPLACE VIEW ListLocalDrivingLicenseApplications_View AS
SELECT
    lda.LocalDrivingLicenseApplicationID,
    lc.ClassName,
    p.NationalNo,
    p.FirstName || ' ' || p.SecondName || ' ' || COALESCE(p.ThirdName, '') || ' ' || p.LastName AS FullName,
    a.ApplicationDate,
    (SELECT COUNT(ta.TestTypeID)
     FROM Tests t
     INNER JOIN TestAppointments ta ON t.TestAppointmentID = ta.TestAppointmentID
     WHERE ta.LocalDrivingLicenseApplicationID = lda.LocalDrivingLicenseApplicationID
       AND t.TestResult = true) AS PassedTestCount,
    CASE
        WHEN a.ApplicationStatus = 1 THEN 'New'
        WHEN a.ApplicationStatus = 2 THEN 'Cancelled'
        WHEN a.ApplicationStatus = 3 THEN 'Completed'
    END AS Status
FROM LocalDrivingLicenseApplications lda
INNER JOIN Applications a ON lda.ApplicationID = a.ApplicationID
INNER JOIN LicenseClasses lc ON lda.LicenseClassID = lc.LicenseClassID
INNER JOIN People p ON a.ApplicantPersonID = p.PersonID;

-- GetAllTestAppointments_View
CREATE OR REPLACE VIEW GetAllTestAppointments_View AS
SELECT
    ta.TestAppointmentID,
    ta.LocalDrivingLicenseApplicationID,
    tt.TestTypeTitle,
    lc.ClassName,
    ta.AppointmentDate,
    ta.PaidFees,
    p.FirstName || ' ' || p.SecondName || ' ' || COALESCE(p.ThirdName, '') || ' ' || p.LastName AS FullName,
    ta.IsLocked
FROM TestAppointments ta
INNER JOIN TestTypes tt ON ta.TestTypeID = tt.TestTypeID
INNER JOIN LocalDrivingLicenseApplications lda ON ta.LocalDrivingLicenseApplicationID = lda.LocalDrivingLicenseApplicationID
INNER JOIN Applications a ON lda.ApplicationID = a.ApplicationID
INNER JOIN People p ON a.ApplicantPersonID = p.PersonID
INNER JOIN LicenseClasses lc ON lda.LicenseClassID = lc.LicenseClassID;-- countries
INSERT INTO Countries (CountryName)
SELECT 'Afghanistan' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Afghanistan');
INSERT INTO Countries (CountryName)
SELECT 'Albania' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Albania');
INSERT INTO Countries (CountryName)
SELECT 'Algeria' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Algeria');
INSERT INTO Countries (CountryName)
SELECT 'Andorra' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Andorra');
INSERT INTO Countries (CountryName)
SELECT 'Angola' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Angola');
INSERT INTO Countries (CountryName)
SELECT 'Antigua and Barbuda' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Antigua and Barbuda');
INSERT INTO Countries (CountryName)
SELECT 'Argentina' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Argentina');
INSERT INTO Countries (CountryName)
SELECT 'Armenia' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Armenia');
INSERT INTO Countries (CountryName)
SELECT 'Austria' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Austria');
INSERT INTO Countries (CountryName)
SELECT 'Azerbaijan' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Azerbaijan');
INSERT INTO Countries (CountryName)
SELECT 'Bahrain' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Bahrain');
INSERT INTO Countries (CountryName)
SELECT 'Bangladesh' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Bangladesh');
INSERT INTO Countries (CountryName)
SELECT 'Barbados' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Barbados');
INSERT INTO Countries (CountryName)
SELECT 'Belarus' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Belarus');
INSERT INTO Countries (CountryName)
SELECT 'Belgium' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Belgium');
INSERT INTO Countries (CountryName)
SELECT 'Belize' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Belize');
INSERT INTO Countries (CountryName)
SELECT 'Benin' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Benin');
INSERT INTO Countries (CountryName)
SELECT 'Bhutan' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Bhutan');
INSERT INTO Countries (CountryName)
SELECT 'Bolivia' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Bolivia');
INSERT INTO Countries (CountryName)
SELECT 'Bosnia and Herzegovina' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Bosnia and Herzegovina');
INSERT INTO Countries (CountryName)
SELECT 'Botswana' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Botswana');
INSERT INTO Countries (CountryName)
SELECT 'Brazil' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Brazil');
INSERT INTO Countries (CountryName)
SELECT 'Brunei' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Brunei');
INSERT INTO Countries (CountryName)
SELECT 'Bulgaria' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Bulgaria');
INSERT INTO Countries (CountryName)
SELECT 'Burkina Faso' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Burkina Faso');
INSERT INTO Countries (CountryName)
SELECT 'Burundi' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Burundi');
INSERT INTO Countries (CountryName)
SELECT 'Cabo Verde' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Cabo Verde');
INSERT INTO Countries (CountryName)
SELECT 'Cambodia' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Cambodia');
INSERT INTO Countries (CountryName)
SELECT 'Cameroon' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Cameroon');
INSERT INTO Countries (CountryName)
SELECT 'Canada' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Canada');
INSERT INTO Countries (CountryName)
SELECT 'Central African Republic' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Central African Republic');
INSERT INTO Countries (CountryName)
SELECT 'Chad' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Chad');
INSERT INTO Countries (CountryName)
SELECT 'Channel Islands' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Channel Islands');
INSERT INTO Countries (CountryName)
SELECT 'Chile' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Chile');
INSERT INTO Countries (CountryName)
SELECT 'China' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'China');
INSERT INTO Countries (CountryName)
SELECT 'Colombia' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Colombia');
INSERT INTO Countries (CountryName)
SELECT 'Comoros' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Comoros');
INSERT INTO Countries (CountryName)
SELECT 'Congo' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Congo');
INSERT INTO Countries (CountryName)
SELECT 'Costa Rica' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Costa Rica');
INSERT INTO Countries (CountryName)
SELECT 'Cote d''Ivoire' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Cote d''Ivoire');
INSERT INTO Countries (CountryName)
SELECT 'Croatia' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Croatia');
INSERT INTO Countries (CountryName)
SELECT 'Cuba' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Cuba');
INSERT INTO Countries (CountryName)
SELECT 'Cyprus' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Cyprus');
INSERT INTO Countries (CountryName)
SELECT 'Czech Republic' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Czech Republic');
INSERT INTO Countries (CountryName)
SELECT 'Denmark' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Denmark');
INSERT INTO Countries (CountryName)
SELECT 'Djibouti' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Djibouti');
INSERT INTO Countries (CountryName)
SELECT 'Dominica' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Dominica');
INSERT INTO Countries (CountryName)
SELECT 'Dominican Republic' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Dominican Republic');
INSERT INTO Countries (CountryName)
SELECT 'DR Congo' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'DR Congo');
INSERT INTO Countries (CountryName)
SELECT 'Ecuador' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Ecuador');
INSERT INTO Countries (CountryName)
SELECT 'Egypt' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Egypt');
INSERT INTO Countries (CountryName)
SELECT 'El Salvador' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'El Salvador');
INSERT INTO Countries (CountryName)
SELECT 'Equatorial Guinea' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Equatorial Guinea');
INSERT INTO Countries (CountryName)
SELECT 'Eritrea' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Eritrea');
INSERT INTO Countries (CountryName)
SELECT 'Estonia' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Estonia');
INSERT INTO Countries (CountryName)
SELECT 'Eswatini' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Eswatini');
INSERT INTO Countries (CountryName)
SELECT 'Ethiopia' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Ethiopia');
INSERT INTO Countries (CountryName)
SELECT 'Faeroe Islands' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Faeroe Islands');
INSERT INTO Countries (CountryName)
SELECT 'Finland' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Finland');
INSERT INTO Countries (CountryName)
SELECT 'France' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'France');
INSERT INTO Countries (CountryName)
SELECT 'French Guiana' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'French Guiana');
INSERT INTO Countries (CountryName)
SELECT 'Gabon' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Gabon');
INSERT INTO Countries (CountryName)
SELECT 'Gambia' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Gambia');
INSERT INTO Countries (CountryName)
SELECT 'Georgia' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Georgia');
INSERT INTO Countries (CountryName)
SELECT 'Germany' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Germany');
INSERT INTO Countries (CountryName)
SELECT 'Ghana' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Ghana');
INSERT INTO Countries (CountryName)
SELECT 'Gibraltar' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Gibraltar');
INSERT INTO Countries (CountryName)
SELECT 'Greece' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Greece');
INSERT INTO Countries (CountryName)
SELECT 'Grenada' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Grenada');
INSERT INTO Countries (CountryName)
SELECT 'Guatemala' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Guatemala');
INSERT INTO Countries (CountryName)
SELECT 'Guinea' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Guinea');
INSERT INTO Countries (CountryName)
SELECT 'Guinea-Bissau' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Guinea-Bissau');
INSERT INTO Countries (CountryName)
SELECT 'Guyana' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Guyana');
INSERT INTO Countries (CountryName)
SELECT 'Haiti' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Haiti');
INSERT INTO Countries (CountryName)
SELECT 'Holy See' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Holy See');
INSERT INTO Countries (CountryName)
SELECT 'Honduras' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Honduras');
INSERT INTO Countries (CountryName)
SELECT 'Hong Kong' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Hong Kong');
INSERT INTO Countries (CountryName)
SELECT 'Hungary' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Hungary');
INSERT INTO Countries (CountryName)
SELECT 'Iceland' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Iceland');
INSERT INTO Countries (CountryName)
SELECT 'India' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'India');
INSERT INTO Countries (CountryName)
SELECT 'Indonesia' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Indonesia');
INSERT INTO Countries (CountryName)
SELECT 'Iran' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Iran');
INSERT INTO Countries (CountryName)
SELECT 'Iraq' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Iraq');
INSERT INTO Countries (CountryName)
SELECT 'Ireland' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Ireland');
INSERT INTO Countries (CountryName)
SELECT 'Isle of Man' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Isle of Man');
INSERT INTO Countries (CountryName)
SELECT 'Israel' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Israel');
INSERT INTO Countries (CountryName)
SELECT 'Italy' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Italy');
INSERT INTO Countries (CountryName)
SELECT 'Jamaica' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Jamaica');
INSERT INTO Countries (CountryName)
SELECT 'Japan' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Japan');
INSERT INTO Countries (CountryName)
SELECT 'Jordan' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Jordan');
INSERT INTO Countries (CountryName)
SELECT 'Kazakhstan' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Kazakhstan');
INSERT INTO Countries (CountryName)
SELECT 'Kenya' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Kenya');
INSERT INTO Countries (CountryName)
SELECT 'Kuwait' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Kuwait');
INSERT INTO Countries (CountryName)
SELECT 'Kyrgyzstan' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Kyrgyzstan');
INSERT INTO Countries (CountryName)
SELECT 'Laos' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Laos');
INSERT INTO Countries (CountryName)
SELECT 'Latvia' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Latvia');
INSERT INTO Countries (CountryName)
SELECT 'Lebanon' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Lebanon');
INSERT INTO Countries (CountryName)
SELECT 'Lesotho' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Lesotho');
INSERT INTO Countries (CountryName)
SELECT 'Liberia' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Liberia');
INSERT INTO Countries (CountryName)
SELECT 'Libya' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Libya');
INSERT INTO Countries (CountryName)
SELECT 'Liechtenstein' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Liechtenstein');
INSERT INTO Countries (CountryName)
SELECT 'Lithuania' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Lithuania');
INSERT INTO Countries (CountryName)
SELECT 'Luxembourg' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Luxembourg');
INSERT INTO Countries (CountryName)
SELECT 'Macao' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Macao');
INSERT INTO Countries (CountryName)
SELECT 'Madagascar' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Madagascar');
INSERT INTO Countries (CountryName)
SELECT 'Malawi' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Malawi');
INSERT INTO Countries (CountryName)
SELECT 'Malaysia' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Malaysia');
INSERT INTO Countries (CountryName)
SELECT 'Maldives' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Maldives');
INSERT INTO Countries (CountryName)
SELECT 'Mali' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Mali');
INSERT INTO Countries (CountryName)
SELECT 'Malta' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Malta');
INSERT INTO Countries (CountryName)
SELECT 'Mauritania' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Mauritania');
INSERT INTO Countries (CountryName)
SELECT 'Mauritius' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Mauritius');
INSERT INTO Countries (CountryName)
SELECT 'Mayotte' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Mayotte');
INSERT INTO Countries (CountryName)
SELECT 'Mexico' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Mexico');
INSERT INTO Countries (CountryName)
SELECT 'Moldova' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Moldova');
INSERT INTO Countries (CountryName)
SELECT 'Monaco' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Monaco');
INSERT INTO Countries (CountryName)
SELECT 'Mongolia' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Mongolia');
INSERT INTO Countries (CountryName)
SELECT 'Montenegro' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Montenegro');
INSERT INTO Countries (CountryName)
SELECT 'Morocco' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Morocco');
INSERT INTO Countries (CountryName)
SELECT 'Mozambique' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Mozambique');
INSERT INTO Countries (CountryName)
SELECT 'Myanmar' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Myanmar');
INSERT INTO Countries (CountryName)
SELECT 'Namibia' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Namibia');
INSERT INTO Countries (CountryName)
SELECT 'Nepal' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Nepal');
INSERT INTO Countries (CountryName)
SELECT 'Netherlands' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Netherlands');
INSERT INTO Countries (CountryName)
SELECT 'Nicaragua' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Nicaragua');
INSERT INTO Countries (CountryName)
SELECT 'Niger' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Niger');
INSERT INTO Countries (CountryName)
SELECT 'Nigeria' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Nigeria');
INSERT INTO Countries (CountryName)
SELECT 'North Korea' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'North Korea');
INSERT INTO Countries (CountryName)
SELECT 'North Macedonia' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'North Macedonia');
INSERT INTO Countries (CountryName)
SELECT 'Norway' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Norway');
INSERT INTO Countries (CountryName)
SELECT 'Oman' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Oman');
INSERT INTO Countries (CountryName)
SELECT 'Pakistan' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Pakistan');
INSERT INTO Countries (CountryName)
SELECT 'Panama' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Panama');
INSERT INTO Countries (CountryName)
SELECT 'Paraguay' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Paraguay');
INSERT INTO Countries (CountryName)
SELECT 'Peru' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Peru');
INSERT INTO Countries (CountryName)
SELECT 'Philippines' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Philippines');
INSERT INTO Countries (CountryName)
SELECT 'Poland' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Poland');
INSERT INTO Countries (CountryName)
SELECT 'Portugal' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Portugal');
INSERT INTO Countries (CountryName)
SELECT 'Qatar' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Qatar');
INSERT INTO Countries (CountryName)
SELECT 'Reunion' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Reunion');
INSERT INTO Countries (CountryName)
SELECT 'Romania' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Romania');
INSERT INTO Countries (CountryName)
SELECT 'Russia' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Russia');
INSERT INTO Countries (CountryName)
SELECT 'Rwanda' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Rwanda');
INSERT INTO Countries (CountryName)
SELECT 'Saint Helena' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Saint Helena');
INSERT INTO Countries (CountryName)
SELECT 'Saint Kitts and Nevis' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Saint Kitts and Nevis');
INSERT INTO Countries (CountryName)
SELECT 'Saint Lucia' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Saint Lucia');
INSERT INTO Countries (CountryName)
SELECT 'Saint Vincent and the Grenadines' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Saint Vincent and the Grenadines');
INSERT INTO Countries (CountryName)
SELECT 'San Marino' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'San Marino');
INSERT INTO Countries (CountryName)
SELECT 'Sao Tome & Principe' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Sao Tome & Principe');
INSERT INTO Countries (CountryName)
SELECT 'Saudi Arabia' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Saudi Arabia');
INSERT INTO Countries (CountryName)
SELECT 'Senegal' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Senegal');
INSERT INTO Countries (CountryName)
SELECT 'Serbia' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Serbia');
INSERT INTO Countries (CountryName)
SELECT 'Seychelles' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Seychelles');
INSERT INTO Countries (CountryName)
SELECT 'Sierra Leone' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Sierra Leone');
INSERT INTO Countries (CountryName)
SELECT 'Singapore' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Singapore');
INSERT INTO Countries (CountryName)
SELECT 'Slovakia' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Slovakia');
INSERT INTO Countries (CountryName)
SELECT 'Slovenia' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Slovenia');
INSERT INTO Countries (CountryName)
SELECT 'Somalia' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Somalia');
INSERT INTO Countries (CountryName)
SELECT 'South Africa' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'South Africa');
INSERT INTO Countries (CountryName)
SELECT 'South Korea' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'South Korea');
INSERT INTO Countries (CountryName)
SELECT 'South Sudan' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'South Sudan');
INSERT INTO Countries (CountryName)
SELECT 'Spain' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Spain');
INSERT INTO Countries (CountryName)
SELECT 'Sri Lanka' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Sri Lanka');
INSERT INTO Countries (CountryName)
SELECT 'State of Palestine' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'State of Palestine');
INSERT INTO Countries (CountryName)
SELECT 'Sudan' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Sudan');
INSERT INTO Countries (CountryName)
SELECT 'Suriname' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Suriname');
INSERT INTO Countries (CountryName)
SELECT 'Sweden' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Sweden');
INSERT INTO Countries (CountryName)
SELECT 'Switzerland' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Switzerland');
INSERT INTO Countries (CountryName)
SELECT 'Syria' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Syria');
INSERT INTO Countries (CountryName)
SELECT 'Taiwan' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Taiwan');
INSERT INTO Countries (CountryName)
SELECT 'Tajikistan' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Tajikistan');
INSERT INTO Countries (CountryName)
SELECT 'Tanzania' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Tanzania');
INSERT INTO Countries (CountryName)
SELECT 'Thailand' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Thailand');
INSERT INTO Countries (CountryName)
SELECT 'The Bahamas' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'The Bahamas');
INSERT INTO Countries (CountryName)
SELECT 'Timor-Leste' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Timor-Leste');
INSERT INTO Countries (CountryName)
SELECT 'Togo' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Togo');
INSERT INTO Countries (CountryName)
SELECT 'Trinidad and Tobago' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Trinidad and Tobago');
INSERT INTO Countries (CountryName)
SELECT 'Tunisia' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Tunisia');
INSERT INTO Countries (CountryName)
SELECT 'Turkey' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Turkey');
INSERT INTO Countries (CountryName)
SELECT 'Turkmenistan' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Turkmenistan');
INSERT INTO Countries (CountryName)
SELECT 'Uganda' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Uganda');
INSERT INTO Countries (CountryName)
SELECT 'Ukraine' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Ukraine');
INSERT INTO Countries (CountryName)
SELECT 'United Arab Emirates' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'United Arab Emirates');
INSERT INTO Countries (CountryName)
SELECT 'United Kingdom' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'United Kingdom');
INSERT INTO Countries (CountryName)
SELECT 'United States' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'United States');
INSERT INTO Countries (CountryName)
SELECT 'Uruguay' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Uruguay');
INSERT INTO Countries (CountryName)
SELECT 'Uzbekistan' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Uzbekistan');
INSERT INTO Countries (CountryName)
SELECT 'Venezuela' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Venezuela');
INSERT INTO Countries (CountryName)
SELECT 'Vietnam' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Vietnam');
INSERT INTO Countries (CountryName)
SELECT 'Western Sahara' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Western Sahara');
INSERT INTO Countries (CountryName)
SELECT 'Yemen' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Yemen');
INSERT INTO Countries (CountryName)
SELECT 'Zambia' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Zambia');
INSERT INTO Countries (CountryName)
SELECT 'Zimbabwe' WHERE NOT EXISTS (SELECT 1 FROM Countries WHERE CountryName = 'Zimbabwe');

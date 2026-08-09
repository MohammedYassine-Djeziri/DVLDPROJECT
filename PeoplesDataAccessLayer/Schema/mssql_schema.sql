-- ============================================================================
-- DVLD Database Schema – SQL Server
-- ============================================================================
-- Idempotent: every CREATE TABLE is wrapped in IF NOT EXISTS, and seed rows
-- check for existence before inserting.  Re-running is safe.
-- ============================================================================

-- ----------------------------------------------------------------------------
-- 1. Countries
-- ----------------------------------------------------------------------------
IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Countries')
BEGIN
    CREATE TABLE [dbo].[Countries] (
        [CountryID]   INT IDENTITY(1,1) NOT NULL,
        [CountryName] NVARCHAR(50)     NOT NULL,
        CONSTRAINT [PK_Countries] PRIMARY KEY CLUSTERED ([CountryID])
    );
END
GO

-- ----------------------------------------------------------------------------
-- 2. People
-- ----------------------------------------------------------------------------
IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'People')
BEGIN
    CREATE TABLE [dbo].[People] (
        [PersonID]             INT IDENTITY(1,1) NOT NULL,
        [NationalNo]           NVARCHAR(20)      NOT NULL,
        [FirstName]            NVARCHAR(20)      NOT NULL,
        [SecondName]           NVARCHAR(20)      NOT NULL,
        [ThirdName]            NVARCHAR(20)      NULL,
        [LastName]             NVARCHAR(20)      NOT NULL,
        [DateOfBirth]          DATETIME          NOT NULL,
        [Gendor]               TINYINT DEFAULT 0 NOT NULL,
        [Address]              NVARCHAR(500)     NOT NULL,
        [Phone]                NVARCHAR(20)      NOT NULL,
        [Email]                NVARCHAR(50)      NULL,
        [NationalityCountryID] INT               NOT NULL,
        [ImagePath]            NVARCHAR(250)     NULL,
        CONSTRAINT [PK_People] PRIMARY KEY CLUSTERED ([PersonID]),
        CONSTRAINT [FK_People_Countries1] FOREIGN KEY ([NationalityCountryID])
            REFERENCES [dbo].[Countries] ([CountryID])
    );
END
GO

-- ----------------------------------------------------------------------------
-- 3. Users
-- ----------------------------------------------------------------------------
IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Users')
BEGIN
    CREATE TABLE [dbo].[Users] (
        [UserID]   INT IDENTITY(1,1) NOT NULL,
        [PersonID] INT               NOT NULL,
        [UserName] NVARCHAR(20)      NOT NULL,
        [Password] NVARCHAR(20)      NOT NULL,
        [IsActive] BIT               NOT NULL,
        CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([UserID]),
        CONSTRAINT [FK_Users_People] FOREIGN KEY ([PersonID])
            REFERENCES [dbo].[People] ([PersonID])
    );
END
GO

-- ----------------------------------------------------------------------------
-- 4. ApplicationTypes
-- ----------------------------------------------------------------------------
IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'ApplicationTypes')
BEGIN
    CREATE TABLE [dbo].[ApplicationTypes] (
        [ApplicationTypeID]    INT IDENTITY(1,1) NOT NULL,
        [ApplicationTypeTitle] NVARCHAR(150)     NOT NULL,
        [ApplicationFees]      SMALLMONEY DEFAULT 0 NOT NULL,
        CONSTRAINT [PK_ApplicationTypes] PRIMARY KEY CLUSTERED ([ApplicationTypeID])
    );
END
GO

-- ----------------------------------------------------------------------------
-- 5. LicenseClasses
-- ----------------------------------------------------------------------------
IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'LicenseClasses')
BEGIN
    CREATE TABLE [dbo].[LicenseClasses] (
        [LicenseClassID]        INT IDENTITY(1,1) NOT NULL,
        [ClassName]             NVARCHAR(50)      NOT NULL,
        [ClassDescription]      NVARCHAR(500)     NOT NULL,
        [MinimumAllowedAge]     TINYINT DEFAULT 18 NOT NULL,
        [DefaultValidityLength] TINYINT DEFAULT 1  NOT NULL,
        [ClassFees]             SMALLMONEY DEFAULT 0 NOT NULL,
        CONSTRAINT [PK_LicenseClasses] PRIMARY KEY CLUSTERED ([LicenseClassID])
    );
END
GO

-- ----------------------------------------------------------------------------
-- 6. TestTypes
-- ----------------------------------------------------------------------------
IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'TestTypes')
BEGIN
    CREATE TABLE [dbo].[TestTypes] (
        [TestTypeID]          INT IDENTITY(1,1) NOT NULL,
        [TestTypeTitle]       NVARCHAR(100)     NOT NULL,
        [TestTypeDescription] NVARCHAR(500)     NOT NULL,
        [TestTypeFees]        SMALLMONEY        NOT NULL,
        CONSTRAINT [PK_TestTypes] PRIMARY KEY CLUSTERED ([TestTypeID])
    );
END
GO
-- ----------------------------------------------------------------------------
-- 7. Applications
-- ----------------------------------------------------------------------------
IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Applications')
BEGIN
    CREATE TABLE [dbo].[Applications] (
        [ApplicationID]     INT IDENTITY(1,1) NOT NULL,
        [ApplicantPersonID] INT               NOT NULL,
        [ApplicationDate]   DATETIME          NOT NULL,
        [ApplicationTypeID] INT               NOT NULL,
        [ApplicationStatus] TINYINT DEFAULT 1 NOT NULL,
        [LastStatusDate]    DATETIME          NOT NULL,
        [PaidFees]          SMALLMONEY        NOT NULL,
        [CreatedByUserID]   INT               NOT NULL,
        CONSTRAINT [PK_Applications] PRIMARY KEY CLUSTERED ([ApplicationID]),
        CONSTRAINT [FK_Applications_ApplicationTypes] FOREIGN KEY ([ApplicationTypeID])
            REFERENCES [dbo].[ApplicationTypes] ([ApplicationTypeID]),
        CONSTRAINT [FK_Applications_People] FOREIGN KEY ([ApplicantPersonID])
            REFERENCES [dbo].[People] ([PersonID]),
        CONSTRAINT [FK_Applications_Users] FOREIGN KEY ([CreatedByUserID])
            REFERENCES [dbo].[Users] ([UserID])
    );
END
GO

-- ----------------------------------------------------------------------------
-- 8. Drivers
-- ----------------------------------------------------------------------------
IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Drivers')
BEGIN
    CREATE TABLE [dbo].[Drivers] (
        [DriverID]        INT IDENTITY(1,1) NOT NULL,
        [PersonID]        INT               NOT NULL,
        [CreatedByUserID] INT               NOT NULL,
        [CreatedDate]     DATETIME          NOT NULL,
        CONSTRAINT [PK_Drivers_1] PRIMARY KEY CLUSTERED ([DriverID]),
        CONSTRAINT [FK_Drivers_People] FOREIGN KEY ([PersonID])
            REFERENCES [dbo].[People] ([PersonID]),
        CONSTRAINT [FK_Drivers_Users] FOREIGN KEY ([CreatedByUserID])
            REFERENCES [dbo].[Users] ([UserID])
    );
END
GO

-- ----------------------------------------------------------------------------
-- 9. Licenses
-- ----------------------------------------------------------------------------
IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Licenses')
BEGIN
    CREATE TABLE [dbo].[Licenses] (
        [LicenseID]        INT IDENTITY(1,1) NOT NULL,
        [ApplicationID]    INT               NOT NULL,
        [DriverID]         INT               NOT NULL,
        [LicenseClass]     INT               NOT NULL,
        [IssueDate]        DATETIME          NOT NULL,
        [ExpirationDate]   DATETIME          NOT NULL,
        [Notes]            NVARCHAR(500)     NULL,
        [PaidFees]         SMALLMONEY        NOT NULL,
        [IsActive]         BIT DEFAULT 1     NOT NULL,
        [IssueReason]      TINYINT DEFAULT 1 NOT NULL,
        [CreatedByUserID]  INT               NOT NULL,
        CONSTRAINT [PK_Licenses] PRIMARY KEY CLUSTERED ([LicenseID]),
        CONSTRAINT [FK_Licenses_Applications] FOREIGN KEY ([ApplicationID])
            REFERENCES [dbo].[Applications] ([ApplicationID]),
        CONSTRAINT [FK_Licenses_Drivers] FOREIGN KEY ([DriverID])
            REFERENCES [dbo].[Drivers] ([DriverID]),
        CONSTRAINT [FK_Licenses_LicenseClasses] FOREIGN KEY ([LicenseClass])
            REFERENCES [dbo].[LicenseClasses] ([LicenseClassID]),
        CONSTRAINT [FK_Licenses_Users] FOREIGN KEY ([CreatedByUserID])
            REFERENCES [dbo].[Users] ([UserID])
    );
END
GO

-- ----------------------------------------------------------------------------
-- 10. LocalDrivingLicenseApplications
-- ----------------------------------------------------------------------------
IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'LocalDrivingLicenseApplications')
BEGIN
    CREATE TABLE [dbo].[LocalDrivingLicenseApplications] (
        [LocalDrivingLicenseApplicationID] INT IDENTITY(1,1) NOT NULL,
        [ApplicationID]                    INT               NOT NULL,
        [LicenseClassID]                   INT               NOT NULL,
        CONSTRAINT [PK_DrivingLicsenseApplications] PRIMARY KEY CLUSTERED ([LocalDrivingLicenseApplicationID]),
        CONSTRAINT [FK_DrivingLicsenseApplications_Applications] FOREIGN KEY ([ApplicationID])
            REFERENCES [dbo].[Applications] ([ApplicationID]),
        CONSTRAINT [FK_DrivingLicsenseApplications_LicenseClasses] FOREIGN KEY ([LicenseClassID])
            REFERENCES [dbo].[LicenseClasses] ([LicenseClassID])
    );
END
GO

END
GO
-- ----------------------------------------------------------------------------
-- 12. TestAppointments
-- ----------------------------------------------------------------------------
IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'TestAppointments')
BEGIN
    CREATE TABLE [dbo].[TestAppointments] (
        [TestAppointmentID]                 INT IDENTITY(1,1) NOT NULL,
        [TestTypeID]                        INT               NOT NULL,
        [LocalDrivingLicenseApplicationID]  INT               NOT NULL,
        [AppointmentDate]                   DATETIME          NOT NULL,
        [PaidFees]                          SMALLMONEY        NOT NULL,
        [CreatedByUserID]                   INT               NOT NULL,
        [IsLocked]                          BIT DEFAULT 0     NOT NULL,
        CONSTRAINT [PK_TestAppointments] PRIMARY KEY CLUSTERED ([TestAppointmentID]),
        CONSTRAINT [FK_TestAppointments_LocalDrivingLicenseApplications] FOREIGN KEY ([LocalDrivingLicenseApplicationID])
            REFERENCES [dbo].[LocalDrivingLicenseApplications] ([LocalDrivingLicenseApplicationID]),
        CONSTRAINT [FK_TestAppointments_TestTypes] FOREIGN KEY ([TestTypeID])
            REFERENCES [dbo].[TestTypes] ([TestTypeID]),
        CONSTRAINT [FK_TestAppointments_Users] FOREIGN KEY ([CreatedByUserID])
            REFERENCES [dbo].[Users] ([UserID])
    );
END
GO

-- ----------------------------------------------------------------------------
-- 13. Tests
-- ----------------------------------------------------------------------------
IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Tests')
BEGIN
    CREATE TABLE [dbo].[Tests] (
        [TestID]            INT IDENTITY(1,1) NOT NULL,
        [TestAppointmentID] INT               NOT NULL,
        [TestResult]        BIT               NOT NULL,
        [Notes]             NVARCHAR(500)     NULL,
        [CreatedByUserID]   INT               NOT NULL,
        CONSTRAINT [PK_Tests] PRIMARY KEY CLUSTERED ([TestID]),
        CONSTRAINT [FK_Tests_TestAppointments] FOREIGN KEY ([TestAppointmentID])
            REFERENCES [dbo].[TestAppointments] ([TestAppointmentID]),
        CONSTRAINT [FK_Tests_Users] FOREIGN KEY ([CreatedByUserID])
            REFERENCES [dbo].[Users] ([UserID])
    );
END
GO

-- ----------------------------------------------------------------------------
-- 14. DetainedLicenses
-- ----------------------------------------------------------------------------
IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'DetainedLicenses')
BEGIN
    CREATE TABLE [dbo].[DetainedLicenses] (
        [DetainID]             INT IDENTITY(1,1) NOT NULL,
        [LicenseID]            INT               NOT NULL,
        [DetainDate]           DATETIME          NOT NULL,
        [FineFees]             SMALLMONEY        NOT NULL,
        [CreatedByUserID]      INT               NOT NULL,
        [IsReleased]           BIT DEFAULT 0     NOT NULL,
        [ReleaseDate]          DATETIME          NULL,
        [ReleasedByUserID]     INT               NULL,
        [ReleaseApplicationID] INT               NULL,
        CONSTRAINT [PK_DetainedLicenses] PRIMARY KEY CLUSTERED ([DetainID]),
        CONSTRAINT [FK_DetainedLicenses_Applications] FOREIGN KEY ([ReleaseApplicationID])
            REFERENCES [dbo].[Applications] ([ApplicationID]),
        CONSTRAINT [FK_DetainedLicenses_Licenses] FOREIGN KEY ([LicenseID])
            REFERENCES [dbo].[Licenses] ([LicenseID]),
        CONSTRAINT [FK_DetainedLicenses_Users] FOREIGN KEY ([CreatedByUserID])
            REFERENCES [dbo].[Users] ([UserID]),
        CONSTRAINT [FK_DetainedLicenses_Users1] FOREIGN KEY ([ReleasedByUserID])
            REFERENCES [dbo].[Users] ([UserID])
    );
END
GO

-- ----------------------------------------------------------------------------
-- 15. InternationalLicenses
-- ----------------------------------------------------------------------------
IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'InternationalLicenses')
BEGIN
    CREATE TABLE [dbo].[InternationalLicenses] (
        [InternationalLicenseID]      INT IDENTITY(1,1) NOT NULL,
        [ApplicationID]               INT               NOT NULL,
        [DriverID]                    INT               NOT NULL,
        [IssuedUsingLocalLicenseID]   INT               NOT NULL,
        [IssueDate]                   DATETIME          NOT NULL,
        [ExpirationDate]              DATETIME          NOT NULL,
        [IsActive]                    BIT               NOT NULL,
        [CreatedByUserID]             INT               NOT NULL,
        CONSTRAINT [PK_InternationalLicenses] PRIMARY KEY CLUSTERED ([InternationalLicenseID]),
        CONSTRAINT [FK_InternationalLicenses_Applications] FOREIGN KEY ([ApplicationID])
            REFERENCES [dbo].[Applications] ([ApplicationID]),
        CONSTRAINT [FK_InternationalLicenses_Drivers] FOREIGN KEY ([DriverID])
            REFERENCES [dbo].[Drivers] ([DriverID]),
        CONSTRAINT [FK_InternationalLicenses_Licenses] FOREIGN KEY ([IssuedUsingLocalLicenseID])
            REFERENCES [dbo].[Licenses] ([LicenseID]),
        CONSTRAINT [FK_InternationalLicenses_Users] FOREIGN KEY ([CreatedByUserID])
            REFERENCES [dbo].[Users] ([UserID])
    );
END
GO
-- ============================================================================
-- VIEWS
-- ============================================================================

-- Drivers_Views
IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.VIEWS WHERE TABLE_NAME = 'Drivers_Views')
    EXEC('CREATE VIEW [dbo].[Drivers_Views] AS SELECT * FROM [dbo].[Drivers]');
GO
ALTER VIEW [dbo].[Drivers_Views] AS
SELECT
    d.DriverID,
    d.PersonID,
    p.NationalNo,
    p.FirstName + ' ' + p.SecondName + ' ' + ISNULL(p.ThirdName, '') + ' ' + p.LastName AS FullName,
    d.CreatedDate,
    (SELECT COUNT(l.LicenseID)
     FROM [dbo].[Licenses] l
     WHERE l.IsActive = 1 AND l.DriverID = d.DriverID) AS NumberOfActiveLicenses
FROM [dbo].[Drivers] d
INNER JOIN [dbo].[People] p ON d.PersonID = p.PersonID;
GO

-- ListLocalDrivingLicenseApplications_View
IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.VIEWS WHERE TABLE_NAME = 'ListLocalDrivingLicenseApplications_View')
    EXEC('CREATE VIEW [dbo].[ListLocalDrivingLicenseApplications_View] AS SELECT * FROM [dbo].[LocalDrivingLicenseApplications]');
GO
ALTER VIEW [dbo].[ListLocalDrivingLicenseApplications_View] AS
SELECT
    lda.LocalDrivingLicenseApplicationID,
    lc.ClassName,
    p.NationalNo,
    p.FirstName + ' ' + p.SecondName + ' ' + ISNULL(p.ThirdName, '') + ' ' + p.LastName AS FullName,
    a.ApplicationDate,
    (SELECT COUNT(ta.TestTypeID)
     FROM [dbo].[Tests] t
     INNER JOIN [dbo].[TestAppointments] ta ON t.TestAppointmentID = ta.TestAppointmentID
     WHERE ta.LocalDrivingLicenseApplicationID = lda.LocalDrivingLicenseApplicationID
       AND t.TestResult = 1) AS PassedTestCount,
    CASE
        WHEN a.ApplicationStatus = 1 THEN 'New'
        WHEN a.ApplicationStatus = 2 THEN 'Cancelled'
        WHEN a.ApplicationStatus = 3 THEN 'Completed'
    END AS Status
FROM [dbo].[LocalDrivingLicenseApplications] lda
INNER JOIN [dbo].[Applications] a ON lda.ApplicationID = a.ApplicationID
INNER JOIN [dbo].[LicenseClasses] lc ON lda.LicenseClassID = lc.LicenseClassID
INNER JOIN [dbo].[People] p ON a.ApplicantPersonID = p.PersonID;
GO

-- GetAllTestAppointments_View
IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.VIEWS WHERE TABLE_NAME = 'GetAllTestAppointments_View')
    EXEC('CREATE VIEW [dbo].[GetAllTestAppointments_View] AS SELECT * FROM [dbo].[TestAppointments]');
GO
ALTER VIEW [dbo].[GetAllTestAppointments_View] AS
SELECT
    ta.TestAppointmentID,
    ta.LocalDrivingLicenseApplicationID,
    tt.TestTypeTitle,
    lc.ClassName,
    ta.AppointmentDate,
    ta.PaidFees,
    p.FirstName + ' ' + p.SecondName + ' ' + ISNULL(p.ThirdName, '') + ' ' + p.LastName AS FullName,
    ta.IsLocked
FROM [dbo].[TestAppointments] ta
INNER JOIN [dbo].[TestTypes] tt ON ta.TestTypeID = tt.TestTypeID
INNER JOIN [dbo].[LocalDrivingLicenseApplications] lda ON ta.LocalDrivingLicenseApplicationID = lda.LocalDrivingLicenseApplicationID
INNER JOIN [dbo].[Applications] a ON lda.ApplicationID = a.ApplicationID
INNER JOIN [dbo].[People] p ON a.ApplicantPersonID = p.PersonID
INNER JOIN [dbo].[LicenseClasses] lc ON lda.LicenseClassID = lc.LicenseClassID;
GO
-- ============================================================================
-- SEED DATA
-- ============================================================================

-- Countries
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Afghanistan')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Afghanistan');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Albania')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Albania');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Algeria')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Algeria');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Andorra')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Andorra');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Angola')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Angola');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Antigua and Barbuda')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Antigua and Barbuda');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Argentina')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Argentina');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Armenia')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Armenia');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Austria')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Austria');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Azerbaijan')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Azerbaijan');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Bahrain')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Bahrain');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Bangladesh')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Bangladesh');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Barbados')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Barbados');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Belarus')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Belarus');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Belgium')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Belgium');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Belize')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Belize');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Benin')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Benin');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Bhutan')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Bhutan');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Bolivia')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Bolivia');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Bosnia and Herzegovina')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Bosnia and Herzegovina');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Botswana')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Botswana');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Brazil')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Brazil');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Brunei')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Brunei');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Bulgaria')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Bulgaria');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Burkina Faso')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Burkina Faso');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Burundi')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Burundi');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Cabo Verde')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Cabo Verde');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Cambodia')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Cambodia');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Cameroon')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Cameroon');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Canada')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Canada');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Central African Republic')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Central African Republic');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Chad')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Chad');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Channel Islands')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Channel Islands');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Chile')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Chile');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'China')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'China');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Colombia')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Colombia');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Comoros')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Comoros');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Congo')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Congo');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Costa Rica')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Costa Rica');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Cote d''Ivoire')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Cote d''Ivoire');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Croatia')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Croatia');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Cuba')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Cuba');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Cyprus')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Cyprus');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Czech Republic')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Czech Republic');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Denmark')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Denmark');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Djibouti')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Djibouti');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Dominica')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Dominica');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Dominican Republic')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Dominican Republic');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'DR Congo')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'DR Congo');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Ecuador')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Ecuador');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Egypt')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Egypt');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'El Salvador')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'El Salvador');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Equatorial Guinea')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Equatorial Guinea');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Eritrea')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Eritrea');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Estonia')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Estonia');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Eswatini')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Eswatini');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Ethiopia')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Ethiopia');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Faeroe Islands')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Faeroe Islands');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Finland')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Finland');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'France')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'France');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'French Guiana')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'French Guiana');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Gabon')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Gabon');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Gambia')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Gambia');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Georgia')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Georgia');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Germany')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Germany');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Ghana')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Ghana');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Gibraltar')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Gibraltar');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Greece')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Greece');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Grenada')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Grenada');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Guatemala')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Guatemala');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Guinea')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Guinea');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Guinea-Bissau')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Guinea-Bissau');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Guyana')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Guyana');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Haiti')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Haiti');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Holy See')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Holy See');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Honduras')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Honduras');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Hong Kong')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Hong Kong');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Hungary')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Hungary');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Iceland')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Iceland');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'India')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'India');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Indonesia')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Indonesia');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Iran')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Iran');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Iraq')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Iraq');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Ireland')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Ireland');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Isle of Man')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Isle of Man');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Israel')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Israel');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Italy')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Italy');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Jamaica')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Jamaica');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Japan')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Japan');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Jordan')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Jordan');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Kazakhstan')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Kazakhstan');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Kenya')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Kenya');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Kuwait')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Kuwait');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Kyrgyzstan')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Kyrgyzstan');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Laos')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Laos');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Latvia')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Latvia');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Lebanon')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Lebanon');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Lesotho')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Lesotho');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Liberia')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Liberia');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Libya')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Libya');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Liechtenstein')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Liechtenstein');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Lithuania')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Lithuania');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Luxembourg')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Luxembourg');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Macao')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Macao');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Madagascar')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Madagascar');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Malawi')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Malawi');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Malaysia')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Malaysia');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Maldives')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Maldives');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Mali')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Mali');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Malta')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Malta');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Mauritania')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Mauritania');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Mauritius')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Mauritius');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Mayotte')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Mayotte');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Mexico')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Mexico');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Moldova')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Moldova');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Monaco')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Monaco');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Mongolia')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Mongolia');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Montenegro')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Montenegro');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Morocco')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Morocco');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Mozambique')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Mozambique');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Myanmar')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Myanmar');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Namibia')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Namibia');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Nepal')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Nepal');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Netherlands')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Netherlands');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Nicaragua')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Nicaragua');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Niger')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Niger');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Nigeria')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Nigeria');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'North Korea')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'North Korea');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'North Macedonia')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'North Macedonia');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Norway')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Norway');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Oman')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Oman');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Pakistan')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Pakistan');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Panama')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Panama');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Paraguay')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Paraguay');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Peru')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Peru');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Philippines')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Philippines');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Poland')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Poland');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Portugal')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Portugal');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Qatar')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Qatar');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Reunion')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Reunion');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Romania')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Romania');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Russia')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Russia');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Rwanda')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Rwanda');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Saint Helena')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Saint Helena');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Saint Kitts and Nevis')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Saint Kitts and Nevis');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Saint Lucia')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Saint Lucia');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Saint Vincent and the Grenadines')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Saint Vincent and the Grenadines');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'San Marino')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'San Marino');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Sao Tome & Principe')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Sao Tome & Principe');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Saudi Arabia')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Saudi Arabia');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Senegal')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Senegal');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Serbia')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Serbia');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Seychelles')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Seychelles');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Sierra Leone')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Sierra Leone');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Singapore')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Singapore');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Slovakia')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Slovakia');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Slovenia')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Slovenia');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Somalia')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Somalia');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'South Africa')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'South Africa');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'South Korea')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'South Korea');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'South Sudan')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'South Sudan');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Spain')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Spain');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Sri Lanka')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Sri Lanka');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'State of Palestine')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'State of Palestine');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Sudan')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Sudan');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Suriname')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Suriname');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Sweden')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Sweden');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Switzerland')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Switzerland');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Syria')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Syria');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Taiwan')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Taiwan');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Tajikistan')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Tajikistan');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Tanzania')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Tanzania');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Thailand')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Thailand');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'The Bahamas')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'The Bahamas');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Timor-Leste')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Timor-Leste');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Togo')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Togo');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Trinidad and Tobago')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Trinidad and Tobago');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Tunisia')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Tunisia');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Turkey')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Turkey');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Turkmenistan')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Turkmenistan');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Uganda')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Uganda');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Ukraine')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Ukraine');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'United Arab Emirates')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'United Arab Emirates');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'United Kingdom')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'United Kingdom');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'United States')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'United States');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Uruguay')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Uruguay');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Uzbekistan')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Uzbekistan');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Venezuela')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Venezuela');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Vietnam')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Vietnam');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Western Sahara')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Western Sahara');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Yemen')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Yemen');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Zambia')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Zambia');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Zimbabwe')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Zimbabwe');
GO

-- ============================================================================
-- SEED DATA
-- ============================================================================

-- Countries
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Afghanistan')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Afghanistan');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Albania')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Albania');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Algeria')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Algeria');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Andorra')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Andorra');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Angola')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Angola');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Antigua and Barbuda')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Antigua and Barbuda');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Argentina')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Argentina');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Armenia')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Armenia');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Austria')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Austria');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Azerbaijan')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Azerbaijan');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Bahrain')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Bahrain');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Bangladesh')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Bangladesh');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Barbados')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Barbados');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Belarus')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Belarus');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Belgium')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Belgium');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Belize')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Belize');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Benin')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Benin');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Bhutan')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Bhutan');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Bolivia')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Bolivia');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Bosnia and Herzegovina')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Bosnia and Herzegovina');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Botswana')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Botswana');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Brazil')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Brazil');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Brunei')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Brunei');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Bulgaria')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Bulgaria');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Burkina Faso')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Burkina Faso');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Burundi')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Burundi');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Cabo Verde')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Cabo Verde');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Cambodia')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Cambodia');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Cameroon')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Cameroon');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Canada')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Canada');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Central African Republic')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Central African Republic');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Chad')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Chad');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Channel Islands')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Channel Islands');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Chile')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Chile');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'China')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'China');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Colombia')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Colombia');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Comoros')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Comoros');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Congo')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Congo');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Costa Rica')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Costa Rica');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Cote d''Ivoire')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Cote d''Ivoire');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Croatia')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Croatia');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Cuba')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Cuba');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Cyprus')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Cyprus');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Czech Republic')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Czech Republic');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Denmark')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Denmark');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Djibouti')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Djibouti');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Dominica')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Dominica');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Dominican Republic')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Dominican Republic');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'DR Congo')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'DR Congo');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Ecuador')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Ecuador');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Egypt')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Egypt');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'El Salvador')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'El Salvador');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Equatorial Guinea')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Equatorial Guinea');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Eritrea')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Eritrea');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Estonia')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Estonia');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Eswatini')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Eswatini');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Ethiopia')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Ethiopia');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Faeroe Islands')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Faeroe Islands');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Finland')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Finland');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'France')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'France');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'French Guiana')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'French Guiana');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Gabon')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Gabon');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Gambia')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Gambia');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Georgia')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Georgia');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Germany')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Germany');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Ghana')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Ghana');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Gibraltar')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Gibraltar');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Greece')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Greece');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Grenada')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Grenada');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Guatemala')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Guatemala');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Guinea')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Guinea');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Guinea-Bissau')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Guinea-Bissau');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Guyana')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Guyana');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Haiti')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Haiti');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Holy See')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Holy See');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Honduras')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Honduras');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Hong Kong')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Hong Kong');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Hungary')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Hungary');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Iceland')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Iceland');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'India')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'India');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Indonesia')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Indonesia');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Iran')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Iran');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Iraq')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Iraq');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Ireland')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Ireland');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Isle of Man')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Isle of Man');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Israel')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Israel');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Italy')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Italy');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Jamaica')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Jamaica');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Japan')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Japan');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Jordan')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Jordan');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Kazakhstan')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Kazakhstan');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Kenya')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Kenya');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Kuwait')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Kuwait');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Kyrgyzstan')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Kyrgyzstan');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Laos')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Laos');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Latvia')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Latvia');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Lebanon')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Lebanon');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Lesotho')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Lesotho');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Liberia')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Liberia');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Libya')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Libya');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Liechtenstein')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Liechtenstein');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Lithuania')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Lithuania');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Luxembourg')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Luxembourg');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Macao')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Macao');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Madagascar')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Madagascar');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Malawi')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Malawi');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Malaysia')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Malaysia');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Maldives')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Maldives');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Mali')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Mali');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Malta')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Malta');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Mauritania')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Mauritania');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Mauritius')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Mauritius');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Mayotte')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Mayotte');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Mexico')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Mexico');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Moldova')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Moldova');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Monaco')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Monaco');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Mongolia')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Mongolia');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Montenegro')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Montenegro');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Morocco')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Morocco');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Mozambique')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Mozambique');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Myanmar')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Myanmar');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Namibia')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Namibia');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Nepal')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Nepal');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Netherlands')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Netherlands');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Nicaragua')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Nicaragua');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Niger')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Niger');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Nigeria')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Nigeria');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'North Korea')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'North Korea');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'North Macedonia')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'North Macedonia');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Norway')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Norway');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Oman')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Oman');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Pakistan')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Pakistan');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Panama')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Panama');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Paraguay')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Paraguay');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Peru')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Peru');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Philippines')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Philippines');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Poland')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Poland');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Portugal')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Portugal');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Qatar')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Qatar');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Reunion')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Reunion');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Romania')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Romania');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Russia')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Russia');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Rwanda')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Rwanda');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Saint Helena')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Saint Helena');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Saint Kitts and Nevis')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Saint Kitts and Nevis');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Saint Lucia')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Saint Lucia');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Saint Vincent and the Grenadines')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Saint Vincent and the Grenadines');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'San Marino')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'San Marino');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Sao Tome & Principe')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Sao Tome & Principe');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Saudi Arabia')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Saudi Arabia');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Senegal')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Senegal');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Serbia')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Serbia');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Seychelles')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Seychelles');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Sierra Leone')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Sierra Leone');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Singapore')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Singapore');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Slovakia')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Slovakia');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Slovenia')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Slovenia');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Somalia')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Somalia');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'South Africa')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'South Africa');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'South Korea')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'South Korea');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'South Sudan')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'South Sudan');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Spain')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Spain');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Sri Lanka')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Sri Lanka');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'State of Palestine')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'State of Palestine');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Sudan')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Sudan');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Suriname')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Suriname');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Sweden')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Sweden');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Switzerland')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Switzerland');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Syria')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Syria');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Taiwan')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Taiwan');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Tajikistan')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Tajikistan');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Tanzania')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Tanzania');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Thailand')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Thailand');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'The Bahamas')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'The Bahamas');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Timor-Leste')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Timor-Leste');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Togo')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Togo');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Trinidad and Tobago')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Trinidad and Tobago');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Tunisia')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Tunisia');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Turkey')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Turkey');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Turkmenistan')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Turkmenistan');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Uganda')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Uganda');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Ukraine')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Ukraine');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'United Arab Emirates')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'United Arab Emirates');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'United Kingdom')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'United Kingdom');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'United States')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'United States');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Uruguay')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Uruguay');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Uzbekistan')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Uzbekistan');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Venezuela')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Venezuela');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Vietnam')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Vietnam');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Western Sahara')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Western Sahara');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Yemen')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Yemen');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Zambia')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Zambia');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Countries] WHERE [CountryName] = N'Zimbabwe')
    INSERT INTO [dbo].[Countries] ([CountryName]) VALUES (N'Zimbabwe');
GO

-- Default Admin User
IF NOT EXISTS (SELECT 1 FROM [dbo].[Users] WHERE [UserName] = N'admin')
BEGIN
    IF NOT EXISTS (SELECT 1 FROM [dbo].[People] WHERE [NationalNo] = N'ADMIN001')
    BEGIN
        INSERT INTO [dbo].[People] ([NationalNo],[FirstName],[SecondName],[ThirdName],[LastName],[DateOfBirth],[Gendor],[Address],[Phone],[Email],[NationalityCountryID],[ImagePath])
        VALUES (N'ADMIN001', N'System', N'Admin', NULL, N'User', '2000-01-01', 0, N'DVLD System', N'0000000000', NULL, 1, NULL);

        DECLARE @AdminPersonID INT = SCOPE_IDENTITY();

        INSERT INTO [dbo].[Users] ([PersonID],[UserName],[Password],[IsActive])
        VALUES (@AdminPersonID, N'admin', N'admin', 1);
    END
END
GO

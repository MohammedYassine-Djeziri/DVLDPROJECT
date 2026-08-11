-- ===========================================================================
-- DVLD schema – PostgreSQL (converted from SQL Server)
-- Idempotent: CREATE TABLE IF NOT EXISTS / CREATE OR REPLACE VIEW
-- ===========================================================================

-- applicationtypes
CREATE TABLE IF NOT EXISTS applicationtypes (
    applicationtypeid int GENERATED ALWAYS AS IDENTITY,
    applicationtypetitle varchar(150) NOT NULL,
    applicationfees numeric(10,4) DEFAULT 0 NOT NULL,
    CONSTRAINT pkapplicationtypes PRIMARY KEY (applicationtypeid)
);

-- countries
CREATE TABLE IF NOT EXISTS countries (
    countryid int GENERATED ALWAYS AS IDENTITY,
    countryname varchar(50) NOT NULL,
    CONSTRAINT pkcountries PRIMARY KEY (countryid)
);

-- licenseclasses
CREATE TABLE IF NOT EXISTS licenseclasses (
    licenseclassid int GENERATED ALWAYS AS IDENTITY,
    classname varchar(50) NOT NULL,
    classdescription varchar(500) NOT NULL,
    minimumallowedage smallint DEFAULT 18 NOT NULL,
    defaultvaliditylength smallint DEFAULT 1 NOT NULL,
    classfees numeric(10,4) DEFAULT 0 NOT NULL,
    CONSTRAINT pklicenseclasses PRIMARY KEY (licenseclassid)
);

-- testtypes
CREATE TABLE IF NOT EXISTS testtypes (
    testtypeid int GENERATED ALWAYS AS IDENTITY,
    testtypetitle varchar(100) NOT NULL,
    testtypedescription varchar(500) NOT NULL,
    testtypefees numeric(10,4) NOT NULL,
    CONSTRAINT pktesttypes PRIMARY KEY (testtypeid)
);

-- people
CREATE TABLE IF NOT EXISTS people (
    personid int GENERATED ALWAYS AS IDENTITY,
    nationalno varchar(20) NOT NULL,
    firstname varchar(20) NOT NULL,
    secondname varchar(20) NOT NULL,
    thirdname varchar(20) NULL,
    lastname varchar(20) NOT NULL,
    dateofbirth timestamp NOT NULL,
    gender smallint DEFAULT 0 NOT NULL,
    address varchar(500) NOT NULL,
    phone varchar(20) NOT NULL,
    email varchar(50) NULL,
    nationalitycountryid int NOT NULL,
    imagepath varchar(250) NULL,
    CONSTRAINT pkpeople PRIMARY KEY (personid),
    CONSTRAINT fkpeoplecountries1 FOREIGN KEY (nationalitycountryid) REFERENCES countries(countryid)
);

-- users
CREATE TABLE IF NOT EXISTS users (
    userid int GENERATED ALWAYS AS IDENTITY,
    personid int NOT NULL,
    username varchar(20) NOT NULL,
    password varchar(20) NOT NULL,
    isactive boolean NOT NULL,
    CONSTRAINT pkusers PRIMARY KEY (userid),
    CONSTRAINT fkuserspeople FOREIGN KEY (personid) REFERENCES people(personid)
);

-- applications
CREATE TABLE IF NOT EXISTS applications (
    applicationid int GENERATED ALWAYS AS IDENTITY,
    applicantpersonid int NOT NULL,
    applicationdate timestamp NOT NULL,
    applicationtypeid int NOT NULL,
    applicationstatus smallint DEFAULT 1 NOT NULL,
    laststatusdate timestamp NOT NULL,
    paidfees numeric(10,4) NOT NULL,
    createdbyuserid int NOT NULL,
    CONSTRAINT pkapplications PRIMARY KEY (applicationid),
    CONSTRAINT fkapplicationsapplicationtypes FOREIGN KEY (applicationtypeid) REFERENCES applicationtypes(applicationtypeid),
    CONSTRAINT fkapplicationspeople FOREIGN KEY (applicantpersonid) REFERENCES people(personid),
    CONSTRAINT fkapplicationsusers FOREIGN KEY (createdbyuserid) REFERENCES users(userid)
);
-- drivers
CREATE TABLE IF NOT EXISTS drivers (
    driverid int GENERATED ALWAYS AS IDENTITY,
    personid int NOT NULL,
    createdbyuserid int NOT NULL,
    createddate timestamp NOT NULL,
    CONSTRAINT pkdrivers1 PRIMARY KEY (driverid),
    CONSTRAINT fkdriverspeople FOREIGN KEY (personid) REFERENCES people(personid),
    CONSTRAINT fkdriversusers FOREIGN KEY (createdbyuserid) REFERENCES users(userid)
);

-- licenses
CREATE TABLE IF NOT EXISTS licenses (
    licenseid int GENERATED ALWAYS AS IDENTITY,
    applicationid int NOT NULL,
    driverid int NOT NULL,
    licenseclass int NOT NULL,
    issuedate timestamp NOT NULL,
    expirationdate timestamp NOT NULL,
    notes varchar(500) NULL,
    paidfees numeric(10,4) NOT NULL,
    isactive boolean DEFAULT true NOT NULL,
    issuereason smallint DEFAULT 1 NOT NULL,
    createdbyuserid int NOT NULL,
    CONSTRAINT pklicenses PRIMARY KEY (licenseid),
    CONSTRAINT fklicensesapplications FOREIGN KEY (applicationid) REFERENCES applications(applicationid),
    CONSTRAINT fklicensesdrivers FOREIGN KEY (driverid) REFERENCES drivers(driverid),
    CONSTRAINT fklicenseslicenseclasses FOREIGN KEY (licenseclass) REFERENCES licenseclasses(licenseclassid),
    CONSTRAINT fklicensesusers FOREIGN KEY (createdbyuserid) REFERENCES users(userid)
);

-- localdrivinglicenseapplications
CREATE TABLE IF NOT EXISTS localdrivinglicenseapplications (
    localdrivinglicenseapplicationid int GENERATED ALWAYS AS IDENTITY,
    applicationid int NOT NULL,
    licenseclassid int NOT NULL,
    CONSTRAINT pkdrivinglicsenseapplications PRIMARY KEY (localdrivinglicenseapplicationid),
    CONSTRAINT fkdrivinglicsenseapplicationsapplications FOREIGN KEY (applicationid) REFERENCES applications(applicationid),
    CONSTRAINT fkdrivinglicsenseapplicationslicenseclasses FOREIGN KEY (licenseclassid) REFERENCES licenseclasses(licenseclassid)
);

-- testappointments
CREATE TABLE IF NOT EXISTS testappointments (
    testappointmentid int GENERATED ALWAYS AS IDENTITY,
    testtypeid int NOT NULL,
    localdrivinglicenseapplicationid int NOT NULL,
    appointmentdate timestamp NOT NULL,
    paidfees numeric(10,4) NOT NULL,
    createdbyuserid int NOT NULL,
    islocked boolean DEFAULT false NOT NULL,
    CONSTRAINT pktestappointments PRIMARY KEY (testappointmentid),
    CONSTRAINT fktestappointmentslocaldrivinglicenseapplications FOREIGN KEY (localdrivinglicenseapplicationid) REFERENCES localdrivinglicenseapplications(localdrivinglicenseapplicationid),
    CONSTRAINT fktestappointmentstesttypes FOREIGN KEY (testtypeid) REFERENCES testtypes(testtypeid),
    CONSTRAINT fktestappointmentsusers FOREIGN KEY (createdbyuserid) REFERENCES users(userid)
);

-- tests
CREATE TABLE IF NOT EXISTS tests (
    testid int GENERATED ALWAYS AS IDENTITY,
    testappointmentid int NOT NULL,
    testresult boolean NOT NULL,
    notes varchar(500) NULL,
    createdbyuserid int NOT NULL,
    CONSTRAINT pktests PRIMARY KEY (testid),
    CONSTRAINT fkteststestappointments FOREIGN KEY (testappointmentid) REFERENCES testappointments(testappointmentid),
    CONSTRAINT fktestsusers FOREIGN KEY (createdbyuserid) REFERENCES users(userid)
);

-- detainedlicenses
CREATE TABLE IF NOT EXISTS detainedlicenses (
    detainid int GENERATED ALWAYS AS IDENTITY,
    licenseid int NOT NULL,
    detaindate timestamp NOT NULL,
    finefees numeric(10,4) NOT NULL,
    createdbyuserid int NOT NULL,
    isreleased boolean DEFAULT false NOT NULL,
    releasedate timestamp NULL,
    releasedbyuserid int NULL,
    releaseapplicationid int NULL,
    CONSTRAINT pkdetainedlicenses PRIMARY KEY (detainid),
    CONSTRAINT fkdetainedlicensesapplications FOREIGN KEY (releaseapplicationid) REFERENCES applications(applicationid),
    CONSTRAINT fkdetainedlicenseslicenses FOREIGN KEY (licenseid) REFERENCES licenses(licenseid),
    CONSTRAINT fkdetainedlicensesusers FOREIGN KEY (createdbyuserid) REFERENCES users(userid),
    CONSTRAINT fkdetainedlicensesusers1 FOREIGN KEY (releasedbyuserid) REFERENCES users(userid)
);

-- internationallicenses
CREATE TABLE IF NOT EXISTS internationallicenses (
    internationallicenseid int GENERATED ALWAYS AS IDENTITY,
    applicationid int NOT NULL,
    driverid int NOT NULL,
    issuedusinglocallicenseid int NOT NULL,
    issuedate timestamp NOT NULL,
    expirationdate timestamp NOT NULL,
    isactive boolean NOT NULL,
    createdbyuserid int NOT NULL,
    CONSTRAINT pkinternationallicenses PRIMARY KEY (internationallicenseid),
    CONSTRAINT fkinternationallicensesapplications FOREIGN KEY (applicationid) REFERENCES applications(applicationid),
    CONSTRAINT fkinternationallicensesdrivers FOREIGN KEY (driverid) REFERENCES drivers(driverid),
    CONSTRAINT fkinternationallicenseslicenses FOREIGN KEY (issuedusinglocallicenseid) REFERENCES licenses(licenseid),
    CONSTRAINT fkinternationallicensesusers FOREIGN KEY (createdbyuserid) REFERENCES users(userid)
);

-- =====================================================================
-- Views
-- =====================================================================

-- driversviews
CREATE OR REPLACE VIEW driversviews AS
SELECT
    d.driverid,
    d.personid,
    p.nationalno,
    p.firstname || ' ' || p.secondname || ' ' || COALESCE(p.thirdname, '') || ' ' || p.lastname AS fullname,
    d.createddate,
    (SELECT COUNT(l.licenseid)
     FROM licenses l
     WHERE l.isactive = true AND l.driverid = d.driverid) AS numberofactivelicenses
FROM drivers d
INNER JOIN people p ON d.personid = p.personid;

-- listlocaldrivinglicenseapplicationsview
CREATE OR REPLACE VIEW listlocaldrivinglicenseapplicationsview AS
SELECT
    lda.localdrivinglicenseapplicationid,
    lc.classname,
    p.nationalno,
    p.firstname || ' ' || p.secondname || ' ' || COALESCE(p.thirdname, '') || ' ' || p.lastname AS fullname,
    a.applicationdate,
    (SELECT COUNT(ta.testtypeid)
     FROM tests t
     INNER JOIN testappointments ta ON t.testappointmentid = ta.testappointmentid
     WHERE ta.localdrivinglicenseapplicationid = lda.localdrivinglicenseapplicationid
       AND t.testresult = true) AS passedtestcount,
    CASE
        WHEN a.applicationstatus = 1 THEN 'New'
        WHEN a.applicationstatus = 2 THEN 'Cancelled'
        WHEN a.applicationstatus = 3 THEN 'Completed'
    END AS status
FROM localdrivinglicenseapplications lda
INNER JOIN applications a ON lda.applicationid = a.applicationid
INNER JOIN licenseclasses lc ON lda.licenseclassid = lc.licenseclassid
INNER JOIN people p ON a.applicantpersonid = p.personid;

-- getalltestappointmentsview
CREATE OR REPLACE VIEW getalltestappointmentsview AS
SELECT
    ta.testappointmentid,
    ta.localdrivinglicenseapplicationid,
    tt.testtypetitle,
    lc.classname,
    ta.appointmentdate,
    ta.paidfees,
    p.firstname || ' ' || p.secondname || ' ' || COALESCE(p.thirdname, '') || ' ' || p.lastname AS fullname,
    ta.islocked
FROM testappointments ta
INNER JOIN testtypes tt ON ta.testtypeid = tt.testtypeid
INNER JOIN localdrivinglicenseapplications lda ON ta.localdrivinglicenseapplicationid = lda.localdrivinglicenseapplicationid
INNER JOIN applications a ON lda.applicationid = a.applicationid
INNER JOIN people p ON a.applicantpersonid = p.personid
INNER JOIN licenseclasses lc ON lda.licenseclassid = lc.licenseclassid;

-- countries
INSERT INTO countries (countryname)
SELECT 'Afghanistan' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Afghanistan');
INSERT INTO countries (countryname)
SELECT 'Albania' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Albania');
INSERT INTO countries (countryname)
SELECT 'Algeria' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Algeria');
INSERT INTO countries (countryname)
SELECT 'Andorra' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Andorra');
INSERT INTO countries (countryname)
SELECT 'Angola' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Angola');
INSERT INTO countries (countryname)
SELECT 'Antigua and Barbuda' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Antigua and Barbuda');
INSERT INTO countries (countryname)
SELECT 'Argentina' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Argentina');
INSERT INTO countries (countryname)
SELECT 'Armenia' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Armenia');
INSERT INTO countries (countryname)
SELECT 'Austria' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Austria');
INSERT INTO countries (countryname)
SELECT 'Azerbaijan' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Azerbaijan');
INSERT INTO countries (countryname)
SELECT 'Bahrain' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Bahrain');
INSERT INTO countries (countryname)
SELECT 'Bangladesh' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Bangladesh');
INSERT INTO countries (countryname)
SELECT 'Barbados' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Barbados');
INSERT INTO countries (countryname)
SELECT 'Belarus' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Belarus');
INSERT INTO countries (countryname)
SELECT 'Belgium' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Belgium');
INSERT INTO countries (countryname)
SELECT 'Belize' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Belize');
INSERT INTO countries (countryname)
SELECT 'Benin' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Benin');
INSERT INTO countries (countryname)
SELECT 'Bhutan' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Bhutan');
INSERT INTO countries (countryname)
SELECT 'Bolivia' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Bolivia');
INSERT INTO countries (countryname)
SELECT 'Bosnia and Herzegovina' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Bosnia and Herzegovina');
INSERT INTO countries (countryname)
SELECT 'Botswana' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Botswana');
INSERT INTO countries (countryname)
SELECT 'Brazil' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Brazil');
INSERT INTO countries (countryname)
SELECT 'Brunei' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Brunei');
INSERT INTO countries (countryname)
SELECT 'Bulgaria' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Bulgaria');
INSERT INTO countries (countryname)
SELECT 'Burkina Faso' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Burkina Faso');
INSERT INTO countries (countryname)
SELECT 'Burundi' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Burundi');
INSERT INTO countries (countryname)
SELECT 'Cabo Verde' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Cabo Verde');
INSERT INTO countries (countryname)
SELECT 'Cambodia' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Cambodia');
INSERT INTO countries (countryname)
SELECT 'Cameroon' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Cameroon');
INSERT INTO countries (countryname)
SELECT 'Canada' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Canada');
INSERT INTO countries (countryname)
SELECT 'Central African Republic' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Central African Republic');
INSERT INTO countries (countryname)
SELECT 'Chad' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Chad');
INSERT INTO countries (countryname)
SELECT 'Channel Islands' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Channel Islands');
INSERT INTO countries (countryname)
SELECT 'Chile' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Chile');
INSERT INTO countries (countryname)
SELECT 'China' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'China');
INSERT INTO countries (countryname)
SELECT 'Colombia' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Colombia');
INSERT INTO countries (countryname)
SELECT 'Comoros' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Comoros');
INSERT INTO countries (countryname)
SELECT 'Congo' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Congo');
INSERT INTO countries (countryname)
SELECT 'Costa Rica' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Costa Rica');
INSERT INTO countries (countryname)
SELECT 'Cote d''Ivoire' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Cote d''Ivoire');
INSERT INTO countries (countryname)
SELECT 'Croatia' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Croatia');
INSERT INTO countries (countryname)
SELECT 'Cuba' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Cuba');
INSERT INTO countries (countryname)
SELECT 'Cyprus' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Cyprus');
INSERT INTO countries (countryname)
SELECT 'Czech Republic' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Czech Republic');
INSERT INTO countries (countryname)
SELECT 'Denmark' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Denmark');
INSERT INTO countries (countryname)
SELECT 'Djibouti' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Djibouti');
INSERT INTO countries (countryname)
SELECT 'Dominica' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Dominica');
INSERT INTO countries (countryname)
SELECT 'Dominican Republic' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Dominican Republic');
INSERT INTO countries (countryname)
SELECT 'DR Congo' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'DR Congo');
INSERT INTO countries (countryname)
SELECT 'Ecuador' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Ecuador');
INSERT INTO countries (countryname)
SELECT 'Egypt' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Egypt');
INSERT INTO countries (countryname)
SELECT 'El Salvador' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'El Salvador');
INSERT INTO countries (countryname)
SELECT 'Equatorial Guinea' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Equatorial Guinea');
INSERT INTO countries (countryname)
SELECT 'Eritrea' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Eritrea');
INSERT INTO countries (countryname)
SELECT 'Estonia' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Estonia');
INSERT INTO countries (countryname)
SELECT 'Eswatini' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Eswatini');
INSERT INTO countries (countryname)
SELECT 'Ethiopia' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Ethiopia');
INSERT INTO countries (countryname)
SELECT 'Faeroe Islands' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Faeroe Islands');
INSERT INTO countries (countryname)
SELECT 'Finland' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Finland');
INSERT INTO countries (countryname)
SELECT 'France' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'France');
INSERT INTO countries (countryname)
SELECT 'French Guiana' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'French Guiana');
INSERT INTO countries (countryname)
SELECT 'Gabon' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Gabon');
INSERT INTO countries (countryname)
SELECT 'Gambia' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Gambia');
INSERT INTO countries (countryname)
SELECT 'Georgia' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Georgia');
INSERT INTO countries (countryname)
SELECT 'Germany' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Germany');
INSERT INTO countries (countryname)
SELECT 'Ghana' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Ghana');
INSERT INTO countries (countryname)
SELECT 'Gibraltar' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Gibraltar');
INSERT INTO countries (countryname)
SELECT 'Greece' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Greece');
INSERT INTO countries (countryname)
SELECT 'Grenada' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Grenada');
INSERT INTO countries (countryname)
SELECT 'Guatemala' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Guatemala');
INSERT INTO countries (countryname)
SELECT 'Guinea' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Guinea');
INSERT INTO countries (countryname)
SELECT 'Guinea-Bissau' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Guinea-Bissau');
INSERT INTO countries (countryname)
SELECT 'Guyana' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Guyana');
INSERT INTO countries (countryname)
SELECT 'Haiti' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Haiti');
INSERT INTO countries (countryname)
SELECT 'Holy See' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Holy See');
INSERT INTO countries (countryname)
SELECT 'Honduras' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Honduras');
INSERT INTO countries (countryname)
SELECT 'Hong Kong' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Hong Kong');
INSERT INTO countries (countryname)
SELECT 'Hungary' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Hungary');
INSERT INTO countries (countryname)
SELECT 'Iceland' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Iceland');
INSERT INTO countries (countryname)
SELECT 'India' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'India');
INSERT INTO countries (countryname)
SELECT 'Indonesia' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Indonesia');
INSERT INTO countries (countryname)
SELECT 'Iran' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Iran');
INSERT INTO countries (countryname)
SELECT 'Iraq' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Iraq');
INSERT INTO countries (countryname)
SELECT 'Ireland' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Ireland');
INSERT INTO countries (countryname)
SELECT 'Isle of Man' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Isle of Man');
INSERT INTO countries (countryname)
SELECT 'Israel' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Israel');
INSERT INTO countries (countryname)
SELECT 'Italy' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Italy');
INSERT INTO countries (countryname)
SELECT 'Jamaica' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Jamaica');
INSERT INTO countries (countryname)
SELECT 'Japan' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Japan');
INSERT INTO countries (countryname)
SELECT 'Jordan' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Jordan');
INSERT INTO countries (countryname)
SELECT 'Kazakhstan' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Kazakhstan');
INSERT INTO countries (countryname)
SELECT 'Kenya' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Kenya');
INSERT INTO countries (countryname)
SELECT 'Kuwait' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Kuwait');
INSERT INTO countries (countryname)
SELECT 'Kyrgyzstan' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Kyrgyzstan');
INSERT INTO countries (countryname)
SELECT 'Laos' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Laos');
INSERT INTO countries (countryname)
SELECT 'Latvia' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Latvia');
INSERT INTO countries (countryname)
SELECT 'Lebanon' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Lebanon');
INSERT INTO countries (countryname)
SELECT 'Lesotho' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Lesotho');
INSERT INTO countries (countryname)
SELECT 'Liberia' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Liberia');
INSERT INTO countries (countryname)
SELECT 'Libya' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Libya');
INSERT INTO countries (countryname)
SELECT 'Liechtenstein' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Liechtenstein');
INSERT INTO countries (countryname)
SELECT 'Lithuania' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Lithuania');
INSERT INTO countries (countryname)
SELECT 'Luxembourg' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Luxembourg');
INSERT INTO countries (countryname)
SELECT 'Macao' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Macao');
INSERT INTO countries (countryname)
SELECT 'Madagascar' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Madagascar');
INSERT INTO countries (countryname)
SELECT 'Malawi' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Malawi');
INSERT INTO countries (countryname)
SELECT 'Malaysia' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Malaysia');
INSERT INTO countries (countryname)
SELECT 'Maldives' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Maldives');
INSERT INTO countries (countryname)
SELECT 'Mali' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Mali');
INSERT INTO countries (countryname)
SELECT 'Malta' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Malta');
INSERT INTO countries (countryname)
SELECT 'Mauritania' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Mauritania');
INSERT INTO countries (countryname)
SELECT 'Mauritius' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Mauritius');
INSERT INTO countries (countryname)
SELECT 'Mayotte' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Mayotte');
INSERT INTO countries (countryname)
SELECT 'Mexico' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Mexico');
INSERT INTO countries (countryname)
SELECT 'Moldova' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Moldova');
INSERT INTO countries (countryname)
SELECT 'Monaco' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Monaco');
INSERT INTO countries (countryname)
SELECT 'Mongolia' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Mongolia');
INSERT INTO countries (countryname)
SELECT 'Montenegro' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Montenegro');
INSERT INTO countries (countryname)
SELECT 'Morocco' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Morocco');
INSERT INTO countries (countryname)
SELECT 'Mozambique' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Mozambique');
INSERT INTO countries (countryname)
SELECT 'Myanmar' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Myanmar');
INSERT INTO countries (countryname)
SELECT 'Namibia' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Namibia');
INSERT INTO countries (countryname)
SELECT 'Nepal' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Nepal');
INSERT INTO countries (countryname)
SELECT 'Netherlands' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Netherlands');
INSERT INTO countries (countryname)
SELECT 'Nicaragua' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Nicaragua');
INSERT INTO countries (countryname)
SELECT 'Niger' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Niger');
INSERT INTO countries (countryname)
SELECT 'Nigeria' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Nigeria');
INSERT INTO countries (countryname)
SELECT 'North Korea' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'North Korea');
INSERT INTO countries (countryname)
SELECT 'North Macedonia' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'North Macedonia');
INSERT INTO countries (countryname)
SELECT 'Norway' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Norway');
INSERT INTO countries (countryname)
SELECT 'Oman' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Oman');
INSERT INTO countries (countryname)
SELECT 'Pakistan' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Pakistan');
INSERT INTO countries (countryname)
SELECT 'Panama' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Panama');
INSERT INTO countries (countryname)
SELECT 'Paraguay' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Paraguay');
INSERT INTO countries (countryname)
SELECT 'Peru' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Peru');
INSERT INTO countries (countryname)
SELECT 'Philippines' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Philippines');
INSERT INTO countries (countryname)
SELECT 'Poland' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Poland');
INSERT INTO countries (countryname)
SELECT 'Portugal' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Portugal');
INSERT INTO countries (countryname)
SELECT 'Qatar' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Qatar');
INSERT INTO countries (countryname)
SELECT 'Reunion' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Reunion');
INSERT INTO countries (countryname)
SELECT 'Romania' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Romania');
INSERT INTO countries (countryname)
SELECT 'Russia' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Russia');
INSERT INTO countries (countryname)
SELECT 'Rwanda' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Rwanda');
INSERT INTO countries (countryname)
SELECT 'Saint Helena' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Saint Helena');
INSERT INTO countries (countryname)
SELECT 'Saint Kitts and Nevis' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Saint Kitts and Nevis');
INSERT INTO countries (countryname)
SELECT 'Saint Lucia' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Saint Lucia');
INSERT INTO countries (countryname)
SELECT 'Saint Vincent and the Grenadines' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Saint Vincent and the Grenadines');
INSERT INTO countries (countryname)
SELECT 'San Marino' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'San Marino');
INSERT INTO countries (countryname)
SELECT 'Sao Tome & Principe' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Sao Tome & Principe');
INSERT INTO countries (countryname)
SELECT 'Saudi Arabia' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Saudi Arabia');
INSERT INTO countries (countryname)
SELECT 'Senegal' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Senegal');
INSERT INTO countries (countryname)
SELECT 'Serbia' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Serbia');
INSERT INTO countries (countryname)
SELECT 'Seychelles' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Seychelles');
INSERT INTO countries (countryname)
SELECT 'Sierra Leone' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Sierra Leone');
INSERT INTO countries (countryname)
SELECT 'Singapore' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Singapore');
INSERT INTO countries (countryname)
SELECT 'Slovakia' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Slovakia');
INSERT INTO countries (countryname)
SELECT 'Slovenia' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Slovenia');
INSERT INTO countries (countryname)
SELECT 'Somalia' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Somalia');
INSERT INTO countries (countryname)
SELECT 'South Africa' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'South Africa');
INSERT INTO countries (countryname)
SELECT 'South Korea' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'South Korea');
INSERT INTO countries (countryname)
SELECT 'South Sudan' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'South Sudan');
INSERT INTO countries (countryname)
SELECT 'Spain' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Spain');
INSERT INTO countries (countryname)
SELECT 'Sri Lanka' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Sri Lanka');
INSERT INTO countries (countryname)
SELECT 'State of Palestine' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'State of Palestine');
INSERT INTO countries (countryname)
SELECT 'Sudan' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Sudan');
INSERT INTO countries (countryname)
SELECT 'Suriname' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Suriname');
INSERT INTO countries (countryname)
SELECT 'Sweden' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Sweden');
INSERT INTO countries (countryname)
SELECT 'Switzerland' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Switzerland');
INSERT INTO countries (countryname)
SELECT 'Syria' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Syria');
INSERT INTO countries (countryname)
SELECT 'Taiwan' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Taiwan');
INSERT INTO countries (countryname)
SELECT 'Tajikistan' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Tajikistan');
INSERT INTO countries (countryname)
SELECT 'Tanzania' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Tanzania');
INSERT INTO countries (countryname)
SELECT 'Thailand' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Thailand');
INSERT INTO countries (countryname)
SELECT 'The Bahamas' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'The Bahamas');
INSERT INTO countries (countryname)
SELECT 'Timor-Leste' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Timor-Leste');
INSERT INTO countries (countryname)
SELECT 'Togo' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Togo');
INSERT INTO countries (countryname)
SELECT 'Trinidad and Tobago' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Trinidad and Tobago');
INSERT INTO countries (countryname)
SELECT 'Tunisia' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Tunisia');
INSERT INTO countries (countryname)
SELECT 'Turkey' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Turkey');
INSERT INTO countries (countryname)
SELECT 'Turkmenistan' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Turkmenistan');
INSERT INTO countries (countryname)
SELECT 'Uganda' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Uganda');
INSERT INTO countries (countryname)
SELECT 'Ukraine' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Ukraine');
INSERT INTO countries (countryname)
SELECT 'United Arab Emirates' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'United Arab Emirates');
INSERT INTO countries (countryname)
SELECT 'United Kingdom' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'United Kingdom');
INSERT INTO countries (countryname)
SELECT 'United States' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'United States');
INSERT INTO countries (countryname)
SELECT 'Uruguay' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Uruguay');
INSERT INTO countries (countryname)
SELECT 'Uzbekistan' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Uzbekistan');
INSERT INTO countries (countryname)
SELECT 'Venezuela' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Venezuela');
INSERT INTO countries (countryname)
SELECT 'Vietnam' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Vietnam');
INSERT INTO countries (countryname)
SELECT 'Western Sahara' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Western Sahara');
INSERT INTO countries (countryname)
SELECT 'Yemen' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Yemen');
INSERT INTO countries (countryname)
SELECT 'Zambia' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Zambia');
INSERT INTO countries (countryname)
SELECT 'Zimbabwe' WHERE NOT EXISTS (SELECT 1 FROM countries WHERE countryname = 'Zimbabwe');

-- ApplicationTypes
INSERT INTO applicationtypes (applicationtypetitle, applicationfees)
SELECT 'New Local Driving License Service', 15
WHERE NOT EXISTS (SELECT 1 FROM applicationtypes WHERE applicationtypetitle = 'New Local Driving License Service');
INSERT INTO applicationtypes (applicationtypetitle, applicationfees)
SELECT 'Renew Driving License Service', 5
WHERE NOT EXISTS (SELECT 1 FROM applicationtypes WHERE applicationtypetitle = 'Renew Driving License Service');
INSERT INTO applicationtypes (applicationtypetitle, applicationfees)
SELECT 'Replacement for a Lost Driving License', 10
WHERE NOT EXISTS (SELECT 1 FROM applicationtypes WHERE applicationtypetitle = 'Replacement for a Lost Driving License');
INSERT INTO applicationtypes (applicationtypetitle, applicationfees)
SELECT 'Replacement for a Damaged Driving License', 5
WHERE NOT EXISTS (SELECT 1 FROM applicationtypes WHERE applicationtypetitle = 'Replacement for a Damaged Driving License');
INSERT INTO applicationtypes (applicationtypetitle, applicationfees)
SELECT 'Release Detained Driving Licsense', 15
WHERE NOT EXISTS (SELECT 1 FROM applicationtypes WHERE applicationtypetitle = 'Release Detained Driving Licsense');
INSERT INTO applicationtypes (applicationtypetitle, applicationfees)
SELECT 'New International License', 50
WHERE NOT EXISTS (SELECT 1 FROM applicationtypes WHERE applicationtypetitle = 'New International License');

-- TestTypes
INSERT INTO testtypes (testtypetitle, testtypedescription, testtypefees)
SELECT 'Vision Test', 'This assesses the applicant''s visual acuity to ensure they have sufficient vision to drive safely.', 10
WHERE NOT EXISTS (SELECT 1 FROM testtypes WHERE testtypetitle = 'Vision Test');
INSERT INTO testtypes (testtypetitle, testtypedescription, testtypefees)
SELECT 'Written (Theory) Test', 'This test assesses the applicant''s knowledge of traffic rules, road signs, and driving regulations. It typically consists of multiple-choice questions, and the applicant must select the correct answer(s). The written test aims to ensure that the applicant understands the rules of the road and can apply them in various driving scenarios.', 20
WHERE NOT EXISTS (SELECT 1 FROM testtypes WHERE testtypetitle = 'Written (Theory) Test');
INSERT INTO testtypes (testtypetitle, testtypedescription, testtypefees)
SELECT 'Practical (Street) Test', 'This test evaluates the applicant''s driving skills and ability to operate a motor vehicle safely on public roads. A licensed examiner accompanies the applicant in the vehicle and observes their driving performance.', 30
WHERE NOT EXISTS (SELECT 1 FROM testtypes WHERE testtypetitle = 'Practical (Street) Test');

-- Default Admin User
DO $$
DECLARE
    adminpersonid INT;
BEGIN
    IF NOT EXISTS (SELECT 1 FROM users WHERE username = 'admin') THEN
        IF NOT EXISTS (SELECT 1 FROM people WHERE nationalno = 'ADMIN001') THEN
            INSERT INTO people (nationalno, firstname, secondname, thirdname, lastname, dateofbirth, gender, address, phone, email, nationalitycountryid, imagepath)
            VALUES ('ADMIN001', 'System', 'Admin', NULL, 'User', '2000-01-01', 0, 'DVLD System', '0000000000', NULL, 1, NULL)
            RETURNING personid INTO adminpersonid;

            INSERT INTO users (personid, username, password, isactive)
            VALUES (adminpersonid, 'admin', 'admin', true);
        END IF;
    END IF;
END $$;

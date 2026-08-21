# DVLD — Driving & Vehicle License Department

A desktop **Driving & Vehicle License Department** management system. It models the full
back-office workflow of a real licensing authority: registering people, creating local &
international driving-license applications, scheduling and grading vision / theory /
practical tests, issuing and renewing licenses, detaining and releasing licenses, and
managing the application types, test types and license classes that drive fees and rules.

The application is a classic **3-tier Windows Forms (.NET Framework 4.8)** solution with a
**provider-agnostic Data Access Layer** that can run against **Microsoft SQL Server *or***
**PostgreSQL** from the *same* binaries — you switch databases by editing a single line in
an `.env` file. On first run the app **creates the database and all tables/seed data itself**,
so there is no manual schema setup.

> This project was written for educational purposes (a Programming / Software-Engineering
> course capstone). It is a faithful implementation of the canonical "DVLD" reference design
> used in many curricula.

---

## Table of Contents

1. [What the application does](#what-the-application-does)
2. [Technology stack](#technology-stack)
3. [Solution architecture](#solution-architecture)
4. [Project layout](#project-layout)
5. [The data model](#the-data-model)
6. [The provider-agnostic DAL](#the-provider-agnostic-dal-the-interesting-part)
7. [Business layer patterns](#business-layer-patterns)
8. [Security & authentication](#security--authentication)
9. [Application startup & lifecycle](#application-startup--lifecycle)
10. [Design decisions & trade-offs](#design-decisions--trade-offs)
11. [Known issues / rough edges](#known-issues--rough-edges)
12. [Prerequisites](#prerequisites)
13. [Setup & running](#setup--running)
14. [Configuration reference (`.env`)](#configuration-reference-env)
15. [Building from the command line](#building-from-the-command-line)
16. [License](#license)

---

## What the application does

DVLD is a back-office tool used by licensing department staff (logged-in **users**) to manage
everything around driving licenses. Each feature is a form launched from the main menu:

| Area | What you can do |
|---|---|
| **People** | Full CRUD of people (the applicants). Each person has a national number, full name, DOB, gender, nationality, address, phone, email and an optional photo. Duplicate national numbers are rejected. |
| **Users** | Staff accounts linked to a Person. CRUD users, activate/deactivate, change password. Login uses a salted PBKDF2 hash (see [Security](#security--authentication)). "Remember me" stores the *hash* (never the plain password) in the Windows registry. |
| **Drivers** | A Driver record is auto-created the first time a Person is issued any license. Browse the driver registry. |
| **Local Driving License Applications** | The core workflow. Create a new LDL application for a person + license class. It goes through a fixed sequence of 3 tests (Vision → Written (Theory) → Practical (Street)), each taken via a *Test Appointment*. Pass all three → the license can be issued. Fail → schedule a retake (a new retake application + appointment). |
| **International License Applications** | Issue an international driving license for a person who already holds a valid local license. One active international license per driver. |
| **License Services** | Renew a driving license, or issue a replacement for a lost/damaged license. Each creates a new Application of the matching type and a new License row. |
| **Detained Licenses** | Detain a license (with a fine), release a detained license (with a separate release application + fee), and browse/manage all detained licenses. |
| **Application Types** | Manage the lookup of application types and their fees (New Local License, Renew, Replacement Lost, Replacement Damaged, Release Detained, New International License). |
| **Test Types** | Manage the 3 test types (Vision / Written / Practical), their descriptions and fees. |
| **License Classes** | The 7 license classes (small motorcycle, heavy motorcycle, ordinary car, commercial, agricultural, small/medium bus, truck/heavy vehicle) with min age, validity length and fees. |
| **License history** | View every license (local + international) ever issued for a given driver/person. |

The whole domain is **fee-driven**: every application type, test type and license class
carries a fee, and the totals are computed and shown on each form before saving.

---

## Technology stack

| Layer | Technology |
|---|---|
| UI | **Windows Forms** (WinForms), .NET Framework 4.8 (`System.Windows.Forms`, `System.Drawing`). Designer-generated forms (`.Designer.cs` + `.resx`). Custom `UserControl`s for reusable panels (person info, license info, retake info, filters). |
| Language | C# (compiled against `Microsoft.CSharp`). |
| Business layer | Plain C# class libraries, no ORM. Hand-written entity classes (`clsApplication`, `clsLicense`, ...) with a `New/Update` mode enum and a `Save()` method. |
| Data access | ADO.NET (`System.Data` / `IDbConnection` / `IDbCommand` / `IDataReader` / `DataTable`). **No Entity Framework, no Dapper.** |
| SQL Server provider | `System.Data.SqlClient` (`SqlConnection`, `SqlCommand`). |
| PostgreSQL provider | **Npgsql 4.1.10** (referenced as a raw `lib/Npgsql.dll`). |
| Schema bootstrap | Two hand-written SQL scripts (`mssql_schema.sql`, `postgresql_schema.sql`) shipped as **embedded resources** and executed by the app on first run. |
| Configuration | A plain-text `.env` file at the repo root (parsed manually; no env-vars required). |
| Auth | PBKDF2-HMAC-SHA256 (100k iterations, 16-byte salt, 32-byte hash) via `Rfc2898DeriveBytes`. "Remember me" via the Windows registry (`HKCU\SOFTWARE\LogInInfo`). |
| Build | MSBuild / Visual Studio 2022 (solution format v17). Targets `AnyCPU`, `Debug` & `Release`. |
| Runtime on non-Windows | A .NET Framework 4.8 **WinExe** cannot run natively on Linux; the developer runs it under **Wine** (Wine exposes the Linux filesystem as the `Z:` drive and synthesizes `C:\windows\...`, which is exactly what shows up in the JIT stack traces). |

---

## Solution architecture

The solution (`DVLD_Project/DVLD_Project.sln`) contains **four projects** with a strict,
one-directional dependency graph — lower layers never reference upper layers:

```
                 ┌────────────────────────┐
                 │   DVLD_Project  (UI)   │   WinForms exe  (WinExe)
                 │   namespace: DVLD_*    │
                 └───────────┬────────────┘
                             │ references
                 ┌───────────▼────────────┐
                 │  DVLDBusinessLayer     │   class library (BLL)
                 │  namespace:            │
                 │  DVLDBusinessLayer     │
                 └───────────┬────────────┘
                             │ references
                 ┌───────────▼────────────┐
                 │ DVLD_DataAccessLayer │   class library (DAL)
                 │  namespace:           │
                 │  DataAccessLayer      │
                 └───────────┬────────────┘
                             │ references (Npgsql.dll only, no project)
                 ┌───────────▼────────────┐
                 │        Npgsql          │   raw DLL in /lib
                 └────────────────────────┘

   ClsUtil  →  standalone helper library (currently a near-empty placeholder).
```

**Dependency rules enforced by project references:**

- `DVLD_Project` (UI) → references `DVLDBusinessLayer` only. It must *not* touch the DAL
  directly.
- `DVLDBusinessLayer` (BLL) → references `DVLD_DataAccessLayer` (DAL). Each business
  class (`clsApplication`, `clsUsers`, ...) delegates every DB call to a matching DAL class
  (`clsSqlApplications`, `clsSqlUsers`, ...).
- `DVLD_DataAccessLayer` (DAL) → references **no other project**; it only references the
  raw `Npgsql.dll`. It exposes the provider-agnostic `clsDatabaseFactory` + `clsSql*`
  classes, all in the `DataAccessLayer` namespace.
- `ClsUtil` → a separate utility library; effectively a placeholder for shared helpers.

This keeps the UI free of any SQL, the BLL free of any provider-specific types, and the DAL
free of any business/UI types.

---

## Project layout

```
DVLD/
├── .env                          # DB credentials + provider (NOT committed in real use)
├── .gitignore                    # ignores bin/obj/.vs/ImageCopy/*.exe/*.dll/*.pdb ...
├── README.md                     # this file
├── lib/
│   ├── Npgsql.dll                # raw Npgsql 4.1.10 binary (the PostgreSQL driver)
│   └── npgsql_extracted/         # extracted nupkg metadata (nuspec, xml docs, ...)
│
├── DVLD_Project/                 # ── UI layer (WinForms, WinExe) ──
│   ├── DVLD_Project.sln         # the solution file
│   ├── DVLD_Project.csproj
│   ├── App.config                # assembly binding redirects (System.Memory, ...)
│   ├── Program.cs                # entry point: init DB → run LogInScreen
│   ├── Global/
│   │   ├── clsCurrentUser.cs     # static "who is logged in" holder
│   │   └── Forms/MainMenu.cs    # the shell window; opens child forms in a panel
│   ├── People/Forms/...          # people CRUD, person info control
│   ├── User/Forms/...            # login, users management, add/update user, change pwd
│   ├── Drivers/Forms/...         # manage drivers
│   ├── LocalDrivingLicenseApplication/Forms/...   # LDL application + management + issue license
│   ├── InternationalLicense/Forms/...             # international license app + management
│   ├── License/Forms/...         # renew, replacement for lost/damaged, license history
│   ├── DetainedLicense/Forms/... # detain, release, manage detained
│   ├── Test/Forms/...            # take a test (vision/written/practical)
│   ├── TestAppointment/Forms/...# schedule test appointments
│   ├── TestType/Forms/...        # manage test types
│   ├── ApplicationType/Forms/... # manage application types
│   ├── Application/Forms/...     # show application info
│   ├── .../CustomControls/...    # reusable UserControls (filter boxes, info cards, ...)
│   └── Resources/...              # icons & images (embedded)
│
├── DVLDBusinessLayer/            # ── Business layer (class library) ──
│   ├── DVLD_BusinessLayer.csproj
│   ├── clsApplication.cs         # base application entity (every workflow starts here)
│   ├── clsLocalDrivingLicenseApp.cs
│   ├── clsLicenses.cs
│   ├── clsInternationalLicense.cs
│   ├── clsDetainedLicense.cs
│   ├── clsDriver.cs
│   ├── clsTest.cs
│   ├── clsTestAppointment.cs
│   ├── clsApplicationTypes.cs    # static lookup helpers (fees, names)
│   ├── clsLicenseClasses.cs
│   ├── clsTestTypes.cs
│   ├── clsPeoples.cs
│   └── clsUsers.cs               # incl. PBKDF2 password hashing
│
├── DVLD_DataAccessLayer/       # ── Data access layer (class library) ──
│   ├── DVLD_DataAccessLayer.csproj
│   ├── clsConnectionSettings.cs  # parses .env, builds connection strings, caches them
│   ├── clsDatabaseFactory.cs     # provider-agnostic connection/command/param/query factory
│   ├── clsDatabaseInitializer.cs # creates DB + runs the embedded schema on first run
│   ├── clsSqlPeoples.cs          # one clsSql* class per entity (People, Users, ...)
│   ├── clsSqlUsers.cs
│   ├── clsSqlApplications.cs
│   ├── clsSqlLocalDrivingLicenseApp.cs
│   ├── clsSqlLicenses.cs
│   ├── clsSqlInternationalLicense.cs
│   ├── clsSqlDetainedLicense.cs
│   ├── clsSqlDriver.cs
│   ├── clsSqlTest.cs
│   ├── clsSqlTestAppointment.cs
│   ├── clsSqlApplicationTypes.cs
│   ├── clsSqlLicenseClasses.cs
│   ├── clsSqlTestTypes.cs
│   ├── Schema/
│   │   ├── mssql_schema.sql       # full T-SQL schema + seed data (embedded resource)
│   │   └── postgresql_schema.sql # full PostgreSQL schema + seed data (embedded resource)
│   └── PG_CONVERSION_GUIDE.md    # developer guide/audit for the dual-provider DAL
│
└── ClsUtil/                      # ── shared utility library ──
    └── Class1.cs                 # ClsUtil.IsValidEmail (regex email validator)
```

---

## The data model

The schema (maintained **twice**, once per provider, kept logically identical) contains
**14 tables** plus one view. The dependency graph below mirrors the real foreign keys:

```
                       Countries
                          │
                          ▼
                        People ◀──────────────┐
                          │                   │
                          ├──< Users          │  (a user IS a person)
                          │                   │
                          └──< Drivers ───────┤   (a driver IS a person, created on first license)
                                   │          │
                                   │          │
ApplicationTypes        LicenseClasses         │
        │                     │               │
        ▼                     ▼               │
    Applications ◀──── LocalDrivingLicenseApplications
        │   │                │
        │   │                ├──< TestAppointments ──< Tests
        │   │                │       │                  │
        │   │                │       └── TestTypes ───┘
        │   │                │
        │   │                └──> Licenses ──> InternationalLicenses
        │   │                       │
        │   │                       └──< DetainedLicenses
        │   │
        │   └──── (Applications also drive: renew, replacement, release, international, retake)
        │
        └──> every Application has: ApplicantPersonID, ApplicationTypeID,
                  ApplicationStatus, PaidFees, CreatedByUserID, dates
```

| Table | Purpose |
|---|---|
| `Countries` | Lookup (~200 countries), seed data included. |
| `People` | The applicants: national number, name, DOB, gender, nationality, address, phone, email, image path. |
| `Users` | Staff accounts. Links to `People`. Stores the **salted password hash** + `IsActive`. |
| `ApplicationTypes` | Lookup of application kinds + their fee. |
| `LicenseClasses` | The 7 license classes (seeded) with min age, validity years and fees. |
| `Applications` | The **central** table. *Every* workflow (new local, renew, replacement, release, international, retake) is an Application row with a type, status, fee, applicant person, and creating user. |
| `Drivers` | One row per person who has ever been issued a license. Created lazily. |
| `Licenses` | A concrete driving license: class, driver, dates, paid fees, issue reason, active flag, notes. |
| `LocalDrivingLicenseApplications` | Thin link between an Application and a LicenseClass — the "I want a local license for class X" record. |
| `TestAppointments` | A scheduled sitting for one test type on one LDL application. Locked once the test is taken. May reference a retake Application. |
| `Tests` | The actual test result (pass/fail, notes, taking user) tied to an appointment. |
| `TestTypes` | Vision / Written / Practical, with fee + description. |
| `DetainedLicenses` | A detention record for a license: fine, dates, detained-by/released-by users, `IsReleased`. |
| `InternationalLicenses` | An international driving license issued from an existing local License. |
| `DetainedLicenses_View` | A read-only view joining DetainedLicenses ↔ Licenses ↔ People for the management grid. |

The two schema files (`mssql_schema.sql`, `postgresql_schema.sql`) are **idempotent**:
every `CREATE TABLE` is wrapped in `IF NOT EXISTS`, and every seed `INSERT` checks for
existence before inserting, so re-running the script is safe.

### Domain rules & ID codes (confirmed from the workflow forms)

**Application types** (`Applications.ApplicationTypeID`, seeded in `ApplicationTypes`):

| ID | Title | Fee | Where it's created |
|---|---|---|---|
| 1 | New Local Driving License Service | 15 | `NewLocalDrivingLicenseApplication` (the LDL flow) |
| 2 | Renew Driving License Service | 5 | `RenewDrivingLicenseForm` |
| 3 | Replacement for a Lost Driving License | 10 | `NewLicenseForDamagedOrLostForm` (Lost radio) |
| 4 | Replacement for a Damaged Driving License | 5 | `NewLicenseForDamagedOrLostForm` (Damaged radio) |
| 5 | Release Detained Driving License | 15 | `ReleaseDetainedLicenseForm` |
| 6 | New International License | 50 | `InternationalLicenseAppForm` |

**License issue reasons** (`Licenses.IssueReason`, see `clsLicenses.GetIssueReasonByCode`):

| Code | Meaning |
|---|---|
| 1 | First Time |
| 2 | Renew |
| 3 | Replacement for Lost |
| 4 | Replacement for Damaged |

**Application statuses** (`Applications.ApplicationStatus`): `1` = New (active), `2` =
Cancelled, `3` = Completed. Issuing a license sets the LDL application to status `3`.

**Local license test sequence** (enforced by `LocalDrivingLicenseApplicationManagement` +
`TestAppointment`/`Test`): Vision (TestTypeID 1) → Written/Theory (2) → Practical/Street (3).
You must pass each test before the next can be scheduled; failing a test schedules a **retake**
(a new retake Application + a new TestAppointment that references it via
`RetakeTestApplicationID`). When all three are passed, "Issue License" becomes available.

**International license rule** (`InternationalLicenseAppForm`): only a **Class 3 — Ordinary
driving license** can be used to issue an international license; one **active** international
license per driver; it expires one year after issue.

**Detain vs Release:** detaining a license does **not** create an Application — it just
inserts a `DetainedLicenses` row with a fine and `IsReleased = false`. Releasing it **does**
create a Release application (type 5) and marks the detention row `IsReleased = true` with the
release date/user.

---

## The provider-agnostic DAL (the interesting part)

This is the most architecturally interesting piece of the project, and worth understanding.

### The goal

Write the data-access code **once**, against the ADO.NET interfaces (`IDbConnection`,
`IDbCommand`, `IDataReader`, `DataTable`), and let it run against **both** SQL Server and
PostgreSQL without any `#if` or duplicated class. The provider is chosen at runtime from
`.env`.

### The pieces

**1. `clsConnectionSettings`** — a lazy, thread-safe singleton that:
- Finds the `.env` file by walking up to 6 parent directories from the app's base directory
  (so it works whether you run from `bin/Debug`, the repo root, or Visual Studio).
- Parses it line-by-line (no external package).
- Reads `DB_PROVIDER`, `DB_SERVER`, `DB_NAME`, `DB_USER`, `DB_PASSWORD`.
- Builds **two** connection strings: a normal one (with `Database=`) and a *server-only*
  one (no database) used by the initializer to create the DB.
- Exposes `IsPostgreSQL` and caches everything once initialized.

**2. `clsDatabaseFactory`** — the provider switch. It exposes:
- `CreateConnection()` → `NpgsqlConnection` or `SqlConnection` depending on `IsPostgreSQL`.
- `CreateCommand(query, conn)`, `AddParam(cmd, name, value[, dbType])` (auto-substitutes
  `DBNull` for nulls — important because Npgsql is stricter than SqlClient about nulls).
- **`GetQuery(...)`** — the query translator, with two overloads:

  | Overload | When to use |
  |---|---|
  | `GetQuery(mssql)` — *auto-convert* | For **simple** SELECT/UPDATE/DELETE. Runs `AutoConvertToPg` which strips `[dbo].`, lowercases identifiers, rewrites simple `'Alias' = col` → `col AS "Alias"`, converts `GETDATE()`→`current_timestamp`, `ISNULL(`→`coalesce(`, `LEN(`→`length(`, `DATALENGTH(`→`octet_length(`, and `SELECT TOP n` → appends `LIMIT n`. It is **token-aware**: it never touches the inside of single-quoted literals, double-quoted identifiers or comments, so casing of literal data is preserved. |
  | `GetQuery(mssql, pg)` — *explicit dual version* | For queries where a mechanical rewrite is **not** enough: SQL-level `+` string concatenation (PG needs `||`), `SCOPE_IDENTITY()` (PG needs `RETURNING <pk>`), `bit`/`boolean` comparisons (`WHERE IsActive = 1` is a *type error* in PG), `!= null` (PG needs `IS NOT NULL`), `CASE`-based aliases, `DATEADD`/`DATEDIFF`/`CONVERT`, etc. The PG string is **hand-written** but its logic is copied 1:1 from the MSSQL string (same columns, same WHERE, same parameter count). |

  The golden rule (documented in `PG_CONVERSION_GUIDE.md`): **the MSSQL string is the single
  source of truth for the logic; the only permitted difference in the PG string is identifier
  casing.** "When in doubt, use the dual version."

**3. `clsDatabaseInitializer.EnsureDatabaseCreated()`** — runs once at startup (called from
`Program.Main`):
1. Opens a *server-only* connection and checks `sys.databases` / `pg_database` for the DB.
2. If missing, runs `CREATE DATABASE`.
3. Loads the matching schema SQL from the **embedded resource**
   (`DataAccessLayer.Schema.mssql_schema.sql` or `…postgresql_schema.sql`).
4. Executes it — for PostgreSQL in one multi-statement batch (Npgsql supports that); for SQL
   Server split on `GO` batch separators.

**4. `clsSql*` classes** — one per entity (`clsSqlApplications`, `clsSqlUsers`, ...). Each
method:
- Gets a connection from the factory.
- Picks the right `GetQuery` overload for that query.
- Adds parameters via `AddParam` (always named `@x`; Npgsql accepts `@`-prefixed names too).
- Executes (`ExecuteScalar` for inserts with `RETURNING`/`SCOPE_IDENTITY`,
  `ExecuteReader` for finds, `ExecuteNonQuery` for updates/deletes).
- Wraps everything in `try/finally` so the connection is always closed.

> **Example (from `clsSqlApplications.AddNewApplication`):**
> ```csharp
> string q = clsDatabaseFactory.GetQuery(
>     "INSERT INTO [dbo].[Applications] (...) VALUES (@PerID,...) ; SELECT SCOPE_IDENTITY() ;",
>     "INSERT INTO applications (...) VALUES (@PerID,...) RETURNING applicationid ;");
> // → on SQL Server the first string is used; on PostgreSQL the second.
> ```

### Why this design

- **One codebase, two databases.** You don't maintain two parallel DALs; you maintain one set
  of `clsSql*` methods and a small translation layer.
- **No ORM.** ADO.NET + `DataTable` keeps the dependency surface tiny and the behavior
  identical across providers, at the cost of more boilerplate.
- **The MSSQL string stays the "source of truth."** This keeps the existing SQL Server
  codebase working unchanged; PostgreSQL is an additive translation, never a rewrite.

See `DVLD_DataAccessLayer/PG_CONVERSION_GUIDE.md` for the full rules and a concrete audit.

---

## Business layer patterns

Each domain entity in `DVLDBusinessLayer` follows the same convention:

```csharp
public class clsXxx
{
    public enum EnMode { New = 0, Update = 1 }
    public EnMode Mode;

    // … public properties for every column …

    // "Update" ctor — used when rehydrating an existing row (Mode = Update).
    clsXxx(int id, /* all columns */) { Mode = EnMode.Update; ... }

    // "New" ctor — used when creating a brand-new entity (Mode = New, id = -1).
    clsXxx(/* all columns except id */) { Mode = EnMode.New; this.Id = -1; }

    // Factory that returns a blank New instance.
    public static clsXxx GetEmptyXxx() { ... }

    // Single entry point for persistence. Routes by Mode.
    public void Save()
    {
        switch (Mode)
        {
            case EnMode.New:    this.Id = clsSqlXxx.AddNew(...); break;
            case EnMode.Update: clsSqlXxx.Update(...);          break;
        }
    }

    // Finders return a populated object or an "empty" one (never null for most entities).
    public static clsXxx FindXxxById(int id) { ... }
}
```

Notable specifics:

- **`clsApplication` is the root of almost every workflow.** Renewing a license, replacing
  a lost/damaged one, releasing a detainment, issuing an international license, scheduling a
  retake — each first creates an `Applications` row with the right `ApplicationTypeID` and
  `PaidFees`, then the specialized child row.
- **`clsLicenseClasses`, `clsApplicationTypes`, `clsTestTypes`** are thin static facades
  over their DAL classes — they expose lookup helpers (`FindAppFeesByAppTypeID`,
  `GetLicenseClassNameFromClassID`, `GetTestFeesFromTestTypeID`, ...).
- **`clsUsers`** owns the password hashing (see [Security](#security--authentication)) and
  enforces "never send the plain password to the DAL."
- **`clsPeoples`** manages the person's image file: on save, the chosen source image is
  copied into the runtime `ImageCopy/` folder; on update, the old image file is deleted when
  replaced/removed.

---

## Security & authentication

- **Password storage.** Passwords are **never** stored in plain text. `clsUsers.HashPassword`
  uses PBKDF2-HMAC-SHA256 via `Rfc2898DeriveBytes` with **100,000 iterations**, a
  **16-byte random salt**, and a **32-byte (256-bit) hash**. The stored string format is
  `"<iterations>:<base64-salt>:<base64-hash>"`.
- **Login verification.** The DAL looks the user up **by user name only** — the plain
  password is never sent to SQL (salted hashes can't be compared in SQL anyway).
  `clsUsers.VerifyPassword` then re-derives the hash in the business layer and compares with a
  **constant-time** `SlowEquals` to avoid timing side-channels.
- **Double-hash guard.** `IsPasswordHashed` detects when a string is already in the stored
  format, so editing a user *without* changing the password never re-hashes the hash.
- **"Remember me".** When the checkbox is ticked, the **stored hash** (not the plain
  password) is written to the Windows registry under `HKCU\SOFTWARE\LogInInfo`. On next
  launch, `LogInScreen_Load` reads it back and `FindByUserNamePass` detects (via
  `IsPasswordHashed`) that the supplied value is already a hash and compares directly,
  re-logging the user in without ever holding the real password.
- **Session.** `DVLD_Project.Global.clsCurrentUser.CurrentUser` is a static field holding the
  logged-in `clsUsers`. It is set on successful login and read by the main menu and every
  "created by user" save.

---

## Application startup & lifecycle

`Program.Main` (`[STAThread]`):

1. **`clsDatabaseInitializer.EnsureDatabaseCreated()`** — connect to the server, create the DB
   if absent, run the embedded schema script (idempotent). On failure, show a message box and
   exit cleanly.
2. Enable visual styles.
3. Construct and run **`LogInScreen`** as the application's main form.

**Login → Main Menu loop (`LogInScreen.btn_LOGIN_Click`):**

1. Look up the user by user name (+ hash from registry for the "remember me" path), verify the
   password. On failure, show "Invalid UserName/Password".
2. If the account is inactive, show "Your account is not active...".
3. Otherwise: hide the login form, create a **fresh** `MainMenu` instance, and `ShowDialog()`.
4. When `MainMenu` closes:
   - If `MainMenu.SignOutRequested` is true → re-`Show()` the login form for a new session.
   - Otherwise → `this.Close()` the login form, which exits the app (it's the main form).

> There are extensive comments in `MainMenu.cs` / `LogInScreen.cs` explaining *why* a fresh
> `MainMenu` is created each login and *why* sign-out just closes (rather than re-`ShowDialog`-
> ing the login from within a modal dialog) — earlier attempts hit WinForms' "Form that is
> already displayed modally cannot be displayed as a modal dialog box" error.

`MainMenu` itself is a shell: a left sidebar of buttons + a context menu of actions, and a
`pnlMenu` panel into which child forms are embedded (`OpenChildForm`: child made top-level =
`false`, borderless, docked-fill, added to the panel, brought to front). Some actions open
modal dialogs (`ShowDialog`) instead.

---

## Design decisions & trade-offs

- **3-tier with hand-written DAL (no ORM).** Maximum control, identical behavior across
  providers, tiny dependency surface. Cost: more boilerplate and manual `ref`-parameter
  finders.
- **Dual-database support via a translation factory, not two DALs.** Lets one binary serve
  SQL Server *or* PostgreSQL from `.env`. Cost: the `AutoConvertToPg` regex is clever but
  fragile (it can't handle `+` concat, `SCOPE_IDENTITY`, `bit`/`boolean`, `!= null`,
  `CASE`-aliases — those must use the explicit dual overload). The project carries a whole
  guide (`PG_CONVERSION_GUIDE.md`) documenting exactly what is and isn't safe to auto-convert.
- **MSSQL is the source of truth.** Every dual query is written T-SQL-first; the PG string is
  a 1:1 transcription with lowercased identifiers. This preserves the original SQL Server
  semantics and keeps the migration reversible.
- **Schema shipped as embedded resources, auto-applied at startup.** Zero manual DB setup for
  a new developer/machine. Cost: schema changes require a rebuild, and the initializer only
  creates — it does **not** migrate an existing DB to a newer schema.
- **`.env` discovered by walking up from the bin directory.** Lets one `.env` at the repo root
  serve every project's output dir. Cost: slightly surprising resolution order.
- **Npgsql referenced as a raw `lib/Npgsql.dll`, not via NuGet `<PackageReference>`.** This was
  a deliberate workaround so the DAL project can be built without restoring the full Npgsql
  NuGet. The cost (documented in comments in `DVLD_Project.csproj`) is that Npgsql's
  transitive dependencies (`System.Memory`, `System.Buffers`, `System.Text.Json`,
  `System.Numerics.Vectors`, ...) are **not** auto-copied into the UI output, so a custom
  MSBuild target `CopyNpgsqlRuntimeDependencies` copies them in after build, and `App.config`
  carries the necessary binding redirects.
- **`DataTable` + `IDataReader` for list/find APIs.** Simple, provider-agnostic, binds
  directly to `DataGridView`. Cost: no compile-time column safety; column ordinals are read by
  index, so the MSSQL and PG `SELECT *` column orders must match (the conversion guide
  verifies this).
- **Business entities use a `New/Update` mode + single `Save()`.** Simple to use from the UI
  (`obj.Save()` "just works" whether you're inserting or updating). Cost: no proper Unit-of-
  Work / transactions across multi-table writes (e.g. issuing a license touches several
  tables in sequence, not atomically).
- **Images stored on disk under `ImageCopy/`, path in DB.** Keeps the DB small. Cost: file
  lifecycle must be managed in code (handled in `clsPeoples.Save`).
- **"Remember me" via the registry.** Convenient on Windows; stores the hash only. Cost:
  Windows-only and registry-coupled.


---

## Prerequisites

- **.NET Framework 4.8** Developer Pack / Targeting Pack (for building). On Linux, build with
  `dotnet build` (the `Microsoft.NETFramework.ReferenceAssemblies` NuGet, referenced by every
  project, supplies the framework DLLs so no local .NET Framework install is needed). MSBuild
  (`msbuild`) also works if installed; on this machine only `dotnet` is available.
- **Wine** (only needed to *run* the built `WinExe` on Linux — the .NET Framework 4.8 Windows
  Forms binary does not execute natively on Linux). Install from your distro's package manager
  (e.g. `sudo pacman -S wine` on Arch/CachyOS, `sudo apt install wine` on Debian/Ubuntu).
- A running **SQL Server** (Express is fine) **or** **PostgreSQL** (≥ 9.6; tested against
  modern 10+).
- **Visual Studio 2022** (recommended) or **VS Code** + `dotnet build`/`msbuild` for command-line builds.
- The repo ships `lib/Npgsql.dll` (Npgsql 4.1.10), so you do **not** need NuGet restore for
  the PostgreSQL driver itself. Its transitive dependencies come from NuGet
  (`System.Memory`, `System.Buffers`, `System.Text.Json`, `System.Numerics.Vectors`, ...)
  and are restored on build.

---

## Setup & running

### 1. Clone & enter

```bash
git clone <repo-url>
cd DVLD
```

### 2. Configure the database connection

Edit the `.env` file at the **repository root**. The project supports both providers; the
current development database is **PostgreSQL** (the repo's own `.env` uses
`DB_PROVIDER=postgresql`, `DB_NAME=DVLD2`, `DB_USER=postgres`), but switching to SQL Server
is a one-line change.

PostgreSQL example (matches the dev setup):

```env
# Valid providers: mssql, postgresql
DB_PROVIDER=postgresql
DB_SERVER=localhost
DB_NAME=DVLD
DB_USER=postgres
DB_PASSWORD=password
```

SQL Server example:

```env
DB_PROVIDER=mssql
DB_SERVER=localhost      # "." or "(local)" also work for a local SQL Server
DB_NAME=DVLD
DB_USER=sa
DB_PASSWORD=password
```

The connection string is assembled at runtime:
- **mssql** → `Server={DB_SERVER};Database={DB_NAME};User Id={DB_USER};Password={DB_PASSWORD};`
- **postgresql** → `Host={DB_SERVER};Database={DB_NAME};Username={DB_USER};Password={DB_PASSWORD};`

> **Automatic database creation:** on first run, `Program.Main` calls
> `clsDatabaseInitializer.EnsureDatabaseCreated()`, which connects to the server, creates the
> database if it doesn't exist, and runs the matching embedded schema script (tables + seed
> data: ~200 countries, 6 application types, 3 test types, 7 license classes, a default admin
> user). You only need a reachable server with the credentials above — **no manual schema
> setup is required.** The scripts are idempotent, so subsequent runs are no-ops.

### 3. Build

```bash
# Option A — command line (restores + builds; verified with dotnet on Linux):
dotnet build DVLD_Project/DVLD_Project.sln -c Debug
# (or `msbuild DVLD_Project/DVLD_Project.sln /p:Configuration=Debug` if you have MSBuild)

# Option B — Visual Studio: open DVLD_Project/DVLD_Project.sln and press Ctrl+Shift+B.
```

### 4. Run

```bash
# Linux — run the .NET Framework WinExe under Wine:
wine DVLD_Project/bin/Debug/DVLD_Project.exe

# Windows — double-click the exe, or:
DVLD_Project/bin/Debug/DVLD_Project.exe
```

> On Linux, the application is run under **Wine** (a .NET Framework 4.8 Windows Forms exe
> cannot execute natively on Linux). Wine maps the host filesystem to the `Z:` drive and
> provides a synthetic `C:\windows\...`, which is why JIT/debug paths look like
> `Z:/home/user/.../DVLD_Project.exe` and `C:/windows/Microsoft.NET/...`. Install Wine from
> your distro's package manager (e.g. `sudo pacman -S wine` on Arch/CachyOS,
> `sudo apt install wine` on Debian/Ubuntu). The .NET Framework 4.8 runtime is supplied by
> Wine's `wine-mono`/`wine-gecko` + the `dotnet48` override via `winetricks` if needed.

Or press **F5** in Visual Studio. On first launch, log in with the **default admin user**
seeded by the schema script (see the `-- Default Admin User` block at the end of the
matching `Schema/*.sql`).

---

## Configuration reference (`.env`)

| Key | Required | Default | Notes |
|---|---|---|---|
| `DB_PROVIDER` | no | `mssql` | One of `mssql`, `sqlserver` (alias) or `postgresql`, `postgres`, `pg` (aliases). |
| `DB_SERVER` | no | `.` (mssql) / `localhost` (pg) | Server host. `.`/`(local)` mean a local SQL Server instance. |
| `DB_NAME` | no | `DVLD_DataBase` | Database name. Created automatically if missing. |
| `DB_USER` | no | `sa` (mssql) | DB user/login. |
| `DB_PASSWORD` | no | *(empty)* | DB password. |

Lines starting with `#` are comments. The file is searched starting from the app's base
directory and up to 6 levels up, so a single root-level `.env` is found from any build output
folder.

> ⚠️ `.env` contains credentials. The repository `.gitignore` ignores `.env`, so it is
> **not** committed. Keep your local copy out of source control.

---

## Building from the command line

```bash
# `dotnet build` restores NuGet packages automatically, then builds:
dotnet build DVLD_Project/DVLD_Project.sln -c Debug
# (equivalent with MSBuild: `msbuild DVLD_Project/DVLD_Project.sln /t:Restore /p:Configuration=Debug`)
# → outputs:
#   ClsUtil/bin/Debug/ClsUtil.dll
#   DVLD_DataAccessLayer/bin/Debug/DVLD_DataAccessLayer.dll   (+ Npgsql deps)
#   DVLDBusinessLayer/bin/Debug/DVLDBusinessLayer.dll
#   DVLD_Project/bin/Debug/DVLD_Project.exe
#   (the custom CopyNpgsqlRuntimeDependencies target copies Npgsql's deps next to the exe)
```

For a Release build, replace `Debug` with `Release`.

---

## License

This project is provided **for educational purposes**. No specific open-source license is
declared; treat it as source-available for learning. The bundled `lib/Npgsql.dll` is Npgsql
(its own license applies — see `lib/npgsql_extracted/Npgsql.nuspec`).

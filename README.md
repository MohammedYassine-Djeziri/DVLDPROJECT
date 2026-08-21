# DVLD — Driving & Vehicle License Department

A desktop application for managing the full back-office workflow of a driving & vehicle
licensing authority: registering people, creating local & international driving-license
applications, scheduling and grading vision / written / practical tests, issuing and
renewing licenses, detaining and releasing licenses, and managing the application types,
test types and license classes that drive fees and rules.

It is a **3-tier Windows Forms (.NET Framework 4.8)** application written in C#, with a
**provider-agnostic data access layer** that can run against **Microsoft SQL Server *or***
**PostgreSQL** from the *same* binaries — you pick the database by editing one line in a
`.env` file. On first launch the app **creates all tables/seed data** inside the (pre-existing,
empty) database named in `.env`, so there is no manual schema setup — but the
database itself must already exist and be empty (see step 2).

> For the full architecture, design decisions, data model and code walkthrough see
> **[ARCHITECTURE.md](./ARCHITECTURE.md)**. This README is the short "what + how to run" guide.

---

## Features

- **People** — full CRUD of applicants (national number, name, DOB, gender, nationality,
  address, phone, email, photo); duplicate national numbers rejected.
- **Users** — staff accounts linked to a Person; CRUD, activate/deactivate, change password;
  login uses a salted **PBKDF2** hash; "Remember me" stores the *hash* (never the plain
  password) in the Windows registry.
- **Drivers** — auto-created the first time a person is issued any license.
- **Local Driving License Applications** — the core flow: create an application for a person +
  license class, then pass Vision → Written → Practical tests (with retakes); issue the
  license when all three pass.
- **International Licenses** — issue from an existing Class 3 (ordinary) license; one active
  per driver; valid 1 year.
- **License services** — renew, or replace a lost/damaged license.
- **Detained licenses** — detain (with a fine) and release.
- **Lookups** — manage Application Types, Test Types and License Classes (fees, rules).
- **License history** — view every license (local + international) for a driver/person.

---

## Tech stack (one-liner)

C# / WinForms / .NET Framework 4.8 · ADO.NET (no ORM) · `System.Data.SqlClient` (SQL Server)
or **Npgsql 4.1.10** (PostgreSQL, shipped as `lib/Npgsql.dll`) · embedded SQL schema scripts
· `.env` config · PBKDF2 auth · runs on Windows natively or under **Wine** on Linux.

---

## Prerequisites

- **.NET Framework 4.8** targeting pack (to build). On Linux, `msbuild` works thanks to the
  `Microsoft.NETFramework.ReferenceAssemblies` NuGet referenced by every project.
- **Visual Studio 2022** (easiest) or VS Code + `msbuild` for command-line builds.
- **SQL Server** (Express is fine) **or** **PostgreSQL** — a reachable server is all you need;
  the app creates the database and schema itself on first run.
- **Wine** — only required to *run* the built `WinExe` on Linux (a .NET Framework 4.8 Windows
  Forms binary does not run natively on Linux). Install from your package manager, e.g.
  `sudo pacman -S wine` (Arch) or `sudo apt install wine` (Debian/Ubuntu).

---

## Setup & running

### 1. Clone

```bash
git clone <repo-url>
cd DVLD
```

### 2. Configure the database — `.env` (repo root)

The project supports both providers. The development database is **PostgreSQL**; the repo's
own `.env` looks like this:

```env
# Valid providers: mssql, postgresql
DB_PROVIDER=postgresql
DB_SERVER=localhost
DB_NAME=DVLD
DB_USER=postgres
DB_PASSWORD=password
```

To use SQL Server instead, change one line and the credentials:

```env
DB_PROVIDER=mssql            # "." or "(local)" also work for DB_SERVER
DB_SERVER=localhost
DB_NAME=DVLD
DB_USER=sa
DB_PASSWORD=password
```

> ⚠️ **Pre-create the database.** The database named by `DB_NAME` must
> **already exist** on the server and be **empty** (no tables in it) before you run
> the app. The app does **not** create the database for you — it connects to the
> existing database you point `DB_NAME` at and creates the schema / tables / seed
> data inside it. If a database with that name doesn't exist yet, create it manually
> first, e.g. for PostgreSQL: `createdb DVLD2`; for SQL Server run
> `CREATE DATABASE DVLD_DataBase;` in your query tool. Re-running the app is safe
> (the schema script uses `IF NOT EXISTS`, so it won't duplicate data).`

The connection string is built at runtime from these values and the `.env` file is found
automatically (searched from the app's folder up to 6 levels, so one root `.env` serves
every build output). See [ARCHITECTURE.md](./ARCHITECTURE.md) for the full reference.

> ⚠️ `.env` holds credentials and is **git-ignored** — keep your local copy out of source
> control.

### 3. Build

#### Linux (command line — `dotnet` CLI)

This is a **.NET Framework 4.8** solution built from the old-style (non-SDK)
`*.csproj` format, so on Linux you build it with **`dotnet restore` + `dotnet msbuild`**
(not `dotnet build`, which is for SDK-style projects). The
`Microsoft.NETFramework.ReferenceAssemblies` NuGet package referenced by every
project supplies the .NET Framework 4.8 reference assemblies, so no Mono or
Windows SDK is needed — only the .NET SDK (the `dotnet` command) installed on
your distro.

```bash
# From the repo root. Restore NuGet packages for all projects:
dotnet restore DVLD_Project/DVLD_Project.sln

# Build the whole solution (Debug). Produces the exe + all dependency DLLs:
dotnet msbuild DVLD_Project/DVLD_Project.sln -t:Build -p:Configuration=Debug -nologo

# For a Release build instead, use:
# dotnet msbuild DVLD_Project/DVLD_Project.sln -t:Build -p:Configuration=Release -nologo
```

> `dotnet msbuild` accepts the same flags as `msbuild`/`MSBuild.exe`. `-nologo`
> just suppresses the banner; `-p:Configuration=Debug|Release` selects the
> configuration. You can also target a single project, e.g.
> `dotnet msbuild DVLD_DataAccessLayer/DVLD_DataAccessLayer.csproj -t:Build`.

#### Windows (Visual Studio)

Open `DVLD_Project/DVLD_Project.sln` in **Visual Studio 2022** and press
`Ctrl+Shift+B` (or build from the IDE Build menu). The .NET Framework 4.8
targeting pack must be installed.

Build outputs (Debug):

```bash
ClsUtil/bin/Debug/ClsUtil.dll
DVLD_DataAccessLayer/bin/Debug/DVLD_DataAccessLayer.dll   (+ Npgsql deps)
DVLDBusinessLayer/bin/Debug/DVLDBusinessLayer.dll
DVLD_Project/bin/Debug/DVLD_Project.exe
```

### 4. Run

```bash
# Linux — run the .NET Framework WinExe under Wine:
wine DVLD_Project/bin/Debug/DVLD_Project.exe

# Windows — double-click the exe, or:
DVLD_Project/bin/Debug/DVLD_Project.exe
```

On first launch the app connects to the existing, empty database named in `.env`,
and runs the matching embedded schema script (tables + seed data: ~200 countries, 6
application types, 3 test types, 7 license classes, a default admin user). This is
idempotent, so later runs are no-ops. If the database can't be initialized, a dialog box
reports the error and the app exits.

### 5. Log in

On first launch, log in with the **default admin user(username = admin , password = 1234)** seeded by the schema script (see the
`-- Default Admin User` block at the end of `DVLD_DataAccessLayer/Schema/mssql_schema.sql`
or `postgresql_schema.sql`). After login you land on the **MainMenu**, which holds every
feature behind its left sidebar / context menus.

---

## License

This project is provided **for educational purposes**. No specific open-source license is
declared; treat it as source-available for learning. The bundled `lib/Npgsql.dll` is Npgsql
(its own license applies — see `lib/npgsql_extracted/Npgsql.nuspec`).

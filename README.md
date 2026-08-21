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
database itself must already exist and be empty (see Installation step 6 below).

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

## Installation

A complete, self-contained guide for a fresh clone — nothing is assumed. The only
things you must install yourself are a **.NET SDK** (to build) and, on Linux, **Wine**
(to run). The PostgreSQL driver (`lib/Npgsql.dll`) and all Npgsql runtime dependencies
are **committed in the repo** and copied next to the exe at build time, so you do **not**
need to download any Npgsql files manually.

### 1. Install the .NET SDK

You build with `dotnet restore` + `dotnet msbuild` (the SDK ships its own MSBuild and
NuGet). No Mono and no .NET Framework 4.8 Developer Pack are required — every project
references the `Microsoft.NETFramework.ReferenceAssemblies` NuGet package, which provides
the net4.8 reference assemblies at build time. This is also why `dotnet restore` **must**
run before `dotnet msbuild`.

```bash
# Arch / CachyOS
sudo pacman -S dotnet-sdk          # or pin a channel: dotnet-sdk-8.0

# Debian / Ubuntu
sudo apt install -y dotnet-sdk-8.0

# Fedora / RHEL
sudo dnf install -y dotnet-sdk-8.0

# Any distro (official Microsoft installer, no distro package needed)
curl -fsSL https://dot.net/v1/dotnet-install.sh | bash -s -- --channel 8.0
```

Verify (should print the SDK version):

```bash
dotnet --info
```

> On Windows you may instead use **Visual Studio 2022** with the .NET Framework 4.8
> targeting pack and build from the IDE (`Ctrl+Shift+B`). The `dotnet` CLI works on
> Windows too.

### 2. Install Wine (Linux only — needed to run the app)

The built executable is a .NET Framework 4.8 Windows Forms binary, so it does not run
natively on Linux — it needs Wine:

```bash
sudo pacman -S wine      # Arch / CachyOS
sudo apt install wine     # Debian / Ubuntu
sudo dnf install wine     # Fedora
```

Not needed if you build and run on Windows.

### 3. Have a database server reachable

Install and run either **PostgreSQL** (the dev provider) or **Microsoft SQL Server**
(Express is fine). You only need a reachable server — you do **not** create tables
manually (the app creates the schema on first run), but you **do** create an empty
database yourself (step 6).

### 4. Clone

```bash
git clone <repo-url>
cd DVLD
```

### 5. Configure the database — `.env` (repo root)

The project supports both providers. Create a `.env` at the repo root (it is
git-ignored, so you must create your own). The repo's development database is
PostgreSQL; a working `.env` looks like this:

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

The connection string is built at runtime from these values, and the `.env` file is
found automatically (searched from the app's folder up to 6 levels, so one root `.env`
serves every build output). See [ARCHITECTURE.md](./ARCHITECTURE.md) for the full
reference.

> ⚠️ `.env` holds credentials and is **git-ignored** — keep your local copy out of
> source control.

### 6. Create an empty database matching `DB_NAME`

> ⚠️ The database named by `DB_NAME` must **already exist** on the server and be
> **empty** (no tables in it) before you run the app. The app does **not** create the
> database for you — it connects to the existing database you point `DB_NAME` at and
> creates the schema / tables / seed data inside it. Re-running is safe (the schema
> script uses `IF NOT EXISTS`).

```bash
# PostgreSQL — name must match DB_NAME in .env:
createdb DVLD

# SQL Server — run in your query tool (ssms / sqlcmd / Azure Data Studio):
CREATE DATABASE DVLD;
```

### 7. Build

From the repo root:

```bash
# Restore NuGet packages for all projects (MUST run before the build):
dotnet restore DVLD_Project/DVLD_Project.sln

# Build the whole solution (Debug). Produces the exe + all dependency DLLs:
dotnet msbuild DVLD_Project/DVLD_Project.sln -t:Build -p:Configuration=Debug -nologo

# For a Release build instead:
# dotnet msbuild DVLD_Project/DVLD_Project.sln -t:Build -p:Configuration=Release -nologo
```

Build outputs (Debug):

```
ClsUtil/bin/Debug/ClsUtil.dll
DVLD_DataAccessLayer/bin/Debug/DVLD_DataAccessLayer.dll   (+ Npgsql deps)
DVLDBusinessLayer/bin/Debug/DVLDBusinessLayer.dll
DVLD_Project/bin/Debug/DVLD_Project.exe
```

> The Npgsql runtime dependencies (`System.Memory`, `System.Buffers`,
> `System.Threading.Tasks.Extensions`, etc.) are copied next to the exe automatically
> by a build target in `DVLD_Project/DVLD_Project.csproj`. If you see
> `Could not load file or assembly 'System.Threading.Tasks.Extensions …' File not found`
> at runtime, you skipped `dotnet restore` or built without it — re-run
> `dotnet restore` then `dotnet msbuild`.

### 8. Run

```bash
# Linux — run the .NET Framework WinExe under Wine:
wine DVLD_Project/bin/Debug/DVLD_Project.exe

# Windows — double-click the exe, or:
DVLD_Project/bin/Debug/DVLD_Project.exe
```

On first launch the app connects to the empty database named in `.env` and runs the
matching embedded schema script (tables + seed data: ~200 countries, 6 application
types, 3 test types, 7 license classes, a default admin user). This is idempotent, so
later runs are no-ops. If the database can't be initialized, a dialog box reports the
error and the app exits.

### 9. Log in

Log in with the **default admin user** seeded by the schema script
(**username = `admin`**, **password = `1234`**) — see the `-- Default Admin User` block
at the end of `DVLD_DataAccessLayer/Schema/mssql_schema.sql` or
`postgresql_schema.sql`. After login you land on the **MainMenu**, which holds every
feature behind its left sidebar / context menus.

---

## License

This project is provided **for educational purposes**. No specific open-source license is
declared; treat it as source-available for learning. The bundled `lib/Npgsql.dll` is Npgsql
(its own license applies — see `lib/npgsql_extracted/Npgsql.nuspec`).

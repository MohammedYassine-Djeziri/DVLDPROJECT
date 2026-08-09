# DVLD - Driving & Vehicle License Department

A Windows Forms application for managing driving licenses, license applications, tests, international licenses, detained licenses, and related operations.

## Project Structure

```
DVLD/
├── DVLD_Project/              # Windows Forms UI
│   ├── Applications/          # License application forms
│   ├── People/                # People management forms
│   ├── Users/                 # User management & login forms
│   └── Global/                # Global utilities (current user, etc.)
├── DVLDBusinessLayer/         # Business logic layer
├── PeoplesDataAccessLayer/    # Data access layer (SQL Server / PostgreSQL)
└── ClsUtil/                   # Utility library
```

## Prerequisites

- **.NET Framework 4.8** (or Mono on Linux)
- **SQL Server** or **PostgreSQL** with the `DVLD_DataBase` database
- **Visual Studio 2022** or **VS Code** (optional)

## Setup

### 1. Clone the repository

```bash
git clone <repo-url>
```

### 2. Open the folder

```bash
cd DVLDPROJECT
```

### 3. Configure the database connection

Edit the `.env` file at the project root with your database credentials:

```env
# Valid providers: mssql, postgresql
DB_PROVIDER=mssql
DB_SERVER=.
DB_NAME=DVLD_DataBase
DB_USER=sa
DB_PASSWORD=sa123456
```

**To use PostgreSQL**, change the provider:
```env
DB_PROVIDER=postgresql
DB_SERVER=localhost
DB_NAME=DVLD_DataBase
DB_USER=postgres
DB_PASSWORD=your_password
```

The connection string is assembled automatically at runtime based on the provider:
- **mssql** → `Server={DB_SERVER};Database={DB_NAME};User Id={DB_USER};Password={DB_PASSWORD};`
- **postgresql** → `Host={DB_SERVER};Database={DB_NAME};Username={DB_USER};Password={DB_PASSWORD};`

**Automatic database creation:** On first run, the application automatically creates the database and all required tables (including seed/lookup data) from the `.env` configuration. You only need a running SQL Server or PostgreSQL instance with the credentials specified in `.env` – no manual schema setup is required.  The process is idempotent; subsequent runs will detect that the database already exists and skip creation.

### 4. Build the project

```bash
# Using MSBuild:
msbuild DVLD_Project/DVLD_Project.sln /p:Configuration=Debug

# Or open DVLD_Project.sln in Visual Studio
```

### 5. Run the application

```bash
mono DVLD_Project/bin/Debug/DVLD_Project.exe
```

Or press **F5** in Visual Studio.

## Features

- People management (CRUD)
- User management with login/logout
- Local driving license applications
- International license applications
- License renewal, replacement for damaged/lost licenses
- Detain and release licenses
- Test appointments and results
- Application type management
- License history tracking

## License

This project is for educational purposes.
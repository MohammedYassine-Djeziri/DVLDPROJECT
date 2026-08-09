using System;
using System.Collections.Generic;
using System.IO;

namespace DataAccessLayer
{
    public static class clsConnectionSettings
    {
        private static string _connectionString;
        private static string _databaseName;
        private static readonly object _lock = new object();

        public static string Provider { get; private set; }

        public static bool IsPostgreSQL => Provider == "postgresql";

        public static string ConnectionString
        {
            get
            {
                EnsureInitialized();
                return _connectionString;
            }
            set
            {
                _connectionString = value;
            }
        }

        /// <summary>
        /// Server-only connection string (no Database= parameter).
        /// Used by the database initializer to connect without a database.
        /// </summary>
        public static string ServerConnectionString
        {
            get
            {
                EnsureInitialized();
                string server = _serverValue;
                string user = _userValue;
                string password = _passwordValue;

                if (IsPostgreSQL)
                {
                    if (string.IsNullOrWhiteSpace(server) || server == ".")
                        server = "localhost";
                    return $"Host={server};Username={user};Password={password};";
                }
                else
                {
                    return $"Server={server};User Id={user};Password={password};";
                }
            }
        }

        /// <summary>
        /// Value of DB_NAME from .env (or default).
        /// </summary>
        public static string DatabaseName
        {
            get
            {
                EnsureInitialized();
                return _databaseName;
            }
        }

        // Cached env values for ServerConnectionString
        private static string _serverValue;
        private static string _userValue;
        private static string _passwordValue;
        private static bool _initialized;

        private static void EnsureInitialized()
        {
            if (_initialized)
                return;

            lock (_lock)
            {
                if (_initialized)
                    return;

                var env = LoadEnvFile();

                string provider = GetEnvValue(env, "DB_PROVIDER")?.ToLower() ?? "mssql";
                string server = GetEnvValue(env, "DB_SERVER") ?? ".";
                string dbName = GetEnvValue(env, "DB_NAME") ?? "DVLD_DataBase";
                string user = GetEnvValue(env, "DB_USER") ?? "sa";
                string password = GetEnvValue(env, "DB_PASSWORD") ?? "";

                // Cache raw values
                _serverValue = server;
                _userValue = user;
                _passwordValue = password;
                _databaseName = dbName;

                switch (provider)
                {
                    case "postgresql":
                    case "postgres":
                    case "pg":
                        provider = "postgresql";
                        if (string.IsNullOrWhiteSpace(server) || server == ".")
                            server = "localhost";
                        _connectionString = $"Host={server};Database={dbName};Username={user};Password={password};";
                        break;

                    case "mssql":
                    case "sqlserver":
                    default:
                        provider = "mssql";
                        _connectionString = $"Server={server};Database={dbName};User Id={user};Password={password};";
                        break;
                }

                Provider = provider;
                _initialized = true;
            }
        }

        private static Dictionary<string, string> LoadEnvFile()
        {
            var env = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            try
            {
                string envPath = FindEnvFile();
                if (envPath != null && File.Exists(envPath))
                {
                    foreach (string line in File.ReadAllLines(envPath))
                    {
                        string trimmed = line.Trim();
                        if (string.IsNullOrEmpty(trimmed) || trimmed.StartsWith("#"))
                            continue;

                        int eqIndex = trimmed.IndexOf('=');
                        if (eqIndex > 0)
                        {
                            string key = trimmed.Substring(0, eqIndex).Trim();
                            string value = trimmed.Substring(eqIndex + 1).Trim();
                            env[key] = value;
                        }
                    }
                }
            }
            catch
            {
            }

            return env;
        }

        private static string FindEnvFile()
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;

            for (int i = 0; i < 6; i++)
            {
                string candidate = Path.Combine(baseDir, ".env");
                if (File.Exists(candidate))
                    return candidate;

                baseDir = Directory.GetParent(baseDir)?.FullName;
                if (baseDir == null)
                    break;
            }

            return null;
        }

        private static string GetEnvValue(Dictionary<string, string> env, string key)
        {
            env.TryGetValue(key, out string value);
            return value;
        }
    }
}
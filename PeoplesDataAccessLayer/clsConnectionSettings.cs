using System;
using System.Collections.Generic;
using System.IO;

namespace DataAccessLayer
{
    public static class clsConnectionSettings
    {
        private static string _connectionString;
        private static readonly object _lock = new object();

        public static string Provider { get; private set; }

        public static bool IsPostgreSQL => Provider == "postgresql";

        public static string ConnectionString
        {
            get
            {
                if (_connectionString == null)
                {
                    lock (_lock)
                    {
                        if (_connectionString == null)
                        {
                            var (connStr, provider) = BuildConnectionString();
                            _connectionString = connStr;
                            Provider = provider;
                        }
                    }
                }
                return _connectionString;
            }
            set
            {
                _connectionString = value;
            }
        }

        private static (string connStr, string provider) BuildConnectionString()
        {
            var env = LoadEnvFile();

            string provider = GetEnvValue(env, "DB_PROVIDER")?.ToLower() ?? "mssql";
            string server = GetEnvValue(env, "DB_SERVER") ?? ".";
            string dbName = GetEnvValue(env, "DB_NAME") ?? "DVLD_DataBase";
            string user = GetEnvValue(env, "DB_USER") ?? "sa";
            string password = GetEnvValue(env, "DB_PASSWORD") ?? "";

            switch (provider)
            {
                case "postgresql":
                case "postgres":
                case "pg":
                    provider = "postgresql";
                    return ($"Host={server};Database={dbName};Username={user};Password={password};", provider);

                case "mssql":
                case "sqlserver":
                default:
                    provider = "mssql";
                    return ($"Server={server};Database={dbName};User Id={user};Password={password};", provider);
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
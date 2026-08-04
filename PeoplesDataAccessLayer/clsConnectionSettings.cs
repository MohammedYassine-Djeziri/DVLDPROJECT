using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace DataAccessLayer
{
    public static class clsConnectionSettings
    {
        private static string _connectionString;
        private static readonly object _lock = new object();

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
                            _connectionString = BuildConnectionString();
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

        private static string BuildConnectionString()
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
                    return $"Host={server};Database={dbName};Username={user};Password={password};";

                case "mssql":
                case "sqlserver":
                default:
                    return $"Server={server};Database={dbName};User Id={user};Password={password};";
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
                        // Skip comments and empty lines
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
                // If .env can't be read, fall back to defaults
            }

            return env;
        }

        private static string FindEnvFile()
        {
            // Start from the assembly location and walk up to find .env
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
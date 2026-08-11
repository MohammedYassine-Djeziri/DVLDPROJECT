using System;
using System.Data;
using System.IO;
using System.Reflection;
using Npgsql;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    /// <summary>
    /// Creates the database and all tables on first run.
    /// Reads the matching schema SQL from embedded resources and executes it
    /// against the target server.  All scripts use IF NOT EXISTS / CREATE IF NOT EXISTS
    /// so re-running is safe (idempotent).
    /// </summary>
    public static class clsDatabaseInitializer
    {


        public static void EnsureDatabaseCreated()
        {
            try
            {
                File.AppendAllText("output.txt", "we start EnsureDatabaseCreated\n");
                string databaseName = clsConnectionSettings.DatabaseName;
                string provider = clsConnectionSettings.Provider;
                bool isPg = clsConnectionSettings.IsPostgreSQL;
                string serverConnStr = clsConnectionSettings.ServerConnectionString;
                string dbConnStr = clsConnectionSettings.ConnectionString;

                //write to the output.txt file to indicate that we are in the EnsureDatabaseCreated method

                File.AppendAllText("output.txt", "we are in EnsureDatabaseCreated\n");


                //Console.WriteLine("we are in EnsureDatabaseCreated");


                // 1. Open a server-only connection (no database) and check if the DB exists.
                using (IDbConnection serverConn = CreateServerConnection(serverConnStr, isPg))
                {
                    File.AppendAllText("output.txt", "we are before serverConn.Open()\n");
                    //it's just a connection to the provider server, not to a specific database, so we can check if the database exists and create it if it doesn't
                    serverConn.Open();
                    File.AppendAllText("output.txt", "we are after serverConn.Open()\n");
                    
                    bool dbExists = CheckDatabaseExists(serverConn, databaseName, isPg);

                    
                    File.AppendAllText("output.txt", $"Database exists: {dbExists}\n");
                    if (!dbExists)
                    {
                        using (IDbCommand cmd = serverConn.CreateCommand())
                        {
                            cmd.CommandText = isPg
                                ? $"CREATE DATABASE \"{databaseName}\""
                                : $"CREATE DATABASE [{databaseName}]";
                            cmd.ExecuteNonQuery();
                        }
                    }

                  
                    
                }

                // 2. Connect to the target database and run the schema script.
                string resourceName = isPg
                    ? "DataAccessLayer.Schema.postgresql_schema.sql"
                    : "DataAccessLayer.Schema.mssql_schema.sql";

                string schemaSql = LoadEmbeddedResource(resourceName);

                

                using (IDbConnection dbConn = CreateDbConnection(dbConnStr, isPg))
                {
                    dbConn.Open();

                    if (isPg)
                    {
                        // Npgsql supports multi-statement scripts in one go.
                        using (IDbCommand cmd = dbConn.CreateCommand())
                        {
                            cmd.CommandText = schemaSql;
                            cmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        // SQL Server: split on GO lines (batch separator).
                        string[] batches = schemaSql.Split(new[] { "\nGO" }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (string batch in batches)
                        {
                            string trimmed = batch.Trim();
                            if (string.IsNullOrEmpty(trimmed))
                                continue;

                            using (IDbCommand cmd = dbConn.CreateCommand())
                            {
                                cmd.CommandText = trimmed;
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Database init failed: " + ex.ToString(), ex);
            }
        }

        private static IDbConnection CreateServerConnection(string connStr, bool isPg)
        {
            if (isPg)
                return new NpgsqlConnection(connStr);
            return new SqlConnection(connStr);
        }

        private static IDbConnection CreateDbConnection(string connStr, bool isPg)
        {
            if (isPg)
                return new NpgsqlConnection(connStr);
            return new SqlConnection(connStr);
        }

        private static bool CheckDatabaseExists(IDbConnection conn, string dbName, bool isPg)
        {
            using (IDbCommand cmd = conn.CreateCommand())
            {
                if (isPg)
                {
                    cmd.CommandText = "SELECT 1 FROM pg_database WHERE datname = @db";
                }
                else
                {
                    cmd.CommandText = "SELECT 1 FROM sys.databases WHERE name = @db";
                }

                IDbDataParameter param = cmd.CreateParameter();
                param.ParameterName = "@db";
                param.Value = dbName;
                cmd.Parameters.Add(param);

                object result = cmd.ExecuteScalar();
                return result != null;
            }
        }

        private static string LoadEmbeddedResource(string resourceName)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                    throw new Exception($"Embedded resource not found: {resourceName}");
                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using Npgsql;

namespace DataAccessLayer
{
    /// <summary>
    /// Provider-agnostic database factory.
    /// Centralizes connection, command, parameter, and query selection
    /// so that the DAL can target SQL Server or PostgreSQL transparently.
    /// </summary>
    public static class clsDatabaseFactory
    {
        // -----------------------------------------------------------------
        //  Connection
        // -----------------------------------------------------------------
        public static IDbConnection CreateConnection()
        {
            string connStr = clsConnectionSettings.ConnectionString;
            if (clsConnectionSettings.IsPostgreSQL)
                return new NpgsqlConnection(connStr);
            return new SqlConnection(connStr);
        }

        // -----------------------------------------------------------------
        //  Command
        // -----------------------------------------------------------------
        public static IDbCommand CreateCommand(string query, IDbConnection connection)
        {
            IDbCommand cmd = connection.CreateCommand();
            cmd.CommandText = query;
            return cmd;
        }

        // -----------------------------------------------------------------
        //  Parameter
        // -----------------------------------------------------------------
        /// <summary>
        /// Adds a named parameter to the command.
        /// DBNull is substituted automatically for null values.
        /// </summary>
        public static IDbDataParameter AddParam(IDbCommand cmd, string name, object value)
        {
            IDbDataParameter param = cmd.CreateParameter();
            param.ParameterName = name;
            param.Value = value ?? DBNull.Value;
            cmd.Parameters.Add(param);
            return param;
        }

        /// <summary>
        /// Adds a named parameter with explicit DbType to the command.
        /// DBNull is substituted automatically for null values.
        /// Use this overload when the provider requires precise type information
        /// (e.g. Npgsql is stricter about implicit type mapping).
        /// </summary>
        public static IDbDataParameter AddParam(IDbCommand cmd, string name, object value, DbType dbType)
        {
            IDbDataParameter param = cmd.CreateParameter();
            param.ParameterName = name;
            param.Value = value ?? DBNull.Value;
            param.DbType = dbType;
            cmd.Parameters.Add(param);
            return param;
        }

        // -----------------------------------------------------------------
        //  Query selection
        // -----------------------------------------------------------------
        /// <summary>
        /// Returns the SQL Server query as-is, or auto-converts bracketed
        /// identifiers to lowercase unquoted identifiers for PostgreSQL.
        /// Call this overload for simple queries (SELECT / UPDATE / DELETE)
        /// that do NOT contain SQL-level '+' string concatenation or
        /// SCOPE_IDENTITY().
        /// </summary>
        public static string GetQuery(string mssql)
        {
            if (clsConnectionSettings.IsPostgreSQL)
                return AutoConvertToPg(mssql);
            return mssql;
        }

        /// <summary>
        /// Returns the appropriate query for the active provider.
        /// Call this overload for INSERT…SCOPE_IDENTITY() or queries that
        /// use SQL-level '+' string concatenation.
        /// </summary>
        public static string GetQuery(string mssql, string pg)
        {
            if (clsConnectionSettings.IsPostgreSQL)
                return pg;
            return mssql;
        }

        // -----------------------------------------------------------------
        //  Helpers
        // -----------------------------------------------------------------
        private static string AutoConvertToPg(string mssql)
        {
            // 1. Remove [dbo]. schema prefix (PG uses public schema by default)
            string result = Regex.Replace(mssql, @"\[dbo\]\.", "", RegexOptions.IgnoreCase);

            // 2. Replace remaining [...] identifiers with lowercase unquoted identifiers.
            //    PostgreSQL folds unquoted identifiers to lowercase, so this keeps the
            //    naming consistent (lowercase everywhere) without quoting.
            result = Regex.Replace(result, @"\[([^\]]+)\]",
                m => m.Groups[1].Value.ToLowerInvariant());

            // 3. Convert 'Alias'=column → column AS "Alias" (SQL Server → PG column alias)
            //    Pattern: 'string literal'=identifier[.identifier]
            //    Handles: 'Int.Lic ID'=InternationalLicenses.InternationalLicenseID
            //             'Lic.ID'=Licenses.LicenseID
            //             'Appointment ID'=TestAppointmentID
            result = Regex.Replace(result, @"'([^']+)'=(\w+(?:\.\w+)?)", @"$2 AS ""$1""");

            return result;
        }
    }
}
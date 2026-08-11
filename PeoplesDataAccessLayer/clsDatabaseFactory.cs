using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
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
            // a) Strip [dbo]. schema prefix (PG uses public schema by default)
            string s = Regex.Replace(mssql, @"\[dbo\]\.", "", RegexOptions.IgnoreCase);

            // b) Replace every [identifier] with the identifier lowercased and UNQUOTED.
            //    Handles table, field, view, schema-qualified, and table.column brackets,
            //    and ]] escaped brackets inside the identifier.
            s = Regex.Replace(s, @"\[((?:\]\]|[^\]])+)\]",
                m => m.Groups[1].Value.Replace("]]", "]").ToLowerInvariant());

            // c) Convert SQL Server column-alias syntax to PG. Whitespace-tolerant.
            //    'Alias'=column        ->  column AS "Alias"
            //    'Alias' = table.col   ->  table.col AS "Alias"
            //    The alias text inside double quotes KEEPS its original case.
            s = Regex.Replace(s, @"'([^']+)'\s*=\s*([A-Za-z_]\w*(?:\.[A-Za-z_]\w*)?)",
                "$2 AS \"$1\"");

            // d)+e) Token-aware pass: lowercase identifiers ONLY in normal state.
            //        Never touches string literals, double-quoted identifiers, or comments.
            s = LowercaseAndConvertScalars(s);

            return s;
        }
/// <summary>
        /// Walks the SQL string with a state machine. In normal (code) state
        /// everything is lowercased and scalar function replacements are applied.
        /// String literals (single-quoted), double-quoted identifiers, and
        /// comments (-- and /* */) are copied verbatim.
        /// </summary>
        private static string LowercaseAndConvertScalars(string s)
        {
            var sb = new StringBuilder(s.Length);
            int i = 0;
            int n = s.Length;
            int? topLimit = null;

            while (i < n)
            {
                char c = s[i];

                // line comment  -- ... \n  (or end of string)
                if (c == '-' && i + 1 < n && s[i + 1] == '-')
                {
                    int start = i;
                    while (i < n && s[i] != '\n') i++;
                    sb.Append(s, start, i - start);
                    continue;
                }

                // block comment  /* ... */
                if (c == '/' && i + 1 < n && s[i + 1] == '*')
                {
                    int start = i;
                    i += 2;
                    while (i + 1 < n && !(s[i] == '*' && s[i + 1] == '/')) i++;
                    if (i + 1 < n) i += 2; else i = n;
                    sb.Append(s, start, i - start);
                    continue;
                }

                // single-quoted string literal  '...'  (SQL escapes '' inside)
                if (c == '\'')
                {
                    int start = i;
                    i++;
                    while (i < n)
                    {
                        if (s[i] == '\'')
                        {
                            if (i + 1 < n && s[i + 1] == '\'') { i += 2; continue; }
                            i++; break;
                        }
                        i++;
                    }
                    sb.Append(s, start, i - start);
                    continue;
                }

                // double-quoted identifier  "..."  (PG preserves case verbatim)
                if (c == '"')
                {
                    int start = i;
                    i++;
                    while (i < n && s[i] != '"') i++;
                    if (i < n) i++; // closing quote
                    sb.Append(s, start, i - start);
                    continue;
                }

                // normal (code) state: gather a run until next special token
                int segStart = i;
                while (i < n)
                {
                    char d = s[i];
                    if (d == '\'' || d == '"' ||
                        (d == '-' && i + 1 < n && s[i + 1] == '-') ||
                        (d == '/' && i + 1 < n && s[i + 1] == '*'))
                        break;
                    i++;
                }
                string seg = s.Substring(segStart, i - segStart);

                // Lowercase the whole normal segment (identifiers, keywords,
                // parameters — all become lowercase in PG).
                string lower = seg.ToLowerInvariant();

                // SELECT TOP <n>  ->  remove the TOP n token and remember n;
                // a LIMIT n clause is appended at the end of the whole string.
                var topMatch = Regex.Match(lower, @"\bselect\s+top\s+(\d+)");
                if (topMatch.Success)
                {
                    topLimit = int.Parse(topMatch.Groups[1].Value);
                    lower = Regex.Replace(lower, @"\bselect\s+top\s+\d+\s*", "select ");
                }

                // Mechanical scalar/function replacements (token-safe within
                // normal segments; never applied inside literals or comments).
                lower = Regex.Replace(lower, @"getutcdate\(\)",
                    "(current_timestamp at time zone 'UTC')");
                lower = Regex.Replace(lower, @"getdate\(\)", "current_timestamp");
                lower = Regex.Replace(lower, @"isnull\(", "coalesce(");
                lower = Regex.Replace(lower, @"\bdatalength\(", "octet_length(");
                lower = Regex.Replace(lower, @"\blen\(", "length(");

                sb.Append(lower);
            }

            string result = sb.ToString();

            if (topLimit.HasValue)
            {
                // Append LIMIT n at the end (before any trailing semicolons/whitespace).
                string trimmed = result.TrimEnd();
                string trailing = result.Substring(trimmed.Length);
                result = trimmed + " limit " + topLimit.Value + trailing;
            }

            return result;
        }
    }
}
using Microsoft.Data.SqlClient;
using System.Data;
using System.Text.RegularExpressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TestApi.Helper
{
    public class SqlHelper
    {
        private readonly string _connectString;
        private readonly ILogger<SqlHelper> _logger;

        private Regex _checkregex => new Regex("@[A-Za-z0-9A-Za-z_]+");

        public SqlHelper(IConfiguration configuration, ILogger<SqlHelper> logger)
        {
            _connectString = configuration.GetConnectionString("DefaultConnection")!;
            _logger = logger;
        }

        private (SqlConnection con, SqlCommand cmd) CreateCommand(string conStr, string query, object[] lsparam)
        {
            SqlConnection con = new SqlConnection(conStr);
            SqlCommand cmd = new SqlCommand(query, con);

            MatchCollection ls = _checkregex!.Matches(query);

            for (int i = 0; i < ls.Count; i++)
            {
                var att = ls[i];

                if (lsparam[i] != null)
                {
                    cmd.Parameters.AddWithValue(att.Value, lsparam[i]);
                    if (lsparam[i]!.GetType() == typeof(DataTable))
                    {
                        // TODO add data table here
                        cmd.Parameters[cmd.Parameters.Count - 1].SqlDbType = SqlDbType.Structured;
                        cmd.Parameters[cmd.Parameters.Count - 1].TypeName = ((DataTable)lsparam[i]!).TableName;
                    }
                }
                else
                {
                    cmd.Parameters.AddWithValue(att.Value, DBNull.Value);
                }
            }

            return (con, cmd);
        }

        public DataTable? DataTableTemplate(string query, params object?[] lsparam)
        {
            try
            {
                (SqlConnection con, SqlCommand cmd) = CreateCommand(_connectString, query, lsparam);

                using SqlDataAdapter sda = new SqlDataAdapter(cmd);
                var data = new DataTable();
                sda.Fill(data);
                return data;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(SqlHelper)} Exception");
                return null;
            }
        }

        public int ExcecuteTemplate(string query, params object?[] lsparam)
        {
            try
            {
                (SqlConnection con, SqlCommand cmd) = CreateCommand(_connectString, query, lsparam);

                con.Open();
                int rowCount = cmd.ExecuteNonQuery();
                con.Close();

                return rowCount;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(SqlHelper)} Exception");
                return -1;
            }
        }

        public object ScalarTemplate(string query, params object?[] lsparam)
        {
            try
            {
                (SqlConnection con, SqlCommand cmd) = CreateCommand(_connectString, query, lsparam);

                con.Open();
                object data = cmd.ExecuteScalar();
                con.Close();

                return data;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(SqlHelper)} Exception");
                return -1;
            }
        }
    }

}


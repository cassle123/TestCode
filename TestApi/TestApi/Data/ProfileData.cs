using System.Data;
using TestApi.Helper;

namespace TestApi.Data
{
    public class ProfileData
    {
        private readonly SqlHelper _sqlHelper;
        public ProfileData(SqlHelper sqlHelper)
        {
            _sqlHelper = sqlHelper;
        }

        public DataTable GetProfileUser(string username)
        {
            string query = "exec GetProfileUser @Username";
            DataTable dataTable = _sqlHelper.DataTableTemplate(query, username);
            return dataTable;
        }
    }
}

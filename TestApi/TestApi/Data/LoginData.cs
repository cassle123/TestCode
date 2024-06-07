using Microsoft.AspNetCore.Identity;
using System.Data;
using TestApi.Helper;
using TestApi.Model;

namespace TestApi.Data
{
    public class LoginData
    {
        private readonly SqlHelper _sqlHelper;

        public LoginData (SqlHelper sqlHelper)
        {
            _sqlHelper = sqlHelper;
        }

        public int CheckUserRegisterExists(string username, string password = null)
        {
            string query = "exec CheckUser @Username, @Password";
            object count = _sqlHelper.ScalarTemplate(query, username, password);
            return Convert.ToInt32(count);
        }

        public int CreateNewUser(string username, string email, string password) {
            string query = "exec CreateUser @Username, @Email, @Password";
            return _sqlHelper.ExcecuteTemplate(query, username, email, password);
        }

        public int CheckUserLogin(string username, string password)
        {
            string query = "exec CheckUser @Username, @Password";
            object count = _sqlHelper.ScalarTemplate(query, username, password );
            return Convert.ToInt32(count);
        }

        public DataTable GetUserMetadata(string username)
        {
            return _sqlHelper.DataTableTemplate("exec GetUserBase @Username", username);
        }
    }
}

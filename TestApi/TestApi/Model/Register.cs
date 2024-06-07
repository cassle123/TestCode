using System.ComponentModel.DataAnnotations;

namespace TestApi.Model
{
    public class Register
    {
        public string Username { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}

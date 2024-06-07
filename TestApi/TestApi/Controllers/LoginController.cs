using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TestApi.Data;
using TestApi.Helper;
using TestApi.Model;
using TestApi.Model.Responses;
using TestApi.Services;

namespace TestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowSpecificOrigin")]
    public class LoginController : ControllerBase
    {

        private readonly SqlHelper _sqlHelper;
        private readonly ITokenService _tokenService;

        private readonly LoginData _loginData;

        public LoginController(SqlHelper sqlHelper, LoginData loginData, ITokenService tokenService)
        {
            _sqlHelper = sqlHelper;
            _tokenService = tokenService;
            _loginData = loginData;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] Register register)
        {
            if (string.IsNullOrEmpty(register.Username) || string.IsNullOrEmpty(register.Email) || string.IsNullOrEmpty(register.Password))
            {
                return BadRequest(ApiResponse.Failed(new Error(404, "Username, email, and password are required.")));
            }

            if (_loginData.CheckUserRegisterExists(register.Username) > 0)
            {
                return BadRequest(ApiResponse.Failed(new Error(404, "User name have exists")));
            }

            var result = _loginData.CreateNewUser(register.Username, register.Email, register.Password);

            if (result > 0)
                return Ok(ApiResponse.Success("User registered successfully", null!));
            else
                return BadRequest(ApiResponse.Failed(new Error(404, "Create new user faild")));
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] Login login)
        {
            if (_loginData.CheckUserLogin(login.UserName, login.Password) > 0)
            {
                var user = _loginData.GetUserMetadata(login.UserName);
                return Ok(ApiResponse.Success("Login success", _tokenService.CreateToken(user!.Rows[0]["Username"].ToString()!, Convert.ToInt32(user.Rows[0]["UserId"]))));
            }
            else
            {
                return BadRequest(ApiResponse.Failed(new Error(404, "User not exists !!!")));
            }
        }

        [HttpGet("errorgo")]
        public IActionResult ErrorGo()
        {
            throw new NotImplementedException();
        }
    }
}

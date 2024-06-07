using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Security.Claims;
using TestApi.Data;
using TestApi.Extensions;
using TestApi.Model;
using TestApi.Model.Responses;

namespace TestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly ProfileData _profileData;
        public ProfileController(ProfileData profileData)
        {
            _profileData = profileData;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetProfileUser()
        {
            var username = User.FindFirst(ClaimTypes.Name)?.Value;
            DataTable dt = _profileData.GetProfileUser(username);
            var user = dt.ToClass<User>().First();

            if (dt.Rows.Count > 0)
            {
                return Ok(ApiResponse.Success("Get profile success", user));
            }
            else
            {
                return BadRequest(ApiResponse.Failed(new Error(404, "User not exists !!!")));
            }

        }
    }
}

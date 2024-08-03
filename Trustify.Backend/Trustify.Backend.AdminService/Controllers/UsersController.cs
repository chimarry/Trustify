using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.WebUtilities;
using System.Net.Http.Headers;

namespace Trustify.Backend.AdminService.Controllers
{
    /// <summary>
    /// Manages users in Keycloak server database.
    /// </summary>
    [Route("users")]
    [ApiController]
    public class UsersController : TrustifyAdminControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> RegisterUser()
        {
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser()
        {
            return Ok();
        }

        [HttpPut("delete")]
        public async Task<IActionResult> DeleteUser()
        {
            return Ok();
        }

        [HttpGet("user-info")]
        public async Task<IActionResult> GetUserInfo()
        {
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            return Ok();
        }
    }
}

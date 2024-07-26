using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Trustify.Backend.AdminService.Controllers
{
    [Route("users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetUsers()
        {
            return Ok();
        }
    }
}

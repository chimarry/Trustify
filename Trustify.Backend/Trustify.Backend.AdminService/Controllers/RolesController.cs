using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Trustify.Backend.AdminService.Keycloak.DTO;
using Trustify.Backend.AdminService.Keycloak.Service;
using Trustify.Backend.AdminService.Models;
using Trustify.Backend.AdminService.Security;

namespace Trustify.Backend.AdminService.Controllers
{
    [Route("roles")]
    [ApiController]
    public class RolesController : TrustifyAdminControllerBase
    {
        private readonly IKeycloakRoleService roleService;

        public RolesController(IKeycloakRoleService roleService)
        {
            this.roleService = roleService;
        }

        [HttpPost]
        [Authorize(Policy = TrustifyPolicy.Restricted)]
        public async Task<IActionResult> AddRole([FromBody] RoleWrapper wrapper)
        {
            string? accessToken = await GetTokenFromRequest();
            if (accessToken == null)
                return BadRequest("No access token");


            var dto = new RoleDTO { ClientRole = true, Name = wrapper.Name, Description = wrapper.Description };
            var result = await roleService.AddRole(dto, "8695a1b6-eba4-45f4-8df6-989847e44984", accessToken);
            return Ok(result);
        }

        [HttpGet]
        [Authorize(Policy = TrustifyPolicy.Restricted)]
        public async Task<IActionResult> GetRoles()
        {
            string? accessToken = await GetTokenFromRequest();
            if (accessToken == null)
                return BadRequest("No access token");
            return Ok(await roleService.GetRoles("8695a1b6-eba4-45f4-8df6-989847e44984", accessToken));
        }
    }
}

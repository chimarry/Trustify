using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Trustify.Backend.AdminService.AutoMapper;
using Trustify.Backend.AdminService.Exceptions;
using Trustify.Backend.AdminService.Keycloak.DTO;
using Trustify.Backend.AdminService.Keycloak.Service;
using Trustify.Backend.AdminService.Models;
using Trustify.Backend.AdminService.Security;

namespace Trustify.Backend.AdminService.Controllers
{
    [Route("roles")]
    [ApiController]
    [Authorize(Policy = TrustifyPolicy.Authenticated)]
    [Authorize(Policy = TrustifyPolicy.All)]

    public class RolesController(IRoleService roleService, IMapper mapper) : TrustifyAdminControllerBase
    {
        private readonly IRoleService roleService = roleService;
        private readonly IMapper mapper = mapper;

        /// <summary>
        /// Adds new role to the client.
        /// </summary>
        /// <param name="wrapper">Role data</param>
        /// <param name="clientId">Clients' identifier</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Policy = TrustifyPolicy.Restricted)]
        public async Task<IActionResult> AddRole([FromBody] RoleWrapper wrapper, [FromQuery] string clientId)
        {
            string accessToken = await GetTokenFromRequestOrThrow();

            var result = await roleService.AddRole(mapper.Map<RoleDTO>(wrapper), clientId, accessToken);
            return HttpResultMessage.FilteredResult(result);
        }

        /// <summary>
        /// Returns all roles that belong to one client.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="UnauthorizedException"></exception>
        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, "The result is returned.", typeof(ResultMessage<RoleDTO>))]
        public async Task<IActionResult> GetRoles([FromQuery] string clientId)
        {
            string accessToken = await GetTokenFromRequestOrThrow();

            return HttpResultMessage.FilteredResult(await roleService.GetRoles(clientId, accessToken));
        }

        /// <summary>
        /// Delete role.
        /// </summary>
        /// <param name="roleName">Roles' name</param>
        /// <param name="clientId">Clients' identifier</param>
        /// <returns></returns>
        /// <exception cref="UnauthorizedException"></exception>
        [HttpPut("delete")]
        [Authorize(Policy = TrustifyPolicy.Restricted)]
        public async Task<IActionResult> DeleteRole([FromQuery] string roleName, [FromQuery] string clientId)
        {
            string accessToken = await GetTokenFromRequestOrThrow();

            return HttpResultMessage.FilteredResult(await roleService.DeleteRole(roleName, clientId, accessToken));
        }

        /// <summary>
        /// Returns one role.
        /// </summary>
        /// <param name="roleName">Roles' name</param>
        /// <param name="clientId">Clients' identifier</param>
        /// <returns></returns>
        /// <exception cref="UnauthorizedException"></exception>
        [HttpGet("role")]
        public async Task<IActionResult> GetRole([FromQuery] string roleName, [FromQuery] string clientId)
        {
            string accessToken = await GetTokenFromRequestOrThrow();

            return HttpResultMessage.FilteredResult(await roleService.GetRole(roleName, clientId, accessToken));
        }
    }
}

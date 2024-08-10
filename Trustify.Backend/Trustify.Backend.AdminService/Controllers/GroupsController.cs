using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
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
    [Route("groups")]
    [ApiController]
    [EnableCors("CorsPolicy")]
    [Authorize(Policy = TrustifyPolicy.Authenticated)]
    [Authorize(Policy = TrustifyPolicy.Restricted)]
    public class GroupsController(IMapper mapper, IGroupService groupService) : TrustifyAdminControllerBase
    {
        private readonly IMapper mapper = mapper;
        private readonly IGroupService groupService = groupService;

        /// <summary>
        /// Adds group of users to the realm. 
        /// </summary>
        /// <param name="wrapper">Group data</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddGroup([FromBody] GroupWrapper wrapper)
        {
            string accessToken = await GetTokenFromRequestOrThrow();

            var result = await groupService.AddGroup(mapper.Map<GroupDTO>(wrapper), accessToken);
            return HttpResultMessage.FilteredResult(result);
        }

        /// <summary>
        /// Add clients roles to the selected group.
        /// </summary>
        /// <param name="wrapper">Data about roles: name and identifier</param>
        /// <returns></returns>
        [HttpPut("roles")]
        public async Task<IActionResult> AddClientRolesToGroup([FromBody] GroupRolesWrapper wrapper)
        {
            string accessToken = await GetTokenFromRequestOrThrow();
            var roles = mapper.Map<IEnumerable<RoleWrapper>, IEnumerable<RoleDTO>>(wrapper.RoleWrappers);

            var result = await groupService.AddClientRolesToGroup(roles, wrapper.GroupId, wrapper.ClientId, accessToken);
            return HttpResultMessage.FilteredResult(result);
        }

        /// <summary>
        /// Delete roles from the group.
        /// </summary>
        /// <param name="wrapper">Data such as group identifider and roles</param>
        /// <returns></returns>
        [HttpPut("roles/delete")]
        public async Task<IActionResult> DeleteClientRolesFromGroups([FromBody] GroupRolesWrapper wrapper)
        {
            string accessToken = await GetTokenFromRequestOrThrow();
            var roles = mapper.Map<IEnumerable<RoleWrapper>, IEnumerable<RoleDTO>>(wrapper.RoleWrappers);

            var result = await groupService.DeleteClientRolesFromGroup(roles, wrapper.GroupId, wrapper.ClientId, accessToken);
            return HttpResultMessage.FilteredResult(result);
        }

        /// <summary>
        /// Deletes selected group from the realm.
        /// </summary>
        /// <param name="groupId">The unique group identifier</param>
        /// <returns></returns>
        [HttpPut("delete")]
        public async Task<IActionResult> DeleteGroup([FromQuery] string groupId)
        {
            string accessToken = await GetTokenFromRequestOrThrow();

            return HttpResultMessage.FilteredResult(await groupService.DeleteGroup(groupId, accessToken));
        }

        /// <summary>
        /// Returns all groups of the realm.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetGroups()
        {
            
            string accessToken = await GetTokenFromRequestOrThrow();

            return HttpResultMessage.FilteredResult(await groupService.GetAllGroups(accessToken));
        }

        /// <summary>
        /// Returns one selected group from the realm.
        /// </summary>
        /// <param name="groupId">Unique identifier of the group</param>
        /// <returns></returns>
        [HttpGet("group")]
        [SwaggerResponse(StatusCodes.Status200OK, "The result is returned.", typeof(ResultMessage<GroupDTO>))]
        public async Task<IActionResult> GetGroup([FromQuery] string groupId)
        {
            string accessToken = await GetTokenFromRequestOrThrow();

            return HttpResultMessage.FilteredResult(await groupService.GetGroup(groupId, accessToken));
        }
    }
}

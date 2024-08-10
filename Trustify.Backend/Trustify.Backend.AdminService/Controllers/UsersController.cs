using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Trustify.Backend.AdminService.AutoMapper;
using Trustify.Backend.AdminService.Keycloak.DTO;
using Trustify.Backend.AdminService.Keycloak.Models;
using Trustify.Backend.AdminService.Keycloak.Service;
using Trustify.Backend.AdminService.Models;
using Trustify.Backend.AdminService.Security;

namespace Trustify.Backend.AdminService.Controllers
{
    /// <summary>
    /// Manages users in Keycloak server database.
    /// </summary>
    [Route("users")]
    [ApiController]
    [Authorize(Policy = TrustifyPolicy.Authenticated)]
    public class UsersController(IMapper mapper, IUserService userService) : TrustifyAdminControllerBase
    {
        private readonly IMapper mapper = mapper;
        private readonly IUserService userService = userService;

        /// <summary>
        /// Registers new user.
        /// </summary>
        /// <param name="user">User's data</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] UserWrapper user)
        {
            string accessToken = await GetTokenFromRequestOrThrow();
            return HttpResultMessage.FilteredResult(await userService.RegisterUser(mapper.Map<UserDTO>(user), accessToken));
        }

        /// <summary>
        /// Adds new user in the group.
        /// </summary>
        /// <param name="wrapper">User and group identifiers</param>
        /// <returns></returns>
        [HttpPut("group/add")]
        public async Task<IActionResult> AddUserInGroup([FromBody] UserIdGroupWrapper wrapper)
        {
            string accessToken = await GetTokenFromRequestOrThrow();
            return HttpResultMessage.FilteredResult(await userService.AddUserInGroup(wrapper.UserId, wrapper.GroupId, accessToken));
        }

        /// <summary>
        /// Removes user from the group.
        /// </summary>
        /// <param name="wrapper">User and group identifiers</param>
        /// <returns></returns>
        [HttpPut("group/remove")]
        public async Task<IActionResult> RemoveUserInGroup([FromBody] UserIdGroupWrapper wrapper)
        {
            string accessToken = await GetTokenFromRequestOrThrow();
            return HttpResultMessage.FilteredResult(await userService.RemoveUserFromGroup(wrapper.UserId, wrapper.GroupId, accessToken));
        }

        [HttpPut]
        public Task<IActionResult> UpdateUser()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes user from the server.
        /// </summary>
        /// <param name="wrapper">User identifier.</param>
        /// <returns></returns>
        [HttpPut("delete")]
        public async Task<IActionResult> DeleteUser([FromBody] UserIdWrapper wrapper)
        {
            string accessToken = await GetTokenFromRequestOrThrow();
            return HttpResultMessage.FilteredResult(await userService.RemoveUser(wrapper.UserId, accessToken));
        }

        /// <summary>
        /// Returns information about one user.
        /// </summary>
        /// <param name="wrapper">User identifier</param>
        /// <returns></returns>
        [HttpPut("user-info")]
        [SwaggerResponse(StatusCodes.Status200OK, "The result is returned.", typeof(ResultMessage<UserDTO>))]
        public async Task<IActionResult> GetUserInfo([FromBody] UserIdWrapper wrapper)
        {

            string accessToken = await GetTokenFromRequestOrThrow();
            return HttpResultMessage.FilteredResult(await userService.GetUser(wrapper.UserId, accessToken));
        }

        /// <summary>
        /// Returns all users.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            string accessToken = await GetTokenFromRequestOrThrow();

            return HttpResultMessage.FilteredResult(await userService.GetAllUsers(accessToken));
        }

        /// <summary>
        /// Requires user to update their password upon login.
        /// </summary>
        /// <param name="wrapper">User's identifier.</param>
        /// <returns></returns>
        [HttpPost("password")]
        public async Task<IActionResult> UpdatePasword([FromBody] UserIdWrapper wrapper)
        {
            string accessToken = await GetTokenFromRequestOrThrow();

            await userService.ExecuteActionsEmail(
                wrapper.UserId, [RequiredActions.UpdatePassword], accessToken);
            return Ok();
        }

        /// <summary>
        /// Returns user groups
        /// </summary>
        /// <param name="wrapper"></param>
        /// <returns></returns>
        [HttpGet("groups")]
        public async Task<IActionResult> GetUserGroups([FromQuery]UserIdWrapper wrapper)
        {
            string accessToken = await GetTokenFromRequestOrThrow();

            return HttpResultMessage.FilteredResult( await userService.GetUserGroups(wrapper.UserId, accessToken));
        }
    }
}

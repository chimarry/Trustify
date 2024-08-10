using AutoMapper;
using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Options;
using System.Text.RegularExpressions;
using Trustify.Backend.AdminService.Exceptions;
using Trustify.Backend.AdminService.Keycloak.DTO;
using Trustify.Backend.AdminService.Keycloak.Models;

namespace Trustify.Backend.AdminService.Keycloak.Service
{
    public class KeycloakUserService(IOptions<KeycloakOptions> keycloakOptions, IHttpService httpService, ILogger<KeycloakRoleService> logger, IExceptionHandler exceptionHandler) : IUserService
    {
        public KeycloakOptions KeycloakOptions { get; set; } = keycloakOptions.Value;

        private readonly IHttpService httpService = httpService;
        private readonly ILogger<KeycloakRoleService> logger = logger;
        private readonly IExceptionHandler exceptionHandler = exceptionHandler;

        public async Task<ResultMessage<IEnumerable<UserDTO>>> GetAllUsers(string accessToken)
        {
            try
            {
                var list = await httpService.GetAdminBaseUrl(accessToken)
                                           .AppendPathSegment($"/users")
                                           .GetJsonAsync<IEnumerable<UserDTO>>();
                return new ResultMessage<IEnumerable<UserDTO>>(list);
            }
            catch (Exception ex)
            {
                (OperationStatus status, string message) = exceptionHandler.HandleException(ex);
                return new ResultMessage<IEnumerable<UserDTO>>(status, message);
            }
        }

        public async Task<ResultMessage<bool>> AddUserInGroup(string userId, string groupId, string accessToken)
        {
            try
            {
                IFlurlResponse? response = await httpService.GetAdminBaseUrl(accessToken)
                     .AppendPathSegment($"/users/{userId}/groups/{groupId}")
                     .PutJsonAsync(new
                     {
                         Realm = KeycloakOptions.Realm,
                         UserId = userId,
                         GroupId = groupId
                     });
                return new ResultMessage<bool>(true, OperationStatus.Success);
            }
            catch (Exception ex)
            {
                (OperationStatus status, string message) = exceptionHandler.HandleException(ex);
                return new ResultMessage<bool>(status, message);
            }
        }

        public async Task<ResultMessage<bool>> RemoveUserFromGroup(string userId, string groupId, string accessToken)
        {
            IFlurlResponse? response = null;
            try
            {
                response = await httpService.GetAdminBaseUrl(accessToken)
                   .AppendPathSegment($"/users/{userId}/groups/{groupId}")
                   .DeleteAsync();
                return new ResultMessage<bool>(true, OperationStatus.Success);
            }
            catch (Exception ex)
            {
                (OperationStatus status, string message) = exceptionHandler.HandleException(ex);
                return new ResultMessage<bool>(status, message);
            }
        }

        public async Task<ResultMessage<IEnumerable<GroupDTO>>> GetUserGroups(string userId, string accessToken)
        {
            IFlurlResponse? response = null;
            try
            {
                var list = await httpService.GetAdminBaseUrl(accessToken)
                .AppendPathSegment($"/users/{userId}/groups")
                .GetJsonAsync<IEnumerable<GroupDTO>>();
                return new ResultMessage<IEnumerable<GroupDTO>>(list);
            }
            catch (Exception ex)
            {
                (OperationStatus status, string message) = exceptionHandler.HandleException(ex);
                return new ResultMessage<IEnumerable<GroupDTO>>(status, message);
            }
        }

        public Task<IEnumerable<RoleDTO>> GetUserRoles(string userId, string accessToken)
        {
            throw new NotImplementedException();
        }

        public async Task<ResultMessage<bool>> RemoveUser(string userId, string accessToken)
        {
            IFlurlResponse? response = null;
            try
            {
                response = await httpService.GetAdminBaseUrl(accessToken)
                                            .AppendPathSegment($"/users/{userId}")
                                            .DeleteAsync();
                return new ResultMessage<bool>(true, OperationStatus.Success);
            }
            catch (Exception ex)
            {
                (OperationStatus status, string message) = exceptionHandler.HandleException(ex);
                return new ResultMessage<bool>(status, message);
            }
        }

        public async Task<ResultMessage<bool>> RegisterUser(UserDTO user, string accessToken)
        {
            IFlurlResponse? response = null;
            try
            {
                response = await httpService.GetAdminBaseUrl(accessToken)
                                .AppendPathSegment($"/users")
                                .PostJsonAsync(user);
                return new ResultMessage<bool>(true);
            }
            catch (Exception ex)
            {
                (OperationStatus status, string message) = exceptionHandler.HandleException(ex);
                return new ResultMessage<bool>(status, message);
            }
        }

        public async Task<ResultMessage<UserDTO>> GetUser(string userId, string accessToken)
        {

            IFlurlResponse? response = null;
            try
            {
                UserDTO user = await httpService.GetAdminBaseUrl(accessToken)
                                           .AppendPathSegment($"/users/{userId}")
                                           .GetJsonAsync<UserDTO>();
                user.Groups = (await GetUserGroups(userId, accessToken)).Result?.Select(x => x.Name ?? string.Empty);
                var roleMappings = await httpService.GetAdminBaseUrl(accessToken)
                    .AppendPathSegment($"/users/{userId}/role-mappings")
                    .GetJsonAsync<RoleMappingsDTO>();
                user.RealmRoles = roleMappings.RealmRoles?.Select(x => x.Name ?? string.Empty);
                if (roleMappings != null && roleMappings.ClientRoles != null)
                {
                    user.ClientRoles = new Dictionary<string, string[]>();
                    foreach (var item in roleMappings.ClientRoles)
                    {
                        string[] roles = item.Value.Mappings.Select(x => x.Name).ToArray();
                        user.ClientRoles.Add(item.Key, roles);
                    }
                }
                return new ResultMessage<UserDTO>(user);
            }
            catch (Exception ex)
            {
                (OperationStatus status, string message) = exceptionHandler.HandleException(ex);
                return new ResultMessage<UserDTO>(status, message);
            }
        }

        public async Task ExecuteActionsEmail(string userId, IEnumerable<string> requiredActions, string accessToken)
        {
            IFlurlResponse response = null;
            try
            {
                // If client_id and redirectUrl are set, use them to redirect user to a specific page after he has updated his profile/password
                response = await httpService.GetAdminBaseUrl(accessToken)
                      .AppendPathSegment($"/users/{userId}/execute-actions-email")
                      // .SetQueryParam("redirect_uri", "https://localhost:44347/api/v1.0/test/super-admin", NullValueHandling.Remove)
                      .SetQueryParam("client_id", "trustify_admin", NullValueHandling.Remove)
                      .SetQueryParam("lifespan", 1000)
                      .PutJsonAsync(requiredActions);
            }
            catch (Exception)
            {
                string? content = await response?.ResponseMessage?.Content?.ReadAsStringAsync();
                throw;
            }
        }
    }
}

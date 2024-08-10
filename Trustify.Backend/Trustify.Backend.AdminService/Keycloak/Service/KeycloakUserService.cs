using AutoMapper;
using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Options;
using System.Text.RegularExpressions;
using Trustify.Backend.AdminService.Keycloak.DTO;
using Trustify.Backend.AdminService.Keycloak.Models;

namespace Trustify.Backend.AdminService.Keycloak.Service
{
    public class KeycloakUserService(IOptions<KeycloakOptions> keycloakOptions, IMapper mapper, IHttpService httpService, ILogger<KeycloakRoleService> logger) : IUserService
    {
        public KeycloakOptions KeycloakOptions { get; set; } = keycloakOptions.Value;

        private readonly IHttpService httpService = httpService;
        private readonly ILogger<KeycloakRoleService> logger = logger;
        private readonly IMapper mapper = mapper;

        public async Task<IEnumerable<UserDTO>> GetAllUsers(string accessToken)
        {
            try
            {
                return await httpService.GetAdminBaseUrl(accessToken)
                                           .AppendPathSegment($"/users")
                                           .GetJsonAsync<IEnumerable<UserDTO>>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> AddUserInGroup(string userId, string groupId, string accessToken)
        {
            IFlurlResponse? response = null;
            try
            {
                response = await httpService.GetAdminBaseUrl(accessToken)
                     .AppendPathSegment($"/users/{userId}/groups/{groupId}")
                     .PutJsonAsync(new
                     {
                         Realm = KeycloakOptions.Realm,
                         UserId = userId,
                         GroupId = groupId
                     });
                return response.StatusCode is 200 or 201 or 204;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> RemoveUserFromGroup(string userId, string groupId, string accessToken)
        {
            IFlurlResponse? response = null;
            try
            {
                response = await httpService.GetAdminBaseUrl(accessToken)
                   .AppendPathSegment($"/users/{userId}/groups/{groupId}")
                   .DeleteAsync();
                return response.StatusCode is 200 or 201 or 204;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<GroupDTO>> GetUserGroups(string userId, string accessToken)
        {
            IFlurlResponse? response = null;
            try
            {
                return await httpService.GetAdminBaseUrl(accessToken)
                .AppendPathSegment($"/users/{userId}/groups")
                .GetJsonAsync<IEnumerable<GroupDTO>>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<IEnumerable<RoleDTO>> GetUserRoles(string userId, string accessToken)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RemoveUser(string userId, string accessToken)
        {
            IFlurlResponse? response = null;
            try
            {
                response = await httpService.GetAdminBaseUrl(accessToken)
                                            .AppendPathSegment($"/users/{userId}")
                                            .DeleteAsync();
                return response.StatusCode is 200 or 201 or 204;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> RegisterUser(UserDTO user, string accessToken)
        {
            IFlurlResponse? response = null;
            try
            {
                response = await httpService.GetAdminBaseUrl(accessToken)
                                .AppendPathSegment($"/users")
                                .PostJsonAsync(user);
                return response.StatusCode is 200 or 201 or 204;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<UserDTO> GetUser(string userId, string accessToken)
        {

            IFlurlResponse? response = null;
            try
            {
                UserDTO user = await httpService.GetAdminBaseUrl(accessToken)
                                           .AppendPathSegment($"/users/{userId}")
                                           .GetJsonAsync<UserDTO>();
                user.Groups = (await GetUserGroups(userId, accessToken)).Select(x => x.Name ?? string.Empty);
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
                return user;
            }
            catch (Exception)
            {
                throw;
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

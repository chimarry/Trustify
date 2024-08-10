using Flurl.Http;
using Flurl.Http.Configuration;
using Microsoft.Extensions.Options;
using Trustify.Backend.AdminService.Keycloak.DTO;
using Trustify.Backend.AdminService.Keycloak.Models;

namespace Trustify.Backend.AdminService.Keycloak.Service
{
    public class KeycloakGroupService : IGroupService
    {
        public KeycloakOptions KeycloakOptions { get; set; }

        private readonly IHttpService httpService;
        private readonly ILogger<KeycloakGroupService> logger;

        public KeycloakGroupService(IOptions<KeycloakOptions> keycloakOptions, IHttpService httpService,
            ILogger<KeycloakGroupService> logger)
        {
            KeycloakOptions = keycloakOptions.Value;
            this.httpService = httpService;
            this.logger = logger;
        }

        /// <summary>
        /// Adds group of users to the realm. 
        /// </summary>
        /// <param name="group">Group data</param>
        /// <param name="token">Access token of logged in user</param>
        /// <returns></returns>
        public async Task<bool> AddGroup(GroupDTO group, string token)
        {
            IFlurlResponse? response = null;
            try
            {
                response = await httpService.GetAdminBaseUrl(token)
                                            .AppendPathSegment("/groups")
                                            .WithOAuthBearerToken(token)
                                            .PostJsonAsync(group);

                return true;

            }
            catch (Exception)
            {
                if (response != null)
                {
                    string message = await response.ResponseMessage.Content.ReadAsStringAsync();
                    logger.LogError("Error message is {message}", message);
                }
                return false;
            }
        }

        /// <summary>
        /// Deletes selected group from the realm.
        /// </summary>
        /// <param name="groupId">The unique group identifier</param>
        /// <param name="token">The access token of the logged in user</param>
        /// <returns></returns>
        public async Task<bool> DeleteGroup(string groupId, string token)
        {
            try
            {
                IFlurlResponse response = await httpService.GetAdminBaseUrl(token)
                                                        .AppendPathSegment("/groups")
                                                        .AppendPathSegment(groupId)
                                                        .WithOAuthBearerToken(token)
                                                        .DeleteAsync();
                return response.StatusCode is 200 or 201 or 204;

            }
            catch (Exception ex)
            {
                logger.LogError("Error message is {message}", ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Add clients roles to the selected group.
        /// </summary>
        /// <param name="roles">Data about roles: name and identifier</param>
        /// <param name="groupId">Unique group identifier</param>
        /// <param name="clientId">Unique identifier of the client</param>
        /// <param name="token">Access token</param>
        /// <returns></returns>
        public async Task<bool> AddClientRolesToGroup(IEnumerable<RoleDTO> roles, string groupId, string clientId, string token)
        {
            string pathSegment = $"groups/{groupId}/role-mappings/clients/{clientId}";
            IFlurlResponse? response = null;
            try
            {
                response = await httpService.GetAdminBaseUrl(token)
                                            .AppendPathSegment(pathSegment)
                                            .WithOAuthBearerToken(token)
                                            .PostJsonAsync(roles);

                return true;

            }
            catch (Exception)
            {
                if (response != null)
                {
                    string message = await response.ResponseMessage.Content.ReadAsStringAsync();
                    logger.LogError("Error message is {message}", message);
                }
                return false;
            }
        }

        /// <summary>
        /// Delete roles from the group.
        /// </summary>
        /// <param name="roles">Role's data array</param>
        /// <param name="groupId">Unique identifier of the group</param>
        /// <param name="clientId">Unique identifier of the client</param>
        /// <param name="token">Access token</param>
        /// <returns></returns>
        public async Task<bool> DeleteClientRolesFromGroup(IEnumerable<RoleDTO> roles, string groupId, string clientId, string token)
        {
            string pathSegment = $"{KeycloakOptions.AdminUrl}/trustify/groups/{groupId}/role-mappings/clients/{clientId}";
            IFlurlResponse? response = null;
            try
            {
                await httpService.SendRequest(pathSegment, roles, token, HttpMethod.Delete);

                return true;

            }
            catch (Exception)
            {
                if (response != null)
                {
                    string message = await response.ResponseMessage.Content.ReadAsStringAsync();
                    logger.LogError("Error message is {message}", message);
                }
                return false;
            }
        }

        /// <summary>
        /// Returns all groups of the realm.
        /// </summary>
        /// <param name="token">Access token</param>
        /// <returns></returns>
        public async Task<IEnumerable<GroupDTO>?> GetAllGroups(string token)
        {
            try
            {
            //    HttpClientHandler httpClientHandler = new HttpClientHandler();
            //    httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain,
            //      errors) => true;
            //    HttpClient httpClient = new HttpClient(httpClientHandler);
            //    var flurlClient = new FlurlClient(httpClient);

            //    return await flurlClient.Request($"{KeycloakOptions.AdminUrl}/{KeycloakOptions.Realm}")
            //                    .WithOAuthBearerToken(token)
                                       return await httpService.GetAdminBaseUrl(token)
                                        .AppendPathSegment("groups")
                                        .GetJsonAsync<IEnumerable<GroupDTO>>();

            }
            catch (Exception ex)
            {
                logger.LogError("Error message is {message}", ex.Message);
                throw ex;
            }
        }

        /// <summary>
        /// Returns one selected group from the realm.
        /// </summary>
        /// <param name="groupId">Unique identifier of the group</param>
        /// <param name="token">Access token</param>
        /// <returns></returns>
        public async Task<GroupDTO?> GetGroup(string groupId, string token)
        {
            try
            {
                return await httpService.GetAdminBaseUrl(token)
                                       .AppendPathSegment("groups")
                                       .AppendPathSegment(groupId)
                                       .WithOAuthBearerToken(token)
                                       .GetJsonAsync<GroupDTO>();

            }
            catch (Exception ex)
            {
                logger.LogError("Error message is {message}", ex.Message);
                return null;
            }
        }
    }
}

using Flurl.Http;
using Flurl.Http.Configuration;
using Microsoft.Extensions.Options;
using Trustify.Backend.AdminService.Exceptions;
using Trustify.Backend.AdminService.Keycloak.DTO;
using Trustify.Backend.AdminService.Keycloak.Models;

namespace Trustify.Backend.AdminService.Keycloak.Service
{
    public class KeycloakGroupService(IOptions<KeycloakOptions> keycloakOptions, IHttpService httpService,
        ILogger<KeycloakGroupService> logger, IExceptionHandler exceptionHandler) : IGroupService
    {
        public KeycloakOptions KeycloakOptions { get; set; } = keycloakOptions.Value;

        private readonly IHttpService httpService = httpService;
        private readonly ILogger<KeycloakGroupService> logger = logger;
        private readonly IExceptionHandler exceptionHandler = exceptionHandler;

        /// <summary>
        /// Adds group of users to the realm. 
        /// </summary>
        /// <param name="group">Group data</param>
        /// <param name="token">Access token of logged in user</param>
        /// <returns></returns>
        public async Task<ResultMessage<bool>> AddGroup(GroupDTO group, string token)
        {
            IFlurlResponse? response = null;
            try
            {
                response = await httpService.GetAdminBaseUrl(token)
                                            .AppendPathSegment("/groups")
                                            .WithOAuthBearerToken(token)
                                            .PostJsonAsync(group);

                return new ResultMessage<bool>(true);

            }
            catch (Exception ex)
            {
                (OperationStatus status, string message) = exceptionHandler.HandleException(ex);
                return new ResultMessage<bool>(status, message);
            }
        }

        /// <summary>
        /// Deletes selected group from the realm.
        /// </summary>
        /// <param name="groupId">The unique group identifier</param>
        /// <param name="token">The access token of the logged in user</param>
        /// <returns></returns>
        public async Task<ResultMessage<bool>> DeleteGroup(string groupId, string token)
        {
            try
            {
                IFlurlResponse response = await httpService.GetAdminBaseUrl(token)
                                                        .AppendPathSegment("/groups")
                                                        .AppendPathSegment(groupId)
                                                        .WithOAuthBearerToken(token)
                                                        .DeleteAsync();

                return new ResultMessage<bool>(true, OperationStatus.Success);
            }
            catch (Exception ex)
            {
                (OperationStatus status, string message) = exceptionHandler.HandleException(ex);
                return new ResultMessage<bool>(status, message);
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
        public async Task<ResultMessage<bool>> AddClientRolesToGroup(IEnumerable<RoleDTO> roles, string groupId, string clientId, string token)
        {
            string pathSegment = $"groups/{groupId}/role-mappings/clients/{clientId}";
            IFlurlResponse? response = null;
            try
            {
                response = await httpService.GetAdminBaseUrl(token)
                                            .AppendPathSegment(pathSegment)
                                            .WithOAuthBearerToken(token)
                                            .PostJsonAsync(roles);

                return new ResultMessage<bool>(true, OperationStatus.Success);

            }
            catch (Exception ex)
            {
                (OperationStatus status, string message) = exceptionHandler.HandleException(ex);
                return new ResultMessage<bool>(status, message);
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
        public async Task<ResultMessage<bool>> DeleteClientRolesFromGroup(IEnumerable<RoleDTO> roles, string groupId, string clientId, string token)
        {
            string pathSegment = $"{KeycloakOptions.AdminUrl}/trustify/groups/{groupId}/role-mappings/clients/{clientId}";
            IFlurlResponse? response = null;
            try
            {
                await httpService.SendRequest(pathSegment, roles, token, HttpMethod.Delete);

                return new ResultMessage<bool>(true, OperationStatus.Success);

            }
            catch (Exception ex)
            {
                (OperationStatus status, string message) = exceptionHandler.HandleException(ex);
                return new ResultMessage<bool>(status, message);
            }
        }

        /// <summary>
        /// Returns all groups of the realm.
        /// </summary>
        /// <param name="token">Access token</param>
        /// <returns></returns>
        public async Task<ResultMessage<IEnumerable<GroupDTO>>> GetAllGroups(string token)
        {
            try
            {

                var list = await httpService.GetAdminBaseUrl(token)
                    .AppendPathSegment("groups")
                    .GetJsonAsync<IEnumerable<GroupDTO>>();
                return new ResultMessage<IEnumerable<GroupDTO>>(list);

            }
            catch (Exception ex)
            {
                (OperationStatus status, string message) = exceptionHandler.HandleException(ex);
                return new ResultMessage<IEnumerable<GroupDTO>>(status, message);
            }
        }

        /// <summary>
        /// Returns one selected group from the realm.
        /// </summary>
        /// <param name="groupId">Unique identifier of the group</param>
        /// <param name="token">Access token</param>
        /// <returns></returns>
        public async Task<ResultMessage<GroupDTO>> GetGroup(string groupId, string token)
        {
            try
            {
                var group = await httpService.GetAdminBaseUrl(token)
                                       .AppendPathSegment("groups")
                                       .AppendPathSegment(groupId)
                                       .WithOAuthBearerToken(token)
                                       .GetJsonAsync<GroupDTO>();
                return new ResultMessage<GroupDTO>(group);

            }
            catch (Exception ex)
            {
                (OperationStatus status, string message) = exceptionHandler.HandleException(ex);
                return new ResultMessage<GroupDTO>(status, message);
            }
        }
    }
}

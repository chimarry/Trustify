using Flurl.Http;
using Microsoft.Extensions.Options;
using Trustify.Backend.AdminService.Keycloak.DTO;
using Trustify.Backend.AdminService.Keycloak.Models;

namespace Trustify.Backend.AdminService.Keycloak.Service
{
    public class KeycloakRoleService : IRoleService
    {
        public KeycloakOptions KeycloakOptions { get; set; }

        private readonly IHttpService httpService;
        private readonly ILogger<KeycloakRoleService> logger;

        public KeycloakRoleService(IOptions<KeycloakOptions> keycloakOptions,
            IHttpService httpService, ILogger<KeycloakRoleService> logger)
        {
            this.KeycloakOptions = keycloakOptions.Value;
            this.httpService = httpService;
            this.logger = logger;
        }

        public async Task<bool> AddRole(RoleDTO role, string clientId, string token)
        {
            IFlurlResponse? response = null;
            try
            {
                response = await httpService.GetAdminBaseUrl(token)
                                            .AppendPathSegment(httpService.GetPathSegment(KeycloakOptions.RolesUrlFormat, clientId))
                                            .WithOAuthBearerToken(token)
                                            .PostJsonAsync(role);
                return response.StatusCode is 200 or 201;
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

        public async Task<bool> DeleteRole(string roleName, string clientId, string token)
        {
            try
            {
                IFlurlResponse response = await httpService.GetAdminBaseUrl(token)
                                                        .AppendPathSegment(httpService.GetPathSegment(KeycloakOptions.RolesUrlFormat, clientId))
                                                        .AppendPathSegment(roleName)
                                                        .WithOAuthBearerToken(token)
                                                        .DeleteAsync();
                return response.StatusCode is 200 or 201;

            }
            catch (Exception ex)
            {
                logger.LogError("Error message is {message}", ex.Message);
                return false;
            }
        }

        public async Task<RoleDTO?> GetRole(string roleName, string clientId, string token)
        {
            try
            {
                return await httpService.GetAdminBaseUrl(token)
                                        .AppendPathSegment(httpService.GetPathSegment(KeycloakOptions.RolesUrlFormat, clientId))
                                        .AppendPathSegment(roleName)
                                        .WithOAuthBearerToken(token)
                                        .GetJsonAsync<RoleDTO>();

            }
            catch (Exception ex)
            {
                logger.LogError("Error message is {message}", ex.Message);
                return null;
            }
        }

        public async Task<IEnumerable<RoleDTO>?> GetRoles(string clientId, string token)
        {
            try
            {
                return await httpService.GetAdminBaseUrl(token)
                                        .AppendPathSegment(httpService.GetPathSegment(KeycloakOptions.RolesUrlFormat, clientId))
                                        .WithOAuthBearerToken(token)
                                        .GetJsonAsync<IEnumerable<RoleDTO>>();

            }
            catch (Exception ex)
            {
                logger.LogError("Error message is {message}", ex.Message);
                return null;
            }
        }
    }
}

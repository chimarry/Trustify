using Flurl.Http;
using Microsoft.Extensions.Options;
using Trustify.Backend.AdminService.Exceptions;
using Trustify.Backend.AdminService.Keycloak.DTO;
using Trustify.Backend.AdminService.Keycloak.Models;

namespace Trustify.Backend.AdminService.Keycloak.Service
{
    public class KeycloakRoleService(IOptions<KeycloakOptions> keycloakOptions,
        IHttpService httpService, ILogger<KeycloakRoleService> logger, IExceptionHandler exceptionHandler) : IRoleService
    {
        public KeycloakOptions KeycloakOptions { get; set; } = keycloakOptions.Value;

        private readonly IHttpService httpService = httpService;
        private readonly ILogger<KeycloakRoleService> logger = logger;
        private readonly IExceptionHandler exceptionHandler = exceptionHandler;

        public async Task<ResultMessage<bool>> AddRole(RoleDTO role, string clientId, string token)
        {
            IFlurlResponse? response = null;
            try
            {
                response = await httpService.GetAdminBaseUrl(token)
                                            .AppendPathSegment(httpService.GetPathSegment(KeycloakOptions.RolesUrlFormat, clientId))
                                            .WithOAuthBearerToken(token)
                                            .PostJsonAsync(role);
                return new ResultMessage<bool>(true, OperationStatus.Success);
            }
            catch (Exception ex)
            {
                (OperationStatus status, string message) = exceptionHandler.HandleException(ex);
                return new ResultMessage<bool>(status, message);
            }
        }

        public async Task<ResultMessage<bool>> DeleteRole(string roleName, string clientId, string token)
        {
            try
            {
                IFlurlResponse response = await httpService.GetAdminBaseUrl(token)
                                                        .AppendPathSegment(httpService.GetPathSegment(KeycloakOptions.RolesUrlFormat, clientId))
                                                        .AppendPathSegment(roleName)
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

        public async Task<ResultMessage<RoleDTO>> GetRole(string roleName, string clientId, string token)
        {
            try
            {
                var role = await httpService.GetAdminBaseUrl(token)
                                         .AppendPathSegment(httpService.GetPathSegment(KeycloakOptions.RolesUrlFormat, clientId))
                                         .AppendPathSegment(roleName)
                                         .WithOAuthBearerToken(token)
                                         .GetJsonAsync<RoleDTO>();
                return new ResultMessage<RoleDTO>(role);

            }
            catch (Exception ex)
            {
                (OperationStatus status, string message) = exceptionHandler.HandleException(ex);
                return new ResultMessage<RoleDTO>(status, message);
            }
        }

        public async Task<ResultMessage<IEnumerable<RoleDTO>>> GetRoles(string clientId, string token)
        {
            try
            {
                var list = await httpService.GetAdminBaseUrl(token)
                                        .AppendPathSegment(httpService.GetPathSegment(KeycloakOptions.RolesUrlFormat, clientId))
                                        .WithOAuthBearerToken(token)
                                        .GetJsonAsync<IEnumerable<RoleDTO>>();
                return new ResultMessage<IEnumerable<RoleDTO>>(list);

            }
            catch (Exception ex)
            {
                (OperationStatus status, string message) = exceptionHandler.HandleException(ex);
                return new ResultMessage<IEnumerable<RoleDTO>>(status, message);
            }
        }
    }
}

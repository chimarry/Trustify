using Flurl.Http;
using Trustify.Backend.AdminService.Keycloak.DTO;

namespace Trustify.Backend.AdminService.Keycloak.Service
{
    public class KeycloakClientService(IHttpService httpService, ILogger<KeycloakClientService> logger) : IClientService
    {
        private readonly IHttpService httpService = httpService;
        private readonly ILogger<KeycloakClientService> logger = logger;

        /// <summary>
        /// Returns basic information about clients.
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ClientDTO>> GetClients(string accessToken)
        {
            return await httpService.GetAdminBaseUrl(accessToken)
                              .AppendPathSegment("/clients")
                              .GetJsonAsync<IEnumerable<ClientDTO>>();
        }
    }
}

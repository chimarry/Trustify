using Flurl.Http;
using Trustify.Backend.AdminService.Exceptions;
using Trustify.Backend.AdminService.Keycloak.DTO;

namespace Trustify.Backend.AdminService.Keycloak.Service
{
    public class KeycloakClientService(IHttpService httpService, ILogger<KeycloakClientService> logger, IExceptionHandler exceptionHandler) : IClientService
    {
        private readonly IHttpService httpService = httpService;
        private readonly ILogger<KeycloakClientService> logger = logger;
        private readonly IExceptionHandler exceptionHandler = exceptionHandler;

        /// <summary>
        /// Returns basic information about clients.
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public async Task<ResultMessage<IEnumerable<ClientDTO>>> GetClients(string accessToken)
        {
            try
            {

                var result = await httpService.GetAdminBaseUrl(accessToken)
                                  .AppendPathSegment("/clients")
                                  .GetJsonAsync<IEnumerable<ClientDTO>>();
                return new ResultMessage<IEnumerable<ClientDTO>>(result);
            }
            catch (Exception ex)
            {
                (OperationStatus status, string message) = exceptionHandler.HandleException(ex);
                return new ResultMessage<IEnumerable<ClientDTO>>(status, message);
            }
        }
    }
}

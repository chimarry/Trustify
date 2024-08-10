using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Trustify.Backend.AdminService.Keycloak.DTO;
using Trustify.Backend.AdminService.Keycloak.Service;
using Trustify.Backend.AdminService.Security;

namespace Trustify.Backend.AdminService.Controllers
{
    [Route("clients")]
    [ApiController]
    [EnableCors("CorsPolicy")]
    [Authorize(Policy = TrustifyPolicy.Authenticated)]
    [Authorize(Policy = TrustifyPolicy.Restricted)]
    public class ClientsController(IClientService clientService) : TrustifyAdminControllerBase
    {
        private readonly IClientService clientService = clientService;

        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, "The result is returned.", typeof(ClientDTO))]
        public async Task<IActionResult> GetClientsInfo()
        {
            string accessToken = await GetTokenFromRequestOrThrow();

            return Ok(await clientService.GetClients(accessToken));
        }
    }
}

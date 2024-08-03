using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Trustify.Backend.AdminService.Exceptions;

namespace Trustify.Backend.AdminService.Controllers
{
    public class TrustifyAdminControllerBase : ControllerBase
    {
        protected async Task<string?> GetTokenFromRequest()
        {
            var authResult = await HttpContext.AuthenticateAsync(OpenIdConnectDefaults.AuthenticationScheme);
            if (authResult?.Succeeded != true)
            {
                throw new UnauthorizedException();
            }

            // Get the access token and refresh token
            return authResult?.Properties?.GetTokenValue("access_token");
        }
    }
}

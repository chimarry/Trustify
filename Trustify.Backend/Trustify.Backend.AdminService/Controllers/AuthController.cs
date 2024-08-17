using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Trustify.Backend.AdminService.Exceptions;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Trustify.Backend.AdminService.Models;
using Microsoft.Extensions.Options;

namespace Trustify.Backend.AdminService.Controllers
{
    [Route("auth")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController(IOptions<FrontendConfig> options) : TrustifyAdminControllerBase
    {
        private readonly FrontendConfig frontendConfig = options.Value;

        /// <summary>
        /// Logins user to the server.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="UnauthorizedException"></exception>
        [HttpGet("login")]
        public async Task Login([FromQuery] string returnUrl)
        {
            await this.HttpContext.ChallengeAsync(OpenIdConnectDefaults.AuthenticationScheme,
                new AuthenticationProperties() { RedirectUri = frontendConfig.RedirectUrl });
        }

        [HttpPost]
        [Route("log-out")]
        [Authorize]

        public async Task Logout(string? returnUrl)
        {
            returnUrl ??= "/api/v1.0/auth/login";
            string newReturnUrl = QueryHelpers.AddQueryString(returnUrl, frontendConfig.RedirectQueryParam, frontendConfig.RedirectUrl);
            await this.HttpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme, new AuthenticationProperties
            {
                RedirectUri = newReturnUrl
            });
            await this.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        /// <summary>
        /// Check if user is logged in.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult IsLoggedIn()
        {
            return Ok(HttpContext.User?.Identity?.IsAuthenticated);
        }

        [HttpGet("access-denied")]
        public IActionResult AccessDenied(string? returnUrl)
        {
            throw new ForbiddenAccessException();
        }
    }
}

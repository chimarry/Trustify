using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using ProductManager.WebAPI.Security;
using System.Security.Claims;
using System.Text.Encodings.Web;
using Trustify.Backend.ApiGateway.Exceptions;
using Trustify.Backend.ApiGateway.Models;

namespace Trustify.Backend.ApiGateway.Security
{
    /// <summary>
    /// Handles authentication with API key.
    /// </summary>
    public class ApiKeyAuthenticationHandler : AuthenticationHandler<ApiKeyAuthenticationOptions>
    {
        private readonly ApiSettings options;

        [Obsolete("Use of ISystemClock")]
        public ApiKeyAuthenticationHandler(IOptionsMonitor<ApiKeyAuthenticationOptions> options,
            ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, IOptions<ApiSettings> apiSettings)
            : base(options, logger, encoder, clock) => this.options = apiSettings.Value;

        /// <summary>
        /// Based on provided HTTP header parameter <see cref="HttpConstants.ApiKey"/> api key, 
        /// returns AuthenticationTicket. If API key is invalid, <see cref="UnauthorizedException"/> is thrown.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="UnauthorizedException"></exception>
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            string apiKey = string.Empty;
            if (Request == null || Request.Headers == null || !Request.Headers.ContainsKey(HttpConstants.ApiKey))
                throw new UnauthorizedException();

            if (Request.Headers != null)
                apiKey = Request.Headers[HttpConstants.ApiKey].ToString();

            if (apiKey != options.ApiKey)
                throw new UnauthorizedException();

            // API key is not related to any user
            Claim[] claims =
            [
                 new Claim(ClaimTypes.Anonymous, apiKey)
            ];

            var claimsIdentity = new ClaimsIdentity(claims, Schemes.ApiKeyScheme);
            var ticket = new AuthenticationTicket(new ClaimsPrincipal(claimsIdentity),
                new AuthenticationProperties(),
               Schemes.ApiKeyScheme);
            return AuthenticateResult.Success(ticket);
        }
    }
}

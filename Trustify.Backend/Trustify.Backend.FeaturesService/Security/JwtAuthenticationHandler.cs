using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace ProductManager.WebAPI.Security
{
    /// <summary>
    /// Handles authentication of an user with JWT token.
    /// </summary>
    public class JwtAuthenticationHandler : AuthenticationHandler<JwtAuthenticationOptions>
    {
        public JwtAuthenticationHandler(IOptionsMonitor<JwtAuthenticationOptions> options,
            ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
        }

        /// <summary>
        /// User must provide valid JWT token to confirm they identity. If JWT token is not valid, 
        /// <see cref="UnauthorizedException"/> is thrown and user is not authenticated. 
        /// Is JWT token is valid, authentication ticket is returned with certain claims, such as JWT token and 
        /// username. Expired token is invalid token.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="UnauthorizedException"></exception>
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            // check token
            // create claims and identity
            //try
            //{
            //    string encodedAccessToken = Request.GetAuthorizationHeader();

            //    Claim[] claims = new[]
            //    {
            //     new Claim(ClaimTypes.Name, parsedToken.Claims.First(x => x.Type==HttpConstants.ClaimName).Value),
            //     new Claim(ClaimTypes.GivenName, parsedToken.Claims.First(x=>x.Type == HttpConstants.ClaimGivenName).Value),
            //     new Claim(ClaimTypes.NameIdentifier, parsedToken.Claims.First(x=>x.Type==HttpConstants.ClaimNameIdentifier).Value)
            //};

            //    var claimsIdentity = new ClaimsIdentity(claims, Schemes.JwtScheme);
            //    var ticket = new AuthenticationTicket(new ClaimsPrincipal(claimsIdentity),
            //          new AuthenticationProperties(),
            //          Schemes.JwtScheme);
            //    return AuthenticateResult.Success(ticket);
            //}
            //catch (SecurityTokenExpiredException)
            //{
            //    throw new UnauthorizedException();
            //}
        }
    }
}

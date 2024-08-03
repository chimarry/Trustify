using Flurl.Http;
using Microsoft.Extensions.Options;

namespace Trustify.Backend.AdminService.Keycloak.Service
{
    public static class KeycloakServiceUtil
    {
        public static IFlurlRequest GetBaseUrl(string url, string bearerToken) =>
          new Flurl.Url(url)
         .AppendPathSegment("/trustify")
         .WithSettings(x => x.JsonSerializer = new Flurl.Http.Configuration.DefaultJsonSerializer())
         .WithOAuthBearerToken(bearerToken);
        //  .WithAuthentication(null, options.Url, authenticationRealm, options.Username, options.Password);

        private static string GetBearerToken()
        {
            return string.Empty;
        }
    }
}

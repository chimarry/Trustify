using Trustify.Backend.FeaturesService.Exceptions;
using Trustify.Backend.FeaturesService.Models;

namespace Trustify.Backend.FeaturesService.Security
{
    public static class SecurityExtensionMethods
    {
        public static string GetAuthorizationHeader(this HttpRequest request)
        {
            if (!request.Headers.ContainsKey(HttpConstants.AuthorizationHeader))
                throw new UnauthorizedException();

            string encodedAccessToken = request.Headers[HttpConstants.AuthorizationHeader];

            if (string.IsNullOrEmpty(encodedAccessToken))
                throw new UnauthorizedException();
            return encodedAccessToken;
        }

        public static string GetCookie(this HttpRequest request, string name)
        {
            if (request?.Cookies == null || request.Cookies.ContainsKey(name))
                throw new UnauthorizedException();

            string? cookie = request.Cookies[name];

            if (string.IsNullOrEmpty(cookie))
                throw new UnauthorizedException();
            return cookie;
        }

        public static void SetCookie(this HttpResponse response, string name, string value, bool useHTTPS)
        {
            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.UtcNow.AddDays(7),
            };
            if (useHTTPS)
            {
                cookieOptions.Secure = true;
                cookieOptions.HttpOnly = true;
            }

            response.Cookies.Append(name, value, cookieOptions);
        }
    }
}

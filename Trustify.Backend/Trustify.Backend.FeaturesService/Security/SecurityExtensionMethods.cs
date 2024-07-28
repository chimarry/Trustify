using ProductManager.Core.ErrorHandling;
using ProductManager.WebAPI.Util;

namespace ProductManager.WebAPI.Security
{
    public static class SecurityExtensionMethods
    {
        public static string GetAuthorizationHeader(this HttpRequest request)
        {
            if (!request.Headers.ContainsKey(HttpConstants.Authorization))
                throw new UnauthorizedException();

            string encodedAccessToken = request.Headers[HttpConstants.Authorization];

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

        public static string GetClientIpAddress(this HttpRequest request, HttpContext context)
        {
            if (request.Headers.ContainsKey(HttpConstants.ForwardedFor))
                return request.Headers[HttpConstants.ForwardedFor];
            else
                return context?.Connection?.RemoteIpAddress?.MapToIPv4()?.ToString() ?? string.Empty;
        }
    }
}

using Flurl.Http;

namespace Trustify.Backend.AdminService.Keycloak.Service
{
    public interface IHttpService
    {
        IFlurlRequest GetAdminBaseUrl(string bearerToken);

        IFlurlRequest GetBaseUrl(string bearerToken);

        string GetPathSegment(string format, string clientId) => string.Format(format, clientId);

        Task SendRequest(string uri, object content, string token, HttpMethod httpMethod);

    }
}

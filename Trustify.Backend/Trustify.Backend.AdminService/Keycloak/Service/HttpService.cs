using Flurl.Http;
using Flurl.Http.Newtonsoft;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;
using Trustify.Backend.AdminService.Keycloak.Models;
using Trustify.Backend.AdminService.Models;

namespace Trustify.Backend.AdminService.Keycloak.Service
{
    public class HttpService(IOptions<KeycloakOptions> keycloakOptions) : IHttpService
    {
        private static readonly HttpClient httpClient = new(new HttpClientHandler()
        {
            ServerCertificateCustomValidationCallback = (x, y, b, a) => true
        });
        public KeycloakOptions KeycloakOptions { get; set; } = keycloakOptions.Value;

        private static readonly NewtonsoftJsonSerializer serializer = new
            (new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });

        /// <summary>
        /// Sends HTTP request with access token to Keycloak.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="bearerToken"></param>
        /// <returns></returns>
        public IFlurlRequest GetAdminBaseUrl(string bearerToken) =>
                 new Flurl.Url($"{KeycloakOptions.AdminUrl}/{KeycloakOptions.Realm}")
                .WithSettings(x => x.JsonSerializer = serializer)
                .WithOAuthBearerToken(bearerToken);

        /// <summary>
        /// Sends HTTP request with access token to Keycloak.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="bearerToken"></param>
        /// <returns></returns>
        public IFlurlRequest GetBaseUrl(string bearerToken) =>
                 new Flurl.Url($"{KeycloakOptions.Url}/{KeycloakOptions.Realm}")
                .WithSettings(x => x.JsonSerializer = serializer)
                .WithOAuthBearerToken(bearerToken);



        public async Task SendRequest(string fullUri, object data, string token, HttpMethod httpMethod)
        {
            using var request = new HttpRequestMessage();

            using StringContent stringContent = new(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            request.Content = stringContent;

            // Set the request method and URL
            request.Method = httpMethod;
            request.RequestUri = new Uri($"{fullUri}");

            // Set individual request headers
            request.Headers.Add("Authorization", $"{HttpConstants.Bearer}{token}");

            // Make the request
            HttpResponseMessage response = await httpClient.SendAsync(request);
            string content = string.Empty;
            if (response.IsSuccessStatusCode)
            {
                content = await response.Content.ReadAsStringAsync();
            }
            content = await response.Content.ReadAsStringAsync();
        }
    }
}

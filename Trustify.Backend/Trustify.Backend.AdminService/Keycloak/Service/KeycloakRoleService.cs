using Flurl.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Text;
using Trustify.Backend.AdminService.Keycloak.DTO;
using Trustify.Backend.AdminService.Keycloak.Models;
using Trustify.Backend.AdminService.Models;

namespace Trustify.Backend.AdminService.Keycloak.Service
{
    public class KeycloakRoleService : IKeycloakRoleService
    {

        private static HttpClient httpClient = new();

        private static readonly JsonSerializerSettings serializerSettings = new() { NullValueHandling = NullValueHandling.Ignore };

        public KeycloakOptions KeycloakOptions { get; set; }

        public KeycloakRoleService(IOptions<KeycloakOptions> keycloakOptions)
        {
            this.KeycloakOptions = keycloakOptions.Value;
        }

        public async Task<object> AddRole(RoleDTO role, string clientId, string token)
        {
            ///{realm}/clients/{id}/roles
            ///
            // PUT /{realm}/users/{id}/groups/{groupId}

            using StringContent stringContent = new StringContent(JsonConvert.SerializeObject(role, serializerSettings), Encoding.UTF8, "application/json");
            using var request = new HttpRequestMessage();
            // Set the request method and URL
            request.Method = HttpMethod.Post;
            request.RequestUri = new Uri($"{KeycloakOptions.Url}/clients/{clientId}/roles");
            request.Content = stringContent;

            // Set individual request headers
            request.Headers.Add("Authorization", $"{HttpConstants.Bearer}{token}");

            // Make the request
            HttpResponseMessage response = await httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                string contdent = await response.Content.ReadAsStringAsync();
                new object();
            }
            string content = await response.Content.ReadAsStringAsync();
            return null;

            //return await KeycloakServiceUtil.GetBaseUrl(KeycloakOptions.Url, token)
            //                          .AppendPathSegment($"/clients/{role.ClientId}/roles")
            //                          .PostJsonAsync(role);
            //await this.GetBaseUrl(options.Realm)
            //    .AppendPathSegment($"/admin/realms/{options.Realm}/users/{userId}/groups/{groupId}")
            //    .PutJsonAsync(new
            //    {
            //        options.Realm,
            //        UserId = userId,
            //        GroupId = groupId
            //    })
            //    .ConfigureAwait(false);
        }

        public async Task<object> GetRoles(string clientId, string token)
        {
            ///{realm}/clients/{id}/roles
            ///
            // PUT /{realm}/users/{id}/groups/{groupId}

            using var request = new HttpRequestMessage();
            // Set the request method and URL
            request.Method = HttpMethod.Get;
            request.RequestUri = new Uri($"{KeycloakOptions.Url}/clients/{clientId}/roles");

            // Set individual request headers
            request.Headers.Add("Authorization", $"{HttpConstants.Bearer}{token}");

            // Make the request
            HttpResponseMessage response = await httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                return content;
            }
            return null;
        }
    }
}

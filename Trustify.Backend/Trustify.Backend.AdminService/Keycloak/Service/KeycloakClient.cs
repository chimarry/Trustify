using Flurl;
using Flurl.Http;
using Flurl.Http.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Trustify.Backend.AdminService.Keycloak.Models;
using NullValueHandling = Flurl.NullValueHandling;

namespace Trustify.Backend.AdminService.Keycloak.Service
{
    public class KeycloakClient
    {
        private readonly KeycloakOptions options;


        public KeycloakClient(KeycloakOptions options)
        {
            this.options = options;
        }

        public async Task<IEnumerable<KeycloakUser>> GetAllUsers()
        {
            // GET /{realm}/users
            return await GetUsers(new Dictionary<string, object>
            {
                ["email"] = string.Empty,
                ["firstName"] = string.Empty,
            });
        }

        public async Task<KeycloakUser> GetUserById(string userId)
        {
            // GET /{realm}/users/{id}
            return await this.GetBaseUrl(options.Realm)
                .AppendPathSegment($"/admin/realms/{options.Realm}/users/{userId}")
                .GetJsonAsync<KeycloakUser>()
                .ConfigureAwait(false);
        }

        public async Task CreateUser(KeycloakUser user)
        {
            // POST /{realm}/users
            var response = await this.GetBaseUrl(options.Realm)
                .AppendPathSegment($"/admin/realms/{options.Realm}/users")
                .PostJsonAsync(user)
                .ConfigureAwait(false);
        }

        public async Task UpdateUser(KeycloakUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            // POST /{realm}/users
            await this.GetBaseUrl(options.Realm)
                .AppendPathSegment($"/admin/realms/{options.Realm}/users/{user.UserId}")
                .PutJsonAsync(user)
                .ConfigureAwait(false);
        }

        public async Task RemoveUser(string userId)
        {
            // DELETE /{realm}/users/{id}
            await this.GetBaseUrl(options.Realm)
                .AppendPathSegment($"/admin/realms/{options.Realm}/users/{userId}")
                .DeleteAsync()
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<Group>> GetGroups(string search)
        {
            return await this.GetBaseUrl(options.Realm)
                .AppendPathSegment($"/admin/realms/{options.Realm}/groups")
                .SetQueryParams(new Dictionary<string, object>()
                {
                    ["search"] = search,
                })
                .GetJsonAsync<IEnumerable<Group>>()
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<Group>> GetUserGroups(string userId)
        {
            // GET /{realm}/users/{id}/groups
            return await this.GetBaseUrl(options.Realm)
                .AppendPathSegment($"/admin/realms/{options.Realm}/users/{userId}/groups")
                .GetJsonAsync<IEnumerable<Group>>()
                .ConfigureAwait(false);
        }

        public async Task AddUserGroup(string userId, string groupId)
        {
            // PUT /{realm}/users/{id}/groups/{groupId}
            await this.GetBaseUrl(options.Realm)
                .AppendPathSegment($"/admin/realms/{options.Realm}/users/{userId}/groups/{groupId}")
                .PutJsonAsync(new
                {
                    options.Realm,
                    UserId = userId,
                    GroupId = groupId
                })
                .ConfigureAwait(false);
        }

        public async Task RemoveUserGroup(string userId, string groupId)
        {
            // DELETE /{realm}/users/{id}/groups/{groupId}
            await this.GetBaseUrl(options.Realm)
                .AppendPathSegment($"/admin/realms/{options.Realm}/users/{userId}/groups/{groupId}")
                .DeleteAsync()
                .ConfigureAwait(false);
        }

        public async Task ExecuteActionsEmail(string userId, IEnumerable<string> requiredActions)
        {
            // PUT /{realm}/users/{id}/execute-actions-email

            // If client_id and redirectUrl are set, use them to redirect user to a specific page after he has updated his profile/password
            await this.GetBaseUrl(options.Realm)
                .AppendPathSegment($"/admin/realms/{options.Realm}/users/{userId}/execute-actions-email")
                .SetQueryParam("redirect_uri", options.RedirectUrlAfterProfileUpdate, NullValueHandling.Remove)
                .SetQueryParam("client_id", string.IsNullOrEmpty(options.RedirectUrlAfterProfileUpdate) ? null : options.ClientIdForUrlRedirectionAfterProfileUpdate, NullValueHandling.Remove)
                .SetQueryParam("lifespan", options.ProfileUpdateLinkLifeSpanInSeconds)
                .PutJsonAsync(requiredActions)
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<KeycloakUser>> GetUsers(
             Dictionary<string, object> queryParams)
        {
            return await this.GetBaseUrl(options.Realm)
                .AppendPathSegment($"/admin/realms/{options.Realm}/users")
                .SetQueryParams(queryParams)
                .GetJsonAsync<IEnumerable<KeycloakUser>>()
                .ConfigureAwait(false);
        }

        private IFlurlRequest GetBaseUrl(string authenticationRealm) =>
             new Flurl.Url(options.Url)
            .AppendPathSegment("/auth")
            .WithSettings(x => x.JsonSerializer = new Flurl.Http.Configuration.DefaultJsonSerializer())
            .WithOAuthBearerToken(GetBearerToken());
        //  .WithAuthentication(null, options.Url, authenticationRealm, options.Username, options.Password);

        private string GetBearerToken()
        {
            return string.Empty;
        }
    }
}

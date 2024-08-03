using Newtonsoft.Json;

namespace Trustify.Backend.AdminService.Keycloak.Models
{
    public class FederatedIdentity
    {
        [JsonProperty("identityProvider")]
        public string? IdentityProvider { get; set; }

        [JsonProperty("userId")]
        public string? UserId { get; set; }

        [JsonProperty("userName")]
        public string? UserName { get; set; }
    }
}

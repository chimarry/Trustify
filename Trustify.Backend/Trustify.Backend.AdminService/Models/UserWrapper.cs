using Newtonsoft.Json;
using Trustify.Backend.AdminService.Keycloak.Models;

namespace Trustify.Backend.AdminService.Models
{
    public class UserWrapper
    {
        [JsonProperty("username")]
        public string? UserName { get; set; }

        [JsonProperty("enabled")]
        public bool? Enabled { get; set; }

        [JsonProperty("emailVerified")]
        public bool? EmailVerified { get; set; }

        [JsonProperty("firstName")]
        public string? FirstName { get; set; }

        [JsonProperty("lastName")]
        public string? LastName { get; set; }

        [JsonProperty("email")]
        public string? Email { get; set; }

        [JsonProperty("requiredActions")]
        public string[]? RequiredActions { get; set; }

        [JsonProperty("credentials")]
        public IEnumerable<Credentials>? Credentials { get; set; }
    }
}

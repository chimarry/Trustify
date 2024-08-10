using Newtonsoft.Json;
using System.Collections.ObjectModel;
using Trustify.Backend.AdminService.Keycloak.Models;

namespace Trustify.Backend.AdminService.Keycloak.DTO
{
    public class UserDTO
    {
        [JsonProperty("id")]
        public string? Id { get; set; } = string.Empty;

        [JsonProperty("createdTimestamp")]
        public long? CreatedTimestamp { get; set; }

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


        [JsonProperty("clientRoles")]
        public Dictionary<string, string[]>? ClientRoles { get; set; }

        [JsonProperty("credentials")]
        public IEnumerable<Credentials>? Credentials { get; set; }

        [JsonProperty("federatedIdentities")]
        public IEnumerable<FederatedIdentity>? FederatedIdentities { get; set; }

        [JsonProperty("federationLink")]
        public string? FederationLink { get; set; }

        [JsonProperty("groups")]
        public IEnumerable<string>? Groups { get; set; }

        [JsonProperty("realmRoles")]
        public IEnumerable<string>? RealmRoles { get; set; }

        //[JsonProperty("self")]
        //public string? Self { get; set; }
    }
}

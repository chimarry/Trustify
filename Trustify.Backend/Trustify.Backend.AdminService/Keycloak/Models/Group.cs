using System.Collections.Generic;
using Newtonsoft.Json;

namespace Trustify.Backend.AdminService.Keycloak.Models
{
    public class Group
    {
        [JsonProperty("id")]
        public string Id { get; set; } = string.Empty;

        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("path")]
        public string? Path { get; set; }

        [JsonProperty("subGroups")]
        public IEnumerable<Group>? Subgroups { get; set; }

        [JsonProperty("realmRoles")]
        public IEnumerable<string>? RealmRoles { get; set; }

        [JsonProperty("clientRoles")]
        public IDictionary<string, IEnumerable<string>>? ClientRoles { get; set; }

        [JsonProperty("attributes")]
        public IDictionary<string, IEnumerable<string>>? Attributes { get; set; }
    }
}

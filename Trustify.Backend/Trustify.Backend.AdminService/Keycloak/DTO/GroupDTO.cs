using Newtonsoft.Json;

namespace Trustify.Backend.AdminService.Keycloak.DTO
{
    public class GroupDTO
    {
        [JsonProperty("id")]
        public string? Id { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("path")]
        public string? Path { get; set; }

        [JsonProperty("realmRoles")]
        public string[]? RealmRoles { get; set; } = [];

        [JsonProperty("clientRoles")]
        public Dictionary<string, string[]> ClientRoles { get; set; } = [];
    }
}

using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Trustify.Backend.AdminService.Keycloak.DTO
{
    public class RoleDTO
    {
        [JsonProperty("id")]
        public string Id { get; set; } = null!;

        [JsonProperty("name")]
        public string Name { get; set; } = null!;

        [JsonProperty("description")]
        public string? Description { get; set; }

        [JsonProperty("clientRole")]
        public bool? ClientRole { get; set; } = true;

        //public string? ClientId { get; set; }

        //public bool? Composite { get; set; } = false;

        //public string? Composites { get; set; } = null;

        //public Dictionary<string, string>? Attributes { get; set; } = [];

    }
}

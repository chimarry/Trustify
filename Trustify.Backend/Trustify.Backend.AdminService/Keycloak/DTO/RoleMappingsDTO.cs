using Newtonsoft.Json;
using Trustify.Backend.AdminService.Keycloak.Models;

namespace Trustify.Backend.AdminService.Keycloak.DTO
{
    public class RoleMappingsDTO
    {
        [JsonProperty("clientMappings")]
        public Dictionary<string, ClientRoleMappingsDTO>? ClientRoles { get; set; }

        [JsonProperty("realmMappings")]
        public IEnumerable<RoleDTO>? RealmRoles { get; set; }
    }
}

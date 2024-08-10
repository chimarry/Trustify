namespace Trustify.Backend.AdminService.Keycloak.DTO
{
    public class ClientRoleMappingsDTO
    {
        public string? Id { get; set; }

        public RoleDTO[] Mappings { get; set; } = [];
    }
}

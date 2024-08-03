using Trustify.Backend.AdminService.Keycloak.DTO;

namespace Trustify.Backend.AdminService.Keycloak.Service
{
    public interface IKeycloakRoleService
    {
        Task<object> AddRole(RoleDTO role,string clientId, string token);

        Task<object> GetRoles(string clientId, string token);
    }
}

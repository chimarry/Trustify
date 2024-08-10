using Trustify.Backend.AdminService.Keycloak.DTO;

namespace Trustify.Backend.AdminService.Keycloak.Service
{
    public interface IRoleService
    {
        Task<bool> AddRole(RoleDTO role, string clientId, string token);

        Task<IEnumerable<RoleDTO>?> GetRoles(string clientId, string token);

        Task<bool> DeleteRole(string roleName, string clientId, string token);

        Task<RoleDTO?> GetRole(string roleName, string clientId, string token);
    }
}

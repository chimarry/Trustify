using Trustify.Backend.AdminService.Keycloak.DTO;

namespace Trustify.Backend.AdminService.Keycloak.Service
{
    public interface IRoleService
    {
        Task<ResultMessage<bool>> AddRole(RoleDTO role, string clientId, string token);

        Task<ResultMessage<IEnumerable<RoleDTO>>> GetRoles(string clientId, string token);

        Task<ResultMessage<bool>> DeleteRole(string roleName, string clientId, string token);

        Task<ResultMessage<RoleDTO>> GetRole(string roleName, string clientId, string token);
    }
}

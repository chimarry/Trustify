using Trustify.Backend.AdminService.Keycloak.DTO;

namespace Trustify.Backend.AdminService.Keycloak.Service
{
    public interface IGroupService
    {
        public Task<bool> AddGroup(GroupDTO group, string token);

        Task<bool> DeleteClientRolesFromGroup(IEnumerable<RoleDTO> roles, string groupId, string clientId, string token);

        Task<bool> AddClientRolesToGroup(IEnumerable<RoleDTO> roles, string groupId, string clientId, string token);

        public Task<bool> DeleteGroup(string groupName, string token);

        public Task<GroupDTO?> GetGroup(string groupName, string token);

        public Task<IEnumerable<GroupDTO>?> GetAllGroups(string token);
    }
}

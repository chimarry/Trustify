using Trustify.Backend.AdminService.Keycloak.DTO;

namespace Trustify.Backend.AdminService.Keycloak.Service
{
    public interface IGroupService
    {
        Task<ResultMessage<bool>> AddGroup(GroupDTO group, string token);

        Task<ResultMessage<bool>> DeleteClientRolesFromGroup(IEnumerable<RoleDTO> roles, string groupId, string clientId, string token);

        Task<ResultMessage<bool>> AddClientRolesToGroup(IEnumerable<RoleDTO> roles, string groupId, string clientId, string token);

        Task<ResultMessage<bool>> DeleteGroup(string groupName, string token);

        Task<ResultMessage<GroupDTO>> GetGroup(string groupName, string token);

        Task<ResultMessage<IEnumerable<GroupDTO>>> GetAllGroups(string token);
    }
}

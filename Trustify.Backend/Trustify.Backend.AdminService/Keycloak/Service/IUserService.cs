using Trustify.Backend.AdminService.Keycloak.DTO;

namespace Trustify.Backend.AdminService.Keycloak.Service
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetAllUsers(string accessToken);

        Task ExecuteActionsEmail(string userId, IEnumerable<string> requiredActions, string accessToken);

        Task<bool> AddUserInGroup(string userId, string groupId, string accessToken);

        Task<bool> RemoveUserFromGroup(string userId, string groupId, string accessToken);

        Task<IEnumerable<GroupDTO>> GetUserGroups(string userId, string accessToken);

        Task<IEnumerable<RoleDTO>> GetUserRoles(string userId, string accessToken);

        Task<bool> RemoveUser(string userId, string accessToken);

        Task<bool> RegisterUser(UserDTO user, string accessToken);

        Task<UserDTO> GetUser(string userId, string accessToken);
    }
}

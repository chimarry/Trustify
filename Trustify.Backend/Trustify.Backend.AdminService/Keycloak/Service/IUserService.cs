using Trustify.Backend.AdminService.Keycloak.DTO;

namespace Trustify.Backend.AdminService.Keycloak.Service
{
    public interface IUserService
    {
        Task<ResultMessage<IEnumerable<UserDTO>>> GetAllUsers(string accessToken);

        Task ExecuteActionsEmail(string userId, IEnumerable<string> requiredActions, string accessToken);

        Task<ResultMessage<bool>> AddUserInGroup(string userId, string groupId, string accessToken);

        Task<ResultMessage<bool>> RemoveUserFromGroup(string userId, string groupId, string accessToken);

        Task<ResultMessage<IEnumerable<GroupDTO>>> GetUserGroups(string userId, string accessToken);

        Task<ResultMessage<bool>> RemoveUser(string userId, string accessToken);

        Task<ResultMessage<bool>> RegisterUser(UserDTO user, string accessToken);

        Task<ResultMessage<UserDTO>> GetUser(string userId, string accessToken);
    }
}

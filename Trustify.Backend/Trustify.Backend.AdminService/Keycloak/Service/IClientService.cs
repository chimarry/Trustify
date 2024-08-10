using Trustify.Backend.AdminService.Keycloak.DTO;

namespace Trustify.Backend.AdminService.Keycloak.Service
{
    public interface IClientService
    {
        Task<IEnumerable<ClientDTO>> GetClients(string token);
    }
}

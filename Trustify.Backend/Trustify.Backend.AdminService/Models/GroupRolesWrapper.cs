namespace Trustify.Backend.AdminService.Models
{
    public class GroupRolesWrapper
    {
        public string GroupId { get; set; } = null!;

        public string ClientId { get; set; } = null!;

        public IEnumerable<RoleWrapper> RoleWrappers { get; set; } = Enumerable.Empty<RoleWrapper>();
    }
}

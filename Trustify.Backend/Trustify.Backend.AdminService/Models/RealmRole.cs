namespace Trustify.Backend.AdminService.Models
{
    public class RealmRole
    {
        public const string TokenClaimName = "realm_roles";
        public const string TrustifyPrefix = "trustify.realm";
        public const string Administrator = $"{TrustifyPrefix}.administrator";
        public const string SuperAdministrator = $"{TrustifyPrefix}.super_administrator";
    }
}

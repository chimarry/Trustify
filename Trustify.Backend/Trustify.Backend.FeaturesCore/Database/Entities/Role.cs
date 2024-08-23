namespace Trustify.Backend.FeaturesCore.Database.Entities
{
    public class Role
    {
        public long RoleId { get; set; }

        public string? KeycloakId { get; set; }

        public string Name { get; set; } = null!;

        public virtual required ICollection<Section> Sections { get; set; }
    }
}

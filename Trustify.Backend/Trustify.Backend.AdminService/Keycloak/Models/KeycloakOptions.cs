namespace Trustify.Backend.AdminService.Keycloak.Models
{
    public class KeycloakOptions
    {
        public string Url { get; set; } = string.Empty;

        public string AdminUrl { get; set; } = string.Empty;

        public string RolesUrlFormat { get; set; } = string.Empty;

        public string Realm { get; set; } = string.Empty;

        public string ClientId { get; set; } = string.Empty;

        public string ClientSecret { get; set; } = string.Empty;

        public string Authority { get; set; } = string.Empty;

        public string RefreshTokenUrl { get; set; } = string.Empty;
    }
}

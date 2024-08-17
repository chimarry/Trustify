namespace Trustify.Backend.AdminService.Models
{
    public class FrontendConfig
    {
        public string ReturnUrl { get; set; } = string.Empty;

        public string RedirectQueryParam { get; set; } = string.Empty;

        public string RedirectUrl { get; set; } = string.Empty;

        public string BaseUrl { get; set; } = string.Empty;
    }
}

using Newtonsoft.Json;

namespace Trustify.Backend.AdminService.Models
{
    public class TokenWrapper
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; } = string.Empty;

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; } = string.Empty;    
    }
}

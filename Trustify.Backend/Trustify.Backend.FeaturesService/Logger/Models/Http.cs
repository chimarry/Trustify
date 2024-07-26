using Newtonsoft.Json;

namespace Trustify.Backend.FeaturesService.Logger.Models
{
    public class Http
    {
        [JsonProperty("method")]
        public string? Method { get; set; }

        [JsonProperty("requestPath")]
        public string? RequestPath { get; set; }

        [JsonProperty("queryString")]
        public string? QueryString { get; set; }

        [JsonProperty("protocol")]
        public string? Protocol { get; set; }

        [JsonProperty("contentType")]
        public string? ContentType { get; set; }
    }
}

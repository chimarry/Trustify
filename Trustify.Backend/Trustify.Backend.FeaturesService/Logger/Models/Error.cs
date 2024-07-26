using Newtonsoft.Json;

namespace Trustify.Backend.FeaturesService.Logger.Models
{
    public class Error
    {
        [JsonProperty("stackTrace")]
        public string? StackTrace { get; set; }

        [JsonProperty("exceptionType")]
        public string? ExceptionType { get; set; }

        [JsonProperty("message")]
        public string? Message { get; set; }
    }
}

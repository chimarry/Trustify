using Newtonsoft.Json;

namespace Trustify.Backend.FeaturesService.Logger.Models
{
    public class Action
    {
        [JsonProperty("actionId")]
        public string? ActionId { get; set; }

        [JsonProperty("actionName")]
        public string? ActionName { get; set; }

        [JsonProperty("environmentUserName")]
        public string? EnvironmentUserName { get; set; }
    }
}

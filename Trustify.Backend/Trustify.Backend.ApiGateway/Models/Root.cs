using Newtonsoft.Json;

namespace Trustify.Backend.ApiGateway.Models
{
    public record Root([property: JsonProperty("trustify_app")] TrustifyApp? TrustifyApp);
}
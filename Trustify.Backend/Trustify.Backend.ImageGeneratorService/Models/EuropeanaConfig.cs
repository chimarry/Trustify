namespace Trustify.Backend.ImageGeneratorService.Models
{
    public class EuropeanaConfig
    {
        public string BaseUrl { get; set; } = null!;
        public string SearchUrlSuffixFormat { get; set; } = null!;
        public string KeyParam { get; set; } = null!;

        public string SearchParam { get; set; } = null!;
        public string ApiKey { get; set; } = null!;
    }
}

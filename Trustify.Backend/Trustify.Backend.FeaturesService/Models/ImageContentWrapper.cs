namespace Trustify.Backend.FeaturesService.Models
{
    public class ImageContentWrapper
    {
        public string? Name { get; set; } = string.Empty;

        public IFormFile? Image { get; set; }
    }
}

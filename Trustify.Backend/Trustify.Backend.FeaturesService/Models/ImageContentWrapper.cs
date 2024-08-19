namespace Trustify.Backend.FeaturesService.Models
{
    public class ImageContentWrapper
    {
        public string Name { get; set; } = null!;

        public IFormFile? Image { get; set; }
    }
}

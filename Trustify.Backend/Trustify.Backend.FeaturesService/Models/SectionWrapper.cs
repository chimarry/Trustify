using Trustify.Backend.FeaturesCore.Database.Entities;

namespace Trustify.Backend.FeaturesService.Models
{
    public class SectionWrapper
    {
        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public long[] ImageContents { get; set; } = [];

        public long[] TextualContents { get; set; } = [];
    }
}

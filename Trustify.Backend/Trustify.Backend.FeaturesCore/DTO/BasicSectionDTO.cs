namespace Trustify.Backend.FeaturesCore.DTO
{
    public class BasicSectionDTO
    {
        public long SectionId { get; set; }

        public string Title { get; set; } = null!;

        public string? Description { get; set; }
    }
}

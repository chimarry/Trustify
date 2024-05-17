namespace Trustify.Backend.FeaturesCore.Database.Entities
{
    public class Section
    {
        public long SectionId { get; set; }

        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public bool IsConfidential { get; set; } = true;

        public virtual required ICollection<ImageContent> ImageContents { get; set; }

        public virtual required ICollection<TextualContent> TextualContents { get; set; }
    }
}

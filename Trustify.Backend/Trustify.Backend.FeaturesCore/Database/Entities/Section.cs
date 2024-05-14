namespace Trustify.Backend.FeaturesCore.Database.Entities
{
    public class Section
    {
        public long SectionId { get; set; }

        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public string Author { get; set; } = null!;

        public bool IsConfidential { get; set; } = true;

        public virtual required ICollection<SectionImageContent> ImageContents { get; set; }

        public virtual required ICollection<SectionTextualContent> TextualContents { get; set; }
    }
}

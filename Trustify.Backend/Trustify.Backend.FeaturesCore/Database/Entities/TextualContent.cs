namespace Trustify.Backend.FeaturesCore.Database.Entities
{
    public class TextualContent
    {
        public long TextualContentId { get; set; }

        public string Title { get; set; } = null!;

        public string Text { get; set; } = null!;

        public string? Author { get; set; } = null;

        public int Lenght { get; set; }

        public DateTime CreatedOn { get; set; }

        public virtual required ICollection<Section> Sections { get; set; }
    }
}
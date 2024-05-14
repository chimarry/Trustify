namespace Trustify.Backend.FeaturesCore.Database.Entities
{
    public class SectionTextualContent
    {
        public long SectionTextualContentId { get; set; }

        public long SectionId { get; set; }

        public long TextualContentId { get; set; }

        public virtual TextualContent TextualContent { get; set; } = null!;

        public virtual Section Section { get; set; } = null!;
    }
}

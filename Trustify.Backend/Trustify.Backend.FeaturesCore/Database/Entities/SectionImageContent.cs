namespace Trustify.Backend.FeaturesCore.Database.Entities
{
    public class SectionImageContent
    {
        public long SectionImageContentId { get; set; }

        public long SectionId { get; set; }

        public long ImageContentId { get; set; }

        public virtual required ImageContent ImageContent { get; set; } = null!;

        public virtual required Section Section { get; set; } = null!;
    }
}

namespace Trustify.Backend.FeaturesCore.Database.Entities
{
    public class ImageContent
    {
        public long ImageContentId { get; set; }

        public string Path { get; set; } = null!;

        public string Name { get; set; } = null!;

        public DateTime UploadedOn { get; set; }

        public string UploadedBy { get; set; } = null!;

        public double Size { get; set; }

        public virtual required ICollection<SectionImageContent> Sections { get; set; }
    }
}

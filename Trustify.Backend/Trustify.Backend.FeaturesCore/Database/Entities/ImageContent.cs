namespace Trustify.Backend.FeaturesCore.Database.Entities
{
    public class ImageContent
    {
        public long ImageContentId { get; set; }

        public string Path { get; set; } = null!;

        public string Name { get; set; } = null!;

        public DateTime UploadedOn { get; set; }

        public double Size { get; set; }

        public virtual required ICollection<Section> Sections { get; set; }
    }
}

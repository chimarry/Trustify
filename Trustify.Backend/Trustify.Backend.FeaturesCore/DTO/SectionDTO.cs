namespace Trustify.Backend.FeaturesCore.DTO
{
    public class SectionDTO : BasicSectionDTO
    {
        public long[] ImageContents { get; set; } = [];

        public long[] TextualContents { get; set; } = [];
    }
}

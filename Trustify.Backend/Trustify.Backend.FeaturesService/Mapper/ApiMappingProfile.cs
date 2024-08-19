using AutoMapper;
using Trustify.Backend.FeaturesCore.DTO;
using Trustify.Backend.FeaturesService.Models;

namespace Trustify.Backend.FeaturesService.Mapper
{
    public class ApiMappingProfile : Profile
    {

        public ApiMappingProfile()
        {
            MapSections();
            MapTextualContent();
            MapImageContent();
        }

        private void MapSections()
        {
            CreateMap<SectionWrapper, SectionDTO>().ForMember(dest => dest.SectionId, opt => opt.Ignore());
        }

        private void MapTextualContent()
        {
            CreateMap<TextualContentWrapper, TextualContentDTO>().ForMember(dest => dest.TextualContentId, opt => opt.Ignore())
                                                                 .ForMember(dest => dest.Lenght, opt => opt.MapFrom(src => src.Text.Length));
        }

        private void MapImageContent()
        {
            CreateMap<ImageContentWrapper, ImageContentDTO>().ForMember(dest => dest.ImageContentId, opt => opt.Ignore())
                                                             .ForMember(dest => dest.UploadedOn, opt => opt.MapFrom(src => DateTime.UtcNow))
                                                             .ForMember(dest => dest.Path, opt => opt.Ignore())
                                                             .ForMember(dest => dest.Size, opt => opt.Ignore());
        }
    }
}
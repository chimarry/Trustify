using AutoMapper;
using System;
using Trustify.Backend.FeaturesCore.Database.Entities;
using Trustify.Backend.FeaturesCore.DTO;

namespace Trustify.Backend.FeaturesCore.AutoMapper
{
    public class CoreMappingProfile : Profile
    {

        public CoreMappingProfile()
        {
            MapImageContent();
            MapTextualContent();
            MapSections();
            MapRoles();
        }

        private void MapRoles()
        {
            CreateMap<Role, RoleDTO>().ReverseMap();
        }

        private void MapSections()
        {
            // CreateMap<long, ImageContent>().ForMember(dest => dest.ImageContentId, opt => opt.MapFrom(src => src));
            // CreateMap<long, TextualContentDTO>().ForMember(dest => dest.TextualContentId, opt => opt.MapFrom(src => src));
            CreateMap<SectionDTO, Section>().ForMember(dest => dest.SectionId, opt => opt.AllowNull())
                                            .ForMember(dest => dest.ImageContents, opt => opt.Ignore())
                                            .ForMember(dest => dest.TextualContents, opt => opt.Ignore())
                                            .ForMember(dest => dest.Roles, opt => opt.Ignore());

            CreateMap<Section, BasicSectionDTO>();
            CreateMap<Section, SectionDTO>().IncludeBase<Section, BasicSectionDTO>()
                                            .ForMember(dest=>dest.Roles, opt=>opt.MapFrom(src=>src.Roles.Select(x=>x.RoleId)))
                                            .ForMember(dest => dest.ImageContents, opt => opt.MapFrom(src => src.ImageContents.Select(x => x.ImageContentId)))
                                            .ForMember(dest => dest.TextualContents, opt => opt.MapFrom(src => src.TextualContents.Select(x => x.TextualContentId)));
        }

        private void MapImageContent()
        {
            CreateMap<ImageContent, ImageContentDTO>().ForMember(dest => dest.UploadedOn, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.UploadedOn, DateTimeKind.Utc)));
            CreateMap<ImageContentDTO, ImageContent>().ForMember(dest => dest.ImageContentId, opt => opt.Ignore())
                                                       .ForMember(dest => dest.UploadedOn, opt => opt.MapFrom(src => DateTime.UtcNow));
        }

        private void MapTextualContent()
        {
            CreateMap<TextualContent, TextualContentDTO>().ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.CreatedOn, DateTimeKind.Utc)));
            CreateMap<TextualContentDTO, TextualContent>().ForMember(dest => dest.Sections, opt => opt.Ignore())
                                                          .ForMember(dest => dest.TextualContentId, opt => opt.AllowNull())
                                                          .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => DateTime.UtcNow));
        }
    }
}
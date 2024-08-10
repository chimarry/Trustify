using AutoMapper;
using Flurl.Util;
using Trustify.Backend.AdminService.Keycloak.DTO;
using Trustify.Backend.AdminService.Models;

namespace Trustify.Backend.AdminService.AutoMapper
{
    public class ApiMappingProfile : Profile
    {

        public ApiMappingProfile()
        {
            MapRoles();
            MapUsers();
            MapGroups();
        }

        private void MapUsers()
        {
            CreateMap<UserWrapper, UserDTO>().ForMember(src => src.Id, opt => opt.AllowNull())
                                             .ForMember(src => src.CreatedTimestamp, opt => opt.AllowNull())
                                             .ForMember(src => src.RealmRoles, opt => opt.Ignore())
                                             .ForMember(src => src.ClientRoles, opt => opt.Ignore())
                                             .ForMember(src => src.Groups, opt => opt.Ignore())
                                             .ForMember(src => src.FederatedIdentities, opt => opt.Ignore())
                                             .ForMember(src => src.FederationLink, opt => opt.Ignore());
        }

        private void MapGroups()
        {
            CreateMap<GroupWrapper, GroupDTO>().ForMember(src => src.Id, opt => opt.AllowNull())
                                               .ForMember(src => src.Name, opt => opt.AllowNull())
                                               .ForMember(src => src.ClientRoles, opt => opt.AllowNull())
                                               .ForMember(src => src.RealmRoles, opt => opt.AllowNull())
                                               .ForMember(src => src.Path, opt => opt.AllowNull());
        }

        private void MapRoles()
        {
            CreateMap<RoleWrapper, RoleDTO>().ForMember(src => src.Id, opt => opt.AllowNull())
                                             .ForMember(src => src.ClientRole, opt => opt.AllowNull());
            CreateMap<RoleDTO, RoleWrapper>();

            /* Create the map using construct using rather than ForMember */
            CreateMap<KeyValuePair<string, ClientRoleMappingsDTO>, KeyValuePair<string, string[]>>()
                  .ConstructUsing(x => new KeyValuePair<string, string[]>(x.Key, x.Value.Mappings.Select(x => x.Name).ToArray()));

        }
    }
}

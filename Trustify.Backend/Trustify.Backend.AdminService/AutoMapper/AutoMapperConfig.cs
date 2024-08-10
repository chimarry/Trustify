using AutoMapper;

namespace Trustify.Backend.AdminService.AutoMapper
{
    /// <summary>
    /// Configure automapper to use certain mapping profiles. 
    /// </summary>
    public static class AutoMapperConfig
    {
        /// <summary>
        /// Create new instance of IMapper properly configured.
        /// </summary>
        public static IMapper CreateMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ApiMappingProfile>();
            });
            return config.CreateMapper();
        }
    }
}

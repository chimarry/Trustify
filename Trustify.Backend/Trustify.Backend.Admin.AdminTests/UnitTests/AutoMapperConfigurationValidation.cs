using Trustify.Backend.AdminService.AutoMapper;
using Xunit;

namespace Trustify.Backend.Admin.AdminTests.UnitTests
{
    /// <summary>
    /// Test class for AutoMapper.
    /// </summary>
    public class AutoMapperConfigurationValidation
    {
        /// <summary>
        /// Validates AutoMapper configuration.
        /// </summary>
        [Fact]
        public void ValidateConfiguration()
        {
            AutoMapperConfig.CreateMapper().ConfigurationProvider.AssertConfigurationIsValid();
        }
    }
}

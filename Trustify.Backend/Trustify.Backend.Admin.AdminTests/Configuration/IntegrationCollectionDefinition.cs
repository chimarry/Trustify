using Xunit;

namespace Trustify.Backend.Admin.AdminTests.Configuration
{
    [CollectionDefinition(IntegrationCollection)]
    public class IntegrationCollectionDefinition : ICollectionFixture<IntegrationFixture>
    {
        public const string IntegrationCollection = nameof(IntegrationCollection);
    }
}

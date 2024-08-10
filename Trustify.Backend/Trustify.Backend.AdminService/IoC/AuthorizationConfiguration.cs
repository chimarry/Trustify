using Trustify.Backend.AdminService.Models;
using Trustify.Backend.AdminService.Security;

namespace Trustify.Backend.AdminService.IoC
{
    public static class AuthorizationConfiguration
    {
        public static void ConfigureAuthorization(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthorizationBuilder()
                .AddPolicy(TrustifyPolicy.Authenticated, p => p.RequireAuthenticatedUser())
                .AddPolicy(TrustifyPolicy.All, p => p.RequireClaim(RealmRole.TokenClaimName, RealmRole.Administrator))
                .AddPolicy(TrustifyPolicy.Restricted, p => p.RequireClaim(RealmRole.TokenClaimName, RealmRole.SuperAdministrator));
                //.AddPolicy(TrustifyPolicy.Restricted, p => p.RequireAuthenticatedUser());
        }

    }
}

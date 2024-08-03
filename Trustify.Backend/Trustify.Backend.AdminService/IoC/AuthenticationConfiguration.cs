using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace Trustify.Backend.AdminService.IoC
{
    public static class AuthenticationConfiguration
    {
        public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
            .AddCookie(options =>
            {
                options.LoginPath = "/Account/Login";
                options.AccessDeniedPath = "/Account/AccessDenied";
            })
            .AddOpenIdConnect(options =>
            {
                options.Authority = "http://192.168.56.101:8080/realms/trustify";
                options.ClientId = "trustify_admin";
                options.ClientSecret = "pBufFTBKcbV7n7O1ExTZDLBwvzaDOfcJ";
                options.ResponseType = "code";
                options.SaveTokens = true;
                options.Scope.Add("openid");
                options.RequireHttpsMetadata = false;
                options.CallbackPath = "/users/login-callback"; // Update callback path
                options.SignedOutCallbackPath = "/users/logout-callback"; // Update signout callback path
                options.ClaimActions.MapJsonKey("preffered_username", "trustify.email");
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    RoleClaimType = ClaimTypes.Role
                };
            });
        }

    }
}

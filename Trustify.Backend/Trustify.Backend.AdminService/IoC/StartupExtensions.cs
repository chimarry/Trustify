using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Security.Claims;
using Trustify.Backend.AdminService.Models;
using Microsoft.AspNetCore.Authentication;

namespace Trustify.Backend.AdminService.IoC
{
    public static class StartupExtensions
    {
        public static void ConfigureAuthenticationAuthorization(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthorization(options =>
                {
                    //options.FallbackPolicy = new AuthorizationPolicyBuilder()
                    //                            .RequireAuthenticatedUser()
                    //                            .RequireAssertion(context => context.Resource == default)
                    //                            .Build();
                    options.AddPolicy("All", policy => policy.RequireRole(new List<string>()
                        {
                            Role.Administrator,
                            Role.SuperAdministrator
                        }));

                    options.AddPolicy("Restricted", policy =>
                    {
                        policy.RequireRole(new List<string>()
                        {
                           Role.SuperAdministrator
                        });
                    });
                });
            // FallbackPolicy are used to force a developer to provide authorized data.
            // If a new endpoint is added, it will not be available until authorized data (like [AllowAnonymous] or [Authorize(PolicyName="PolicyName")]) is provided.
            // Restricting all and excluding only needed is one of the best practice in the industry.
            // options.FallbackPolicy = new AuthorizationPolicyBuilder()
            //     .RequireAuthenticatedUser()
            //     // Restrict all endpoints defined in controllers. Ignore swagger API calls.
            //     .RequireAssertion(context => context.Resource == default)
            //     .Build();
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
            .AddCookie(options =>
            {
                options.LoginPath = "/Account/Login";
                options.AccessDeniedPath = "/users/access-denied";
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
            //services.AddAuthorization(o =>
            //{

            //    o.AddPolicy("Admin", new AuthorizationPolicyBuilder()
            //                             .RequireAuthenticatedUser().RequireAssertion(ctx =>
            //                             {
            //                                 return ctx.User.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value.StartsWith("trustify", StringComparison.InvariantCultureIgnoreCase));
            //                             })
            //                             //.RequireRole(RoleName.CompanyUser)
            //                             //.RequireClaim(ClaimTypes.Role, RoleName.CompanyUser)
            //                             // .AddAuthenticationSchemes(CookieAuthenticationDefaults.AuthenticationScheme)
            //                             .Build());
            //});

        }

        public static void ConfigureLogger(this WebApplicationBuilder builder)
        {
            builder.Configuration.AddJsonFile("appsettings.Logging.json", optional: false, reloadOnChange: true);
            builder.Host.UseSerilog((context, services, loggerConfiguration) =>
                        {
                            loggerConfiguration
                                .MinimumLevel.Information()
                                .Enrich.FromLogContext()
                                .WriteTo.Console()
                                .ReadFrom.Configuration(context.Configuration);
                        });
        }
    }
}

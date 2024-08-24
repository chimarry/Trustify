using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Flurl.Http;
using Trustify.Backend.AdminService.Models;
using Trustify.Backend.AdminService.Exceptions;
using Trustify.Backend.AdminService.Keycloak.Models;

namespace Trustify.Backend.AdminService.IoC
{
    public static class AuthenticationConfiguration
    {
        public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var keycloakOptions = new KeycloakOptions();
            configuration.GetSection("KeycloakOptions").Bind(keycloakOptions);

            var frontendConfig = new FrontendConfig();
            configuration.GetSection("Frontend").Bind(frontendConfig);

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
            .AddCookie(options =>
            {
                options.Cookie.Name = "TRF_COOKIE";
                options.Cookie.HttpOnly = true;
                //options.Cookie.SameSite = SameSiteMode.None;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.LoginPath = "/api/v1.0/auth/login";
                options.AccessDeniedPath = "/api/v1.0/auth/access-denied";

                options.ExpireTimeSpan = TimeSpan.FromMinutes(2);
                options.Events = new CookieAuthenticationEvents
                {
                    // This is fired every time the cookie is validated in cookie middleware.
                    OnValidatePrincipal = async x =>
                    {
                        var now = DateTimeOffset.UtcNow;
                        TimeSpan timeElapsed = now.Subtract(x.Properties.IssuedUtc ?? DateTimeOffset.MaxValue);
                        TimeSpan? timeRemaining = x.Properties.ExpiresUtc?.Subtract(now) ?? TimeSpan.Zero;

                        bool isExpired = timeElapsed > timeRemaining;
                        if (isExpired && x.Principal?.Identity != null)
                        {
                            var identity = (ClaimsIdentity)x.Principal.Identity;
                            Claim? accessTokenClaim = identity.FindFirst("access_token");
                            Claim? refreshTokenClaim = identity.FindFirst("refresh_token");

                            if (accessTokenClaim != null && refreshTokenClaim != null)
                            {
                                var refreshToken = refreshTokenClaim.Value;
                                var response = await new Flurl.Url(keycloakOptions.RefreshTokenUrl)
                                                              .WithHeader("Content-Type", "application/x-www-form-urlencoded")
                                                              .PostUrlEncodedAsync(new Dictionary<string, string>()
                                                                {
                                                                    {"client_id", keycloakOptions.ClientId },
                                                                    {"client_secret",keycloakOptions.ClientSecret },
                                                                    {"grant_type","refresh_token"},
                                                                    {"refresh_token",refreshToken }
                                                                });

                                if (response.StatusCode >= 200 && response.StatusCode < 400)
                                {
                                    identity.RemoveClaim(accessTokenClaim);
                                    identity.RemoveClaim(refreshTokenClaim);
                                    var stringic = await response.GetStringAsync();
                                    var tokens = await response.GetJsonAsync<TokenWrapper>()
                                                            ?? throw new UnauthorizedException();
                                    identity.AddClaims(
                                    [
                                                    new Claim("access_token", tokens.AccessToken),
                                                    new Claim("refresh_token", tokens.RefreshToken)
                                    ]);

                                    x.ShouldRenew = true;
                                }
                            }
                        }
                    }
                };
            })
            .AddOpenIdConnect(options =>
            {
                options.Authority = keycloakOptions.Authority;
                options.ClientId = keycloakOptions.ClientId;
                options.ClientSecret = keycloakOptions.ClientSecret;
                options.ResponseType = "code";
                options.Backchannel = new HttpClient(new HttpClientHandler()
                {
                    MaxResponseHeadersLength = 40000,
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
                    {
                        return true;
                    }
                })
                {
                };
                options.Backchannel.DefaultRequestHeaders.Add("Acces-Control-Allow-Origin",
                    frontendConfig.BaseUrl);
                options.BackchannelHttpHandler = new HttpClientHandler()
                {
                    ClientCertificateOptions = ClientCertificateOption.Manual,
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
                    {
                        return true;
                    }
                };
                options.SaveTokens = true;
                options.Scope.Clear();
                options.Scope.Add("openid");
                options.SkipUnrecognizedRequests = true;
                options.RequireHttpsMetadata = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    RoleClaimType = ClaimTypes.Role,
                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                };
                options.Events = new OpenIdConnectEvents
                {
                    OnTokenValidated = x =>
                    {
                        if (x.Principal != null && x.Principal.Identity != null && x.TokenEndpointResponse != null)
                        {
                            var identity = (ClaimsIdentity)x.Principal.Identity;
                            identity.AddClaims(
                            [
                                new Claim("access_token", x.TokenEndpointResponse.AccessToken),
                                new Claim("refresh_token", x.TokenEndpointResponse.RefreshToken)
                            ]);

                            if (x.Properties != null)
                            {
                                x.Properties.IsPersistent = true;
                                x.Properties.ExpiresUtc = new JwtSecurityToken(x.TokenEndpointResponse.AccessToken).ValidTo;
                            }
                        }
                        return Task.CompletedTask;
                    }
                };
            });
        }

    }
}

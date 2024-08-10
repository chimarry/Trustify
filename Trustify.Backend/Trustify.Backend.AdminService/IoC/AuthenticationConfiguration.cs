using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Trustify.Backend.AdminService.Keycloak.Models;
using Flurl.Http;
using System.Net;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.CookiePolicy;

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
                options.Cookie.HttpOnly = true;
                options.Cookie.SameSite = SameSiteMode.None;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.LoginPath = "/Account/Login";
                options.AccessDeniedPath = "/Account/AccessDenied";

                options.ExpireTimeSpan = TimeSpan.FromMinutes(2);
                options.Events = new CookieAuthenticationEvents
                {
                    // this event is fired everytime the cookie has been validated by the cookie middleware,
                    // so basically during every authenticated request
                    // the decryption of the cookie has already happened so we have access to the user claims
                    // and cookie properties - expiration, etc..
                    OnValidatePrincipal = async x =>
                    {
                        // since our cookie lifetime is based on the access token one,
                        // check if we're more than halfway of the cookie lifetime
                        var now = DateTimeOffset.UtcNow;
                        var timeElapsed = now.Subtract(x.Properties.IssuedUtc.Value);
                        var timeRemaining = x.Properties.ExpiresUtc.Value.Subtract(now);
                        bool isExpired = timeElapsed > timeRemaining;
                        if (isExpired)
                        {
                            var identity = (ClaimsIdentity)x.Principal.Identity;
                            var accessTokenClaim = identity.FindFirst("access_token");
                            var refreshTokenClaim = identity.FindFirst("refresh_token");

                            // if we have to refresh, grab the refresh token from the claims, and request
                            // new access token and refresh token
                            var refreshToken = refreshTokenClaim.Value;

                            var response = await new Flurl.Url("http://192.168.56.101:8080/realms/trustify/protocol/openid-connect/token")
                                                          .PostMultipartAsync(x => x.AddString("client_secret", "pBufFTBKcbV7n7O1ExTZDLBwvzaDOfcJ")
                                                                                    .AddString("grant_type", "refresh_token")
                                                                                    .AddString("client_id", "trustify_admin")
                                                                                    .AddString("refresh_token", refreshToken));


                            if (response.StatusCode >= 200 && response.StatusCode < 400)
                            {
                                // everything went right, remove old tokens and add new ones
                                identity.RemoveClaim(accessTokenClaim);
                                identity.RemoveClaim(refreshTokenClaim);

                                //identity.AddClaims(new[]
                                ////{
                                //                new Claim("access_token", response.),
                                //                new Claim("refresh_token", response.RefreshToken)
                                //            });

                                // indicate to the cookie middleware to renew the session cookie
                                // the new lifetime will be the same as the old one, so the alignment
                                // between cookie and access token is preserved
                                x.ShouldRenew = true;
                            }
                        }
                    }
                };
            })
            .AddOpenIdConnect(options =>
            {
                options.Authority = "https://192.168.56.101:8443/realms/trustify";
                options.ClientId = "trustify_admin";
                options.ClientSecret = "pBufFTBKcbV7n7O1ExTZDLBwvzaDOfcJ";
                options.NonceCookie.SameSite = SameSiteMode.None;
                options.CorrelationCookie.SameSite = SameSiteMode.None;
                options.NonceCookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                options.CorrelationCookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                options.ResponseType = "code";
                options.Backchannel = new HttpClient(new HttpClientHandler()
                {

                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
                    {
                        return true;
                    }
                })
                {
                };
                options.Backchannel.DefaultRequestHeaders.Add("Acces-Control-Allow-Origin",
                    "https://192.168.100.6:4200");
                options.BackchannelHttpHandler = new HttpClientHandler()
                {
                    ClientCertificateOptions = ClientCertificateOption.Manual,
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
                    {
                        return true;
                    }
                };
                options.SaveTokens = true;
                options.Scope.Add("openid");
                options.RequireHttpsMetadata = true;
                options.CallbackPath = "/users/login-callback"; // Update callback path
                options.SignedOutCallbackPath = "/users/logout-callback"; // Update signout callback path
                options.ClaimActions.MapJsonKey("preffered_username", "trustify.email");
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    RoleClaimType = ClaimTypes.Role,
                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                };
                options.Events = new OpenIdConnectEvents
                {
                    // that event is called after the OIDC middleware received the auhorisation code,
                    // redeemed it for an access token and a refresh token,
                    // and validated the identity token
                    OnTokenValidated = x =>
                    {
                        // store both access and refresh token in the claims - hence in the cookie
                        var identity = (ClaimsIdentity)x.Principal.Identity;
                        identity.AddClaims(new[]
                        {
                                new Claim("access_token", x.TokenEndpointResponse.AccessToken),
                                new Claim("refresh_token", x.TokenEndpointResponse.RefreshToken)
                            });

                        // so that we don't issue a session cookie but one with a fixed expiration
                        x.Properties.IsPersistent = true;

                        // align expiration of the cookie with expiration of the
                        // access token
                        var accessToken = new JwtSecurityToken(x.TokenEndpointResponse.AccessToken);
                        x.Properties.ExpiresUtc = accessToken.ValidTo;

                        return Task.CompletedTask;
                    }
                };
            });
        }

    }
}

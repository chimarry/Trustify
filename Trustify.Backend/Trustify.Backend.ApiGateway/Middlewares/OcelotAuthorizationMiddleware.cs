using Newtonsoft.Json;
using Ocelot.Authorization;
using Ocelot.Errors;
using Ocelot.Middleware;
using Trustify.Backend.ApiGateway.Models;

namespace Trustify.Backend.ApiGateway.Middlewares
{
    public static class OcelotAuthorizationMiddleware
    {
        public static Func<HttpContext, Func<Task>, Task> Authorize()
        {
            return async (httpContext, next) =>
            {
                bool isAuthorized = false;
                var downstreamRoute = httpContext.Items.DownstreamRoute();
                downstreamRoute.RouteClaimsRequirement.TryGetValue("roles", out string? roles);

                // Get roles
                string[] realRoles = roles.ToStringArray();
                Root? item = JsonConvert.DeserializeObject<Root>(httpContext.User.Claims.First(x => x.Type == "resource_access").Value) ?? new Root(null);
                if (item != null && item.TrustifyApp != null)
                {
                    string[] claimRoles = [.. item.TrustifyApp.Roles];
                    isAuthorized = claimRoles.Intersect(realRoles).Any();
                }
                if (!isAuthorized)
                {
                    httpContext.Items.UpsertErrors((List<Error>)([
                        new UnauthorizedError("Forbidden access")
                    ]));
                    return;
                }
                await next.Invoke();
            };
        }
    }
}

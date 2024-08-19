using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace Trustify.Backend.ApiGateway.ApiConfig
{
    /// <summary>
    ///  Implements MvcOptions extention methods in order to use a global route prefix in controllers
    /// </summary>
    public static class MvcOptionsExtensions
    {
        public static void UseGeneralRoutePrefix(this MvcOptions opts, IRouteTemplateProvider routeAttribute) =>
            opts.Conventions.Add(new RoutePrefixConvention(routeAttribute));

        public static void UseGeneralRoutePrefix(this MvcOptions opts, string prefix) =>
            opts.UseGeneralRoutePrefix(new RouteAttribute(prefix));
    }
}

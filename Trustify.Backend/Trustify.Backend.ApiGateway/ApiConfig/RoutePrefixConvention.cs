using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Routing;

namespace Trustify.Backend.ApiGateway.ApiConfig
{
    public class RoutePrefixConvention : IApplicationModelConvention
    {
        private readonly AttributeRouteModel _routePrefix;

        public RoutePrefixConvention(IRouteTemplateProvider route) =>
            _routePrefix = new AttributeRouteModel(route);

        public void Apply(ApplicationModel application)
        {
            foreach (SelectorModel selector in application.Controllers.SelectMany(c => c.Selectors))
            {
                if (selector.AttributeRouteModel != null)
                    selector.AttributeRouteModel = AttributeRouteModel.CombineAttributeRouteModel(_routePrefix, selector.AttributeRouteModel);
                else
                    selector.AttributeRouteModel = _routePrefix;
            }
        }
    }
}

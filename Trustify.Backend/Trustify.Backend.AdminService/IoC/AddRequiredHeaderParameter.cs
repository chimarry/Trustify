using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Trustify.Backend.AdminService.Models;

namespace Trustify.Backend.AdminService.IoC
{
    public class AddRequiredHeaderParameter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Parameters ??= [];
            operation.Parameters.Add(new OpenApiParameter
            {
                Name = HttpConstants.ApiKey,
                In = ParameterLocation.Header,
                Schema = new OpenApiSchema { Type = "String" },
                Description = "API key",
                Required = false
            });

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = HttpConstants.Authorization,
                In = ParameterLocation.Header,
                Schema = new OpenApiSchema { Type = "String" },
                Description = "Authorization",
                Required = false
            });
        }
    }
}

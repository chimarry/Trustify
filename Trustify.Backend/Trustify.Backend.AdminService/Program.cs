using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Trustify.Backend.AdminService.Exceptions;
using Trustify.Backend.AdminService.IoC;
using Trustify.Backend.AdminService.Keycloak.Models;
using Trustify.Backend.AdminService.Keycloak.Service;

var builder = WebApplication.CreateBuilder(args);

// Get api version
string apiVersion = builder.Configuration.GetSection("ApiSettings")["ApiVersion"] ?? throw new AppNotConfiguredException();
string apiTitle = builder.Configuration.GetSection("ApiSettings")["ApiTitle"] ?? throw new AppNotConfiguredException();

builder.ConfigureLogger();

builder.Services.AddControllers(x => { x.UseGeneralRoutePrefix($"api/v{apiVersion}"); });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc($"v{apiVersion}", new OpenApiInfo { Title = apiTitle, Version = $"v{apiVersion}" });
    string xmlFile = "Trustify.Admin.xml";
    c.IncludeXmlComments(xmlFile);
    c.OperationFilter<AddRequiredHeaderParameter>();
});

builder.Services.ConfigureAuthorization(builder.Configuration);
builder.Services.ConfigureAuthentication(builder.Configuration);
builder.Services.AddScoped<IKeycloakRoleService, KeycloakRoleService>();
builder.Services.Configure<KeycloakOptions>(builder.Configuration.GetSection("KeycloakOptions"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint($"/swagger/v{apiVersion}/swagger.json", apiTitle);
    });
}

app.UseHttpsRedirection();

//app.UseMiddleware<RequestResponseLoggingMiddleware>();

app.UseCookiePolicy();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

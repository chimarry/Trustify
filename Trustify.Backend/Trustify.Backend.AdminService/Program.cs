using Flurl;
using Flurl.Http;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Server.Kestrel.Https;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json.Serialization;
using Trustify.Backend.AdminService.AutoMapper;
using Trustify.Backend.AdminService.Exceptions;
using Trustify.Backend.AdminService.IoC;
using Trustify.Backend.AdminService.Keycloak.Models;
using Trustify.Backend.AdminService.Keycloak.Service;
using Trustify.Backend.AdminService.Middlewares;
using Trustify.Backend.AdminService.Models;

var builder = WebApplication.CreateBuilder(args);

// Get api version
string apiVersion = builder.Configuration.GetSection("ApiSettings")["ApiVersion"] ?? throw new AppNotConfiguredException();
string apiTitle = builder.Configuration.GetSection("ApiSettings")["ApiTitle"] ?? throw new AppNotConfiguredException();

builder.ConfigureLogger();

builder.Services.AddControllers(x => { x.UseGeneralRoutePrefix($"api/v{apiVersion}"); }).AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});
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
builder.Services.Configure<FrontendConfig>(builder.Configuration.GetSection("Frontend"));
builder.Services.Configure<KeycloakOptions>(builder.Configuration.GetSection("KeycloakOptions"));

builder.Services.AddScoped<IRoleService, KeycloakRoleService>();
builder.Services.AddScoped<IGroupService, KeycloakGroupService>();
builder.Services.AddScoped<IUserService, KeycloakUserService>();
builder.Services.AddScoped<IClientService, KeycloakClientService>();
builder.Services.AddScoped<IHttpService, HttpService>();
builder.Services.AddScoped<IExceptionHandler, ExceptionHandler>();
builder.Services.AddSingleton(AutoMapperConfig.CreateMapper());
builder.Services.AddHttpClient(string.Empty, _ => { })
                    .ConfigurePrimaryHttpMessageHandler(() =>
                    {
                        return new HttpClientHandler
                        {
                            ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                        };
                    });
FlurlHttp.Clients
         .WithDefaults(builder => builder.ConfigureInnerHandler(x =>
                                {
                                    x.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
                                })
                       );
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
    options.CheckConsentNeeded = context => false;
    options.HttpOnly = HttpOnlyPolicy.None;
    options.Secure = CookieSecurePolicy.Always;
    //options.Secure = _environment.IsDevelopment() ? CookieSecurePolicy.None : CookieSecurePolicy.Always;
});

builder.Services.AddCors(
    options =>
    {
        options.AddPolicy(name: "CorsPolicy",
                          policy =>
                          {
                              policy.AllowAnyOrigin()
                              .AllowAnyHeader()
                              .AllowAnyMethod();
                          });
    });

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

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseCookiePolicy();

app.UseCors("CorsPolicy");


app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }

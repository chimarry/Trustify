using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.ObjectPool;
using Newtonsoft.Json;
using Ocelot.Authorization;
using Ocelot.Configuration;
using Ocelot.DependencyInjection;
using Ocelot.Errors;
using Ocelot.Middleware;
using Ocelot.Requester;
using Serilog;
using System.Security.Claims;
using System.Text.Json.Serialization;
using Trustify.Backend.ApiGateway;
using Trustify.Backend.ApiGateway.ApiConfig;
using Trustify.Backend.ApiGateway.Middlewares;
using Trustify.Backend.ApiGateway.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
builder.Configuration.AddJsonFile("appsettings.Logging.json", optional: false, reloadOnChange: true);
builder.Services.AddControllers(x => { x.UseGeneralRoutePrefix($"api"); }).AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});

// Add Ocelot
builder.Services.AddOcelot(builder.Configuration);

builder.Host.UseSerilog((context, services, loggerConfiguration) =>
{
    loggerConfiguration
        .MinimumLevel.Information()
        .Enrich.WithProperty("ApplicationContext", "Ocelot.APIGateway")
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .ReadFrom.Configuration(context.Configuration);
});

Log.Logger = new LoggerConfiguration().Enrich.FromLogContext()
                                       .ReadFrom.Configuration(builder.Configuration).CreateLogger();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, o =>
    {
        o.MetadataAddress = "https://192.168.56.101:8443/realms/trustify/.well-known/openid-configuration";
        o.RequireHttpsMetadata = true;
        o.Authority = "https://192.168.56.101:8443/realms/trustify";
        o.Audience = "account";
        o.Backchannel = new HttpClient(new HttpClientHandler()
        {
            ServerCertificateCustomValidationCallback = (a, b, c, d) => true
        });
        o.SaveToken = true;
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
builder.Services.AddHttpClient(string.Empty, _ => { })
                    .ConfigurePrimaryHttpMessageHandler(() =>
                    {
                        return new HttpClientHandler
                        {
                            ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                        };
                    });
builder.Services.TryAddSingleton<IExceptionToErrorMapper, HttpExceptionToErrorMapper>();
var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


var configuration = new OcelotPipelineConfiguration
{
    AuthorizationMiddleware = OcelotAuthorizationMiddleware.Authorize()
};

await app.UseOcelot(configuration);

await app.RunAsync();


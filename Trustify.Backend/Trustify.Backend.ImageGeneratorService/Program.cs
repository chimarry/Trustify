using Microsoft.AspNetCore.Authentication.JwtBearer;
using Serilog;
using System.Diagnostics;
using Trustify.Backend.ImageGeneratorService.Middlewares;
using Trustify.Backend.ImageGeneratorService.Models;


var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.Logging.json", optional: false, reloadOnChange: true);

builder.Host.UseSerilog((context, services, loggerConfiguration) =>
{
    loggerConfiguration
        .MinimumLevel.Information()
        .Enrich.FromLogContext()
        .ReadFrom.Configuration(context.Configuration);
});
Serilog.Debugging.SelfLog.Enable(msg => Debug.WriteLine(msg));

builder.Services.Configure<EuropeanaConfig>(builder.Configuration.GetSection("EuropeanaConfig"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

//options.Authority = keycloakOptions.Authority;
//options.ClientId = keycloakOptions.ClientId;
//options.ClientSecret = keycloakOptions.ClientSecret;
//options.ResponseType = "code";
//options.Backchannel = new HttpClient(new HttpClientHandler()
//{
//    MaxResponseHeadersLength = 40000,
//    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
//    {
//        return true;
//    }
//})
//{
//};
//options.Backchannel.DefaultRequestHeaders.Add("Acces-Control-Allow-Origin",
//    frontendConfig.BaseUrl);
//options.BackchannelHttpHandler = new HttpClientHandler()
//{
//    ClientCertificateOptions = ClientCertificateOption.Manual,
//    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
//    {
//        return true;
//    }
//};
//options.SaveTokens = true;
//options.Scope.Clear();
//options.Scope.Add("openid");
////options.SkipUnrecognizedRequests = true;
//options.RequireHttpsMetadata = true;
//options.TokenValidationParameters = new TokenValidationParameters
//{
//    RoleClaimType = ClaimTypes.Role,
//    RequireExpirationTime = true,
//    ValidateLifetime = true,
//};



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<RequestResponseLoggingMiddleware>();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

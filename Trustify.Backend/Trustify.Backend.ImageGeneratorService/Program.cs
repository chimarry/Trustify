using Serilog;
using System.Diagnostics;
using Trustify.Backend.ImageGeneratorService.Middlewares;


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

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<RequestResponseLoggingMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();

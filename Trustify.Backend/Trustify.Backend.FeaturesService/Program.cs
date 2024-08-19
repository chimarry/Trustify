using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Trustify.Backend.FeaturesCore.DTO;
using Trustify.Backend.FeaturesCore.Services;
using Trustify.Backend.FeaturesService.IoC;
using Trustify.Backend.FeaturesService.Mapper;
using Trustify.Backend.FeaturesService.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.Logging.json", optional: false, reloadOnChange: true);

builder.Host.UseSerilog((context, services, loggerConfiguration) =>
{
    loggerConfiguration
        .MinimumLevel.Information()
        .Enrich.FromLogContext()
        .ReadFrom.Configuration(context.Configuration);
});

// Add services to the container.
builder.Services.ConfigureDb(builder.Configuration);

builder.Services.Configure<FileStorageOptions>(builder.Configuration.GetSection("FileStorageOptions"));

builder.Services.AddSingleton(AutoMapperConfig.CreateMapper());
builder.Services.AddScoped<IImageContentService, ImageContentService>();
builder.Services.AddScoped<IFileStorageService, LocalFileStorageService>();
builder.Services.AddScoped<ITextualContentService, TextualContentService>();
builder.Services.AddScoped<ISectionsService, SectionsService>();
builder.Services.AddScoped<IExceptionHandler, ExceptionHandler>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

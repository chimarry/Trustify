using Serilog;

namespace Trustify.Backend.AdminService.IoC
{
    public static class LoggerConfiguration
    {
        public static void ConfigureLogger(this WebApplicationBuilder builder)
        {
            builder.Configuration.AddJsonFile("appsettings.Logging.json", optional: false, reloadOnChange: true);
            builder.Host.UseSerilog((context, services, loggerConfiguration) =>
            {
                loggerConfiguration
                    .MinimumLevel.Information()
                    .Enrich.FromLogContext()
                    .WriteTo.Console()
                    .ReadFrom.Configuration(context.Configuration);
            });
        }
    }
}

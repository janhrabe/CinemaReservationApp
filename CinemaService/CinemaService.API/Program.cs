
using CinemaService.API.Configurations;
using RegistrationService.API.Configurations;
using Serilog.Extensions.Logging;

namespace CinemaService.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var logger = Log.Logger = new LoggerConfiguration()
                    .Enrich.FromLogContext()
                    .WriteTo.Console()
                    .CreateLogger();

            logger.Information("Starting api");

            builder.AddLoggerConfigs();

            var appLoger = new SerilogLoggerFactory(logger)
                .CreateLogger<Program>();

            builder.Services.AddOptionConfigs(builder.Configuration, appLoger, builder);
            builder.Services.AddServiceConfigs(appLoger, builder);

            builder.Services.AddFastEndpoints()
                .SwaggerDocument(o =>
                {
                    o.ShortSchemaNames = true;
                });

            var app = builder.Build();

            app.UseAppMiddlewareAndSeedDatabase();

            app.Run();
        }
    }
}

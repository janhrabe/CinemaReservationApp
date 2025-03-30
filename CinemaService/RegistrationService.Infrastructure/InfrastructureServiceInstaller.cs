using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RegistrationService.Infrastructure.Repositories;

namespace CinemaService.Infrastructure
{
    public static class InfrastructureServiceInstaller
    {
        public static IServiceCollection AddInfrastructureServices(
            this IServiceCollection services,
            ConfigurationManager config,
            ILogger logger)
        {
            services.AddDbContextFactory<AppDbContext, ApDbContextFactory>();
            services.AddScoped<AppDbContext>(sp => sp.GetRequiredService<IDbContextFactory<AppDbContext>>().CreateDbContext());

            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>))
            .AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>))

            .AddScoped(typeof(HallRepository))
            .AddScoped(typeof(IRepository<Hall>), typeof(HallRepository))
            .AddScoped(typeof(IReadRepository<Hall>), typeof(HallRepository))

            .AddScoped(typeof(RowRepository))
            .AddScoped(typeof(IRepository<Row>), typeof(RowRepository))
            .AddScoped(typeof(IReadRepository<Row>), typeof(RowRepository))

            .AddScoped(typeof(SeatRepository))
            .AddScoped(typeof(IRepository<Seat>), typeof(SeatRepository))
            .AddScoped(typeof(IReadRepository<Seat>), typeof(SeatRepository));

            logger.LogInformation("{Project} services registered", "Infrastructure");

            return services;
        }
    }
}

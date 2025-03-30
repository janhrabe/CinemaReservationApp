using System.Reflection;
using CinemaService.UseCases.Halls.Create;
using RegistrationService.Core.Entities;


namespace CinemaService.API.Configurations;
public static class MediatrConfigs
{
    public static IServiceCollection AddMediatrConfigs(this IServiceCollection services)
    {
        var mediatRAssemblies = new[]
          {
        Assembly.GetAssembly(typeof(Hall)), // Core
        Assembly.GetAssembly(typeof(CreateHallHandler)) // UseCases
      };

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(mediatRAssemblies!))
                .AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>))
                .AddScoped<IDomainEventDispatcher, MediatRDomainEventDispatcher>();

        return services;
    }
}

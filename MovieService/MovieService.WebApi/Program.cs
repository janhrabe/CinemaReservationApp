using System.Reflection;
using FastEndpoints;
using FastEndpoints.Swagger;
using JasperFx;
using Marten;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MovieService.CinemaClient;
using MovieService.Client;
using MovieService.Core.Interfaces;
using MovieService.Infrastructure.Repositories;
using MovieService.UseCases.Movie.Create;
using MovieService.UseCases.Projection.Create;
using MovieService.WebApi.Clients;
using MovieService.WebApi.Config;

namespace MovieService.WebApi
{


    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("MovieServiceConnection");

            builder.Services.Configure<CinemaServiceConfig>(builder.Configuration.GetSection("CinemaServiceConfig"));
            builder.Services.Configure<ReservationServiceConfig>(builder.Configuration.GetSection("ReservationServiceConfig"));


            builder.Services.AddMarten(opts =>
            {
                opts.Connection(connectionString);
                opts.AutoCreateSchemaObjects = AutoCreate.All;
            })
                            .UseLightweightSessions();

            builder.Services.AddScoped<IProjectionRepository, ProjectionRepository>();
            builder.Services.AddScoped<IMovieRepository, MovieRepository>();

            builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
            builder.Services.AddMediatR(Assembly.GetAssembly(typeof(CreateMovieHandler)));
            builder.Services.AddMediatR(Assembly.GetAssembly(typeof(CreateProjectionHandler)));

            builder.Services.AddHttpClient<CinemaHttpClient>();

            builder.Services.AddHttpClient<IHallsClient, HallsClient>((sp, client) =>
            {
                var options = sp.GetRequiredService<IOptions<CinemaServiceConfig>>().Value;
                var baseUrl = $"https://{options.Host}:{options.Port}";
                client.BaseAddress = new Uri(baseUrl);
            });

            builder.Services.AddHttpClient<IRowsClient, RowsClient>((sp, client) =>
            {
                var options = sp.GetRequiredService<IOptions<CinemaServiceConfig>>().Value;
                var baseUrl = $"https://{options.Host}:{options.Port}";
                client.BaseAddress = new Uri(baseUrl);
            });

            builder.Services.AddHttpClient<ISeatsClient, SeatsClient>((sp, client) =>
            {
                var options = sp.GetRequiredService<IOptions<CinemaServiceConfig>>().Value;
                var baseUrl = $"https://{options.Host}:{options.Port}";
                client.BaseAddress = new Uri(baseUrl);
            });
            builder.Services.AddHttpClient<IReservationClient, ReservationServiceClient>((sp, client) =>
            {
                var options = sp.GetRequiredService<IOptions<ReservationServiceConfig>>().Value;
                var baseUrl = $"https://{options.Host}:{options.Port}";
                client.BaseAddress = new Uri(baseUrl);
            });

            builder.Services.AddFastEndpoints();
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.SwaggerDocument(o =>
            {
                o.ShortSchemaNames = true;
            });

            builder.Services.AddOpenApi();

            builder.Services.AddAuthorization();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.UseFastEndpoints();
            app.UseSwaggerGen();
            app.Run();
        }
    }
}

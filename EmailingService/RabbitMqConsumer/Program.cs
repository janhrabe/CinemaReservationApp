using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMqConsumer.RabbitMQ;


namespace RabbitMqConsumer
{
    internal class Program
    {
        static async Task Main()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            var configuration = builder.Build();

            var services = new ServiceCollection();

            services.Configure<RabbitMQSettings>(configuration.GetSection("RabbitMQ"));

            services.AddLogging(config => config.AddConsole());

            services.AddSingleton<RabbitMQManager>(sp =>
            {
                var settings = sp.GetRequiredService<IOptions<RabbitMQSettings>>().Value;

                return new RabbitMQManager(
                    settings.HostName,
                    settings.NotificationQueueName,
                    settings.NotificationExchangeName
                    );
            });

            using var serviceProvider = services.BuildServiceProvider();
            var rabbitConsumer = serviceProvider.GetRequiredService<RabbitMQManager>();

            var cts = new CancellationTokenSource();


            try
            {
                Console.WriteLine("Listening for messages..");
                await rabbitConsumer.ConsumeMessagesContinuousAsync(cts.Token);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
using Microsoft.Extensions.Logging;
using RabbitMqConsumer.RabbitMQ;

namespace RabbitMqConsumer
{
    public class MessageHandleService(ILogger<MessageHandleService> logger)
    {
        private readonly ILogger<MessageHandleService> _logger = logger;

        public async Task HandleMessageAsync(RabbitMQMessageModel rabbitMessage)
        {
            _logger.LogInformation($"Published at: {rabbitMessage.CreatedAt}");
            Console.WriteLine($"Published at: {rabbitMessage.CreatedAt}");
            _logger.LogInformation("Handling Message...");
        }


    }
}

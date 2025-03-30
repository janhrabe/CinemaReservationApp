using Microsoft.Extensions.Logging;
using RabbitMqConsumer.RabbitMQ;

namespace RabbitMqConsumer
{
    public class MessageConsumer(RabbitMQManager rabbitMQManager, ILogger<MessageConsumer> logger)
    {
        private readonly RabbitMQManager _rabbitMqManager = rabbitMQManager;
        private readonly ILogger<MessageConsumer> _logger = logger;

        public void ConsumeMessage()
        {
            _rabbitMqManager.ConsumeMessageAsync();
            _logger.LogInformation("Consuming messages..");
        }
    }
}



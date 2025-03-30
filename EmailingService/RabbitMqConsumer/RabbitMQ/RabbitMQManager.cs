using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMqConsumer.RabbitMQ
{
    public class RabbitMQManager
    {
        private readonly ConnectionFactory _factory;
        private readonly string _notificationQueueName;
        private readonly string _notificationExchangeName;

        public RabbitMQManager(string uri, string notificationQueueName, string notificationExchangeName)
        {
            _factory = new ConnectionFactory()
            {
                Uri = new Uri(uri),
            };
            _notificationQueueName = notificationQueueName;
            _notificationExchangeName = notificationExchangeName;
        }

        public async Task ConsumeMessageAsync()
        {
            using var connection = await _factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();

            await channel.ExchangeDeclareAsync(_notificationExchangeName, ExchangeType.Direct, durable: true, autoDelete: false, arguments: null);
            await channel.QueueDeclareAsync(_notificationQueueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

            var routingKey = "reservation.created";
            await channel.QueueBindAsync(_notificationQueueName, _notificationExchangeName, routingKey);

            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.ReceivedAsync += async (mode, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var jsonDeserialized = JsonSerializer.Deserialize<RabbitMQMessageModel>(message);

                Console.WriteLine($"Received message: {message}");
                await Task.CompletedTask;
            };

            await channel.BasicConsumeAsync(queue: _notificationQueueName, autoAck: true, consumer: consumer);
        }

        public async Task ConsumeMessagesContinuousAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await ConsumeMessageAsync();
            }
        }
    }
}

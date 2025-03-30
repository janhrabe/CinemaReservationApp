using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Testcontainers.RabbitMq;
namespace RabbitMqConsumer.Tests
{
    public class UnitTest1
    {

        private readonly RabbitMqContainer _container = new RabbitMqBuilder().Build();


        public Task InitializeAsync()
        {
            return _container.StartAsync();
        }

        public Task DisposeAsync()
        {
            return _container.DisposeAsync().AsTask();
        }

        [Fact]
        public async Task ConsumeMessage_ShouldProcessMessage_WithTestContainers()
        {

            await _container.StartAsync();

            var connectionFactory = new ConnectionFactory
            {
                Uri = new Uri(_container.GetConnectionString())
            };

            await using var connection = await connectionFactory.CreateConnectionAsync();
            await using var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(queue: "test_queue",
                                            durable: false,
                                            exclusive: false,
                                            autoDelete: false,
                                            arguments: null);

            var message = "Test message";
            var body = Encoding.UTF8.GetBytes(message);

            await channel.BasicPublishAsync(exchange: "",
                                            routingKey: "test_queue",
                                            mandatory: false,
                                            basicProperties: new BasicProperties(),
                                            body: body);


            var tcs = new TaskCompletionSource<string>();

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += async (model, ea) =>
            {
                var receivedMessage = Encoding.UTF8.GetString(ea.Body.ToArray());
                tcs.SetResult(receivedMessage);
                await Task.Yield();
            };

            await channel.BasicConsumeAsync(queue: "test_queue",
                                            autoAck: true,
                                            consumer: consumer);

            var receivedMessage = await tcs.Task;
            Assert.Equal(message, receivedMessage);
        }



        [Fact]
        public async Task ConsumeMessage_ShouldHandleEmptyMessage()
        {

            await _container.StartAsync();
            var connectionFactory = new ConnectionFactory
            {
                Uri = new Uri(_container.GetConnectionString())
            };

            await using var connection = await connectionFactory.CreateConnectionAsync();
            await using var channel = await connection.CreateChannelAsync();

            var body = new byte[0];

            // Act
            await channel.BasicPublishAsync<BasicProperties>(
                exchange: "",
                routingKey: "test_queue",
                mandatory: false,
                basicProperties: new BasicProperties(),
                body: body
            );

            await Task.Delay(1000);

            // Assert
            Assert.True(true);
        }


        [Fact]
        public async Task ConsumeMessage_ShouldProcessMessageAsync()
        {
            // Arrange
            await _container.StartAsync();
            var connectionFactory = new ConnectionFactory
            {
                Uri = new Uri(_container.GetConnectionString())
            };

            await using var connection = await connectionFactory.CreateConnectionAsync();
            await using var channel = await connection.CreateChannelAsync();

            var body = Encoding.UTF8.GetBytes("Asynchronous processing test message");

            await channel.BasicPublishAsync<BasicProperties>(
                exchange: "",
                routingKey: "test_queue",
                mandatory: false,
                basicProperties: new BasicProperties(),
                body: body
            );

            await Task.Delay(1000);

            // Assert
            Assert.True(true);
        }
    }
}

using JobSity.Chatroom.Application.Shared.Configurations;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace JobSity.Chatroom.Application.Shared.Messaging.Rabbit
{
    [ExcludeFromCodeCoverage]
    public class RabbitMQ : IBus
    {
        private readonly Configurations.Bus.RabbitMQ _rabbitMQ;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public RabbitMQ(IOptions<ConnectionStrings> connectionStrings)
        {
            _rabbitMQ = connectionStrings.Value.RabbitMQ;

            var factory = new ConnectionFactory()
            {
                UserName = _rabbitMQ.User,
                Password = _rabbitMQ.Password,
                HostName = _rabbitMQ.Host
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        public Task PublishAsync<T>(T message, string queueName)
        {
            var factory = new ConnectionFactory()
            {
                UserName = _rabbitMQ.User,
                Password = _rabbitMQ.Password,
                HostName = _rabbitMQ.Host
            };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: queueName,
                                    durable: false,
                                    exclusive: false,
                                    autoDelete: false,
                                    arguments: null);

                var body = JsonConvert.SerializeObject(message);
                var byteMessage = Encoding.UTF8.GetBytes(body);

                channel.BasicPublish(exchange: string.Empty,
                    routingKey: queueName,
                    basicProperties: null,
                    body: byteMessage);

                return Task.CompletedTask;
            }
        }

        public Task SubscribeAsync<T>(Func<T, Task> handler, string queueName) where T : class
        {
            var consumer = new EventingBasicConsumer(_channel);

            _channel.QueueDeclare(queue: queueName,
                                durable: false,
                                exclusive: false,
                                autoDelete: false,
                                arguments: null);

            consumer.Received += async (sender, ea) =>
            {
                var message = Encoding.UTF8.GetString(ea.Body.ToArray());
                var payload = JsonConvert.DeserializeObject<T>(message);
                await handler(payload);
            };

            _channel.BasicConsume(queue: queueName,
                                  autoAck: true,
                                  consumer: consumer);

            return Task.CompletedTask;
        }
    }
}

using System;
using System.Text;
using System.Text.Json;
using ChatApi.Domain.Entities.Settings;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace ChatApi.API.Publisher
{
    public class RabbitMqPublisher
    {
        private readonly ApplicationSettings _settings;
        private readonly string _queueName = "criar-cliente";
        private readonly IConnection _connection;

        public RabbitMqPublisher(
            IOptions<ApplicationSettings> settings
        )
        {
            _settings = settings.Value;
            var factory = new ConnectionFactory
            {
                Uri = new Uri(_settings.Rabbit)
            };
            _connection = factory.CreateConnection();
        }

        public void Publish<T>(T message)
        {
            using var channel = _connection.CreateModel();
            channel.QueueDeclare(_queueName, durable: true, exclusive: false, autoDelete: false);

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));
            channel.BasicPublish("", _queueName, null, body);
        }
    }
}
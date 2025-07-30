using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ChatApi.Infrastructure.Interfaces
{
    public class RabbitConsumer<T> where T : class
    {
        private readonly IRepository<T> _repository;
        private readonly string _queueName;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public RabbitConsumer(IRepository<T> repository, IConnection connection)
        {
            _repository = repository;
            _connection = connection;
            _channel = _connection.CreateModel();
            _queueName = typeof(T).Name.ToLower();

            _channel.QueueDeclare(queue: _queueName,
                                  durable: true,
                                  exclusive: false,
                                  autoDelete: false,
                                  arguments: null);
        }

        public void Start()
        {
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var entity = JsonSerializer.Deserialize<T>(message);
                if (entity != null)
                {
                    await _repository.CreateAsync(entity);
                }
                _channel.BasicAck(ea.DeliveryTag, false);
            };

            _channel.BasicConsume(queue: _queueName,
                                 autoAck: false,
                                 consumer: consumer);
        }
    }
}
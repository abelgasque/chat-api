using System.Threading.Tasks;

namespace ChatApi.Application.Interfaces
{
    public interface IRabbitMqPublisher
    {
        Task PublishAsync<T>(string routingKey, T message);
    }
}
using RabbitMQ.Client;
namespace RabbitmqDotNetCore.Rabbitmq
{
    public interface IRabbitmqConnect
    {
        IConnection CreateConnection();
    }
}

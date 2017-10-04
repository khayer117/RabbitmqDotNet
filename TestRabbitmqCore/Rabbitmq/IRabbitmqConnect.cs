using RabbitMQ.Client;
namespace RabbitmqDotNetCore.Rabbitmq
{
    public interface IRabbitmqConnect
    {
        IConnection CreateConnection();
        IModel SetExchange(string exchangeName, string exchangeType);
        IModel SetQueue(string exchangeName, string queueName, string routingKey);
    }
}

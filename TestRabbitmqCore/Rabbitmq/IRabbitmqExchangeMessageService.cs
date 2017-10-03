using RabbitmqDotNetCore.Infrastructure;

namespace RabbitmqDotNetCore.Rabbitmq
{
    public interface IRabbitmqExchangeMessageService
    {
        void SetExchange(string exchangeName, string exchangeType);
        void SetDefaultExchange();
        void BasicPublish(string exchangeName,IQueueCommand command,string routingKey="");
        void SetQueue(string exchangeName, string queueName, string routingKey);
        void ReceiveMessages(string quaueName);
    }
}

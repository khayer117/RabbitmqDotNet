using RabbitmqDotNetCore.Infrastructure;

namespace RabbitmqDotNetCore.Rabbitmq
{
    public interface IRabbitmqExchangeMessageService
    {
        void SetExchange();
        void BasicPublic(string exchangeName,IQueueCommand command);
        void ReceiveMessages(string quaueName);
    }
}

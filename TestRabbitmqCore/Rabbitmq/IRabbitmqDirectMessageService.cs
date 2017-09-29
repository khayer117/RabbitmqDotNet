using RabbitmqDotNetCore.Core;
namespace RabbitmqDotNetCore.Rabbitmq
{
    public interface IRabbitmqDirectMessageService
    {
        void EnQueue(string queueName, IQueueCommand command);
    }
}

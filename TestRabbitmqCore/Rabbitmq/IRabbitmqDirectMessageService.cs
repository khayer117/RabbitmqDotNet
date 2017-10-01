using RabbitmqDotNetCore.Infrastructure;

namespace RabbitmqDotNetCore.Rabbitmq
{
    public interface IRabbitmqDirectMessageService
    {
        void EnQueue(string queueName, IQueueCommand command);
    }
}

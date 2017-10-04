namespace RabbitmqDotNetCore.Rabbitmq
{
    public interface IRabbitmqConsumerService
    {
        /// <summary>
        /// Declare Queue, then bind with exchage using routing key 
        /// </summary>
        void SetQueue(string exchangeName, string exchangeType, string queueName, string routingKey);
        
        /// <summary>
        /// Attached Reciever for specific queue 
        /// </summary>
        void ReceiveMessages(string quaueName);
    }
}

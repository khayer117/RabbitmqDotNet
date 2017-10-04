namespace RabbitmqDotNetCore.Rabbitmq
{
    public class QueueConfig
    {
        public string QueueName { get; set; }
        public string RoutingKey { get; set; }
        public bool IsDurable { get; set; }

        public QueueConfig(string queueName,string routingKey = "",bool isDurable=true)
        {
            this.QueueName = queueName;
            this.RoutingKey = routingKey;
            this.IsDurable = isDurable;
        }
    }
}

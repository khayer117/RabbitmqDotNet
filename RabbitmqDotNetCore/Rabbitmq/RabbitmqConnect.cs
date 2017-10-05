using RabbitMQ.Client;

namespace RabbitmqDotNetCore.Rabbitmq
{
    public class RabbitmqConnect:IRabbitmqConnect
    {
        public IConnection CreateConnection()
        {
            ConnectionFactory connectionFactory = new ConnectionFactory();
            connectionFactory.HostName = GlobalDictionary.RabbitmqHost;
            connectionFactory.UserName = GlobalDictionary.RabbitmqUserName;
            connectionFactory.Password = GlobalDictionary.RabbitmqPassword;

            return connectionFactory.CreateConnection();
        }

        public IModel SetExchange(string exchangeName, string exchangeType)
        {
            var connection = CreateConnection();
            var channel = connection.CreateModel();
            channel.ExchangeDeclare(exchangeName, exchangeType, true, false, null);

            return channel;
        }

        public IModel SetQueue(string exchangeName, string queueName, string routingKey)
        {
            var connection = CreateConnection();
            var channel = connection.CreateModel();

            channel.QueueDeclare(queueName, true, false, false, null);
            channel.QueueBind(queueName, exchangeName, routingKey);

            return channel;
        }
    }
}

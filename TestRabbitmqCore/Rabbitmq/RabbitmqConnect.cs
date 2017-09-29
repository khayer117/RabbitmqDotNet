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

            //var factory = new ConnectionFactory() { HostName = "localhost" };
            //return factory.CreateConnection();
        }
    }
}

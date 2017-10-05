using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitmqDotNetCore.Infrastructure;
using System.Collections.Generic;
using System.Text;

namespace RabbitmqDotNetCore.Rabbitmq
{
    public class RabbitmqProducerService: IRabbitmqProducerService
    {
        private IRabbitmqConnect rabbitmqConnect;
        private IModel Channel;
        private ILogger logger;
        private const string commandTypeName = "commandType";

        public RabbitmqProducerService(IRabbitmqConnect rabbitmqConnect,
            ILogger logger)
        {
            this.rabbitmqConnect = rabbitmqConnect;
            this.logger = logger;
        }

        public void SetExchange(string exchangeName,string exchangeType)
        {
            this.Channel = this.rabbitmqConnect.SetExchange(exchangeName,
                exchangeType);
        }
        public void SetDefaultExchange()
        {
            var connection = this.rabbitmqConnect.CreateConnection();
            var channel = connection.CreateModel();

            foreach (var exchangeConfig in GlobalDictionary.ExchangeConfig)
            {
                var exchangeName = exchangeConfig.Key;
                channel.ExchangeDeclare(exchangeName, ExchangeType.Direct, true, false, null);

                foreach (var queueConfig in exchangeConfig.Value)
                {
                    channel.QueueDeclare(queueConfig.QueueName, queueConfig.IsDurable, false, false, null);
                    channel.QueueBind(queueConfig.QueueName, exchangeName, queueConfig.RoutingKey);
                }
            }

            this.Channel = channel;
        }
        public void BasicPublish(string exchangeName,IQueueCommand command, string routingKey = "")
        {
            var basicProperties = this.Channel.CreateBasicProperties();
            basicProperties.Headers = new Dictionary<string, object>
            {
                {commandTypeName, command.GetType().AssemblyQualifiedName}
            };

            var message = JsonConvert.SerializeObject(command);
            var body = Encoding.UTF8.GetBytes(message);

            var address = new PublicationAddress(ExchangeType.Fanout, exchangeName, routingKey);

            this.Channel.BasicPublish(address, basicProperties, body);
            this.logger.Info($"Publish to {exchangeName}");
        }
    }
}

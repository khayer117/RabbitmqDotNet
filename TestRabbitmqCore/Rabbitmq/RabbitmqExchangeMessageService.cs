using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RabbitmqDotNetCore.Infrastructure;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitmqDotNetCore.Rabbitmq
{
    public class RabbitmqExchangeMessageService: IRabbitmqExchangeMessageService
    {
        private IRabbitmqConnect rabbitmqConnect;
        private IModel Channel;
        private ICommandBus commandBus;
        private IActionCommandDispacher actionCommandDispacher;
        private ILogger logger;
        private const string commandTypeName = "commandType";

        public RabbitmqExchangeMessageService(IRabbitmqConnect rabbitmqConnect,
            ICommandBus commandBus,
            IActionCommandDispacher actionCommandDispacher,
            ILogger logger)
        {
            this.rabbitmqConnect = rabbitmqConnect;
            this.commandBus = commandBus;
            this.actionCommandDispacher = actionCommandDispacher;
            this.logger = logger;
        }

        public void SetExchange(string exchangeName,string exchangeType)
        {
            var connection = this.rabbitmqConnect.CreateConnection();
            var channel = connection.CreateModel();
            channel.ExchangeDeclare(exchangeName, exchangeType, true, false, null);

            this.Channel = channel;
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
        public void SetQueue(string exchangeName, string queueName, string routingKey)
        {
            var connection = this.rabbitmqConnect.CreateConnection();
            var channel = connection.CreateModel();

            channel.QueueDeclare(queueName, true, false, false, null);
            channel.QueueBind(queueName, exchangeName, routingKey);
        }
        public void ReceiveMessages(string quaueName)
        {
            var connection = this.rabbitmqConnect.CreateConnection();
            var channel = connection.CreateModel();
            channel.BasicQos(0, 1, false);
            var eventingBasicConsumer = new EventingBasicConsumer(channel);

            eventingBasicConsumer.Received += (sender, basicDeliveryEventArgs) =>
            {
                var message = Encoding.UTF8.GetString(basicDeliveryEventArgs.Body);

                var basicProperties = basicDeliveryEventArgs.BasicProperties;

                if (basicProperties.Headers!=null && basicProperties.Headers.ContainsKey(commandTypeName))
                {
                    var commandTypeBytes = basicProperties.Headers[commandTypeName] as byte[];
                    var commandType = Encoding.UTF8.GetString(commandTypeBytes);

                    var cType = Type.GetType(commandType);
                    var recieveCommand = JsonConvert.DeserializeObject(message, cType);

                    this.logger.Info($"Message received from {quaueName}: {JsonConvert.SerializeObject(recieveCommand)}");
                    // Method 1: Using Generic Command buss
                    //this.commandBus.Send<NoCommandResult>(recieveCommand);

                    // Method 2: Using Action command dispather. No return type.
                    this.actionCommandDispacher.Send(recieveCommand);
                }
                else
                {
                    this.logger.Info($"Skup message. Invalid {commandTypeName} type in property header.");
                }
                
                channel.BasicAck(basicDeliveryEventArgs.DeliveryTag, false);
            };

            channel.BasicConsume(quaueName, false, eventingBasicConsumer);
            this.logger.Info($"Waiting for messeage from {quaueName}");
        }
    }
}

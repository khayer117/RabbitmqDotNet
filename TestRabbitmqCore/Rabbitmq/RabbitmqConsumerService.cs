using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitmqDotNetCore.Infrastructure;
using System;
using System.Text;

namespace RabbitmqDotNetCore.Rabbitmq
{
    public class RabbitmqConsumerService:IRabbitmqConsumerService
    {
        private IRabbitmqConnect rabbitmqConnect;
        private IModel Channel;
        private ICommandBus commandBus;
        private IActionCommandDispacher actionCommandDispacher;
        private ILogger logger;
        private const string commandTypeName = "commandType";

        public RabbitmqConsumerService(IRabbitmqConnect rabbitmqConnect,
            ICommandBus commandBus,
            IActionCommandDispacher actionCommandDispacher,
            ILogger logger)
        {
            this.rabbitmqConnect = rabbitmqConnect;
            this.commandBus = commandBus;
            this.actionCommandDispacher = actionCommandDispacher;
            this.logger = logger;
        }
        public void SetQueue(string exchangeName, string exchangeType, string queueName, string routingKey)
        {
            this.rabbitmqConnect.SetExchange(exchangeName, exchangeType);
            this.rabbitmqConnect.SetQueue(exchangeName, queueName, routingKey);
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

                if (basicProperties.Headers != null && basicProperties.Headers.ContainsKey(commandTypeName))
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

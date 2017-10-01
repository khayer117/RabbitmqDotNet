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
        private const string commandTypeName = "commandType";

        public RabbitmqExchangeMessageService(IRabbitmqConnect rabbitmqConnect,
            ICommandBus commandBus)
        {
            this.rabbitmqConnect = rabbitmqConnect;
            this.commandBus = commandBus;
        }

        public void SetExchange()
        {
            var connection = this.rabbitmqConnect.CreateConnection();
            var channel = connection.CreateModel();

            foreach (var exchangeConfig in GlobalDictionary.ExchangeConfig)
            {
                var exchangeName = exchangeConfig.Key;
                channel.ExchangeDeclare(exchangeName, ExchangeType.Fanout, true, false, null);

                foreach (var quaueName in exchangeConfig.Value)
                {
                    channel.QueueDeclare(quaueName, true, false, false, null);
                    channel.QueueBind(quaueName, exchangeName, "");
                }
            }

            this.Channel = channel;
        }
        public void BasicPublic(string exchangeName,IQueueCommand command)
        {
            var basicProperties = this.Channel.CreateBasicProperties();
            basicProperties.Headers = new Dictionary<string, object>
            {
                {commandTypeName, command.GetType().AssemblyQualifiedName}
            };

            var message = JsonConvert.SerializeObject(command);
            var body = Encoding.UTF8.GetBytes(message);

            var address = new PublicationAddress(ExchangeType.Fanout, exchangeName, "");

            this.Channel.BasicPublish(address, basicProperties, body);
            Console.WriteLine($"Publish to {exchangeName}");
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
                    var customerCommand = JsonConvert.DeserializeObject(message, cType);

                    Console.WriteLine(string.Concat($"Message received from {quaueName}: ",
                                JsonConvert.SerializeObject(customerCommand)));

                    this.commandBus.Send<NoCommandResult>(customerCommand);
                }
                else
                {
                    Console.WriteLine($"Skup message. Invalid {commandTypeName} type in property header.");
                }
                
                channel.BasicAck(basicDeliveryEventArgs.DeliveryTag, false);
            };

            channel.BasicConsume(quaueName, false, eventingBasicConsumer);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RabbitmqDotNetCore.Core;
using RabbitMQ.Client;

namespace RabbitmqDotNetCore.Rabbitmq
{
    public class RabbitmqDirectMessageService:IRabbitmqDirectMessageService
    {
        private IRabbitmqConnect rabbitmqConnect;
        private const string commandTypeName = "commandType";

        public RabbitmqDirectMessageService(IRabbitmqConnect rabbitmqConnect)
        {
            this.rabbitmqConnect = rabbitmqConnect;
        }

        private string GetExchangeName()
        {
            return GlobalDictionary.DefaultExchangeName;
        }

        private IModel CreateQueue(string queueName)
        {
            var connection = this.rabbitmqConnect.CreateConnection();
            var channel = connection.CreateModel();

            channel.QueueDeclare(queue: queueName,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            return channel;
        }

        public void EnQueue(string queueName, IQueueCommand command)
        {
            using (var channel = CreateQueue(queueName))
            {

                var basicProperties = channel.CreateBasicProperties();
                basicProperties.Headers = new Dictionary<string, object>
                {
                    {commandTypeName, command.GetType().AssemblyQualifiedName}
                };

                var message = JsonConvert.SerializeObject(command);
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: GetExchangeName(),
                    routingKey: queueName,
                    basicProperties: null,
                    body: body);

                Console.WriteLine("EnQueqe to ",queueName);
            }
        }
    }
}

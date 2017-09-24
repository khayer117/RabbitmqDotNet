using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitmqDotNetCore;

namespace RabbitmqProducer
{
    class Program
    {
        static void Main(string[] args)
        {

            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "hello",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var customerUpdate = new UpdateCustomerCommand()
                {
                    CustomerId = 101,
                    CustomerName = "Test Name"
                };

                var basicProperties = channel.CreateBasicProperties();
                basicProperties.Headers = new Dictionary<string, object>
                {
                    {"commandType", customerUpdate.GetType().AssemblyQualifiedName}
                };


                string message = JsonConvert.SerializeObject(customerUpdate);
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: "hello",
                                     basicProperties: basicProperties,
                                     body: body);
                Console.WriteLine(" [x] Sent {0}", message);
            }

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}

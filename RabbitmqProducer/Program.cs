using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitmqDotNetCore;

namespace RabbitmqProducer
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = CreateContainer();

            while (true)
            {
                var testProducer = container.Resolve<TestProducer>();

                // Example 1: through predefine queqe and exchange bind
                testProducer.TestPreConfigExchangeMessage();

                // Example 2: through only exchase, Consumer will bind the queue.
                testProducer.TestPostConfigExchangeMessage();

                Console.WriteLine("Type [q] to exit.");
                var value = Console.ReadLine();
                if (!string.IsNullOrEmpty(value) && value.Equals("q", StringComparison.CurrentCultureIgnoreCase))
                {
                    break;
                }
            }

        }
        private static IContainer CreateContainer()
        {
            var builder = new ContainerBuilder();

            var assemblies = AssembliesProvider.Instance.Assemblies.ToArray();
            builder.RegisterAssemblyModules(assemblies);

            var container = builder.Build();

            return container;
        }
    }
}

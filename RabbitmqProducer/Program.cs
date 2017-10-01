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

            //new TestProducer().TestDirectMessage();
            var testProducer = container.Resolve<TestProducer>();
            testProducer.TestExchangeMessage();
            
            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
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

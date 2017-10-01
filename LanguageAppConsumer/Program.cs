using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using RabbitmqDotNetCore;
using RabbitmqDotNetCore.Rabbitmq;

namespace LanguageAppConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = CreateContainer();

            var exchangeService = container.Resolve<RabbitmqExchangeMessageService>();
            exchangeService.ReceiveMessages(GlobalDictionary.QueueExchangeLanguageApp);
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

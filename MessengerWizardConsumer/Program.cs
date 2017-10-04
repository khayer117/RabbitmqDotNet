using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using RabbitmqDotNetCore;
using RabbitmqDotNetCore.Rabbitmq;

namespace MessengerWizardConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = CreateContainer();

            Console.WriteLine("Messenger App Consumer started.");
            var rabbitmqConsumerService = container.Resolve<IRabbitmqConsumerService>();

            //Queued has been preconfired in producer end.
            Task.Run(() => rabbitmqConsumerService.ReceiveMessages(GlobalDictionary.QueueDataSyncExchangeMessengerWizard));

            //Configure queued
            rabbitmqConsumerService.SetQueue(GlobalDictionary.DataSyncDirectExchange,
                RabbitmqExchangeType.Direct,
                GlobalDictionary.QueueSyncFileSettingExchange,
                GlobalDictionary.RoutingKeyDataSyncFileSetting);

            Task.Run(() => rabbitmqConsumerService.ReceiveMessages(GlobalDictionary.QueueSyncFileSettingExchange));

            Console.ReadKey();
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

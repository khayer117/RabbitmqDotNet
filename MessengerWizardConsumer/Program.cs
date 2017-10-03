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

            //var testSync = container.Resolve<TestMWDataSync>();
            //testSync.TestCommand().Wait();
            Console.WriteLine("Messenger App Consumer started.");
            var exchangeService = container.Resolve<IRabbitmqExchangeMessageService>();

            Task.Run(() => exchangeService.ReceiveMessages(GlobalDictionary.QueueDataSyncExchangeMessengerWizard));

            //Post config method
            exchangeService.SetQueue(GlobalDictionary.DataSyncDirectExchange,
                GlobalDictionary.QueueSyncFileSettingExchange,
                GlobalDictionary.RoutingKeyDataSyncFileSetting);

            Task.Run(() => exchangeService.ReceiveMessages(GlobalDictionary.QueueSyncFileSettingExchange));

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

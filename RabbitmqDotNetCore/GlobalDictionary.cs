using RabbitmqDotNetCore.Rabbitmq;
using System.Collections.Generic;

namespace RabbitmqDotNetCore
{
    public class GlobalDictionary
    {
        public const string QueueDirectMessengerWizardDataSync = "datasync";
        public const string QueueDataSyncExchangeMessengerWizard = "messengerwizard.queue.datasync";
        public const string QueueDataSyncExchangeLanguageApp = "languageapp.queue.datasync";
        public const string QueueSyncFileSettingExchange = "languageapp.queue.filesetting";

        public const string RabbitmqUserName = "guest";
        public const string RabbitmqPassword = "guest";
        public const string RabbitmqHost = "localhost";
        public const string DefaultExchangeName = "";

        public const string DataSyncFanoutExchange = "datasync.fanout.exchange";
        public const string DataSyncDirectExchange = "datasync.direct.exchange";

        public const string RoutingKeyDataSyncTableData = "datasyncTableData";
        public const string RoutingKeyDataSyncFileSetting = "datasyncFileSetting";

        public static Dictionary<string,ICollection<QueueConfig>> ExchangeConfig = new Dictionary<string, ICollection<QueueConfig>>()
        {
            {
                DataSyncDirectExchange , new List<QueueConfig>()
                                        {
                                            new QueueConfig(QueueDataSyncExchangeMessengerWizard,RoutingKeyDataSyncTableData),
                                            new QueueConfig(QueueDataSyncExchangeLanguageApp,RoutingKeyDataSyncTableData)
                                        }
            }   
        };

        public static string[] AssemblyNames = new[]
        {
            "RabbitmqDotNetCore",
            "RabbitmqProducer",
            "LanguageAppConsumer",
            "MessengerWizardConsumer",
            "RabbitmqConsumer"
        };
    }
}

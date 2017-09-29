using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitmqDotNetCore
{
    public class GlobalDictionary
    {
        public const string QueueDirectMessengerWizardDataSync = "datasync";
        public const string QueueExchangeMessengerWizard = "messengerwizard.queue.datasync";
        public const string QueueExchangeLanguageApp = "languageapp.queue.datasync";

        public const string RabbitmqUserName = "guest";
        public const string RabbitmqPassword = "guest";
        public const string RabbitmqHost = "localhost";
        public const string DefaultExchangeName = "";

        public const string DataSyncFanoutExchange = "datasync.fanout.exchange";
        public static Dictionary<string,string[]> ExchangeConfig = new Dictionary<string, string[]>()
        {
            {DataSyncFanoutExchange,new []{ QueueExchangeMessengerWizard, QueueExchangeLanguageApp}}
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

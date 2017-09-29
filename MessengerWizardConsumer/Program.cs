using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitmqDotNetCore;
using RabbitmqDotNetCore.Rabbitmq;

namespace MessengerWizardConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            var exchangeService = new RabbitmqExchangeMessageService(new RabbitmqConnect());
            
            exchangeService.ReceiveMessages(GlobalDictionary.QueueExchangeMessengerWizard);
        }
    }
}

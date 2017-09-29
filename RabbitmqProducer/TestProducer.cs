using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitmqDotNetCore;
using RabbitmqDotNetCore.Core;
using RabbitmqDotNetCore.Rabbitmq;

namespace RabbitmqProducer
{
    public class TestProducer
    {
        private readonly IRabbitmqExchangeMessageService rabbitmqExchangeMessageService;

        public TestProducer(IRabbitmqExchangeMessageService rabbitmqExchangeMessageService)
        {
            this.rabbitmqExchangeMessageService = rabbitmqExchangeMessageService;

        }
        public void TestDirectMessage()
        {
            var command = new UpdatePublicationOwner()
            {
                Name = "Test Name",
                ObjectId = "334343"
            };

            var rabbitmqService = new RabbitmqDirectMessageService(new RabbitmqConnect());
            rabbitmqService.EnQueue(GlobalDictionary.QueueDirectMessengerWizardDataSync,command);
        }

        public void TestExchangeMessage()
        {
            
            this.rabbitmqExchangeMessageService.SetExchange();

            while (true)
            {
                Console.WriteLine("Name(q for quit):");
                var name = Console.ReadLine();
                if (string.IsNullOrEmpty(name) || name.Equals("q"))
                {
                    break;
                }

                var command = new UpdatePublicationOwner()
                {
                    Name = name,
                    ObjectId = (new Random()).Next().ToString()
                };
                this.rabbitmqExchangeMessageService.BasicPublic(GlobalDictionary.DataSyncFanoutExchange, command);
            }
        }
    }
}

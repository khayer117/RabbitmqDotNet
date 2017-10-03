using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitmqDotNetCore;
using RabbitmqDotNetCore.Features;
using RabbitmqDotNetCore.Infrastructure;
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
            var command = new UpdatePublicationOwnerCommand()
            {
                Name = "Test Name",
                ObjectId = "334343"
            };

            var rabbitmqService = new RabbitmqDirectMessageService(new RabbitmqConnect());
            rabbitmqService.EnQueue(GlobalDictionary.QueueDirectMessengerWizardDataSync,command);
        }

        public void TestPreConfigExchangeMessage()
        {
            
            this.rabbitmqExchangeMessageService.SetDefaultExchange();

            
            var updatePublicationOwnerCommand = new UpdatePublicationOwnerCommand()
            {
                Name = "Fx home",
                ObjectId = (new Random()).Next().ToString()
            };

            this.rabbitmqExchangeMessageService.BasicPublish(GlobalDictionary.DataSyncFanoutExchange,
                updatePublicationOwnerCommand,
                GlobalDictionary.RoutingKeyDataSyncTableData);
        }

        public void TestPostConfigExchangeMessage()
        {
            this.rabbitmqExchangeMessageService.SetExchange(GlobalDictionary.DataSyncDirectExchange,RabbitmqExchangeType.Direct);

            var updateFileSettingCommand = new UpdateFileSettingCommand()
            {
                IsHomeOwnerModalOn = true
            };

            this.rabbitmqExchangeMessageService.BasicPublish(GlobalDictionary.DataSyncDirectExchange,
                updateFileSettingCommand,
                GlobalDictionary.RoutingKeyDataSyncFileSetting);
        }
    }
}

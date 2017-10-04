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
        private readonly IRabbitmqProducerService rabbitmqExchangeMessageService;

        public TestProducer(IRabbitmqProducerService rabbitmqExchangeMessageService)
        {
            this.rabbitmqExchangeMessageService = rabbitmqExchangeMessageService;

        }
        public void TestPreConfigExchangeMessage()
        {
            
            this.rabbitmqExchangeMessageService.SetDefaultExchange();

            
            var updatePublicationOwnerCommand = new UpdatePublicationOwnerCommand()
            {
                Name = "Fx home",
                ObjectId = (new Random()).Next().ToString()
            };

            this.rabbitmqExchangeMessageService.BasicPublish(GlobalDictionary.DataSyncDirectExchange,
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

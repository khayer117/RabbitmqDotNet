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
        private readonly IRabbitmqProducerService rabbitmqProducerService;

        public TestProducer(IRabbitmqProducerService rabbitmqProducerService)
        {
            this.rabbitmqProducerService = rabbitmqProducerService;

        }
        public void TestPreConfigExchangeMessage()
        {
            
            this.rabbitmqProducerService.SetDefaultExchange();

            
            var updatePoCommand = new UpdatePoCommand()
            {
                Name = "Fx home",
                ObjectId = (new Random()).Next().ToString()
            };

            this.rabbitmqProducerService.BasicPublish(GlobalDictionary.DataSyncDirectExchange,
                updatePoCommand,
                GlobalDictionary.RoutingKeyDataSyncTableData);
        }

        public void TestPostConfigExchangeMessage()
        {
            this.rabbitmqProducerService.SetExchange(GlobalDictionary.DataSyncDirectExchange,RabbitmqExchangeType.Direct);

            var updateFileSettingCommand = new UpdateFileSettingCommand()
            {
                IsStartupModalOn = true
            };

            this.rabbitmqProducerService.BasicPublish(GlobalDictionary.DataSyncDirectExchange,
                updateFileSettingCommand,
                GlobalDictionary.RoutingKeyDataSyncFileSetting);
        }
    }
}

﻿using RabbitmqDotNetCore.Infrastructure;

namespace RabbitmqDotNetCore.Rabbitmq
{
    public interface IRabbitmqProducerService
    {
        /// <summary>
        /// Declare Exchange 
        /// </summary>
        void SetExchange(string exchangeName, string exchangeType);
        
        /// <summary>
        /// Declare exchage and Queue from predefine configuration
        /// </summary>
        void SetDefaultExchange();
        
        /// <summary>
        /// Publish command to specific Exchange 
        /// </summary>
        void BasicPublish(string exchangeName,IQueueCommand command,string routingKey="");
    }
}

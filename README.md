# Rabbitmq Dot Net
RabbitmqDotNet is a complete startup project for Rabbitmq using .NET. This contains simple but rich library using command segrigation technique. The core api of RabbitmqDotNet has been wrapped to simple services. Producer will put command and consumer will recive corresponding command to handle. No need custom object casting and refelction.

## Knowledge
Need basic idea on [Rabbitmq](https://www.rabbitmq.com/tutorials/tutorial-one-dotnet.html).

## Prerequisite
* Install RabbitMq in local machine. Follow [this](https://www.rabbitmq.com/install-windows.html) instruction.
* Install RabbitMQ.Client using Nuget

## How to Start
* Producer: Simple publish to exchage
```csharp
this.rabbitmqProducerService.SetExchange(GlobalDictionary.DataSyncDirectExchange,RabbitmqExchangeType.Direct);
var updateFileSettingCommand = new UpdateFileSettingCommand()
{
    IsStartupModalOn = true
};
this.rabbitmqProducerService.BasicPublish(GlobalDictionary.DataSyncDirectExchange,
                updateFileSettingCommand,
                GlobalDictionary.RoutingKeyDataSyncFileSetting);
```
* Consumer: Reciever from Queue
```csharp
rabbitmqConsumerService.SetQueue(GlobalDictionary.DataSyncDirectExchange,
	RabbitmqExchangeType.Direct,
	GlobalDictionary.QueueSyncFileSettingExchange,
	GlobalDictionary.RoutingKeyDataSyncFileSetting);
Task.Run(() => rabbitmqConsumerService.ReceiveMessages(GlobalDictionary.QueueSyncFileSettingExchange));
```
* Consumer Command Handler
```csharp
public class UpdateFileSettingHandler:IActionCommandHandler<UpdateFileSettingCommand>
{
    private ILogger logger;
    public UpdateFileSettingHandler(ILogger logger)
    {
        this.logger = logger;
    }
    public async Task Handle(UpdateFileSettingCommand command)
    {
        this.logger.Info($"Handle File setting: {command.IsStartupModalOn}");
    }
}
```
### Special Note
If Rabbitmq client get timeout exception to connect to queue, restart the rabbitmq service from Task Manager > Rabbitmq. This can be happen first time after window start.
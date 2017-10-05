# Rabbitmq Dot Net
RabbitmqDotNet is a complete startup project for Rabbitmq using .NET. This contains simple but rich library using command segregation technique. The core api of RabbitmqDotNet has been wrapped to simple services. Producer puts command and consumer recieved corresponding command to handle. No need any custom object casting and reflection.

## Knowledge
Need basic idea on [Rabbitmq](https://www.rabbitmq.com/tutorials/tutorial-one-dotnet.html).

## Prerequisite
* Install RabbitMq in local machine. Follow [this](https://www.rabbitmq.com/install-windows.html) instruction.
* Install RabbitMQ.Client using Nuget

## How to Start
* Create a command in RabbitmqDotNetCore\Features\DataSyncCommand
```csharp
public class UpdateFileSettingCommand:IQueueCommand
{
	public bool IsStartupModalOn;
}
```
* Producer: Simple publish to exchange
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
* Consumer: Create reciever from Queue
```csharp
rabbitmqConsumerService.SetQueue(GlobalDictionary.DataSyncDirectExchange,
	RabbitmqExchangeType.Direct,
	GlobalDictionary.QueueSyncFileSettingExchange,
	GlobalDictionary.RoutingKeyDataSyncFileSetting);
Task.Run(() => rabbitmqConsumerService.ReceiveMessages(GlobalDictionary.QueueSyncFileSettingExchange));
```
* Create a consumer Command Handler
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
using RabbitmqDotNetCore.Infrastructure;

namespace RabbitmqDotNetCore.Features
{
    public class UpdateFileSettingCommand:IQueueCommand
    {
        public bool IsHomeOwnerModalOn;
    }
}

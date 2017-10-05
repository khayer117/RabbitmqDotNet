using RabbitmqDotNetCore.Infrastructure;

namespace RabbitmqDotNetCore.Features
{
    public class UpdatePoCommand:IQueueCommand
    {
        public string ObjectId;
        public string Name;
    }
}

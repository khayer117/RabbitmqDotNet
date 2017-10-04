using RabbitmqDotNetCore.Infrastructure;

namespace RabbitmqDotNetCore.Features
{
    public class UpdatePublicationOwnerCommand:IQueueCommand
    {
        public string ObjectId;
        public string Name;
    }
}

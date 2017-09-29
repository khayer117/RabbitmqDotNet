using RabbitmqDotNetCore.Core;

namespace RabbitmqDotNetCore
{
    public class UpdateCustomerCommand : IQueueCommand
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
    }
}

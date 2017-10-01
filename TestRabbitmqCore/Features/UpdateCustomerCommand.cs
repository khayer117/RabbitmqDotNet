using RabbitmqDotNetCore.Infrastructure;

namespace RabbitmqDotNetCore
{
    public class UpdateCustomerCommand : IQueueCommand
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
    }
}

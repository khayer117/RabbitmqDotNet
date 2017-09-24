using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitmqDotNetCore
{
    public class UpdateCustomerCommand
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
    }
}

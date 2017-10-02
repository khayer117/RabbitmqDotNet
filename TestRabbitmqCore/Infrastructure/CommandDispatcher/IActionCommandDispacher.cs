using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitmqDotNetCore.Infrastructure
{
    public interface IActionCommandDispacher
    {
        Task Send(object command);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitmqDotNetCore.Infrastructure
{
    public interface IActionCommandHandler<in TActionCommand>
    {
        Task Handle(TActionCommand command);
    }
}

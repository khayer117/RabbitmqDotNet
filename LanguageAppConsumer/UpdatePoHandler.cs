using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitmqDotNetCore.Features;
using RabbitmqDotNetCore.Infrastructure;

namespace LanguageAppConsumer
{
    public class UpdatePoHandler:ICommandHandler<UpdatePoCommand,NoCommandResult>
    {
        public async Task<NoCommandResult> Handle(UpdatePoCommand command)
        {
            Console.WriteLine("Handle Command for Language App:" + command.Name);
            return NoCommandResult.Instance;
        }
    }
}

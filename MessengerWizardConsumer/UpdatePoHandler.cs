using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitmqDotNetCore.Features;
using RabbitmqDotNetCore.Infrastructure;

namespace MessengerWizardConsumer
{
    public class UpdatePoHandler:ICommandHandler<UpdatePoCommand,NoCommandResult>
    {
        public async Task<NoCommandResult> Handle(UpdatePoCommand command)
        {
            Console.WriteLine("Handle Command for Messenger Wizard:" + command.Name);
            return NoCommandResult.Instance;
        }
    }
}

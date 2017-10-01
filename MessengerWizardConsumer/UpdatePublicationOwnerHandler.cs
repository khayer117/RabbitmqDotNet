using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitmqDotNetCore.Infrastructure;

namespace MessengerWizardConsumer
{
    public class UpdatePublicationOwnerHandler:ICommandHandler<UpdatePublicationOwnerCommand,NoCommandResult>
    {
        public async Task<NoCommandResult> Handle(UpdatePublicationOwnerCommand command)
        {
            Console.WriteLine("Handle Command for Messenger Wizard:" + command.Name);
            return NoCommandResult.Instance;
        }
    }
}

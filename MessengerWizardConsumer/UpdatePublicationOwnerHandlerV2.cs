using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitmqDotNetCore.Features;
using RabbitmqDotNetCore.Infrastructure;

namespace MessengerWizardConsumer
{
    public class UpdatePublicationOwnerHandlerV2:IActionCommandHandler<UpdatePublicationOwnerCommand>
    {
        public async Task Handle(UpdatePublicationOwnerCommand command)
        {
            Console.WriteLine("Handle UpdatePo Command(v2) for Messenger Wizard:" + command.Name);
        }
    }
}

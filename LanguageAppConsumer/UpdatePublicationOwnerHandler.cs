using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitmqDotNetCore.Infrastructure;

namespace LanguageAppConsumer
{
    public class UpdatePublicationOwnerHandler:ICommandHandler<UpdatePublicationOwnerCommand,NoCommandResult>
    {
        public async Task<NoCommandResult> Handle(UpdatePublicationOwnerCommand command)
        {
            Console.WriteLine("Handle Command for Language App:" + command.Name);
            return NoCommandResult.Instance;
        }
    }
}

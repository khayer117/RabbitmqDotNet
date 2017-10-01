using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitmqDotNetCore.Infrastructure;

namespace MessengerWizardConsumer
{
    public class TestCommandHandler:ICommandHandler<TestCommand,NoCommandResult>
    {
        public async Task<NoCommandResult> Handle(TestCommand command)
        {
            Console.WriteLine("Handler: " + command.Id);
            return NoCommandResult.Instance;
        }
    }
}

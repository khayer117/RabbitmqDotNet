using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitmqDotNetCore.Infrastructure;

namespace MessengerWizardConsumer
{
    public class TestMWDataSync
    {
        private ICommandBus commandBus;

        public TestMWDataSync(ICommandBus commandBus)
        {
            this.commandBus = commandBus;
        }

        public async Task TestCommand()
        {
            var testCommand = new TestCommand() {Id = 20};
            await this.commandBus.Send<NoCommandResult>(testCommand);
        }
    }
}

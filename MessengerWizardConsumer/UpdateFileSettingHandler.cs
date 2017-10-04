using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitmqDotNetCore.Features;
using RabbitmqDotNetCore.Infrastructure;

namespace MessengerWizardConsumer
{
    public class UpdateFileSettingHandler:IActionCommandHandler<UpdateFileSettingCommand>
    {
        private ILogger logger;

        public UpdateFileSettingHandler(ILogger logger)
        {
            this.logger = logger;
        }
        public async Task Handle(UpdateFileSettingCommand command)
        {
            this.logger.Info($"Handle File setting: {command.IsStartupModalOn}");
        }
    }
}

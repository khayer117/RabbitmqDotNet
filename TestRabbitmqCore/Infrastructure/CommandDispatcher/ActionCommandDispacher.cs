using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitmqDotNetCore.Infrastructure
{
    public class ActionCommandDispacher:IActionCommandDispacher
    {
        private static readonly Type GenericCommandHandlerType = typeof(IActionCommandHandler<>);
        private readonly Func<Type, object> resolver;
        private readonly ILogger logger;

        public ActionCommandDispacher(Func<Type, object> resolver, ILogger logger)
        {
            this.resolver = resolver;
            this.logger = logger;
        }
        public async Task Send(object command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            var handlerType = GenericCommandHandlerType.MakeGenericType(command.GetType());
            dynamic handler = null;

            try
            {
                handler = this.resolver(handlerType);

            }
            catch (Exception e)
            {
                this.logger.Warn($"Could not find \"{command.ToString()}\" handler. Skipped this command.");
                return;
            }

            if (handler == null)
            {
                this.logger.Warn($"Could not find \"{command.ToString()}\" handler. Skipped this command.");
                return;
            }

            try
            {
                await handler.Handle((dynamic)command);

                var disposable = handler as IDisposable;
                disposable?.Dispose();
            }
            catch (Exception e)
            {
                this.logger.Error(e, "CommandBus- Handler {Handler} generates error.", handler.ToString());
                throw;
            }
        }
    }
}

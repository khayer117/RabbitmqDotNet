namespace RabbitmqDotNetCore.Infrastructure
{
    using System;
    using System.Globalization;
    using System.Threading.Tasks;

    public class CommandBus : ICommandBus
    {
        private static readonly Type GenericCommandHandlerType = typeof(ICommandHandler<,>);

        private readonly Func<Type, object> resolver;
        private readonly ILogger logger;

        public CommandBus(Func<Type, object> resolver, ILogger logger)
        {
            this.resolver = resolver;
            this.logger = logger;
        }

        public async Task<TResult> Send<TResult>(object command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            var handlerType = GenericCommandHandlerType.MakeGenericType(command.GetType(), typeof(TResult));
            dynamic handler = this.resolver(handlerType);

            if (handler == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "Unable to find any handler for command \"{0}\"!", command.GetType()));
            }

            try
            {
                dynamic result;
                result = await handler.Handle((dynamic)command);

                var disposable = handler as IDisposable;
                disposable?.Dispose();

                return result;
            }
            catch (Exception e)
            {
                this.logger.Error(e, "CommandBus- Handler {Handler} generates error.", handler.ToString());
                throw;
            }
        }
    }
}
namespace RabbitmqDotNetCore.Infrastructure
{
    using System.Threading.Tasks;

    public interface ICommandHandler<in TCommand, TCommandResult>
    {
        Task<TCommandResult> Handle(TCommand command);
    }
}
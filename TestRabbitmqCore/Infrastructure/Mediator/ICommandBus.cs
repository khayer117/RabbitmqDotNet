namespace RabbitmqDotNetCore.Infrastructure
{
    using System.Threading.Tasks;

    public interface ICommandBus
    {
        Task<TResult> Send<TResult>(object command);
    }
}
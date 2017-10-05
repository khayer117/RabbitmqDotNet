using System.Threading.Tasks;

namespace RabbitmqDotNetCore.Infrastructure
{
    public interface IActionCommandDispacher
    {
        Task Send(object command);
    }
}

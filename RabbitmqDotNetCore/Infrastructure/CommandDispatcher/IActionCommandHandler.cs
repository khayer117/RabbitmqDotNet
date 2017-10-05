using System.Threading.Tasks;

namespace RabbitmqDotNetCore.Infrastructure
{
    public interface IActionCommandHandler<in TActionCommand>
    {
        Task Handle(TActionCommand command);
    }
}

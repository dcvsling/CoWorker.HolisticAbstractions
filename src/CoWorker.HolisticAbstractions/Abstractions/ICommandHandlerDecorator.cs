using System.Threading.Tasks;

namespace CoWorker.HolisticAbstractions
{

    public interface ICommandHandlerDecorator<TCommand> : ICommandHandler<TCommand> where TCommand : class
    {
    }
} 

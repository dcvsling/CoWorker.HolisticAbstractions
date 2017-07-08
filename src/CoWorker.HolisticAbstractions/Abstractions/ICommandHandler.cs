
namespace CoWorker.HolisticAbstractions
{
    public interface ICommandHandler<in TCommand> where TCommand : class
    {
        void Invoke(TCommand command);
    }
} 

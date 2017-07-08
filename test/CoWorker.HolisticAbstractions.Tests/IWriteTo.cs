namespace CoWorker.HolisticAbstractions.Tests
{

    public interface IWriteTo<TCommand> : ICommandHandler<TCommand> where TCommand : class
    {
    }
}

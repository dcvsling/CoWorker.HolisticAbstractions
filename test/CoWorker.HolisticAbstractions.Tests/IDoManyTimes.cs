namespace CoWorker.HolisticAbstractions.Tests
{

    public interface IDoManyTimes<TCommand> : ICommandHandlerDecorator<TCommand> where TCommand : class
    {
    }
}

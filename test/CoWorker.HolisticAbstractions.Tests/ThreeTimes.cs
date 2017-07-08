namespace CoWorker.HolisticAbstractions.Tests
{
    public class ThreeTimes<TCommand> : IDoManyTimes<TCommand> where TCommand : class
    {
        private readonly IWriteTo<TCommand> _handler;
        public ThreeTimes(IWriteTo<TCommand> handler)
        {
            _handler = handler;
        }
        public void Invoke(TCommand command)
        {
            _handler.Invoke(command);
            _handler.Invoke(command);
            _handler.Invoke(command);
        }
    }
}

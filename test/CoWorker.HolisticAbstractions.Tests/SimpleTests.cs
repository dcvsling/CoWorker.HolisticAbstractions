using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Xunit;

namespace CoWorker.HolisticAbstractions.Tests
{
    public class SimpleTests
    {
        private static IServiceProvider Service => new ServiceCollection()
               .AddCommandDispatcher()
               .AddHandler<Command, AssertCommandHandler>()
               .AddHandler<AlwaysTrueCommand, AssertCommandHandler>()
               .BuildServiceProvider();
        [Fact]
        public void test_direct_command_and_handler()
        {
            Service.GetService<ITaskProcessor<Command>>().Send(new Command(() => 1 == 1));
        }
        [Fact]
        public void test_inherint_command_and_handler()
        {
            Service.GetService<ITaskProcessor<AlwaysTrueCommand>>().Send(new AlwaysTrueCommand());
        }
    }

    public class Command
    {
        private Func<bool> assert;
        public Command(Func<bool> assert)
        {
            this.assert = assert;
        }
        public virtual void TryAssert(Action<Func<bool>> callback)
        {
            callback(assert);
        }
    }

    public class AlwaysTrueCommand : Command
    {
        public AlwaysTrueCommand() : base(() => true)
        {
        }
    }

    public class AssertCommandHandler : ICommandHandler<Command>
    {
        public Task InvokeAsync(Command command)
        {
            command.TryAssert(x => Assert.True(x()));
            return Task.CompletedTask;
        }
    }
}

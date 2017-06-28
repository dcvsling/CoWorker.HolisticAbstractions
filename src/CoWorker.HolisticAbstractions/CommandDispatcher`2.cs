using System.Net.Http.Headers;

using System.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace CoWorker.HolisticAbstractions
{

    public class CommandDispatcher<TCommand, THandler> : ICommandDispatcher<TCommand, THandler>
        where TCommand : class
        where THandler : class,ICommandHandler<TCommand>
    {
        private readonly IFactory<THandler> _factory;
        private IDictionary<Type, ICommandHandler> _cache;
        public CommandDispatcher(IFactory<THandler> factory)
        {
            _factory = factory;
            _cache = new Dictionary<Type, ICommandHandler>();
        }
        async public Task InvokeAsync(TCommand command)
        {
            await _factory.Create(handler => handler is ICommandHandler<TCommand>)
                .Aggregate(Task.FromResult(command), InvokeAsync);
        }

        async private Task<TCommand> InvokeAsync(Task<TCommand> command, THandler handler)
        {
            var result = await command;
            await handler.InvokeAsync(await command);
            return result;
        }
    }
} 

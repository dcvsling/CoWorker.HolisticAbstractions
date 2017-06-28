using System.Net.Http.Headers;

using System.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace CoWorker.HolisticAbstractions
{

    public interface ICommandDispatcher<TCommand,THandler>
        where TCommand : class
        where THandler : ICommandHandler<TCommand>
    {
        Task InvokeAsync(TCommand command);
    }
} 

using System.Net.Http.Headers;

using System.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace CoWorker.HolisticAbstractions
{
    public interface ICommandHandler<in TCommand> : ICommandHandler where TCommand : class
    {
        Task InvokeAsync(TCommand command);
    }
} 

using System.Net.Http.Headers;

using System.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace CoWorker.HolisticAbstractions
{

    public interface ITaskBuilder
    {
        void Register<TCommand, THandler>()
            where TCommand : class
            where THandler : class, ICommandHandler<TCommand>;
    }
} 

using System.Net.Http.Headers;

using System.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace CoWorker.HolisticAbstractions
{
    
    public class TaskBuilder<TContext> : ITaskBuilder<TContext> where TContext:class
    {
        internal IList<(Type CommandType, Func<IServiceProvider, Func<object, Task>> HandlerInvoke)> list;
        public TaskBuilder()
        {
            list = new List<(Type, Func<IServiceProvider, Func<object, Task>>)>();
        }

        public virtual void Register<TCommand, THandler>()
            where TCommand : class
            where THandler : class,ICommandHandler<TCommand>
        {
            this.list.Add(
                (typeof(TCommand),
                p => async obj =>
                {
                    if (!(obj is TCommand cmd)) return;
                    await p.GetService<THandler>().InvokeAsync(cmd);
                }));
        }
    }
} 

using System.Net.Http.Headers;

using System.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace CoWorker.HolisticAbstractions
{

    public class TaskProcessor<TContext> : ITaskProcessor<TContext> where TContext : class
    {
        private readonly IDictionary<Type, Lazy<Func<object, Task>>> _map;
        private readonly IServiceProvider _provider;
        private readonly TaskBuilder<TContext> _builder;
        private readonly IEnumerable<IConfigureOptions<ITaskBuilder<TContext>>> _configures;

        public TaskProcessor(
            IServiceProvider provider,
            IEnumerable<IConfigureOptions<ITaskBuilder<TContext>>> configures,
            TaskBuilder<TContext> builder)
        {
            _map = new Dictionary<Type, Lazy<Func<object, Task>>>();
            _configures = configures;
            _provider = provider;
            _builder = builder;
            _configures.ToList().ForEach(x => x.Configure(_builder));
            _builder.list.ToList().ForEach(x => _map.AddOrAppend(
                x.CommandType, 
                new Lazy<Func<object, Task>>(() => cmd => x.HandlerInvoke(this._provider)(cmd)),
                Merge)
                );
        }
        public Task Send(TContext context)
        {
            Task.WaitAll(_map.Where(x => x.Key == context.GetType())
                .Select(x => x.Value)
                .Select(x => Task.Run(() => x.Value(context)))
                .ToArray());
            return Task.CompletedTask;
        }

        private Lazy<Func<object,Task>> Merge(Lazy<Func<object, Task>> old, Lazy<Func<object, Task>> current)
            => new Lazy<Func<object, Task>>(() => async cmd =>
            {
                await old.Value(cmd);
                await current.Value(cmd);
            });
    }
} 

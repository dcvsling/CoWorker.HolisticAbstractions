using System.Net.Http.Headers;

using System.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace CoWorker.HolisticAbstractions
{

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCommandDispatcher(this IServiceCollection services)
            => services.AddOptions()
                .AddSingleton(typeof(ITaskProcessor<>), typeof(TaskProcessor<>))
                .AddSingleton(typeof(TaskBuilder<>))
                .AddSingleton(typeof(ITaskBuilder<>), typeof(TaskBuilder<>))
                .AddSingleton(typeof(IFactory<>), typeof(Factory<>))
                .AddSingleton(typeof(ICommandDispatcher<,>), typeof(CommandDispatcher<,>));

        public static IServiceCollection AddHandler<TCommand, THandler>(this IServiceCollection services)
            where TCommand : class
            where THandler : class, ICommandHandler<TCommand>
            => services.AddSingleton<TCommand>()
                .AddSingleton<THandler>()
                .Configure<ITaskBuilder<TCommand>>(b => b.Register<TCommand,THandler>());
        
        public static IDictionary<TKey,TValue> AddOrAppend<TKey, TValue>(
            this IDictionary<TKey,TValue> map,
            TKey key,
            TValue value,
            Func<TValue,TValue,TValue> append)
        {
            if (!map.ContainsKey(key)) map.Add(key, value);
            else map[key] = append(map[key], value);
            return map;
        }
    }
} 

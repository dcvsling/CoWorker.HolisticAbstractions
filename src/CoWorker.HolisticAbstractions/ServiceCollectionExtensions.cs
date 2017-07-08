using System.Windows.Input;

namespace CoWorker.HolisticAbstractions
{
    using Microsoft.Extensions.DependencyInjection;

    using System;
    using System.Collections;
    using System.Collections.Generic;

    public interface IServiceCollection<T> : IServiceCollection { }
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddHandler<T, TServiceType, TImplementationType>(this IServiceCollection services)
            where TServiceType : class, ICommandHandler<T>
            where TImplementationType : class, TServiceType
            where T : class
        {
            services.InGeneric<T>(srv => srv.AddSingleton(
                typeof(TServiceType).GetGenericTypeDefinition(),
                typeof(TImplementationType).GetGenericTypeDefinition()));
            return services;
        }

        public static IServiceCollection AddHandlerDecorator<T, TServiceType, TImplementationType>(this IServiceCollection services)
            where TServiceType : class, ICommandHandlerDecorator<T>
            where TImplementationType : class, TServiceType
            where T : class
        {
            services.InGeneric<T>(srv => srv.AddSingleton(
                typeof(TServiceType).GetGenericTypeDefinition(),
                typeof(TImplementationType).GetGenericTypeDefinition()));
            return services;
        }

        private static IServiceCollection InGeneric<T>(this IServiceCollection services,Action<IServiceCollection<T>> config)
        {
            config(new ServiceCollectionDecorator<T>(services));
            return services;
        }

        //public static IServiceCollection AddHandler<T, TServiceType(
        //    this IServiceCollection services,
        //    Action<T> command)
        //    where TServiceType : class, ICommandHandler<T>
        //    where T : class
        //    => services.AddSingleton(
        //        typeof(TServiceType).GetGenericTypeDefinition(),
        //        new AnonymousCommandHandler<T>(command));

        //public static IServiceCollection AddHandlerDecorator<T, TServiceType, TImplementationType>(
        //    this IServiceCollection services,
        //    Action<T> command)
        //    where TServiceType : class, ICommandHandlerDecorator<T>
        //    where T : class
        //    => services.AddSingleton(
        //        typeof(TServiceType).GetGenericTypeDefinition(),
        //        typeof(TImplementationType).GetGenericTypeDefinition());
    }

    public class AnonymousCommandHandler<TCommand> : ICommandHandler<TCommand> where TCommand : class
    {
        private readonly Action<TCommand> _handler;
        public AnonymousCommandHandler(Action<TCommand> handler)
        {
            _handler = handler;
        }
        public void Invoke(TCommand command)
        {
            this._handler(command);
        }
    }

    public class AnonymousCommandHandlerDecorator<TCommand> : ICommandHandlerDecorator<TCommand> where TCommand : class
    {
        private readonly ICommandHandler<TCommand> _handler;
        private readonly Action<ICommandHandler<TCommand>,TCommand> _decorate;

        public AnonymousCommandHandlerDecorator(Action<ICommandHandler<TCommand>,TCommand> decorate, ICommandHandler<TCommand> handler)
        {
            _handler = handler;
            _decorate = decorate;
        }

        public void Invoke(TCommand command)
        {
            _decorate(_handler,command);
        }
    }
    
    #region
    internal class ServiceCollectionDecorator<T> : IServiceCollection<T>
    {
        private readonly IServiceCollection _services;

        public ServiceCollectionDecorator(IServiceCollection services)
        {
            _services = services;
        }

        void ICollection<ServiceDescriptor>.Add(ServiceDescriptor item)
        {
            if (item.ServiceType.IsGenericType
                && (item.ImplementationType?.IsGenericType ?? false))
            {
                item = ServiceDescriptor.Describe(
                    item.ServiceType.GetGenericTypeDefinition(),
                    item.ImplementationType.GetGenericTypeDefinition(),
                    item.Lifetime);
            }
            this.Add(item);
        }
        #region impl
        public ServiceDescriptor this[Int32 index] { get => this._services[index]; set => this._services[index] = value; }

        public Int32 Count => this._services.Count;

        public Boolean IsReadOnly => this._services.IsReadOnly;

        public void Add(ServiceDescriptor item) => this._services.Add(item);
        public void Clear() => this._services.Clear();
        public Boolean Contains(ServiceDescriptor item) => this._services.Contains(item);
        public void CopyTo(ServiceDescriptor[] array, Int32 arrayIndex) => this._services.CopyTo(array, arrayIndex);
        public IEnumerator<ServiceDescriptor> GetEnumerator() => this._services.GetEnumerator();
        public Int32 IndexOf(ServiceDescriptor item) => this._services.IndexOf(item);
        public void Insert(Int32 index, ServiceDescriptor item) => this._services.Insert(index, item);
        public Boolean Remove(ServiceDescriptor item) => this._services.Remove(item);
        public void RemoveAt(Int32 index) => this._services.RemoveAt(index);
        IEnumerator IEnumerable.GetEnumerator() => this._services.GetEnumerator();
        #endregion
    }
    #endregion
}

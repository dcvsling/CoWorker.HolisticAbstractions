using System.Net.Http.Headers;

using System.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace CoWorker.HolisticAbstractions
{
    public class Factory<T> : IFactory<T> where T : class
    {
        private readonly IServiceProvider _provider;
        public Factory(IServiceProvider provider)
        {
            _provider = provider;
        }
        public IEnumerable<T> Create(Func<T, Boolean> predicate)
            => _provider.GetServices<T>().Where(predicate);
    }
} 

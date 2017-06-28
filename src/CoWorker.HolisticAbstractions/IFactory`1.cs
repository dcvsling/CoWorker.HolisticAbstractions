using System.Net.Http.Headers;

using System.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace CoWorker.HolisticAbstractions
{

    public interface IFactory<T> where T : class
    {
        IEnumerable<T> Create(Func<T, bool> predicate);
    }
} 

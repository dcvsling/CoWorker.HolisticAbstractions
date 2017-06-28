using System.Threading.Tasks;

namespace CoWorker.HolisticAbstractions
{

    public interface ITaskProcessor<TContext> where TContext : class
    {
        Task Send(TContext context);
    }
} 

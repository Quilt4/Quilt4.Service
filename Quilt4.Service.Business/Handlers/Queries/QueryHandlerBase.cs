using System.Diagnostics;
using Quilt4.Service.Interface.Repository;

namespace Quilt4.Service.Business.Handlers.Queries
{
    public abstract class QueryHandlerBase<TIn, TOut>
    {
        protected QueryHandlerBase(IReadRepository readRepository)
        {
            ReadRepository = readRepository;
        }

        protected IReadRepository ReadRepository { get; }

        protected abstract TOut DoHandle(TIn input);

        public TOut Handle(TIn input)
        {
            TOut response;
            var sw = new Stopwatch();
            sw.Start();
            try
            {
                response =  DoHandle(input);
            }
            finally
            {
                sw.Stop();
                var type = GetType();
                Debug.WriteLine($"{type.Name} took {sw.ElapsedMilliseconds}ms to execute");

                //TODO: Log every query that takes over 100 ms as poor performance
            }

            return response;
        }
    }
}
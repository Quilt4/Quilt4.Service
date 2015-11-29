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
            return DoHandle(input);
        }
    }
}
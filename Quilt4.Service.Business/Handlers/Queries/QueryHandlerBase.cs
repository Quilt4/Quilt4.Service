using Quilt4.Service.Interface.Repository;

namespace Quilt4.Service.Business.Handlers.Queries
{
    public abstract class QueryHandlerBase<TIn, TOut>
    {
        private readonly IReadRepository _readRepository;

        protected QueryHandlerBase(IReadRepository readRepository)
        {
            _readRepository = readRepository;
        }

        protected IReadRepository ReadRepository { get { return _readRepository; } }

        protected abstract TOut DoHandle(TIn input);

        public TOut Handle(TIn input)
        {
            return DoHandle(input);
        }
    }
}
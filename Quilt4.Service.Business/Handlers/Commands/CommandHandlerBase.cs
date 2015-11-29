using System.Threading.Tasks;
using Quilt4.Service.Interface.Repository;

namespace Quilt4.Service.Business.Handlers.Commands
{
    public abstract class CommandHandlerBase<T>
    {
        private readonly IDataRepository _repository;
        private readonly IUpdateReadRepository _writeRepository;

        protected CommandHandlerBase(IDataRepository repository, IUpdateReadRepository writeRepository)
        {
            _repository = repository;
            _writeRepository = writeRepository;
        }

        protected IDataRepository Repository { get { return _repository; } }
        protected IUpdateReadRepository WriteRepository { get { return _writeRepository; } }

        protected abstract void DoHandle(T input);

        public Task StartHandle(T input)
        {            
            var task = Task.Run(() => DoHandle(input));
            return task;
        }
    }
}
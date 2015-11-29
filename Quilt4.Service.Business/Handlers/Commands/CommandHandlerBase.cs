using System.Threading.Tasks;
using Quilt4.Service.Interface.Repository;

namespace Quilt4.Service.Business.Handlers.Commands
{
    public abstract class CommandHandlerBase<T>
    {
        protected CommandHandlerBase(IDataRepository repository, IUpdateReadRepository writeRepository)
        {
            Repository = repository;
            WriteRepository = writeRepository;
        }

        protected IDataRepository Repository { get; }
        protected IUpdateReadRepository WriteRepository { get; }

        protected abstract void DoHandle(T input);

        public Task StartHandle(T input)
        {
            var task = Task.Run(() => DoHandle(input));
            return task;
        }
    }
}
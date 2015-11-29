using System;
using System.Threading.Tasks;
using Quilt4.Service.Interface.Business;
using Quilt4.Service.Interface.Repository;

namespace Quilt4.Service.Business.Handlers.Commands
{
    public abstract class CommandHandlerBase<T>
    {
        protected abstract void Handle(T input);

        public Task StartHandle(T input)
        {            
            var task = Task.Run(() => Handle(input));
            return task;
        }
    }

    public class CreateProjectCommandHandler : CommandHandlerBase<ICreateProjectCommandInput>
    {
        private readonly IRepository _repository;
        private readonly IWriteRepository _writeRepository;

        public CreateProjectCommandHandler(IRepository repository, IWriteRepository writeRepository)
        {
            _repository = repository;
            _writeRepository = writeRepository;
        }

        protected override void Handle(ICreateProjectCommandInput input)
        {
            _repository.CreateProject(input.UserName, input.ProjectKey, input.ProjectName, RandomUtility.GetRandomString(32), DateTime.UtcNow, input.DashboardColor ?? "Blue");
            _writeRepository.UpdateDashboardPageProject(input.ProjectKey);
            _writeRepository.UpdateProjectPageProject(input.ProjectKey);
        }
    }
}
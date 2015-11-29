using System;
using Quilt4.Service.Interface.Business;
using Quilt4.Service.Interface.Repository;

namespace Quilt4.Service.Business.Handlers.Commands
{
    public class CreateProjectCommandHandler : CommandHandlerBase<ICreateProjectCommandInput>
    {
        public CreateProjectCommandHandler(IDataRepository repository, IUpdateReadRepository writeRepository)
            : base(repository, writeRepository)
        {
        }

        protected override void DoHandle(ICreateProjectCommandInput input)
        {
            Repository.CreateProject(input.UserName, input.ProjectKey, input.ProjectName, RandomUtility.GetRandomString(32), DateTime.UtcNow, input.DashboardColor ?? "blue");
            WriteRepository.UpdateDashboardPageProject(input.ProjectKey);
            WriteRepository.UpdateProjectPageProject(input.ProjectKey);
        }
    }
}
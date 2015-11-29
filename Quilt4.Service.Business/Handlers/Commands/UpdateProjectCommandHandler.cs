using System;
using Quilt4.Service.Interface.Business;
using Quilt4.Service.Interface.Repository;

namespace Quilt4.Service.Business.Handlers.Commands
{
    public class UpdateProjectCommandHandler : CommandHandlerBase<IUpdateProjectCommandInput>
    {
        public UpdateProjectCommandHandler(IDataRepository repository, IUpdateReadRepository writeRepository)
            : base(repository, writeRepository)
        {
        }

        protected override void DoHandle(IUpdateProjectCommandInput input)
        {
            Repository.UpdateProject(input.UserName, input.ProjectKey, input.ProjectName, DateTime.UtcNow, input.DashboardColor);
            WriteRepository.UpdateDashboardPageProject(input.ProjectKey);
            WriteRepository.UpdateProjectPageProject(input.ProjectKey);
        }
    }
}
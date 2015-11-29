using System.Collections.Generic;
using System.Linq;
using Quilt4.Service.Entity;
using Quilt4.Service.Interface.Business;
using Quilt4.Service.Interface.Repository;

namespace Quilt4.Service.Business.Handlers.Queries
{
    public class GetProjectsQueryHandler : QueryHandlerBase<IGetProjectQueryInput, IEnumerable<IProject>>
    {
        public GetProjectsQueryHandler(IReadRepository readRepository)
            : base(readRepository)
        {
        }

        protected override IEnumerable<IProject> DoHandle(IGetProjectQueryInput input)
        {
            var response = ReadRepository.GetDashboardProjects(input.UserName);
            var result = response.Select(x => new Project(x.ProjectKey, x.Name, x.DashboardColor));
            return result;
        }
    }
}
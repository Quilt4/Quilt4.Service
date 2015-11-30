using System;
using System.Collections.Generic;
using System.Linq;
using Quilt4.Service.Entity;
using Quilt4.Service.Interface.Business;
using Quilt4.Service.Interface.Repository;

namespace Quilt4.Service.Business.Handlers.Queries
{
    public class GetProjectQueryHandler : QueryHandlerBase<IGetProjectQueryInput, IProject>
    {
        public GetProjectQueryHandler(IReadRepository readRepository)
            : base(readRepository)
        {
        }

        protected override IProject DoHandle(IGetProjectQueryInput input)
        {
            var x = ReadRepository.GetProject(input.UserName, input.ProjectKey);
            var result = new Project(x.ProjectKey, x.Name, x.DashboardColor, x.ProjectApiKey);
            return result;
        }
    }

    public class GetProjectsQueryHandler : QueryHandlerBase<IGetProjectsQueryInput, IEnumerable<IProject>>
    {
        public GetProjectsQueryHandler(IReadRepository readRepository)
            : base(readRepository)
        {
        }

        protected override IEnumerable<IProject> DoHandle(IGetProjectsQueryInput input)
        {
            throw new NotImplementedException();
            //var x = ReadRepository.GetProject(input.UserName, input.ProjectKey);
            //var result = new Project(x.ProjectKey, x.Name, x.DashboardColor, x.ProjectApiKey);
            //return result;
        }
    }
}
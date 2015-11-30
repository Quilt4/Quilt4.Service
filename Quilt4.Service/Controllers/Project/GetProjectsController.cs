using System.Linq;
using System.Web.Http;
using Quilt4.Service.Business.Handlers.Queries;
using Quilt4.Service.Entity;
using Tharga.Quilt4Net.DataTransfer;

namespace Quilt4.Service.Controllers.Project
{
    public class GetProjectsController : ApiController
    {
        private readonly GetProjectsQueryHandler _handler;

        public GetProjectsController(GetProjectsQueryHandler handler)
        {
            _handler = handler;
        }

        [HttpGet]
        [Authorize]
        [Route("api/Project/List")]
        public ProjectResponse[] GetProjects()
        {
            var output = _handler.Handle(new GetProjectsQueryInput(User.Identity.Name));
            var response = output.Select(x => new ProjectResponse { Name = x.Name, ProjectKey = x.ProjectKey, DashboardColor = x.DashboardColor }).ToArray();
            return response;
        }
    }
}
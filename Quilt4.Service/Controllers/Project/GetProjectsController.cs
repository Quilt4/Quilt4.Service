using System.Linq;
using System.Web.Http;
using Quilt4.Service.Business.Handlers.Queries;
using Quilt4.Service.DataTransfer;

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
        public ProjectPageProjectResponse[] GetProjects()
        {
            var output = _handler.Handle(new GetProjectQueryInput(User.Identity.Name));
            return output.Select(x => new ProjectPageProjectResponse { Name = x.Name }).ToArray();
        }
    }
}
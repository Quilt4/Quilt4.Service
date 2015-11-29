using System;
using System.Linq;
using System.Web.Http;
using Quilt4.Service.Business.Handlers.Commands;
using Quilt4.Service.DataTransfer;
using Quilt4.Service.Interface.Business;

namespace Quilt4.Service.Controllers
{
    [Authorize]
    [RoutePrefix("api/Project")]
    public class CreateProjectController : ApiController
    {
        private readonly CreateProjectCommandHandler _handler;

        public CreateProjectController(CreateProjectCommandHandler handler)
        {
            _handler = handler;
        }

        [HttpPost]
        [Route("Create")]
        public void CreateProject(CreateProjectRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request), "No request object provided.");
            if (request.Key == Guid.Empty) throw new ArgumentException("Key cannot be empty Guid.");
            if (string.IsNullOrEmpty(request.Name)) throw new ArgumentException("No name provided.");

            _handler.StartHandle(new CreateProjectCommandInput(User.Identity.Name, request.Key, request.Name, request.DashboardColor));
        }
    }

    [Authorize]
    [RoutePrefix("api/Project")]
    public class ProjectController : ApiController
    {
        private readonly IProjectBusiness _projectBusiness;

        public ProjectController(IProjectBusiness projectBusiness)
        {
            _projectBusiness = projectBusiness;
        }
        
        [HttpGet]
        [Route("List")]
        public ProjectPageProjectResponse[] GetProjects()
        {
            var projects = _projectBusiness.GetProjects(User.Identity.Name);

            return projects.Select(x => new ProjectPageProjectResponse { Name = x.Name } ).ToArray();
        }

        //[HttpPost]
        //[Route("Create")]
        //public void CreateProject(CreateProjectRequest request)
        //{
        //    if (request == null) throw new ArgumentNullException(nameof(request), "No request object provided.");
        //    if (request.Key == Guid.Empty) throw new ArgumentException("Key cannot be empty Guid.");
        //    if (string.IsNullOrEmpty(request.Name)) throw new ArgumentException("No name provided.");

        //    _projectBusiness.GetCommandHandler<ICreateProjectCommandInput>().Handle(new CreateProjectCommandInput(User.Identity.Name, request.Key, request.Name, request.DashboardColor));
        //}

        //[HttpPost]
        //[Route("api/project/update")]
        //public UpdateProjectResponse UpdateProject(UpdateProjectRequest request)
        //{
        //    if (request == null)
        //        throw new ArgumentNullException(nameof(request), "No request object provided.");

        //    _projectBusiness.UpdateProject(Guid.Parse(request.Id), request.Name, request.DashboardColor);

        //    return new UpdateProjectResponse();
        //}

        //[Route("api/project/{projectId}/application/{applicationId}/version")]
        //public IEnumerable<ProjectPageVersionResponse> GetVersions(string projectId, string applicationId)
        //{
        //    return _projectBusiness.GetVersions(null, Guid.Parse(projectId), Guid.Parse(applicationId)) .ToProjectPageVersionResponses();
        //}
    }

    //TODO: Move the definition to the toolkit (
    public class UpdateProjectResponse
    {
    }

    public class CreateProjectRequest
    {
        public Guid Key { get; set; }
        public string Name { get; set; }
        public string DashboardColor { get; set; }
    }

    public class UpdateProjectRequest
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string DashboardColor { get; set; }
    }
}
using System;

namespace Quilt4.Service.Controllers
{
    //[Authorize]
    //[RoutePrefix("api/Project")]
    //public class ProjectController : ApiController
    //{
    //    private readonly IProjectBusiness _projectBusiness;

    //    public ProjectController(IProjectBusiness projectBusiness)
    //    {
    //        _projectBusiness = projectBusiness;
    //    }
        
    //    //[Route("api/project/{projectId}/application/{applicationId}/version")]
    //    //public IEnumerable<ProjectPageVersionResponse> GetVersions(string projectId, string applicationId)
    //    //{
    //    //    return _projectBusiness.GetVersions(null, Guid.Parse(projectId), Guid.Parse(applicationId)) .ToProjectPageVersionResponses();
    //    //}
    //}

    //TODO: Move the definition to the toolkit
    public class CreateProjectRequest
    {
        public Guid ProjectKey { get; set; }
        public string Name { get; set; }
        public string DashboardColor { get; set; }
    }

    public class UpdateProjectRequest
    {
        public Guid ProjectKey { get; set; }
        public string Name { get; set; }
        public string DashboardColor { get; set; }
    }
}
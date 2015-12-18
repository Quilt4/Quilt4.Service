using System;
using System.Collections.Generic;
using System.Web.Http;
using Quilt4.Service.Controllers.Web.DataTransfer;
using Quilt4.Service.Converters;
using Quilt4.Service.DataTransfer;
using Quilt4.Service.Interface.Business;

namespace Quilt4.Service.Controllers.Web
{
    [Route("api/Web/Project")]
    public class WebProjectController : ApiController
    {
        private readonly IProjectBusiness _projectBusiness;

        public WebProjectController(IProjectBusiness projectBusiness)
        {
            _projectBusiness = projectBusiness;
        }
        
        [HttpGet]
        [Authorize]
        [Route("api/project/{projectId}")]
        public ProjectPageProjectResponse GetProject(string projectId)
        {
            return _projectBusiness.GetProject(User.Identity.Name, Guid.Parse(projectId)).ToProjectPageProjectResponse();
        }

        [HttpGet]
        [Authorize]
        [Route("api/project/{projectId}/application/{applicationId}/version")]
        public IEnumerable<ProjectPageVersionResponse> GetVersions(string projectId, string applicationId)
        {
            return _projectBusiness.GetVersions(null, Guid.Parse(projectId), Guid.Parse(applicationId)).ToProjectPageVersionResponses();
        }

        [HttpPost]
        [Authorize]
        [Route("api/project/create")]
        public void CreateProject(ProjectInput request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request), "No request object provided.");

            _projectBusiness.CreateProject(User.Identity.Name, request.ProjectKey, request.Name, request.DashboardColor);
        }

        [HttpPost]
        [Authorize]
        [Route("api/project/update")]
        public void UpdateProject(ProjectInput request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request), "No request object provided.");

            _projectBusiness.UpdateProject(request.ProjectKey, request.Name, request.DashboardColor, User.Identity.Name);
        }
    }
}
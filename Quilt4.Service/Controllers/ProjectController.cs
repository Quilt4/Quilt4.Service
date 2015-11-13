using System;
using System.Collections.Generic;
using System.Web.Http;
using Quil4.Service.Interface.Business;
using Quilt4.Service.Converters;
using Quilt4.Service.DataTransfer;

namespace Quilt4.Service.Controllers
{
    public class ProjectController : ApiController
    {
        private readonly IProjectBusiness _projectBusiness;

        public ProjectController(IProjectBusiness projectBusiness)
        {
            _projectBusiness = projectBusiness;
        }

        [HttpGet]
        public ProjectPageProjectResponse GetProject(string id)
        {
            return _projectBusiness.GetProject(null, Guid.Parse(id)).ToProjectPageProjectResponse();
        }


        [Route("api/project/{projectId}/application/{applicationId}/version")]
        public IEnumerable<ProjectPageVersionResponse> GetVersions(string projectId, string applicationId)
        {
            return
                _projectBusiness.GetVersions(null, Guid.Parse(projectId), Guid.Parse(applicationId))
                    .ToProjectPageVersionResponses();
        }
    }
}
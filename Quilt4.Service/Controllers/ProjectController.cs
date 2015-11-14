using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
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
            //IEnumerable<string> headerValues = request.Headers.GetValues("MyCustomID");
            //var id = headerValues.FirstOrDefault();

            return _projectBusiness.GetProject(null, Guid.Parse(id)).ToProjectPageProjectResponse();
        }

        [HttpGet]
        public ProjectPageProjectResponse[] GetProjects()
        {
            var re = Request;
            var headers = re.Headers;
            if (!string.IsNullOrEmpty(headers.Authorization.Scheme))
            {
                var sessionKey = headers.Authorization.Parameter;
                var xxx = Convert.FromBase64String(sessionKey);
                var key = Encoding.UTF8.GetString(xxx);
            }

            //IEnumerable<string> headerValues = request.Headers.GetValues("MyCustomID");
            //var id = headerValues.FirstOrDefault();

            //return _projectBusiness.GetProject(null, Guid.Parse(id)).ToProjectPageProjectResponse();
            return new[] { new ProjectPageProjectResponse { Name = "A", } };
        }

        [Route("api/project/{projectId}/application/{applicationId}/version")]
        public IEnumerable<ProjectPageVersionResponse> GetVersions(string projectId, string applicationId)
        {
            return _projectBusiness.GetVersions(null, Guid.Parse(projectId), Guid.Parse(applicationId)) .ToProjectPageVersionResponses();
        }
    }
}
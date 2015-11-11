using System;
using System.Collections.Generic;
using System.Web.Http;
using Quil4.Service.Interface.Business;
using Quilt4.Service.Converters.Project;
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
        public IEnumerable<ProjectResponse> GetAllProjects()
        {
            return _projectBusiness.GetProjects(null).ToProjectResponses();
        }

        [HttpGet]
        public ProjectResponse GetProject(string id)
        {
            return _projectBusiness.GetProject(null, Guid.Parse(id)).ToProjectResponse();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Quilt4.Service.Converters;
using Quilt4.Service.Interface.Business;
using Quilt4Net.Core.DataTransfer;

namespace Quilt4.Service.Controllers.WebAPI
{
    [Authorize]
    public class ProjectController : ApiController
    {
        private readonly IProjectBusiness _projectBusiness;

        public ProjectController(IProjectBusiness projectBusiness)
        {
            _projectBusiness = projectBusiness;
        }

        [Route("api/Project")]
        public IEnumerable<ProjectResponse> Get()
        {
            var projects = _projectBusiness.GetProjects(User.Identity.Name);
            return projects.Select(x => x.ToProjectData());
        }

        [Route("api/Project/{projectKey}")]
        public ProjectResponse Get(Guid projectKey)
        {
            return _projectBusiness.GetProject(projectKey).ToProjectData();
        }

        [Route("api/Project")]
        public void Post([FromBody]ProjectResponse value)
        {
            if (!string.IsNullOrEmpty(value.ProjectApiKey)) throw new InvalidOperationException("Cannot provide a value for ProjectApiKey, this value is assigned by the server.");
            _projectBusiness.CreateProject(User.Identity.Name, value.ProjectKey, value.Name, value.DashboardColor);
        }

        [Route("api/Project/{id}")]
        public void Put(Guid id, [FromBody]ProjectResponse value)
        {
            if(id != value.ProjectKey) throw new InvalidOperationException("Provided id and key does not match.");
            if (!string.IsNullOrEmpty(value.ProjectApiKey)) throw new InvalidOperationException("Cannot provide a value for ProjectApiKey, this value is assigned by the server.");
            _projectBusiness.UpdateProject(id, value.Name, value.DashboardColor, User.Identity.Name);
        }

        [Route("api/Project/{id}")]
        public void Delete(Guid id)
        {
            _projectBusiness.DeleteProject(id);
        }

        [HttpGet]
        [Route("api/Project/Members/{projectKey}")]
        public IEnumerable<MemberResponse> GetMembers([FromUri] Guid projectKey)
        {
            return _projectBusiness.GetMembers(projectKey).Select(x => new MemberResponse { UserName = x.UserName, EMail = x.EMail, Confirmed = x.Confirmed, Role = x.Role });
        }
    }    
}
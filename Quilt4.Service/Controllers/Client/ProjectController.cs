using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Quilt4.Service.Controllers.Client.Converters;
using Quilt4.Service.Interface.Business;
using Tharga.Quilt4Net.DataTransfer;

namespace Quilt4.Service.Controllers.Client
{
    [Authorize]
    [Route("api/Client/Project")]
    [Route("api/Client/Project/{id}")]
    public class ClientProjectController : ApiController
    {
        private readonly IProjectBusiness _projectBusiness;

        public ClientProjectController(IProjectBusiness projectBusiness)
        {
            _projectBusiness = projectBusiness;
        }

        public IEnumerable<ProjectResponse> Get()
        {
            var projects = _projectBusiness.GetProjects(User.Identity.Name);
            return projects.Select(x => x.ToProjectResponse());
        }

        public ProjectResponse Get(Guid id)
        {
            return _projectBusiness.GetProject(id).ToProjectResponse();
        }

        public void Post([FromBody]CreateProjectRequest value)
        {
            _projectBusiness.CreateProject(User.Identity.Name, value.ProjectKey, value.Name, value.DashboardColor);
        }

        public void Put(Guid id, [FromBody]CreateProjectRequest value)
        {
            if(id != value.ProjectKey) throw new InvalidOperationException("Provided id and key does not match.");
            _projectBusiness.UpdateProject(id, value.Name, value.DashboardColor);
        }

        public void Delete(Guid id)
        {
            _projectBusiness.DeleteProject(id);
        }
    }
}
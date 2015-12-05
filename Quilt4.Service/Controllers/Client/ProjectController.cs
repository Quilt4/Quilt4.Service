using System;
using System.Collections.Generic;
using System.Web.Http;
using Quilt4.Service.Converters;
using Quilt4.Service.DataTransfer;
using Quilt4.Service.Interface.Business;

namespace Quilt4.Service.Controllers
{
    //[Route("api/Project")]
    public class ProjectController : ApiController
    {
        private readonly IProjectBusiness _projectBusiness;

        public ProjectController(IProjectBusiness projectBusiness)
        {
            _projectBusiness = projectBusiness;
        }

        // GET: api/Project
        [HttpGet]
        [Authorize]
        [Route("api/Project/List")]
        public IEnumerable<ProjectPageProjectResponse> Get()
        {
            return new[] { new ProjectPageProjectResponse { Name = "A", } };
        }

        // GET: api/Project/5
        public ProjectPageProjectResponse Get(string id)
        {
            return _projectBusiness.GetProject(null, Guid.Parse(id)).ToProjectPageProjectResponse();
        }

        // POST: api/Project
        public void Post([FromBody]string value)
        {
            throw new NotImplementedException();
        }

        // PUT: api/Project/5
        public void Put(int id, [FromBody]string value)
        {
            throw new NotImplementedException();
        }

        // DELETE: api/Project/5
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
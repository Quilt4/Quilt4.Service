using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Cors.Core;
using Microsoft.AspNet.Mvc;
using Quilt4.Api.Converters;
using Quilt4.Api.DataTransfer;
using Quilt4.Api.Entities;
using Quilt4.Api.Interfaces;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Quilt4.Api.Controllers
{
    [EnableCors("AllowAllOrigins")]
    [Route("api/[controller]")]
    public class ProjectController : Controller
    {
        private readonly IProjectBusiness _projectBusiness;

        public ProjectController(IProjectBusiness projectBusiness)
        {
            _projectBusiness = projectBusiness;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<ProjectResponse> Get()
        {
            //TODO: Get username from header
            //TODO: Check that the call is valid
            var projects = _projectBusiness.GetProjects(string.Empty).Select(x => x.ToProjectResponse());
            return projects;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ProjectResponse Get(string id)
        {
            //TODO: Get username from header
            //TODO: Check that the call is valid
            var project = _projectBusiness.GetProject(string.Empty, Guid.Parse(id)).ToProjectResponse();
            return project;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]ProjectRequest value)
        {
            //TODO: Get username from header
            //TODO: Check that the call is valid
            var project = new Project(Guid.NewGuid(), value.Name, string.Empty, new Entities.Version[] { }, new Session[] { }, new Entities.IssueType[] { }, "red");
            _projectBusiness.CreateProject(project);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(string id, [FromBody]ProjectRequest value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
        }
    }
}
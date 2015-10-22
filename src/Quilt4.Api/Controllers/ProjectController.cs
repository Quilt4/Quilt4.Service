using System.Collections.Generic;
using Microsoft.AspNet.Mvc;
using Quilt4.Api.Entities;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Quilt4.Api.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        // POST api/values
        [HttpPost("Create")]
        public void Post([FromBody]CreateUserRequest value)
        {
        }
    }

    [Route("api/[controller]")]
    public class ProjectController : Controller
    {
        // GET: api/values
        [HttpGet]
        public IEnumerable<Project> Get()
        {
            return new[] { new Project { Name = "Project 1" }, new Project { Name = "Project 2" } };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Project Get(int id)
        {
            return new Project { Name = "Project " + id };
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]Project value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]Project value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

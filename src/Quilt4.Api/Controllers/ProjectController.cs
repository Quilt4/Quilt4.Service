using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Cors.Core;
using Microsoft.AspNet.Mvc;
using Quilt4.Api.DataTransfer;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Quilt4.Api.Controllers
{
    [EnableCors("AllowAllOrigins")]
    [Route("api/[controller]")]
    public class ProjectController : Controller
    {
        private List<Project> _projects = new List<Project>
        {
            new Project
            {
                Id = "1",
                Name = "Eplicta2",
                Versions = 2,
                Sessions = 4,
                IssueTypes = 12,
                Issues = 20,
                DashboardColor = "red"
            },
            new Project
            {
                Id = "2",
                Name = "Florida",
                Versions = 20,
                Sessions = 3299,
                IssueTypes = 1,
                Issues = 2130,
                DashboardColor = "blue"
            }
        };

            // GET: api/values
        [HttpGet]
        public IEnumerable<Project> Get()
        {
            return _projects;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Project Get(string id)
        {
            return _projects.FirstOrDefault(x => x.Id == id);
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

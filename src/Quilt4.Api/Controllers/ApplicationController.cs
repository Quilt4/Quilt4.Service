using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Cors.Core;
using Microsoft.AspNet.Mvc;
using Quilt4.Api.DataTransfer;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Quilt4.Api.Controllers
{
    [EnableCors("AllowAllOrigins")]
    [Route("api/[controller]")]
    public class ApplicationController : Controller
    {
        private List<Application> _applications = new List<Application>
        {
            new Application
            {
                Id = "1",
                ProjectId = "1",
                Name = "Eplicta.MediaMapper.Web",
                Versions = 2,
                Sessions = 4,
                IssueTypes = 12,
                Issues = 20,
            },
            new Application
            {
                Id = "2",
                ProjectId = "1",
                Name = "Eplicat.Cruiser.Web",
                Versions = 3,
                Sessions = 45,
                IssueTypes = 1,
                Issues = 18,
            },
            new Application
            {
                Id = "1",
                ProjectId = "2",
                Name = "Florida.Web",
                Versions = 2,
                Sessions = 4,
                IssueTypes = 12,
                Issues = 20,
            },
            new Application
            {
                Id = "2",
                ProjectId = "2",
                Name = "Florida.Nevada",
                Versions = 3,
                Sessions = 45,
                IssueTypes = 1,
                Issues = 18,
            },
        };

        [HttpGet("{ProjectId}")]
        public IEnumerable<Application> Get(string projectId)
        {
            return _applications.Where(x => x.ProjectId == projectId);
        }

        [HttpGet("{ProjectId}/{ApplicationId}")]
        public Application Get(string projectId, string applicationId)
        {
            return _applications.FirstOrDefault(x => x.ProjectId == projectId && x.Id == applicationId);
        }
    }
}

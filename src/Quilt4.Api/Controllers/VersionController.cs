using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Cors.Core;
using Microsoft.AspNet.Mvc;
using Version = Quilt4.Api.DataTransfer.Version;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Quilt4.Api.Controllers
{
    [EnableCors("AllowAllOrigins")]
    [Route("api/[controller]")]
    public class VersionController : Controller
    {
        private List<Version> _versions = new List<Version>
        {
            new Version
            {
                Id = "1",
                ProjectId = "1",
                ApplicationId = "1",
                Name = "1.0",
                Sessions = 4,
                IssueTypes = 12,
                Issues = 20,
                Enviroments = new List<string> {"Prod", "CI" },
                Last = DateTime.UtcNow.AddDays(-4)
            },
            new Version
            {
                Id = "2",
                ProjectId = "1",
                ApplicationId = "1",
                Name = "1.1",
                Sessions = 4,
                IssueTypes = 12,
                Issues = 20,
                Enviroments = new List<string> {"Dev", "CI" },
                Last = DateTime.UtcNow.AddDays(-3)
            },
            new Version
            {
                Id = "3",
                ProjectId = "1",
                ApplicationId = "1",
                Name = "1.3",
                Sessions = 4,
                IssueTypes = 12,
                Issues = 20,
                Enviroments = new List<string> {"Prod", "Dev" },
                Last = DateTime.UtcNow.AddDays(-2)
            },
            new Version
            {
                Id = "4",
                ProjectId = "1",
                ApplicationId = "1",
                Name = "1.4",
                Sessions = 4,
                IssueTypes = 12,
                Issues = 20,
                Enviroments = new List<string> {"Prod" },
                Last = DateTime.UtcNow
            },
        };

        [HttpGet("{ProjectId}/{ApplicationId}")]
        public IEnumerable<Version> Get(string projectId, string applicationId)
        {
            return _versions.Where(x => x.ProjectId == projectId && x.ApplicationId == applicationId);
        }

        [HttpGet("{ProjectId}/{ApplicationId}/{VersionId}")]
        public Version Get(string projectId, string applicationId, string versionId)
        {
            return _versions.FirstOrDefault(x => x.ProjectId == projectId && x.ApplicationId == applicationId && x.Id == versionId);
        }
    }
}

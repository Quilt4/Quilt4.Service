using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Cors.Core;
using Microsoft.AspNet.Mvc;
using Quilt4.Api.DataTransfer;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Quilt4.Api.Controllers
{
    [EnableCors("AllowAllOrigins")]
    [Route("api/[controller]")]
    public class IssueController : Controller
    {
        private readonly List<Issue> _issues = new List<Issue>
        {
            new Issue
            {
                Id = "1",
                ProjectId = "1",
                ApplicationId = "1",
                VersionId = "1",
                IssueTypeId = "1",
                Enviroment = "Prod",
                User = "Daniel",
                Time = DateTime.UtcNow.AddDays(-4),
                Data = null
            },
            new Issue
            {
                Id = "2",
                ProjectId = "1",
                ApplicationId = "1",
                VersionId = "1",
                IssueTypeId = "1",
                Enviroment = "CI",
                User = "Pelle",
                Time = DateTime.UtcNow.AddDays(-3),
                Data = null
            },
            new Issue
            {
                Id = "3",
                ProjectId = "1",
                ApplicationId = "1",
                VersionId = "1",
                IssueTypeId = "1",
                Enviroment = "Dev",
                User = "Jonathan",
                Time = DateTime.UtcNow,
                Data = new Dictionary<string, string> { {"Message", "Pelle är bäst :D" }, { "TheCount", "230"} }
            }
        };

        [HttpGet("{ProjectId}/{ApplicationId}/{VersionId}/{IssueTypeId}")]
        public IEnumerable<Issue> Get(string projectId, string applicationId, string versionId, string issueTypeId)
        {
            return _issues.Where(x => x.ProjectId == projectId && x.ApplicationId == applicationId && x.VersionId == versionId && x.IssueTypeId == issueTypeId);
        }

    }
}
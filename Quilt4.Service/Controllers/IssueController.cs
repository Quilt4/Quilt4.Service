using System;
using System.Collections.Generic;
using System.Web.Http;
using Quil4.Service.Interface.Business;
using Quilt4.Service.Converters.Issue;
using Quilt4.Service.DataTransfer;

namespace Quilt4.Service.Controllers
{
    public class IssueController : ApiController
    {
        private readonly IIssueBusiness _issueBusiness;

        public IssueController(IIssueBusiness issueBusiness)
        {
            _issueBusiness = issueBusiness;
        }

        [Route("api/project/{projectId}/application/{applicationId}/version/{versionId}/issuetype/{issueTypeId}/issue")]
        public IEnumerable<IssueResponse> GetAllIssues(string projectId, string applicationId, string versionId,
            string issueTypeId)
        {
            return _issueBusiness.GetIssues(null, Guid.Parse(projectId), Guid.Parse(applicationId),
                Guid.Parse(versionId), Guid.Parse(issueTypeId)).ToIssueResponses();
        }

        [Route(
            "api/project/{projectId}/application/{applicationId}/version/{versionId}/issuetype/{issueTypeId}/issue/{issueId}"
            )]
        public IssueResponse GetIssue(string projectId, string applicationId, string versionId,
            string issueTypeId, string issueId)
        {
            return _issueBusiness.GetIssue(null, Guid.Parse(projectId), Guid.Parse(applicationId),
                Guid.Parse(versionId), Guid.Parse(issueTypeId), Guid.Parse(issueId)).ToIssueResponse();
        }
    }
}
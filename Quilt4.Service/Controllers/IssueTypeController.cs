using System;
using System.Collections.Generic;
using System.Web.Http;
using Quil4.Service.Interface.Business;
using Quilt4.Service.Converters.IssueType;
using Quilt4.Service.DataTransfer;

namespace Quilt4.Service.Controllers
{
    public class IssueTypeController : ApiController
    {
        private readonly IIssueTypeBusiness _issueTypeBusiness;
        public IssueTypeController(IIssueTypeBusiness issueTypeBusiness)
        {
            _issueTypeBusiness = issueTypeBusiness;
        }

        [Route("api/project/{projectId}/application/{applicationId}/version/{versionId}/issuetype")]
        public IEnumerable<IssueTypeResponse> GetAllIssueTypes(string projectId, string applicationId, string versionId)
        {
            return _issueTypeBusiness.GetIssueTypes(null, Guid.Parse(projectId), Guid.Parse(applicationId), Guid.Parse(versionId)).ToIssueTypeResponses();
        }

        [Route("api/project/{projectId}/application/{applicationId}/version/{versionId}/issuetype/{issueTypeId}")]
        public IssueTypeResponse GetIssueType(string projectId, string applicationId, string versionId, string issueTypeId)
        {
            return _issueTypeBusiness.GetIssueType(null, Guid.Parse(projectId), Guid.Parse(applicationId), Guid.Parse(versionId), Guid.Parse(issueTypeId)).ToIssueTypeResponse();
        }
    }
}
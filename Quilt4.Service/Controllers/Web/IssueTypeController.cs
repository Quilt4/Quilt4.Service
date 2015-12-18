using System;
using System.Diagnostics;
using System.Web.Http;
using Quilt4.Service.Converters;
using Quilt4.Service.DataTransfer;
using Quilt4.Service.Interface.Business;

namespace Quilt4.Service.Controllers
{
    public class IssueTypeController : ApiController
    {
        private readonly IIssueTypeBusiness _issueTypeBusiness;

        public IssueTypeController(IIssueTypeBusiness issueTypeBusiness)
        {
            _issueTypeBusiness = issueTypeBusiness;
        }

        [Authorize]
        [Route("api/project/{projectId}/application/{applicationId}/version/{versionId}/issuetype/{issueTypeId}")]
        public IssueTypePageIssueTypeResponse GetIssueType(string projectId, string applicationId, string versionId,
            string issueTypeId)
        {

            return _issueTypeBusiness.GetIssueType(User.Identity.Name, Guid.Parse(projectId), Guid.Parse(applicationId),
                    Guid.Parse(versionId), Guid.Parse(issueTypeId)).ToIssueTypePageIssueTypeResponse();
        }
    }
}
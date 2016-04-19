using System;
using System.Web.Http;
using Quilt4.Service.Controllers.WebAPI.Web.DataTransfer;
using Quilt4.Service.Converters;
using Quilt4.Service.Interface.Business;

namespace Quilt4.Service.Controllers.WebAPI.Web
{
    public class IssueTypeController : ApiController
    {
        private readonly IIssueTypeBusiness _issueTypeBusiness;

        public IssueTypeController(IIssueTypeBusiness issueTypeBusiness)
        {
            _issueTypeBusiness = issueTypeBusiness;
        }

        [Authorize]
        [Route("api/issuetype/{issueTypeKey}")]
        public IssueTypePageIssueTypeResponse GetIssueType(string issueTypeKey)
        {
            return _issueTypeBusiness.GetIssueType(User.Identity.Name, Guid.Parse(issueTypeKey)).ToIssueTypePageIssueTypeResponse();
        }
    }
}
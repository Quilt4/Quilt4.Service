using System.Web;
using System.Web.Http;
using Quilt4.Service.Converters;
using Quilt4.Service.Interface.Business;
using Quilt4Net.Core.DataTransfer;

namespace Quilt4.Service.Controllers.Client
{
    [Authorize]
    public class ClientIssueController : ApiController
    {
        private readonly IIssueBusiness _issueBusiness;

        public ClientIssueController(IIssueBusiness issueBusiness)
        {
            _issueBusiness = issueBusiness;
        }

        [AllowAnonymous]
        [Route("api/Client/Issue")]
        public IssueResponse Post([FromBody] object value)
        {
            var issueRequest = value.ToIssueRequest();
            var data = issueRequest.ToRegisterIssueRequestEntity(HttpContext.Current.Request.UserHostAddress);
            var response = _issueBusiness.RegisterIssue(data);
            return new IssueResponse
            {
                Ticket = response.Ticket.ToString(),
                IssueKey = issueRequest.IssueKey,
                ServerTime = response.ServerTime,
                //TODO: Append correct paths here
                IssueTypeUrl = "p1/SomePathToIssueType",
                IssueUrl = "p2/SomePathToIssue",
            };
        }
    }
}
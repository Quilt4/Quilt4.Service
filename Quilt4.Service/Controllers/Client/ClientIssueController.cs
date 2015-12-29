using System;
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
                ClientTime = issueRequest.ClientTime,
                Data = issueRequest.Data,
                IssueType = issueRequest.IssueType,
                SessionKey = issueRequest.SessionKey,
                IssueKey = issueRequest.IssueKey,
                IssueThreadKey = issueRequest.IssueThreadKey,
                UserHandle = issueRequest.UserHandle,
                ServerTime = response.ServerTime,                
            };
        }
    }

    //TODO: Remove this class and use the one from quilt4net nuget instead.
    public class IssueTypeResponse
    {
        public Guid IssueTypeKey { get; set; }
        public Guid VersionKey { get; set; }
        public string Type { get; set; }
        public string Level { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public int Ticket { get; set; }
        public DateTime CreationServerTime { get; set; }
        public DateTime LastIssueServerTime { get; set; }
    }
}
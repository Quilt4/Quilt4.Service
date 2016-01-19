using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Quilt4.Service.Converters;
using Quilt4.Service.Entity;
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

        [Route("api/Client/Issue/QueryByVersionKey")]
        public IEnumerable<IssueResponse> QueryByVersionKey([FromBody]Guid versionKey)
        {
            var result = _issueBusiness.GetIssueList(User.Identity.GetUserName(), versionKey);
            var response = result.Select(x => x.ToIssueResponse());
            return response;
        }

        [AllowAnonymous]
        [Route("api/Client/Issue")]
        public IssueResponse Post([FromBody] object value)
        {
            var issueRequest = value.ToIssueRequest();
            var data = issueRequest.ToRegisterIssueRequestEntity(HttpContext.Current.Request.UserHostAddress);
            var response = _issueBusiness.RegisterIssue(data);
            return response.ToIssueResponse();
        }
    }
}
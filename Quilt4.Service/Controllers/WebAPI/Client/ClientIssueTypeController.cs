using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Quilt4.Service.Interface.Business;
using Quilt4Net.Core.DataTransfer;

namespace Quilt4.Service.Controllers.Client
{
    [Authorize]
    public class ClientIssueTypeController : ApiController
    {
        private readonly IIssueBusiness _issueBusiness;

        public ClientIssueTypeController(IIssueBusiness issueBusiness)
        {
            _issueBusiness = issueBusiness;
        }

        [HttpPost]
        [Route("api/Client/IssueType/QueryByVersionKey")]
        public IEnumerable<IssueTypeResponse> QueryByVersionKey([FromBody]Guid versionKey)
        {
            var response = _issueBusiness.GetIssueTypeList(User.Identity.Name, versionKey).Select(x => new IssueTypeResponse { Ticket = x.Ticket, CreationServerTime = x.CreationServerTime, IssueTypeKey = x.IssueTypeKey, VersionKey = x.VersionKey, Message = x.Message, Type = x.Type, Level = x.Level, StackTrace = x.StackTrace });
            return response;
        }
    }
}
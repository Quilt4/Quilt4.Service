using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Quilt4.Service.Converters;
using Quilt4.Service.Interface.Business;
using Quilt4Net.Core.DataTransfer;

namespace Quilt4.Service.Controllers.WebAPI.Client
{
    [Authorize]
    public class ClientIssueTypeController : ApiController
    {
        private readonly IIssueBusiness _issueBusiness;

        public ClientIssueTypeController(IIssueBusiness issueBusiness)
        {
            _issueBusiness = issueBusiness;
        }

        [HttpGet]
        [Route("api/Client/IssueType/{versionKey}")]
        public IEnumerable<IssueTypeResponse> Get([FromUri]Guid versionKey)
        {
            var response = _issueBusiness.GetIssueTypeList(User.Identity.Name, versionKey).Select(x => x.ToIssueTypeResponse());
            return response;
        }
    }
}
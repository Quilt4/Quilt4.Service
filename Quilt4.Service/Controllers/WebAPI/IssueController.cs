using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Quilt4.Service.Converters;
using Quilt4.Service.Interface.Business;
using Quilt4Net.Core.DataTransfer;

namespace Quilt4.Service.Controllers.WebAPI
{
    [Authorize]
    public class IssueController : ApiController
    {
        private readonly IIssueBusiness _issueBusiness;
        private readonly ISettingBusiness _settingBusiness;

        public IssueController(IIssueBusiness issueBusiness, ISettingBusiness settingBusiness)
        {
            _issueBusiness = issueBusiness;
            _settingBusiness = settingBusiness;
        }

        [HttpGet]
        [Route("api/Issue/{versionKey}")]
        public IEnumerable<IssueResponse> Get([FromUri] Guid versionKey)
        {
            var result = _issueBusiness.GetIssueList(User.Identity.GetUserName(), versionKey);
            var response = result.Select(x => x.ToIssueResponse(_settingBusiness.WebUrl));
            return response;
        }

        [AllowAnonymous]
        [Route("api/Issue")]
        public IssueResponse Post([FromBody] object value)
        {
            var issueRequest = value.ToIssueRequest();
            var data = issueRequest.ToRegisterIssueRequestEntity(HttpContext.Current.Request.UserHostAddress);
            var response = _issueBusiness.RegisterIssue(data);
            return response.ToIssueResponse(_settingBusiness.WebUrl);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Quilt4.Service.Interface.Business;

namespace Quilt4.Service.Controllers.Client
{
    [Authorize]
    public class ClientApplicationController : ApiController
    {
        private readonly IApplicationBusiness _applicationBusiness;

        public ClientApplicationController(IApplicationBusiness applicationBusiness)
        {
            _applicationBusiness = applicationBusiness;
        }

        [Route("api/Client/Application/QueryByProject")]
        public IEnumerable<ApplicationResponse> QueryByProject([FromBody]Guid projectKey)
        {
            var response = _applicationBusiness.GetApplications(User.Identity.Name, projectKey);
            return response.Select(x => new ApplicationResponse { ApplicationKey = x.ApplicationKey, Name = x.Name });
        }
    }

    [Authorize]
    public class ClientVersionController : ApiController
    {
        private readonly IVersionBusiness _versionBusiness;

        public ClientVersionController(IVersionBusiness versionBusiness)
        {
            _versionBusiness = versionBusiness;
        }

        [HttpPost]
        [Route("api/Client/Version/QueryByApplication")]
        public IEnumerable<VersionResponse> QueryByApplication([FromBody]Guid applicationKey)
        {
            var response = _versionBusiness.GetVersions(User.Identity.Name, applicationKey);
            return response.Select(x => new VersionResponse { VersionKey = x.VersionKey, VersionNumber = x.VersionNumber });
        }
    }

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
            var response = _issueBusiness.GetIssueTypeList(User.Identity.Name, versionKey).Select(x => new IssueTypeResponse { Ticket = x.Ticket, CreationServerTime = x.CreationServerTime, IssueTypeKey = x.IssueTypeKey, LastIssueServerTime = x.LastIssueServerTime, VersionKey = x.VersionKey, Message = x.Message, Type = x.Type, Level = x.Level, StackTrace = x.StackTrace });
            return response;
        }
    }

    //TODO: Remove VersionResponse and replace it with the Quilt4net nuget version
    public class VersionResponse
    {
        public Guid VersionKey { get; set; }
        public string VersionNumber { get; set; }
    }

    //TODO: Remove ApplicationResponse and replace it with the Quilt4net nuget version
    public class ApplicationResponse
    {
        public Guid ApplicationKey { get; set; }
        public string Name { get; set; }
    }
}
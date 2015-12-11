using System;
using System.Web.Http;
using Quilt4.Service.Converters;
using Quilt4.Service.DataTransfer;
using Quilt4.Service.Interface.Business;

namespace Quilt4.Service.Controllers
{
    public class VersionController : ApiController
    {
        private readonly IVersionBusiness _versionBusiness;

        public VersionController(IVersionBusiness versionBusiness)
        {
            _versionBusiness = versionBusiness;
        }

        [Authorize]
        [Route("api/project/{projectId}/application/{applicationId}/version/{versionId}")]
        public VersionPageVersionResponse GetVersion(string projectId, string applicationId, string versionId)
        {
            return _versionBusiness.GetVersion(null, Guid.Parse(projectId), Guid.Parse(applicationId), Guid.Parse(versionId)).ToVersionPageVersionResponse();
        }
    }
}
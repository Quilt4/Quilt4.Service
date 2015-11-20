using System;
using System.Threading;
using System.Web.Http;
using Quil4.Service.Interface.Business;
using Quilt4.Service.Converters;
using Quilt4.Service.DataTransfer;

namespace Quilt4.Service.Controllers
{
    public class VersionController : ApiController
    {
        private readonly IVersionBusiness _versionBusiness;

        public VersionController(IVersionBusiness versionBusiness)
        {
            _versionBusiness = versionBusiness;
        }

        [Route("api/project/{projectId}/application/{applicationId}/version/{versionId}")]
        public VersionPageVersionResponse GetVersion(string projectId, string applicationId, string versionId)
        {
            return
                _versionBusiness.GetVersion(null, Guid.Parse(projectId), Guid.Parse(applicationId),
                    Guid.Parse(versionId)).ToVersionPageVersionResponse();
        }
    }
}
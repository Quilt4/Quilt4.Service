using System;
using System.Collections.Generic;
using System.Web.Http;
using Quil4.Service.Interface.Business;
using Quilt4.Service.Converters.Version;
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

        [Route("api/project/{projectId}/application/{applicationId}/version")]
        public IEnumerable<VersionResponse> GetAllVersions(string projectId, string applicationId)
        {
            return _versionBusiness.GetVersions(null, Guid.Parse(projectId), Guid.Parse(applicationId)).ToVersionResponses();
        }


        [Route("api/project/{projectId}/application/{applicationId}/version/{versionId}")]
        public VersionResponse GetVersion(string projectId, string applicationId, string versionId)
        {
            return _versionBusiness.GetVersion(null, Guid.Parse(projectId), Guid.Parse(applicationId), Guid.Parse(versionId)).ToVersionResponse();
        }
    }
}
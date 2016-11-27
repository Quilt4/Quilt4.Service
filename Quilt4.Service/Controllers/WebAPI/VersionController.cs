using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Quilt4.Service.Interface.Business;
using Quilt4Net.Core.DataTransfer;

namespace Quilt4.Service.Controllers.WebAPI
{
    [Authorize]
    public class VersionController : ApiController
    {
        private readonly IVersionBusiness _versionBusiness;

        public VersionController(IVersionBusiness versionBusiness)
        {
            _versionBusiness = versionBusiness;
        }

        [HttpGet]
        [Route("api/Version/{applicationKey}")]
        public IEnumerable<VersionResponse> Get([FromUri]Guid applicationKey)
        {
            var response = _versionBusiness.GetVersions(User.Identity.Name, applicationKey);
            return response.Select(x => new VersionResponse { VersionKey = x.VersionKey, VersionNumber = x.VersionNumber });
        }
    }
}
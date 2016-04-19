using System;
using System.Web.Http;
using Quilt4.Service.Controllers.WebAPI.Web.DataTransfer;
using Quilt4.Service.Converters;
using Quilt4.Service.Interface.Business;

namespace Quilt4.Service.Controllers.WebAPI.Web
{
    public class VersionController : ApiController
    {
        private readonly IVersionBusiness _versionBusiness;

        public VersionController(IVersionBusiness versionBusiness)
        {
            _versionBusiness = versionBusiness;
        }

        [Authorize]
        [Route("api/version/{versionId}")]
        public VersionPageVersionResponse GetVersion(string versionId)
        {
            return _versionBusiness.GetVersion(User.Identity.Name, Guid.Parse(versionId)).ToVersionPageVersionResponse();
        }
    }
}
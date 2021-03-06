using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Quilt4.Service.Interface.Business;
using Quilt4Net.Core.DataTransfer;

namespace Quilt4.Service.Controllers.WebAPI.Client
{
    [Authorize]
    public class ClientApplicationController : ApiController
    {
        private readonly IApplicationBusiness _applicationBusiness;

        public ClientApplicationController(IApplicationBusiness applicationBusiness)
        {
            _applicationBusiness = applicationBusiness;
        }

        [HttpGet]
        [Route("api/Client/Application/{projectKey}")]
        public IEnumerable<ApplicationResponse> Get([FromUri]Guid projectKey)
        {
            var response = _applicationBusiness.GetApplications(User.Identity.Name, projectKey);
            return response.Select(x => new ApplicationResponse { ApplicationKey = x.ApplicationKey, Name = x.Name });
        }
    }
}
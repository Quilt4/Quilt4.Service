using System;
using System.Collections.Generic;
using System.Web.Http;
using Quil4.Service.Interface.Business;
using Quilt4.Service.Converters.Application;
using Quilt4.Service.DataTransfer;

namespace Quilt4.Service.Controllers
{
    public class ApplicationController : ApiController
    {
        private readonly IApplicationBusiness _applicationBusiness;
        public ApplicationController(IApplicationBusiness applicationBusiness)
        {
            _applicationBusiness = applicationBusiness;
        }

        [Route("api/project/{projectId}/application")]
        public IEnumerable<ApplicationResponse> GetAllApplications(string projectId)
        {
            return _applicationBusiness.GetApplications(null, Guid.Parse(projectId)).ToApplicationResponses();
        }

        [Route("api/project/{projectId}/application/{applicationId}")]
        public ApplicationResponse GetApplication(string projectId, string applicationId)
        {
            return _applicationBusiness.GetApplication(null, Guid.Parse(projectId), Guid.Parse(applicationId)).ToApplicationResponse();
        }
    }
}
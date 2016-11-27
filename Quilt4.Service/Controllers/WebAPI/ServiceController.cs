using System.Web.Http;
using Quilt4.Service.Interface.Business;
using Quilt4Net.Core.DataTransfer;

namespace Quilt4.Service.Controllers.WebAPI
{
    [Authorize(Roles = Constants.Administrators)]
    public class ServiceController : ApiController
    {
        private readonly IServiceBusiness _serviceBusiness;

        public ServiceController(IServiceBusiness serviceBusiness)
        {
            _serviceBusiness = serviceBusiness;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("api/Service")]
        public ServiceInfoResponse Get()
        {
            var isAuthenticated = User.Identity.IsAuthenticated;
            var isAdministrator = User.IsInRole(Constants.Administrators);

            var data = _serviceBusiness.GetServiceInfo();
            var response = new ServiceInfoResponse
            {
                Version = data.Version
            };

            if (isAuthenticated)
            {
                response.Environment = data.Environment;

                if (isAdministrator)
                {
                    response.StartTime = data.StartTime;
                    response.Database = new DatabaseInfoResponse
                    {
                        Version = data.DatabaseInfo.Version,
                        CanConnect = data.DatabaseInfo.CanConnect,
                        Database = data.DatabaseInfo.Database
                    };
                    response.CanWriteToSystemLog = data.CanWriteToSystemLog;
                    response.HasOwnProjectApiKey = data.HasOwnProjectApiKey;
                }
                else
                {
                    response.Message = "Login as a user with administrative rights to get more information.";
                }
            }
            else
            {
                response.Message = "Login to get more information.";
            }

            return response;
        }
    }
}
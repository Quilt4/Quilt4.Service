using System.Web.Http;
using Quilt4.Service.Interface.Business;
using Quilt4Net.Core.DataTransfer;

namespace Quilt4.Service.Controllers
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
        [Route("api/Service")]
        public ServiceInfoResponse Get()
        {
            var data = _serviceBusiness.GetServiceInfo();
            return new ServiceInfoResponse
            {
                Version = data.Version,
                StartTime = data.StartTime,
                Environment = data.Environment,
                Database = new DatabaseInfoResponse
                {
                    Version = data.DatabaseInfo.Version,
                    CanConnect = data.DatabaseInfo.CanConnect,
                    Database = data.DatabaseInfo.Database,
                },
                CanWriteToSystemLog = data.CanWriteToSystemLog,
                HasOwnProjectApiKey = data.HasOwnProjectApiKey,
            };
        }
    }    
}
using System.Web.Http;
using Quilt4.Service.Interface.Business;
using Quilt4Net.Core.DataTransfer;

namespace Quilt4.Service.Controllers.Client
{
    public class ServiceController : ApiController
    {
        private readonly IServiceBusiness _serviceBusiness;

        public ServiceController(IServiceBusiness serviceBusiness)
        {
            _serviceBusiness = serviceBusiness;
        }

        [HttpPost]
        [Route("api/Client/Service/QueryInfo")]
        public ServiceInfoResponse QueryInfo()
        {
            var data = _serviceBusiness.GetServiceInfo();
            return new ServiceInfoResponse
            {
                Version = data.Version,
                StartTime = data.StartTime,
                Environment = data.Environment,
                DatabaseInfo = data.DatabaseInfo,
            };
        }
    }    
}
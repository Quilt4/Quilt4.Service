using System.Web.Http;
using Quilt4.Service.Interface.Business;

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
                Environment = data.Environment,
                DatabaseInfo = data.DatabaseInfo,
            };
        }
    }

    public class ServiceInfoResponse
    {
        //TODO: Replace with nuget version
        public string Version { get; set; }
        public string Environment { get; set; }
        public string DatabaseInfo { get; set; }
    }
}
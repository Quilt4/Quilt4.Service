using System.Collections.Generic;
using System.Web.Http;
using Quilt4.Service.Interface.Business;

namespace Quilt4.Service.Controllers.WebAPI
{
    [Authorize(Roles = Constants.Administrators)]
    public class ServiceLogController : ApiController
    {
        private readonly IServiceLog _serviceLog;

        public ServiceLogController(IServiceLog serviceLog)
        {
            _serviceLog = serviceLog;
        }

        [Route("api/Service/Log")]
        public IEnumerable<IServiceLogItem> Get()
        {
            return _serviceLog.GetAllLogEntries();
        }
    }
}
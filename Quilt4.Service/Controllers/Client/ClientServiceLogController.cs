using System.Collections.Generic;
using System.Web.Http;
using Quilt4.Service.Interface.Business;
using Quilt4.Service.Models;

namespace Quilt4.Service.Controllers.Client
{
    [Authorize(Roles = "Administrators")]
    public class ClientServiceLogController : ApiController
    {
        private readonly IServiceLog _serviceLog;

        public ClientServiceLogController(IServiceLog serviceLog)
        {
            _serviceLog = serviceLog;
        }

        [Route("api/Client/Service/Log")]
        public IEnumerable<IServiceLogItem> Get()
        {
            return _serviceLog.GetAllLogEntries();
        }
    }
}
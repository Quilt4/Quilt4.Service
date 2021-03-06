﻿using System.Collections.Generic;
using System.Web.Http;
using Quilt4.Service.Interface.Business;

namespace Quilt4.Service.Controllers.WebAPI.Client
{
    [Authorize(Roles = Constants.Administrators)]
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
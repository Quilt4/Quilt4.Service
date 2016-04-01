using System;
using System.Web.Mvc;
using Quilt4.Service.Interface.Business;
using Quilt4.Service.Models;

namespace Quilt4.Service.Controllers
{
    public class SharedController : Controller
    {
        private readonly IServiceLog _serviceLog;
        private readonly IServiceBusiness _serviceBusiness;

        public SharedController(IServiceLog serviceLog, IServiceBusiness serviceBusiness)
        {
            _serviceLog = serviceLog;
            _serviceBusiness = serviceBusiness;
        }

        public ActionResult _SystemStatus()
        {
            Exception exception;
            var systemStatus = new SystemStatus
            {
                CanWriteToLog = _serviceLog.CanWriteToLog(out exception),
                CanConnectToDatabase = _serviceBusiness.GetDatabaseInfo().CanConnect
            };
            return PartialView(systemStatus);
        }
    }
}
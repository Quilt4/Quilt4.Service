using System.Web.Mvc;
using Quilt4.Service.Interface.Business;
using Quilt4Net.Core.Interfaces;

namespace Quilt4.Service.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ISessionHandler _sessionHandler;
        //private readonly IServiceBusiness _serviceBusiness;

        public HomeController() //ISessionHandler sessionHandler, IServiceBusiness serviceBusiness)
        {
            //_sessionHandler = sessionHandler;
            //_serviceBusiness = serviceBusiness;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Contribute()
        {
            return View();
        }

        public ActionResult System()
        {
            //var applicationData = _sessionHandler.Client.Information.Aplication.GetApplicationData();
            //@ViewBag.Version = applicationData.Version + " (" + _sessionHandler.Environment + ")";
            //@ViewBag.StartInfo = _sessionHandler.ClientStartTime + " (" + _sessionHandler.IsRegisteredOnServer + ")";

            //var serviceInfo = _serviceBusiness.GetServiceInfo();
            //@ViewBag.CanWriteToSystemLog = serviceInfo.CanWriteToSystemLog;
            //@ViewBag.HasOwnProjectApiKey = serviceInfo.HasOwnProjectApiKey;

            //var databaseInfo = serviceInfo.DatabaseInfo;
            //@ViewBag.DatabaseInfo = $"{databaseInfo.DataSource}.{databaseInfo.Database} (Version: {databaseInfo.Version}) {(databaseInfo.CanConnect ? "Online" : "Offline")}";

            //@ViewBag.UserName = _sessionHandler.Client.Information.User.GetDataUser().UserName;

            return View();
        }
    }
}
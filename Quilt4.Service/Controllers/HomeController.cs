using System.Web.Mvc;
using Quilt4.Service.Interface.Business;
using Quilt4Net.Interfaces;

namespace Quilt4.Service.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISessionHandler _sessionHandler;
        private readonly IServiceBusiness _serviceBusiness;
        private readonly ISettingBusiness _settingBusiness;

        public HomeController(ISessionHandler sessionHandler, IServiceBusiness serviceBusiness, ISettingBusiness settingBusiness)
        {
            _sessionHandler = sessionHandler;
            _serviceBusiness = serviceBusiness;
            _settingBusiness = settingBusiness;
        }

        public ActionResult Index()
        {
            ViewBag.WebUrl = _settingBusiness.GetSetting("WebUrl", "https://quilt4.com/");
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
            var applicationData = _sessionHandler.Client.Information.Application.GetApplicationData();
            @ViewBag.Version = applicationData.Version + " (" + _sessionHandler.Environment + ")";
            @ViewBag.StartInfo = _sessionHandler.ClientStartTime + " (" + _sessionHandler.IsRegisteredOnServer + ")";

            var serviceInfo = _serviceBusiness.GetServiceInfo();
            @ViewBag.CanWriteToSystemLog = serviceInfo.CanWriteToSystemLog;
            @ViewBag.HasOwnProjectApiKey = serviceInfo.HasOwnProjectApiKey;

            var databaseInfo = serviceInfo.DatabaseInfo;
            @ViewBag.DatabaseInfo = $"{databaseInfo.DataSource}.{databaseInfo.Database} (Version: {databaseInfo.Version}) {(databaseInfo.CanConnect ? "Online" : "Offline")}";

            @ViewBag.UserName = _sessionHandler.Client.Information.User.GetDataUser().UserName;

            return View();
        }
    }
}
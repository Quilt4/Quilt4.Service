using System.Collections.Generic;
using System.Web.Mvc;
using Quilt4.Service.Areas.Admin.Models;

namespace Quilt4.Service.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin/Admin
        public ActionResult Index()
        {
            return View(new SettingsModel { Values = new Dictionary<string, string> { { "A", "1" }, { "B", "2" } } });
        }
    }
}
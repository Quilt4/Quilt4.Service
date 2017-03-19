using System.Linq;
using System.Web.Mvc;
using Quilt4.Service.Converters;
using Quilt4.Service.Interface.Business;

namespace Quilt4.Service.Areas.User.Controllers
{
    [Authorize]
    public class ProjectController : Controller
    {
        private readonly IProjectBusiness _projectBusiness;

        public ProjectController(IProjectBusiness projectBusiness)
        {
            _projectBusiness = projectBusiness;
        }

        public ActionResult Index()
        {
            var projects = _projectBusiness.GetProjects(User.Identity.Name);
            var model = projects.Select(x => x.ToProjectData());
            return View(model);
        }
    }
}
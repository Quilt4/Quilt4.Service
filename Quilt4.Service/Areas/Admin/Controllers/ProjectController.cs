using System.Linq;
using System.Web.Mvc;
using Quilt4.Service.Areas.Admin.Models;
using Quilt4.Service.Interface.Business;

namespace Quilt4.Service.Areas.Admin.Controllers
{
    [Authorize(Roles = Constants.Administrators)]
    public class ProjectController : Controller
    {
        private readonly IProjectBusiness _projectBusiness;

        public ProjectController(IProjectBusiness projectBusiness)
        {
            _projectBusiness = projectBusiness;
        }

        // GET: Admin/Project
        public ActionResult Index()
        {
            var projects = _projectBusiness.GetAllProjects();
            var projectModels = projects.Select(x => new ProjectModel { ProjectKey = x.ProjectKey, Name = x.Name, ApplicationCount = x.Applications.Count() }).ToArray();
            return View(projectModels);
        }
    }
}
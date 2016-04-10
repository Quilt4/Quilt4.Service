using System;
using System.Linq;
using System.Web.Mvc;
using Quilt4.Service.Areas.Admin.Models;
using Quilt4.Service.Interface.Business;

namespace Quilt4.Service.Areas.Admin.Controllers
{
    [Authorize(Roles = Constants.Administrators)]
    public class VersionController : Controller
    {
        private readonly IVersionBusiness _versionBusiness;

        public VersionController(IVersionBusiness versionBusiness)
        {
            _versionBusiness = versionBusiness;
        }

        public ActionResult Index(Guid id)
        {
            var applications = _versionBusiness.GetVersions(id);
            var view = applications.Select(x => new VersionModel { VersionKey = x.VersionKey, VersionNumber = x.VersionNumber });
            return View(view);
        }
    }

    [Authorize(Roles = Constants.Administrators)]
    public class ApplicationController : Controller
    {
        private readonly IApplicationBusiness _applicationBusiness;

        public ApplicationController(IApplicationBusiness applicationBusiness)
        {
            _applicationBusiness = applicationBusiness;
        }

        public ActionResult Index(Guid id)
        {
            var applications = _applicationBusiness.GetApplications(id);
            var view = applications.Select(x => new ApplicationModel { ApplicationKey = x.ApplicationKey, Name = x.Name });
            return View(view);
        }
    }
}
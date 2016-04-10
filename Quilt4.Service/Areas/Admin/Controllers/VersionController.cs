using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

        public ActionResult Details(Guid id)
        {
            var version = _versionBusiness.GetVersion(id);
            return View(new VersionDetailsModel
            {
                VersionKey = version.VersionKey,
                VersionNumber = version.VersionNumber,
                IssueTypes = version.IssueTypes.Select(x => new IssueTypeModel
                {
                    LastIssue = x.LastIssue,
                    Level = x.Level,
                    IssueCount = x.IssueCount,
                    IssueTypeKey = x.IssueTypeKey,
                    Type = x.Type,
                    Message = x.Message,
                    Enviroments = x.Enviroments.ToArray(),
                    FirstIssue = x.FirstIssue,
                    Ticket = x.Ticket
                }).ToArray(),
                Sessions = version.Sessions.Select(x => new SessionModel
                {
                    LastUsedServerTime = x.LastUsedServerTime
                }).ToArray()
            });
        }
    }
}
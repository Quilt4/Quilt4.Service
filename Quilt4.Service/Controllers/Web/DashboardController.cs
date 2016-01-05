using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Quilt4.Service.Controllers.Web.DataTransfer;
using Quilt4.Service.Converters;
using Quilt4.Service.Interface.Business;

namespace Quilt4.Service.Controllers.Web
{
    public class DashboardController : ApiController
    {
        private readonly IDashboardBusiness _dashboardBusiness;

        public DashboardController(IDashboardBusiness dashboardBusiness)
        {
            _dashboardBusiness = dashboardBusiness;
        }

        [Route("api/dashboard/project")]
        [Authorize]
        public IEnumerable<DashboardPageProjectResponse> GetAllProjects()
        {
            var projects = _dashboardBusiness.GetProjects(User.Identity.Name);

            return projects.ToDashboardProjectResponses();
        }

        [Route("api/dashboard/invitation")]
        [Authorize]
        public IEnumerable<ProjectInvitationResponse> GetAllInvitations()
        {
            var projects = _dashboardBusiness.GetInvitations(User.Identity.Name);

            return projects.Select(x => new ProjectInvitationResponse
            {
                ProjectKey = x.ProjectKey,
                Name = x.Name,
                InvitedByUserName = x.InvitedByUserName,
                InviteTime = x.InviteTime
            });
        }
    }
}
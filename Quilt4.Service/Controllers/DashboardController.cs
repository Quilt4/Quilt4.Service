using System.Collections.Generic;
using System.Web.Http;
using Quil4.Service.Interface.Business;
using Quilt4.Service.Converters;
using Quilt4.Service.DataTransfer;

namespace Quilt4.Service.Controllers
{
    public class DashboardController : ApiController
    {
        private readonly IDashboardBusiness _dashboardBusiness;

        public DashboardController(IDashboardBusiness dashboardBusiness)
        {
            _dashboardBusiness = dashboardBusiness;
        }

        [Route("api/dashboard/project")]
        public IEnumerable<DashboardPageProjectResponse> GetAllProjects()
        {
            var projects = _dashboardBusiness.GetProjects(null);

            return projects.ToDashboardProjectResponses();
        }
    }
}
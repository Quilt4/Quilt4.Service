//using System.Collections.Generic;
//using System.Linq;
//using Quilt4.Service.Controllers.WebAPI.Web.DataTransfer;
//using Quilt4.Service.Entity;

//namespace Quilt4.Service.Converters
//{
//    public static class DashboardPageConverter
//    {
//        public static IEnumerable<DashboardPageProjectResponse> ToDashboardProjectResponses(
//            this IEnumerable<DashboardPageProject> items)
//        {
//            return items?.Select(x => x.ToDashboardProjectResponse());
//        }

//        public static DashboardPageProjectResponse ToDashboardProjectResponse(this DashboardPageProject item)
//        {
//            if (item == null)
//                return null;

//            return new DashboardPageProjectResponse
//            {
//                ProjectKey = item.ProjectKey.ToString(),
//                Name = item.Name,
//                VersionCount = item.Versions,
//                SessionCount = item.Sessions,
//                IssueTypeCount = item.IssueTypes,
//                IssueCount = item.Issues,
//                DashboardColor = item.DashboardColor
//            };
//        }
//    }
//}
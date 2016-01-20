//using System.Collections.Generic;
//using System.Linq;

//namespace Quilt4.Service.SqlRepository.Extensions
//{
//    public static class DashboardProjectExtensions
//    {
//        public static IEnumerable<Entity.DashboardPageProject> ToDashboardProjects(
//            this IEnumerable<DashboardPageProject> items)
//        {
//            return items?.Select(x => x.ToDashboardProject());
//        }

//        public static Entity.DashboardPageProject ToDashboardProject(this DashboardPageProject item)
//        {
//            if (item == null)
//                return null;

//            return new Entity.DashboardPageProject
//            {
//                ProjectKey = item.ProjectKey,
//                Name = item.Name,
//                Versions = item.VersionCount,
//                Sessions = item.SessionCount,
//                IssueTypes = item.IssueTypeCount,
//                Issues = item.IssueCount,
//                DashboardColor = item.DashboardColor
//            };
//        }
//    }
//}
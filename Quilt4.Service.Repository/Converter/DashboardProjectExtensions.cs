using System.Collections.Generic;
using System.Linq;
using Quilt4.Service.SqlRepository.Read;

namespace Quilt4.Service.SqlRepository.Converter
{
    internal static class DashboardProjectExtensions
    {
        public static IEnumerable<Entity.DashboardPageProject> ToDashboardProjects(this IEnumerable<DashboardPageProject> items)
        {
            return items?.Select(x => x.ToDashboardProject());
        }

        public static Entity.DashboardPageProject ToDashboardProject(this DashboardPageProject item)
        {
            if (item == null)
                return null;

            return new Entity.DashboardPageProject
            {                
                ProjectKey = item.ProjectKey,
                Name = item.Name,
                VersionCount = item.VersionCount,
                SessionCount = item.SessionCount,
                IssueTypeCount = item.IssueTypeCount,
                IssueCount = item.IssueCount,
                DashboardColor = item.DashboardColor,                
            };
        }
    }
}
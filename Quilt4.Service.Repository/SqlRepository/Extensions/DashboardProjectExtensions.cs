using System.Collections.Generic;
using System.Linq;

namespace Quilt4.Service.Repository.SqlRepository.Extensions
{
    public static class DashboardProjectExtensions
    {
        public static IEnumerable<Entity.DashboardPageProject> ToDashboardProjects(
            this IEnumerable<DashboardPageProject> items)
        {
            return items?.Select(x => x.ToDashboardProject());
        }

        public static Entity.DashboardPageProject ToDashboardProject(this DashboardPageProject item)
        {
            if (item == null)
                return null;

            return new Entity.DashboardPageProject
            {
                Id = item.Id,
                Name = item.Name,
                Versions = item.Versions,
                Sessions = item.Sessions,
                IssueTypes = item.IssueTypes,
                Issues = item.Issues,
                DashboardColor = item.DashboardColor
            };
        }
    }
}
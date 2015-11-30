using System.Collections.Generic;
using System.Linq;
using Quilt4.Service.Interface.Business;
using Quilt4.Service.SqlRepository.Read;

namespace Quilt4.Service.SqlRepository.Converter
{
    internal static class DashboardProjectExtensions
    {
        public static IEnumerable<IDashboardPageProject> ToDashboardProjects(this IEnumerable<DashboardPageProject> items)
        {
            return items?.Select(x => x.ToDashboardProject());
        }

        public static IDashboardPageProject ToDashboardProject(this DashboardPageProject item)
        {
            if (item == null)
                return null;

            return new Entity.DashboardPageProject(item.ProjectKey, item.Name, item.VersionCount, item.SessionCount, item.IssueTypeCount, item.IssueCount, item.DashboardColor);
        }
    }
}
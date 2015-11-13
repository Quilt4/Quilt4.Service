using System.Collections.Generic;
using System.Linq;
using Quilt4.Service.DataTransfer;
using Quilt4.Service.Entity;

namespace Quilt4.Service.Converters
{
    public static class DashboardPageConverter
    {
        public static IEnumerable<DashboardPageProjectResponse> ToDashboardProjectResponses(
            this IEnumerable<DashboardPageProject> items)
        {
            return items?.Select(x => x.ToDashboardProjectResponse());
        }

        public static DashboardPageProjectResponse ToDashboardProjectResponse(this DashboardPageProject item)
        {
            if (item == null)
                return null;

            return new DashboardPageProjectResponse
            {
                Id = item.Id.ToString(),
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
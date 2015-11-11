using System.Collections.Generic;
using System.Linq;
using Quilt4.Service.DataTransfer;

namespace Quilt4.Service.Converters.Project
{
    public static class ProjectConverter
    {
        public static IEnumerable<ProjectResponse> ToProjectResponses(this IEnumerable<Entity.Project> items)
        {
            return items?.Select(x => x.ToProjectResponse());
        }

        public static ProjectResponse ToProjectResponse(this Entity.Project item)
        {
            if (item == null)
                return null;

            return new ProjectResponse
            {
                Id = item.Id.ToString(),
                Name = item.Name,
                Versions = item.Versions,
                Sessions = item.Sessions,
                IssueTypes = item.IssueTypes,
                Issues = item.IssueTypes,
                DashboardColor = item.DashboardColor
            };
        }
    }
}
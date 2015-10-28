using System.Linq;
using Quilt4.Api.DataTransfer;

namespace Quilt4.Api.Converters
{
    public static class ProjectConverter
    {
        public static ProjectResponse ToProjectResponse(this Entities.Project item)
        {
            return new ProjectResponse
            {
                ProjectId = item.ProjectId,
                Name = item.Name,
                VersionCount = item.Versions.Length,
                SessionCount = item.Sessions.Length,
                IssueTypeCount = item.IssueTypes.Length,
                IssueCount = item.IssueTypes.SelectMany(x => x.Issues).Count(),
                DashboardColor = item.DashboardColor,
            };
        }
    }
}
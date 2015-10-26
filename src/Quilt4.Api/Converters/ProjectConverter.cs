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
                Id = item.ProjectId.ToString(),
                Name = item.Name,
                Versions = item.Versions.Length,
                Sessions = item.Sessions.Length,
                IssueTypes = item.IssueTypes.Length,
                Issues = item.IssueTypes.SelectMany(x => x.Issues).Count(),
                DashboardColor = item.DashboardColor,
            };
        }
    }
}
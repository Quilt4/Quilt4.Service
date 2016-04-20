using System.Collections.Generic;
using System.Linq;
using Quilt4.Service.Controllers.WebAPI.Web.DataTransfer;
using Quilt4.Service.Entity;

namespace Quilt4.Service.Converters
{
    public static class ProjectPageConverter
    {
        public static ProjectPageProjectResponse ToProjectPageProjectResponse(this ProjectPageProject item)
        {
            if (item == null)
                return null;

            return new ProjectPageProjectResponse
            {
                ProjectKey = item.ProjectKey.ToString(),
                Name = item.Name,
                DashboardColor = item.DashboardColor,
                ClientToken = item.ProjectApiKey,
                Applications = item.Applications?.ToProjectPageApplicationResponses().ToArray() ?? new ProjectPageApplicationResponse[] {}
            };
        }

        public static IEnumerable<ProjectPageApplicationResponse> ToProjectPageApplicationResponses(this IEnumerable<ProjectPageApplication> items)
        {
            return items?.Select(x => x.ToProjectPageApplicationResponse());
        }

        public static ProjectPageApplicationResponse ToProjectPageApplicationResponse(this ProjectPageApplication item)
        {
            if (item == null)
                return null;

            return new ProjectPageApplicationResponse
            {
                ApplicationKey = item.ApplicationKey.ToString(),
                Name = item.Name,
                VersionCount = item.VersionCount
            };
        }

        public static IEnumerable<ProjectPageVersionResponse> ToProjectPageVersionResponses(
            this IEnumerable<ProjectPageVersion> items)
        {
            return items?.Select(x => x.ToProjectPageVersionResponse());
        }

        public static ProjectPageVersionResponse ToProjectPageVersionResponse(this ProjectPageVersion item)
        {
            if (item == null)
                return null;

            return new ProjectPageVersionResponse
            {
                VersionKey = item.VersionKey.ToString(),
                ProjectKey = item.ProjectKey.ToString(),
                ApplicationKey = item.ApplicationKey.ToString(),
                VersionNumber = item.VersionNumber,
                SessionCount = item.SessionCount,
                IssueTypeCount = item.IssueTypeCount,
                IssueCount = item.IssueCount,
                LastSession = item.LastSession,
                FirstSession = item.FirstSession,
                LastIssue = item.LastIssue,
                Enviroments = item.Enviroments
            };
        }
    }
}
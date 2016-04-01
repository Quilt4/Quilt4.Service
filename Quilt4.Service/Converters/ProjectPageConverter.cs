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
                Id = item.ProjectKey.ToString(),
                Name = item.Name,
                DashboardColor = item.DashboardColor,
                ClientToken = item.ProjectApiKey,
                Applications = item.Applications?.ToProjectPageApplicationResponses().ToArray() ?? new ProjectPageApplicationResponse[] {}
            };
        }

        public static IEnumerable<ProjectPageApplicationResponse> ToProjectPageApplicationResponses(
            this IEnumerable<ProjectPageApplication> items)
        {
            return items?.Select(x => x.ToProjectPageApplicationResponse());
        }

        public static ProjectPageApplicationResponse ToProjectPageApplicationResponse(this ProjectPageApplication item)
        {
            if (item == null)
                return null;

            return new ProjectPageApplicationResponse
            {
                Id = item.Id.ToString(),
                Name = item.Name,
                Versions = item.Versions
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
                Id = item.Id.ToString(),
                ProjectId = item.ProjectId.ToString(),
                ApplicationId = item.ApplicationId.ToString(),
                Version = item.Version,
                Sessions = item.Sessions,
                IssueTypes = item.IssueTypes,
                Issues = item.Issues,
                Last = item.Last,
                Enviroments = item.Enviroments
            };
        }
    }
}
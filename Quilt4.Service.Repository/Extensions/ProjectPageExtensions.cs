using System.Collections.Generic;
using System.Linq;

namespace Quilt4.Service.SqlRepository.Extensions
{
    public static class ProjectPageExtensions
    {
        public static Entity.ProjectPageProject ToProjectPageProject(this ProjectPageProject item, IQueryable<ProjectPageApplication> projectPageApplicaitons)
        {
            if (item == null)
                return null;

            return new Entity.ProjectPageProject
            {
                ProjectKey = item.ProjectKey,
                Name = item.Name,
                DashboardColor = item.DashboardColor,
                ProjectApiKey = item.ProjectApiKey,
                Applications = projectPageApplicaitons.ToProjectPageApplications().ToArray()
            };
        }

        public static IEnumerable<Entity.ProjectPageApplication> ToProjectPageApplications(
            this IQueryable<ProjectPageApplication> items)
        {
            return items?.Select(x => x.ToProjectPageApplication());
        }

        public static Entity.ProjectPageApplication ToProjectPageApplication(this ProjectPageApplication item)
        {
            if (item == null)
                return null;

            return new Entity.ProjectPageApplication
            {
                Id = item.ApplicationKey,
                Name = item.Name,
                Versions = item.VersionCount
            };
        }

        public static IEnumerable<Entity.ProjectPageVersion> ToProjectPageVersions(
            this IEnumerable<ProjectPageVersion> items)
        {
            return items?.Select(x => x.ToProjectPageVersion());
        }

        public static Entity.ProjectPageVersion ToProjectPageVersion(this ProjectPageVersion item)
        {
            return new Entity.ProjectPageVersion
            {
                Id = item.VersionKey,
                ProjectId = item.ProjectKey,
                ApplicationId = item.ApplicationKey,
                Version = item.VersionNumber,
                Sessions = item.SessionCount,
                IssueTypes = item.IssueTypeCount,
                Issues = item.IssueCount,
                Last = item.LastUpdateServerTime,
                Enviroments = item.Enviroments.Split(';')
            };
        }
    }
}
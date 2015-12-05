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
                Id = item.Id,
                Name = item.Name,
                DashboardColor = item.DashboardColor,
                ClientToken = item.ClientToken,
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
                Id = item.Id,
                Name = item.Name,
                Versions = item.Versions
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
                Id = item.Id,
                ProjectId = item.ProjectId,
                ApplicationId = item.ApplicationId,
                Version = item.Version,
                Sessions = item.Sessions,
                IssueTypes = item.IssueTypes,
                Issues = item.Issues,
                Last = item.Last,
                Enviroments = item.Enviroments.Split(';')
            };
        }
    }
}
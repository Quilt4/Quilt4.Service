using System;
using System.Collections.Generic;
using System.Linq;
using Quil4.Service.Interface.Repository;
using Quilt4.Service.Repository.SqlRepository.Extensions;

namespace Quilt4.Service.Repository.SqlRepository
{
    public class SqlReadRepository : IReadRepository
    {
        public Entity.ProjectPageProject GetProject(string userId, Guid projectId)
        {
            using (var context = new Quilt4DataContext())
            {
                var projectPageApplicaitons = context.ProjectPageApplications.Where(x => x.ProjectId == projectId);

                return context.ProjectPageProjects.SingleOrDefault(x => x.Id == projectId).ToProjectPageProject(projectPageApplicaitons);
            }
        }

        public IEnumerable<Entity.ProjectPageVersion> GetVersions(string userId, Guid projectId, Guid applicationId)
        {
            using (var context = new Quilt4DataContext())
            {
                return
                    context.ProjectPageVersions.Where(x => x.ProjectId == projectId && x.ApplicationId == applicationId)
                        .ToProjectPageVersions().ToArray();
            }
        }

        public Entity.VersionPageVersion GetVersion(string userId, Guid projectId, Guid applicationId, Guid versionId)
        {
            using (var context = new Quilt4DataContext())
            {
                var versionPageIssueTypes =
                    context.VersionPageIssueTypes.Where(
                        x => x.ProjectId == projectId && x.ApplicationId == applicationId && x.VersionId == versionId);

                return
                    context.VersionPageVersions.SingleOrDefault(
                        x => x.Id == versionId && x.ProjectId == projectId && x.ApplicaitonId == applicationId)
                        .ToVersionPageVersion(versionPageIssueTypes);
            }
        }

        public Entity.IssueTypePageIssueType GetIssueType(string userId, Guid projectId, Guid applicationId,
            Guid versionId, Guid issueTypeId)
        {
            using (var context = new Quilt4DataContext())
            {
                var issueTypePageIssues =
                    context.IssueTypePageIssues.Where(
                        x =>
                            x.ProjectId == projectId && x.ApplicationId == applicationId && x.VersionId == versionId &&
                            x.IssueTypeId == issueTypeId);

                return
                    context.IssueTypePageIssueTypes.SingleOrDefault(
                        x =>
                            x.Id == issueTypeId && x.ProjectId == projectId && x.ApplicationId == applicationId &&
                            x.VersionId == versionId).ToIssueTypePageIssueType(issueTypePageIssues);
            }
        }

        public IEnumerable<Entity.DashboardPageProject> GetDashboardProjects(string userId)
        {
            using (var context = new Quilt4DataContext())
            {
                return context.DashboardPageProjects.ToDashboardProjects().ToArray();
            }
        }
    }
}
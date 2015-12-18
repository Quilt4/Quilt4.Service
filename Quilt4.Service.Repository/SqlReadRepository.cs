using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Quilt4.Service.Interface.Repository;
using Quilt4.Service.SqlRepository.Extensions;
using static System.String;

namespace Quilt4.Service.SqlRepository
{
    public class SqlReadRepository : IReadRepository
    {

        public Entity.ProjectPageProject GetProject(string userName, Guid projectId)
        {
            using (var context = GetDataContext())
            {
                var user = context.Users.SingleOrDefault(x => x.UserName.ToLower() == userName.ToLower());

                var projectUser = context.ProjectUsers.SingleOrDefault(x => x.ProjectId == projectId && x.UserId == user.UserId);

                if (projectUser == null)
                    throw new InvalidOperationException("The user doesn't have access to the provided project.");

                var projectPageApplicaitons = context.ProjectPageApplications.Where(x => x.ProjectId == projectId);

                return context.ProjectPageProjects.SingleOrDefault(x => x.Id == projectId).ToProjectPageProject(projectPageApplicaitons);
            }
        }

        public Entity.ProjectPageProject GetProject(Guid projectId)
        {
            using (var context = GetDataContext())
            {
                var projectPageApplicaitons = context.ProjectPageApplications.Where(x => x.ProjectId == projectId);
                return context.ProjectPageProjects.SingleOrDefault(x => x.Id == projectId).ToProjectPageProject(projectPageApplicaitons);
            }
        }

        private Quilt4DataContext GetDataContext()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            return new Quilt4DataContext(connectionString);
        }

        public IEnumerable<Entity.ProjectPageVersion> GetVersions(string userId, Guid projectId, Guid applicationId)
        {
            using (var context = GetDataContext())
            {
                return context.ProjectPageVersions.Where(x => x.ProjectId == projectId && x.ApplicationId == applicationId).ToProjectPageVersions().ToArray();
            }
        }

        public Entity.VersionPageVersion GetVersion(string userName, Guid projectId, Guid applicationId, Guid versionId)
        {
            using (var context = GetDataContext())
            {
                var user = context.Users.SingleOrDefault(x => x.UserName.ToLower() == userName.ToLower());
                var projectUser = context.ProjectUsers.SingleOrDefault(x => x.ProjectId == projectId && x.UserId == user.UserId);

                if (projectUser == null)
                    throw new InvalidOperationException("The user doesn't have access to the provided project.");

                var versionPageIssueTypes = context.VersionPageIssueTypes.Where(x => x.ProjectId == projectId && x.ApplicationId == applicationId && x.VersionId == versionId);
                return context.VersionPageVersions.SingleOrDefault(x => x.Id == versionId && x.ProjectId == projectId && x.ApplicaitonId == applicationId).ToVersionPageVersion(versionPageIssueTypes);
            }
        }

        public Entity.IssueTypePageIssueType GetIssueType(string userName, Guid projectId, Guid applicationId,
            Guid versionId, Guid issueTypeId)
        {
            using (var context = GetDataContext())
            {
                var user = context.Users.SingleOrDefault(x => x.UserName.ToLower() == userName.ToLower());

                var projectUser = context.ProjectUsers.SingleOrDefault(x => x.ProjectId == projectId && x.UserId == user.UserId);

                if (projectUser == null)
                    throw new InvalidOperationException("The user doesn't have access to the provided project.");

                var issueTypePageIssues = context.IssueTypePageIssues.Where(x => x.ProjectId == projectId && x.ApplicationId == applicationId && x.VersionId == versionId && x.IssueTypeId == issueTypeId);
                return context.IssueTypePageIssueTypes.SingleOrDefault(x => x.Id == issueTypeId && x.ProjectId == projectId && x.ApplicationId == applicationId && x.VersionId == versionId).ToIssueTypePageIssueType(issueTypePageIssues);
            }
        }

        public IEnumerable<Entity.DashboardPageProject> GetDashboardProjects(string userName)
        {
            using (var context = GetDataContext())
            {
                var user = context.Users.SingleOrDefault(x => x.UserName.ToLower() == userName.ToLower());
                var projectsIds = context.ProjectUsers.Where(x => x.UserId == user.UserId).Select(x => x.ProjectId);
                return context.DashboardPageProjects.Where(x => projectsIds.Contains(x.Id)).ToDashboardProjects().ToArray();
            }
        }
    }
}
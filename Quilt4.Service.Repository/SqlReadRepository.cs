using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Quilt4.Service.Interface.Repository;
using Quilt4.Service.SqlRepository.Extensions;

namespace Quilt4.Service.SqlRepository
{
    public class SqlReadRepository : IReadRepository
    {

        public Entity.ProjectPageProject GetProject(string userName, Guid projectKey)
        {
            using (var context = GetDataContext())
            {
                var user = context.Users.SingleOrDefault(x => x.UserName.ToLower() == userName.ToLower());

                var projectUser = context.ProjectUsers.SingleOrDefault(x => x.Project.ProjectKey == projectKey && x.UserId == user.UserId);

                if (projectUser == null)
                    throw new InvalidOperationException("The user doesn't have access to the provided project.");

                var projectPageApplicaitons = context.ProjectPageApplications.Where(x => x.ProjectKey == projectKey);
                return context.ProjectPageProjects.SingleOrDefault(x => x.ProjectKey == projectKey).ToProjectPageProject(projectPageApplicaitons);

                var applications = context.Applications.Where(x => x.Project.User.UserName == userName);
                var project = context.Projects.SingleOrDefault(x => x.ProjectKey == projectKey);
                //var response = new ProjectPageProject
                //{
                //    DashboardColor = project.DashboardColor,
                //    Name = project.Name,
                //    ProjectKey = project.ProjectKey,
                //    ProjectApiKey = project.ProjectApiKey,
                //    ProjectPageProjectId = -1,
                //};

                //return response;
            }
        }

        public Entity.ProjectPageProject GetProject(Guid projectKey)
        {
            using (var context = GetDataContext())
            {
                var projectPageApplicaitons = context.ProjectPageApplications.Where(x => x.ProjectKey == projectKey);
                return context.ProjectPageProjects.SingleOrDefault(x => x.ProjectKey == projectKey).ToProjectPageProject(projectPageApplicaitons);
            }
        }

        private Quilt4DataContext GetDataContext()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            return new Quilt4DataContext(connectionString);
        }

        public IEnumerable<Entity.ProjectPageVersion> GetVersions(string userId, Guid projectKey, Guid applicationKey)
        {
            using (var context = GetDataContext())
            {
                return context.ProjectPageVersions.Where(x => x.ProjectKey == projectKey && x.ApplicationKey == applicationKey).ToProjectPageVersions().ToArray();
            }
        }

        public Entity.VersionPageVersion GetVersion(string userName, Guid projectKey, Guid applicationKey, Guid versionKey)
        {
            using (var context = GetDataContext())
            {
                var user = context.Users.SingleOrDefault(x => x.UserName.ToLower() == userName.ToLower());
                var projectUser = context.ProjectUsers.SingleOrDefault(x => x.Project.ProjectKey == projectKey && x.UserId == user.UserId);

                if (projectUser == null)
                    throw new InvalidOperationException("The user doesn't have access to the provided project.");

                var versionPageIssueTypes = context.VersionPageIssueTypes.Where(x => x.ProjectKey == projectKey && x.ApplicationKey == applicationKey && x.VersionKey == versionKey);
                return context.VersionPageVersions.SingleOrDefault(x => x.VersionKey == versionKey && x.ProjectKey == projectKey && x.ApplicationKey == applicationKey).ToVersionPageVersion(versionPageIssueTypes);
            }
        }

        public Entity.IssueTypePageIssueType GetIssueType(string userName, Guid projectKey, Guid applicationKey,
            Guid versionKey, Guid issueTypeKey)
        {
            using (var context = GetDataContext())
            {
                var user = context.Users.SingleOrDefault(x => x.UserName.ToLower() == userName.ToLower());

                var projectUser = context.ProjectUsers.SingleOrDefault(x => x.Project.ProjectKey == projectKey && x.UserId == user.UserId);

                if (projectUser == null)
                    throw new InvalidOperationException("The user doesn't have access to the provided project.");

                var issueTypePageIssues = context.IssueTypePageIssues.Where(x => x.ProjectKey == projectKey && x.ApplicationKey == applicationKey && x.VersionKey == versionKey && x.IssueTypeKey == issueTypeKey);
                return context.IssueTypePageIssueTypes.SingleOrDefault(x => x.IssueTypeKey == issueTypeKey && x.ProjectKey == projectKey && x.ApplicationKey == applicationKey && x.VersionKey == versionKey).ToIssueTypePageIssueType(issueTypePageIssues);
            }
        }

        public IEnumerable<Entity.DashboardPageProject> GetDashboardProjects(string userName)
        {
            using (var context = GetDataContext())
            {
                return context.Projects.Select(x => new Entity.DashboardPageProject
                {
                    ProjectKey = x.ProjectKey,
                    Sessions = context.Sessions.Count(y => y.Version.Application.Project.User.UserName == userName),
                    Issues = context.Issues.Count(y => y.IssueType.Version.Application.Project.User.UserName == userName),
                    DashboardColor = x.DashboardColor,
                    Name = x.Name,
                    IssueTypes = context.IssueTypes.Count(y => y.Version.Application.Project.User.UserName == userName),
                    Versions = context.Versions.Count(y => y.Application.Project.User.UserName == userName),
                });

                //NOTE: Old code that did not work because the tables where not updated
                //var user = context.Users.SingleOrDefault(x => x.UserName.ToLower() == userName.ToLower());
                //var projectKeys = context.ProjectUsers.Where(x => x.UserId == user.UserId).Select(x => x.Project.ProjectKey);
                //return context.DashboardPageProjects.Where(x => projectKeys.Contains(x.ProjectKey)).ToDashboardProjects().ToArray();
            }
        }
    }
}
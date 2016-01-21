using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Quilt4.Service.Entity;
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

                //var projectPageApplicaitons = context.ProjectPageApplications.Where(x => x.ProjectKey == projectKey);
                //return context.ProjectPageProjects.SingleOrDefault(x => x.ProjectKey == projectKey).ToProjectPageProject(projectPageApplicaitons);

                var applications = context.Applications.Where(x => x.Project.User.UserName == userName);
                var project = context.Projects.SingleOrDefault(x => x.ProjectKey == projectKey);
                var response = new Entity.ProjectPageProject
                {
                    DashboardColor = project.DashboardColor,
                    Name = project.Name,
                    ProjectKey = project.ProjectKey,
                    ProjectApiKey = project.ProjectApiKey,
                    Applications = project.Applications.Select(x => new ProjectPageApplication
                    {
                        Id = x.ApplicationKey,
                        Name = x.Name,
                        Versions = x.Versions.Count,
                    }).ToArray()
                };

                return response;
            }
        }

        public Entity.ProjectPageProject GetProject(Guid projectKey)
        {
            using (var context = GetDataContext())
            {
                var project = context.Projects.SingleOrDefault(x => x.ProjectKey == projectKey);
                return new Entity.ProjectPageProject
                {
                    ProjectKey = project.ProjectKey,
                    DashboardColor = project.DashboardColor,
                    Name = project.Name,
                    ProjectApiKey = project.ProjectApiKey,
                    Applications = context.Applications.Where(x => true).Select(x => new Entity.ProjectPageApplication
                    {
                        Name = x.Name,
                        Id = x.ApplicationKey,
                        Versions = context.Versions.Count(y => y.Application.ApplicationKey == x.ApplicationKey)
                    }).ToArray(),
                };

                //var projectPageApplicaitons = context.ProjectPageApplications.Where(x => x.ProjectKey == projectKey);
                //return context.ProjectPageProjects.SingleOrDefault(x => x.ProjectKey == projectKey).ToProjectPageProject(projectPageApplicaitons);
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
                return context.Versions.Select(x => new Entity.ProjectPageVersion
                {
                    Id = x.VersionKey,
                    Version = x.VersionNumber,
                    ApplicationId = x.Application.ApplicationKey,
                    Enviroments = x.Application.Versions.SelectMany(y => y.Sessions).Select(y => y.Enviroment).ToArray(),
                    Issues = x.Application.Versions.SelectMany(y => y.Sessions).SelectMany(y => y.Issues).Count(),
                    IssueTypes = -1, //x.Application.Versions.SelectMany(y => y.Sessions).SelectMany(y => y.Issues).SelectMany(y => y.IssueTypeId).Count(),
                    Sessions = x.Application.Versions.SelectMany(y => y.Sessions).Count(),
                    ProjectId = x.Application.Project.ProjectKey,
                    Last = null //NOTE: What is this?
                });
                //return context.ProjectPageVersions.Where(x => x.ProjectKey == projectKey && x.ApplicationKey == applicationKey).ToProjectPageVersions().ToArray();
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


                //TODO: Fix!
                var versionPageIssueTypes = context.Versions.Select(x => new Entity.VersionPageVersion()).FirstOrDefault();
                return versionPageIssueTypes;
                //var versionPageIssueTypes = context.VersionPageIssueTypes.Where(x => x.ProjectKey == projectKey && x.ApplicationKey == applicationKey && x.VersionKey == versionKey);
                //return context.VersionPageVersions.SingleOrDefault(x => x.VersionKey == versionKey && x.ProjectKey == projectKey && x.ApplicationKey == applicationKey).ToVersionPageVersion(versionPageIssueTypes);
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

                //TODO: Fix!
                return context.IssueTypes.Select(x => new IssueTypePageIssueType { }).FirstOrDefault();
                //var issueTypePageIssues = context.IssueTypePageIssues.Where(x => x.ProjectKey == projectKey && x.ApplicationKey == applicationKey && x.VersionKey == versionKey && x.IssueTypeKey == issueTypeKey);
                //return context.IssueTypePageIssueTypes.SingleOrDefault(x => x.IssueTypeKey == issueTypeKey && x.ProjectKey == projectKey && x.ApplicationKey == applicationKey && x.VersionKey == versionKey).ToIssueTypePageIssueType(issueTypePageIssues);
            }
        }

        public IEnumerable<Entity.DashboardPageProject> GetDashboardProjects(string userName)
        {
            using (var context = GetDataContext())
            {
                var response = context.Projects.Select(x => new Entity.DashboardPageProject
                {
                    ProjectKey = x.ProjectKey,
                    Sessions = context.Sessions.Count(y => y.Version.Application.Project.User.UserName == userName),
                    Issues = context.Issues.Count(y => y.IssueType.Version.Application.Project.User.UserName == userName),
                    DashboardColor = x.DashboardColor,
                    Name = x.Name,
                    IssueTypes = context.IssueTypes.Count(y => y.Version.Application.Project.User.UserName == userName),
                    Versions = context.Versions.Count(y => y.Application.Project.User.UserName == userName),
                }).ToArray();

                return response;

                //NOTE: Old code that did not work because the tables where not updated
                //var user = context.Users.SingleOrDefault(x => x.UserName.ToLower() == userName.ToLower());
                //var projectKeys = context.ProjectUsers.Where(x => x.UserId == user.UserId).Select(x => x.Project.ProjectKey);
                //return context.DashboardPageProjects.Where(x => projectKeys.Contains(x.ProjectKey)).ToDashboardProjects().ToArray();
            }
        }
    }
}
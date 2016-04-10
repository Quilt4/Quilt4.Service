using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Quilt4.Service.Entity;
using Quilt4.Service.Interface.Repository;
using Quilt4Net;
using Quilt4.Service.SqlRepository.Converters;

namespace Quilt4.Service.SqlRepository
{
    public class SqlReadRepository : IReadRepository
    {

        public ProjectPageProject GetProject(string userName, Guid projectKey)
        {
            using (var context = GetDataContext())
            {
                var user = context.Users.SingleOrDefault(x => x.UserName == userName);

                var projectUser = context.ProjectUsers.SingleOrDefault(x => x.Project.ProjectKey == projectKey && x.UserId == user.UserId);

                if (projectUser == null) throw new InvalidOperationException("The user doesn't have access to the provided project.").AddData("userName", userName).AddData("projectKey", projectKey);

                var response = context.Projects.SingleOrDefault(x => x.ProjectKey == projectKey).ToProjectPageProject();
                return response;
            }
        }

        public ProjectPageProject GetProject(Guid projectKey)
        {
            using (var context = GetDataContext())
            {
                var response = context.Projects.SingleOrDefault(x => x.ProjectKey == projectKey).ToProjectPageProject();
                return response;
            }
        }

        private Quilt4DataContext GetDataContext()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            return new Quilt4DataContext(connectionString);
        }

        public IEnumerable<ProjectPageVersion> GetVersions(string userName, Guid projectKey, Guid applicationKey)
        {
            using (var context = GetDataContext())
            {
                return context.Versions
                    .Where(x => x.Application.ApplicationKey == applicationKey && x.Application.Project.ProjectKey == projectKey)
                    .Select(x => new ProjectPageVersion
                    {
                        VersionKey = x.VersionKey,
                        Version = x.VersionNumber,
                        ApplicationKey = x.Application.ApplicationKey,
                        Enviroments = x.Application.Versions.SelectMany(y => y.Sessions.Where(z => z.Version.VersionKey == x.VersionKey)).Select(y => y.Enviroment).ToArray(),
                        IssueCount = x.Application.Versions.SelectMany(y => y.Sessions.Where(z => z.Version.VersionKey == x.VersionKey)).SelectMany(y => y.Issues).Count(),
                        IssueTypeCount = x.IssueTypes.Count,
                        SessionCount = x.Application.Versions.SelectMany(y => y.Sessions.Where(z => z.Version.VersionKey == x.VersionKey)).Count(),
                        ProjectKey = x.Application.Project.ProjectKey,
                        LastIssue = x.IssueTypes.Max(y => y.CreationServerTime),
                        LastSession = x.Sessions.Where(z => z.Version.VersionKey == x.VersionKey).Max(y => y.LastUsedServerTime),
                        FirstSession = x.Sessions.Where(z => z.Version.VersionKey == x.VersionKey).Min(y => y.LastUsedServerTime),
                    }).ToArray();
            }
        }

        public VersionPageVersion GetVersion(string userName, Guid projectKey, Guid applicationKey, Guid versionKey)
        {
            using (var context = GetDataContext())
            {
                var user = context.Users.SingleOrDefault(x => x.UserName.ToLower() == userName.ToLower());
                var projectUser = context.ProjectUsers.SingleOrDefault(x => x.Project.ProjectKey == projectKey && x.UserId == user.UserId);

                if (projectUser == null)
                    throw new InvalidOperationException("The user doesn't have access to the provided project.");

                //TODO: Not sure that this is correct. And it is very slow.
                var versionPageIssueTypes = context.Versions.Where(x => x.VersionKey == versionKey && x.Application.ApplicationKey == applicationKey && x.Application.Project.ProjectKey == projectKey)
                    .Select(x => new Entity.VersionPageVersion
                {
                    ApplicationId = x.Application.ApplicationKey,
                    Version = x.VersionNumber,
                    Id = x.VersionKey,
                    ProjectId = x.Application.Project.ProjectKey,
                    ApplicationName = x.Application.Name,
                    ProjectName = x.Application.Project.Name,
                    IssueTypes = x.IssueTypes.Select(y => new VersionPageIssueType
                    {
                        Id = y.IssueTypeKey,
                        Level = y.Level,
                        Message = y.Message,
                        Issues = y.Issues.Count,
                        Ticket = y.Ticket,
                        LastIssue = y.Issues.Max(z => x.CreationServerTime),
                        Type = y.Type,
                        Enviroments = context.Sessions.Where(z => z.Issues.Any(z1 => z1.IssueType.IssueTypeKey == y.IssueTypeKey)).Select(y2 => y2.Enviroment)
                    }).ToArray()
                }).FirstOrDefault();
                return versionPageIssueTypes;
                //var versionPageIssueTypes = context.VersionPageIssueTypes.Where(x => x.ProjectKey == projectKey && x.ApplicationKey == applicationKey && x.VersionKey == versionKey);
                //return context.VersionPageVersions.SingleOrDefault(x => x.VersionKey == versionKey && x.ProjectKey == projectKey && x.ApplicationKey == applicationKey).ToVersionPageVersion(versionPageIssueTypes);
            }
        }

        public IssueTypePageIssueType GetIssueType(string userName, Guid issueTypeKey)
        {
            using (var context = GetDataContext())
            {
                var user = context.Users.SingleOrDefault(x => x.UserName == userName);
                if (user == null) throw new InvalidOperationException("Cannot find specified user.").AddData("userName", userName);

                var issueType = context.IssueTypes.Single(x => x.IssueTypeKey == issueTypeKey);

                var projectUser = context.ProjectUsers.SingleOrDefault(x => x.Project.ProjectKey == issueType.Version.Application.Project.ProjectKey && x.UserId == user.UserId);
                if (projectUser == null) throw new InvalidOperationException("The user doesn't have access to the provided project.").AddData("userName", userName).AddData("issueTypeKey", issueTypeKey).AddData("projectKey", issueType.Version.Application.Project.ProjectKey);

                return issueType.ToIssueTypePageIssueType();
            }
        }

        public IEnumerable<Entity.DashboardPageProject> GetDashboardProjects(string userName)
        {
            using (var context = GetDataContext())
            {
                var response = context.Projects.Select(x => new Entity.DashboardPageProject
                {
                    ProjectKey = x.ProjectKey,
                    Name = x.Name,
                    DashboardColor = x.DashboardColor,
                    Applications = x.Applications.Count(),
                    Versions = x.Applications.SelectMany(y => y.Versions).Count(),
                    IssueTypes = x.Applications.SelectMany(y => y.Versions).SelectMany(y => y.IssueTypes).Count(),
                    Issues = x.Applications.SelectMany(y => y.Versions).SelectMany(y => y.IssueTypes).SelectMany(y => y.Issues).Count(),
                    Sessions = x.Applications.SelectMany(y => y.Versions).SelectMany(y => y.Sessions).Count(),
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
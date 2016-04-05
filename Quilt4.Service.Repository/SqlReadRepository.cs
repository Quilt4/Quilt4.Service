using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Quilt4.Service.Entity;
using Quilt4.Service.Interface.Repository;
using Quilt4.Service.SqlRepository.Extensions;
using Quilt4Net;

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
                return context.Versions.Where(x => x.Application.ApplicationKey == applicationKey && x.Application.Project.ProjectKey == projectKey)
                    .Select(x => new Entity.ProjectPageVersion
                {
                    Id = x.VersionKey,
                    Version = x.VersionNumber,
                    ApplicationId = x.Application.ApplicationKey,
                    Enviroments = x.Application.Versions.SelectMany(y => y.Sessions).Select(y => y.Enviroment).ToArray(),
                    Issues = x.Application.Versions.SelectMany(y => y.Sessions).SelectMany(y => y.Issues).Count(),
                    IssueTypes = x.IssueTypes.Count,
                    Sessions = x.Application.Versions.SelectMany(y => y.Sessions).Count(),
                    ProjectId = x.Application.Project.ProjectKey,
                    Last = x.IssueTypes.Max(y => y.CreationServerTime)
                }).ToArray();
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

        public IssueTypePageIssueType GetIssueType(string userName, Guid projectKey, Guid applicationKey, Guid versionKey, Guid issueTypeKey)
        {
            using (var context = GetDataContext())
            {
                var user = context.Users.SingleOrDefault(x => string.Equals(x.UserName, userName, StringComparison.CurrentCultureIgnoreCase));
                if (user == null) throw new InvalidOperationException("Cannot find specified user.").AddData("userName", userName);

                var projectUser = context.ProjectUsers.SingleOrDefault(x => x.Project.ProjectKey == projectKey && x.UserId == user.UserId);
                if (projectUser == null) throw new InvalidOperationException("The user doesn't have access to the provided project.").AddData("userName", userName).AddData("projectKey", projectKey);

                //TODO: Not sure that this is correct. And it is very slow.
                var resp = context.IssueTypes.Select(x => new IssueTypePageIssueType
                {
                    Id = x.IssueTypeKey,
                    Message = x.Message,
                    Ticket = x.Ticket,
                    Type = x.Type,
                    Version = x.Version.VersionNumber,
                    ApplicationName = x.Version.Application.Name,
                    Level = x.Level,
                    ProjectName = x.Version.Application.Project.Name,
                    StackTrace = x.StackTrace,
                    VersionId = x.Version.VersionKey,
                    ApplicationId = x.Version.Application.ApplicationKey,
                    ProjectId = x.Version.Application.Project.ProjectKey,
                    Issues = x.Issues.Select(y => ToIssueTypePageIssue(y)).ToArray()
                }).FirstOrDefault();

                return resp;
            }
        }

        private static IssueTypePageIssue ToIssueTypePageIssue(Issue y)
        {
            var response = new IssueTypePageIssue
            {
                Id = y.IssueKey,
                User = y.Session.ApplicationUser.UserName,
                Data = y.IssueDatas != null ? y.IssueDatas.ToDictionary(yy => yy.Name, yy => yy.Value) : new Dictionary<string, string>(),
                Enviroment = y.Session.Enviroment,
                Time = y.CreationServerTime,
            };
            return response;
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
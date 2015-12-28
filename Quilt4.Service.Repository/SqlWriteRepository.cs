using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Transactions;
using Newtonsoft.Json;
using Quilt4.Service.Interface.Repository;

namespace Quilt4.Service.SqlRepository
{
    public class SqlWriteRepository : IWriteRepository
    {
        public void UpdateDashboardPageProject(Guid projectKey)
        {
            using (var context = GetDataContext())
            {
                var project = context.Projects.Single(x => x.ProjectKey == projectKey);
                var versions = project.Applications.SelectMany(x => x.Versions).Count();
                var sessions = project.Applications.SelectMany(x => x.Versions).SelectMany(x => x.Sessions).Count();
                var issueTypes = project.Applications.SelectMany(x => x.Versions).SelectMany(y => y.IssueTypes).Count();
                var issues =
                    project.Applications.SelectMany(x => x.Versions)
                        .SelectMany(y => y.IssueTypes)
                        .SelectMany(z => z.Issues)
                        .Count();

                var dashboardProject = context.DashboardPageProjects.SingleOrDefault(x => x.ProjectKey == projectKey);

                if (dashboardProject != null)
                {
                    dashboardProject.Name = project.Name;
                    dashboardProject.VersionCount = versions;
                    dashboardProject.SessionCount = sessions;
                    dashboardProject.IssueTypeCount = issueTypes;
                    dashboardProject.IssueCount = issues;
                    dashboardProject.DashboardColor = project.DashboardColor;
                }
                else
                {
                    var newDashboardProject = new DashboardPageProject
                    {
                        ProjectKey = projectKey,
                        Name = project.Name,
                        VersionCount = versions,
                        SessionCount = sessions,
                        IssueTypeCount = issueTypes,
                        IssueCount = issues,
                        DashboardColor = project.DashboardColor
                    };

                    context.DashboardPageProjects.InsertOnSubmit(newDashboardProject);
                }

                context.SubmitChanges();
            }
        }

        public void UpdateProjectPageProject(Guid projectKey)
        {
            using (var context = GetDataContext())
            {
                var project = context.Projects.Single(x => x.ProjectKey == projectKey);

                var projectPageProject = context.ProjectPageProjects.SingleOrDefault(x => x.ProjectKey == projectKey);

                if (projectPageProject != null)
                {
                    projectPageProject.Name = project.Name;
                    projectPageProject.DashboardColor = project.DashboardColor;
                    projectPageProject.ProjectApiKey = project.ProjectApiKey;
                }
                else
                {
                    var newProjectPageProject = new ProjectPageProject
                    {
                        ProjectKey = projectKey,
                        Name = project.Name,
                        DashboardColor = project.DashboardColor,
                        ProjectApiKey = project.ProjectApiKey,
                    };

                    context.ProjectPageProjects.InsertOnSubmit(newProjectPageProject);
                }

                context.SubmitChanges();
            }
        }
        
        public void WriteToReadDb()
        {
            using (var scope = new TransactionScope())
            {
                using (var context = GetDataContext())
                {
                    var projects = context.Projects.ToArray();

                    var issueTypePageIssueIds = context.IssueTypePageIssues.Select(x => x.IssueTypeKey);
                    var issuesToUpdate = context.Issues.Where(x => !issueTypePageIssueIds.Contains(x.IssueKey)).ToArray();

                    foreach (var issue in issuesToUpdate)
                    {
                        var issueType = issue.IssueType;
                        var version = issue.IssueType.Version;
                        var application = issue.IssueType.Version.Application;
                        var project = issue.IssueType.Version.Application.Project;
                        var session = issue.Session;

                        AddIssueTypePageIssue(issue, issueType, version, application, project, session, context);

                        AddUpdateIssueTypePageIssueType(context, issueType, version, application, project);

                        AddUpdateVersionPageIssueType(context, issueType, version, application, project);

                        AddUpdateVersionPageVersion(context, version, application, project);

                        AddUpdateProjectPageVersion(context, version, application, project);

                        AddUpdateProjectPageApplication(context, application, project);

                        AddUpdateProjectPageProject(context, project);

                        AddUpdateDashboardPageProject(context, project);

                        context.SubmitChanges();
                    }

                    foreach (var project in projects)
                    {
                        var sessions = project.Applications.SelectMany(x => x.Versions).SelectMany(x => x.Sessions).Count();
                        var versions = project.Applications.SelectMany(x => x.Versions);

                        UpdateProjectPageVersionSessionCount(context, versions);

                        UpdateDashboardPageProjectSessionCount(context, project.ProjectKey, sessions);

                        context.SubmitChanges();
                    }
                }

                scope.Complete();
            }
        }

        private void UpdateDashboardPageProjectSessionCount(Quilt4DataContext context, Guid projectKey, int sessionCount)
        {
            var dashboardPageProject = context.DashboardPageProjects.SingleOrDefault(x => x.ProjectKey == projectKey);
            if(dashboardPageProject == null)
                return;
            dashboardPageProject.SessionCount = sessionCount;
        }

        private void UpdateProjectPageVersionSessionCount(Quilt4DataContext context, IEnumerable<Version> versions)
        {
            if(versions == null || !versions.Any())

            foreach (var version in versions)
            {
                var sessionCount = version.Sessions.Count;
                var dashboardProject = context.ProjectPageVersions.SingleOrDefault(x => x.VersionKey == version.VersionKey);
                
                if(dashboardProject == null)
                    continue;

                dashboardProject.SessionCount = sessionCount;
            }
        }

        private static void AddUpdateDashboardPageProject(Quilt4DataContext context, Project project)
        {
            var dashboardPageProject = context.DashboardPageProjects.SingleOrDefault(x => x.ProjectKey == project.ProjectKey);

            var versions = project.Applications.SelectMany(x => x.Versions).Count();
            var sessions = project.Applications.SelectMany(x => x.Versions).SelectMany(x => x.Sessions).Count();
            var issueTypes = project.Applications.SelectMany(x => x.Versions).SelectMany(y => y.IssueTypes).Count();
            var issues = project.Applications.SelectMany(x => x.Versions).SelectMany(y => y.IssueTypes).SelectMany(x => x.Issues).Count();

            if (dashboardPageProject != null)
            {
                dashboardPageProject.Name = project.Name;
                dashboardPageProject.DashboardColor = project.DashboardColor;
                dashboardPageProject.VersionCount = versions;
                dashboardPageProject.SessionCount = sessions;
                dashboardPageProject.IssueTypeCount = issueTypes;
                dashboardPageProject.IssueCount = issues;
            }
            else
            {
                var newDashboardPageProject = new DashboardPageProject
                {
                    ProjectKey = project.ProjectKey,
                    Name = project.Name,
                    DashboardColor = project.DashboardColor,
                    VersionCount = versions,
                    SessionCount = sessions,
                    IssueTypeCount = issueTypes,
                    IssueCount = issues,
                };

                context.DashboardPageProjects.InsertOnSubmit(newDashboardPageProject);
            }
        }

        private static void AddUpdateProjectPageProject(Quilt4DataContext context, Project project)
        {
            var projectPageProject = context.ProjectPageProjects.SingleOrDefault(x => x.ProjectKey == project.ProjectKey);

            if (projectPageProject == null)
            {
                var newProjectPageProject = new ProjectPageProject
                {
                    ProjectKey = project.ProjectKey,
                    ProjectApiKey = project.ProjectApiKey,
                    DashboardColor = project.DashboardColor,
                    Name = project.Name,
                };

                context.ProjectPageProjects.InsertOnSubmit(newProjectPageProject);
            }
            else
            {
                projectPageProject.ProjectApiKey = project.ProjectApiKey;
                projectPageProject.DashboardColor = project.DashboardColor;
                projectPageProject.Name = project.Name;
            }
        }

        private static void AddUpdateProjectPageApplication(Quilt4DataContext context, Application application, Project project)
        {
            var projectPageApplication = context.ProjectPageApplications.SingleOrDefault(x => x.ApplicationKey == application.ApplicationKey);

            var versions = application.Versions.Count;

            if (projectPageApplication != null)
            {
                projectPageApplication.VersionCount = versions;
            }
            else
            {
                var newProjectPageApplication = new ProjectPageApplication
                {
                    ApplicationKey = application.ApplicationKey,
                    Name = application.Name,
                    ProjectKey = project.ProjectKey,
                    VersionCount = versions
                };

                context.ProjectPageApplications.InsertOnSubmit(newProjectPageApplication);
            }
        }

        private static void AddUpdateProjectPageVersion(Quilt4DataContext context, Version version, Application application, Project project)
        {
            var projectPageVersion = context.ProjectPageVersions.SingleOrDefault(x => x.VersionKey == version.VersionKey);

            var sessions = version.Sessions.Count;
            var issueTypes = version.IssueTypes.Count;
            var issues = version.IssueTypes.SelectMany(x => x.Issues).Count();
            var lastUpdateServerTime = version.IssueTypes.SelectMany(x => x.Issues).Max(x => x.CreationServerTime);
            var enviroments = string.Join(";", version.Sessions.Select(x => x.Enviroment).Distinct());


            if (projectPageVersion != null)
            {
                projectPageVersion.SessionCount = sessions;
                projectPageVersion.IssueTypeCount = issueTypes;
                projectPageVersion.IssueCount = issues;
                projectPageVersion.LastUpdateServerTime = lastUpdateServerTime;
                projectPageVersion.Enviroments = enviroments;
            }
            else
            {
                var newProjectPageVersion = new ProjectPageVersion
                {
                    VersionKey = version.VersionKey,
                    ProjectKey = project.ProjectKey,
                    ApplicationKey = application.ApplicationKey,
                    VersionNumber = version.VersionNumber,
                    SessionCount = sessions,
                    IssueTypeCount = issueTypes,
                    IssueCount = issues,
                    LastUpdateServerTime = lastUpdateServerTime,
                    Enviroments = enviroments,
                };
                context.ProjectPageVersions.InsertOnSubmit(newProjectPageVersion);
            }
        }

        private static void AddUpdateVersionPageVersion(Quilt4DataContext context, Version version, Application application, Project project)
        {
            var versionPageVersion = context.VersionPageVersions.SingleOrDefault(x => x.VersionKey == version.VersionKey);

            if (versionPageVersion == null)
            {
                var newVersionPageVersion = new VersionPageVersion
                {
                    VersionKey = version.VersionKey,
                    ProjectKey = project.ProjectKey,
                    ApplicationKey = application.ApplicationKey,
                    ProjectName = project.Name,
                    ApplicationName = application.Name,
                    VersionNumber = version.VersionNumber,
                };

                context.VersionPageVersions.InsertOnSubmit(newVersionPageVersion);
            }
        }

        private static void AddUpdateVersionPageIssueType(Quilt4DataContext context, IssueType issueType, Version version, Application application, Project project)
        {
            var versionPageIssueType = context.VersionPageIssueTypes.SingleOrDefault(x => x.IssueTypeKey == issueType.IssueTypeKey);
            var issueCount = issueType.Issues.Count;
            var lastIssueServerTime = issueType.Issues.Max(x => x.CreationServerTime);
            var enviroments = string.Join(";", issueType.Issues.Select(x => x.Session).Select(y => y.Enviroment).Distinct());

            if (versionPageIssueType != null)
            {
                versionPageIssueType.IssueCount = issueCount;
                versionPageIssueType.LastIssueServerTime = lastIssueServerTime;
                versionPageIssueType.Enviroments = enviroments;
            }
            else
            {
                var newVersionPageIssueType = new VersionPageIssueType
                {
                    IssueTypeKey = issueType.IssueTypeKey,
                    ProjectKey = project.ProjectKey,
                    ApplicationKey = application.ApplicationKey,
                    VersionKey = version.VersionKey,
                    Enviroments = enviroments,
                    IssueCount = issueCount,
                    LastIssueServerTime = lastIssueServerTime,
                    Level = issueType.Level,
                    Message = issueType.Message,
                    Ticket = issueType.Ticket,
                    Type = issueType.Type
                };

                context.VersionPageIssueTypes.InsertOnSubmit(newVersionPageIssueType);
            }
        }

        private static void AddUpdateIssueTypePageIssueType(Quilt4DataContext context, IssueType issueType, Version version, Application application, Project project)
        {
            var issueTypePageIssueType = context.IssueTypePageIssueTypes.SingleOrDefault(x => x.IssueTypeKey == issueType.IssueTypeKey);

            if (issueTypePageIssueType == null)
            {
                var newIssueTypePageIssueType = new IssueTypePageIssueType
                {
                    IssueTypeKey = issueType.IssueTypeKey,
                    ProjectKey = project.ProjectKey,
                    ApplicationKey = application.ApplicationKey,
                    VersionKey = version.VersionKey,
                    ProjectName = project.Name,
                    ApplicationName = application.Name,
                    VersionNumber = version.VersionNumber,
                    Level = issueType.Level,
                    Message = issueType.Message,
                    StackTrace = issueType.StackTrace,
                    Ticket = issueType.Ticket,
                    Type = issueType.Type,
                };

                context.IssueTypePageIssueTypes.InsertOnSubmit(newIssueTypePageIssueType);
            }
        }

        private static void AddIssueTypePageIssue(Issue issue, IssueType issueType, Version version, Application application, Project project, Session session, Quilt4DataContext context)
        {
            var dataDictionary = JsonConvert.SerializeObject(issue.IssueDatas.ToDictionary(data => data.Name, data => data.Value));

            var issueTypePageIssue = new IssueTypePageIssue
            {
                ProjectKey = project.ProjectKey,
                ApplicationKey = application.ApplicationKey,
                VersionKey = version.VersionKey,
                IssueTypeKey = issueType.IssueTypeKey,
                Data = dataDictionary,
                Enviroment = session.Enviroment,
                ApplicationUserName = session.ApplicationUser.UserName,
                LastUpdateServerTime = issue.CreationServerTime,                
            };

            context.IssueTypePageIssues.InsertOnSubmit(issueTypePageIssue);
        }

        private Quilt4DataContext GetDataContext()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            return new Quilt4DataContext(connectionString);
        }
    }
}
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
        public void UpdateDashboardPageProject(Guid projectId)
        {
            using (var context = GetDataContext())
            {
                var project = context.Projects.Single(x => x.Id == projectId);
                var versions = project.Applications.SelectMany(x => x.Versions).Count();
                var sessions = project.Applications.SelectMany(x => x.Sessions).Count();
                var issueTypes = project.Applications.SelectMany(x => x.Versions).SelectMany(y => y.IssueTypes).Count();
                var issues =
                    project.Applications.SelectMany(x => x.Versions)
                        .SelectMany(y => y.IssueTypes)
                        .SelectMany(z => z.Issues)
                        .Count();

                var dashboardProject = context.DashboardPageProjects.SingleOrDefault(x => x.Id == projectId);

                if (dashboardProject != null)
                {
                    dashboardProject.Name = project.Name;
                    dashboardProject.Versions = versions;
                    dashboardProject.Sessions = sessions;
                    dashboardProject.IssueTypes = issueTypes;
                    dashboardProject.Issues = issues;
                    dashboardProject.DashboardColor = project.DashboardColor;
                }
                else
                {
                    var newDashboardProject = new DashboardPageProject
                    {
                        Id = projectId,
                        Name = project.Name,
                        Versions = versions,
                        Sessions = sessions,
                        IssueTypes = issueTypes,
                        Issues = issues,
                        DashboardColor = project.DashboardColor
                    };

                    context.DashboardPageProjects.InsertOnSubmit(newDashboardProject);
                }

                context.SubmitChanges();
            }
        }

        public void UpdateProjectPageProject(Guid projectId)
        {
            using (var context = GetDataContext())
            {
                var project = context.Projects.Single(x => x.Id == projectId);

                var projectPageProject = context.ProjectPageProjects.SingleOrDefault(x => x.Id == projectId);

                if (projectPageProject != null)
                {
                    projectPageProject.Name = project.Name;
                    projectPageProject.DashboardColor = project.DashboardColor;
                    projectPageProject.ClientToken = project.ClientToken;
                }
                else
                {
                    var newProjectPageProject = new ProjectPageProject
                    {
                        Id = projectId,
                        Name = project.Name,
                        DashboardColor = project.DashboardColor,
                        ClientToken = project.ClientToken,
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

                    var issueTypePageIssueIds = context.IssueTypePageIssues.Select(x => x.Id);
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
                        var sessions = project.Applications.SelectMany(x => x.Sessions).Count();
                        var versions = project.Applications.SelectMany(x => x.Versions);

                        UpdateProjectPageVersionSessionCount(context, versions);

                        UpdateDashboardPageProjectSessionCount(context, project.Id, sessions);

                        context.SubmitChanges();
                    }
                }

                scope.Complete();
            }
        }

        private void UpdateDashboardPageProjectSessionCount(Quilt4DataContext context, Guid projectId, int sessions)
        {
            var dashboardPageProject = context.DashboardPageProjects.SingleOrDefault(x => x.Id == projectId);
            if(dashboardPageProject == null)
                return;
            dashboardPageProject.Sessions = sessions;
        }

        private void UpdateProjectPageVersionSessionCount(Quilt4DataContext context, IEnumerable<Version> versions)
        {
            if(versions == null || !versions.Any())

            foreach (var version in versions)
            {
                var sessions = version.Sessions.Count;
                var dashboardProject = context.ProjectPageVersions.SingleOrDefault(x => x.Id == version.Id);
                
                if(dashboardProject == null)
                    continue;

                dashboardProject.Sessions = sessions;
            }
        }

        private static void AddUpdateDashboardPageProject(Quilt4DataContext context, Project project)
        {
            var dashboardPageProject = context.DashboardPageProjects.SingleOrDefault(x => x.Id == project.Id);

            var versions = project.Applications.SelectMany(x => x.Versions).Count();
            var sessions = project.Applications.SelectMany(x => x.Sessions).Count();
            var issueTypes = project.Applications.SelectMany(x => x.Versions).SelectMany(y => y.IssueTypes).Count();
            var issues = project.Applications.SelectMany(x => x.Versions).SelectMany(y => y.IssueTypes).SelectMany(x => x.Issues).Count();

            if (dashboardPageProject != null)
            {
                dashboardPageProject.Name = project.Name;
                dashboardPageProject.DashboardColor = project.DashboardColor;
                dashboardPageProject.Versions = versions;
                dashboardPageProject.Sessions = sessions;
                dashboardPageProject.IssueTypes = issueTypes;
                dashboardPageProject.Issues = issues;
            }
            else
            {
                var newDashboardPageProject = new DashboardPageProject
                {
                    Id = project.Id,
                    Name = project.Name,
                    DashboardColor = project.DashboardColor,
                    Versions = versions,
                    Sessions = sessions,
                    IssueTypes = issueTypes,
                    Issues = issues,
                };

                context.DashboardPageProjects.InsertOnSubmit(newDashboardPageProject);
            }
        }

        private static void AddUpdateProjectPageProject(Quilt4DataContext context, Project project)
        {
            var projectPageProject = context.ProjectPageProjects.SingleOrDefault(x => x.Id == project.Id);

            if (projectPageProject == null)
            {
                var newProjectPageProject = new ProjectPageProject
                {
                    Id = project.Id,
                    ClientToken = project.ClientToken,
                    DashboardColor = project.DashboardColor,
                    Name = project.Name,
                };

                context.ProjectPageProjects.InsertOnSubmit(newProjectPageProject);
            }
            else
            {
                projectPageProject.ClientToken = project.ClientToken;
                projectPageProject.DashboardColor = project.DashboardColor;
                projectPageProject.Name = project.Name;
            }
        }

        private static void AddUpdateProjectPageApplication(Quilt4DataContext context, Application application, Project project)
        {
            var projectPageApplication = context.ProjectPageApplications.SingleOrDefault(x => x.Id == application.Id);

            var versions = application.Versions.Count;

            if (projectPageApplication != null)
            {
                projectPageApplication.Versions = versions;
            }
            else
            {
                var newProjectPageApplication = new ProjectPageApplication
                {
                    Id = application.Id,
                    Name = application.Name,
                    ProjectId = project.Id,
                    Versions = versions
                };

                context.ProjectPageApplications.InsertOnSubmit(newProjectPageApplication);
            }
        }

        private static void AddUpdateProjectPageVersion(Quilt4DataContext context, Version version, Application application, Project project)
        {
            var projectPageVersion = context.ProjectPageVersions.SingleOrDefault(x => x.Id == version.Id);

            var sessions = version.Sessions.Count;
            var issueTypes = version.IssueTypes.Count;
            var issues = version.IssueTypes.SelectMany(x => x.Issues).Count();
            var last = version.IssueTypes.SelectMany(x => x.Issues).Max(x => x.ClientTime);
            var enviroments = string.Join(";", version.Sessions.Select(x => x.Enviroment).Distinct());


            if (projectPageVersion != null)
            {
                projectPageVersion.Sessions = sessions;
                projectPageVersion.IssueTypes = issueTypes;
                projectPageVersion.Issues = issues;
                projectPageVersion.Last = last;
                projectPageVersion.Enviroments = enviroments;
            }
            else
            {
                var newProjectPageVersion = new ProjectPageVersion
                {
                    Id = version.Id,
                    ProjectId = project.Id,
                    ApplicationId = application.Id,
                    Version = version.Version1,
                    Sessions = sessions,
                    IssueTypes = issueTypes,
                    Issues = issues,
                    Last = last,
                    Enviroments = enviroments,
                };
                context.ProjectPageVersions.InsertOnSubmit(newProjectPageVersion);
            }
        }

        private static void AddUpdateVersionPageVersion(Quilt4DataContext context, Version version, Application application, Project project)
        {
            var versionPageVersion = context.VersionPageVersions.SingleOrDefault(x => x.Id == version.Id);

            if (versionPageVersion == null)
            {
                var newVersionPageVersion = new VersionPageVersion
                {
                    Id = version.Id,
                    ProjectId = project.Id,
                    ApplicaitonId = application.Id,
                    ProjectName = project.Name,
                    ApplicationName = application.Name,
                    Version = version.Version1,
                };

                context.VersionPageVersions.InsertOnSubmit(newVersionPageVersion);
            }
        }

        private static void AddUpdateVersionPageIssueType(Quilt4DataContext context, IssueType issueType, Version version, Application application, Project project)
        {
            var versionPageIssueType = context.VersionPageIssueTypes.SingleOrDefault(x => x.Id == issueType.Id);
            var issueCount = issueType.Issues.Count;
            var lastIssue = issueType.Issues.Max(x => x.ClientTime);
            var enviroments = string.Join(";", issueType.Issues.Select(x => x.Session).Select(y => y.Enviroment).Distinct());

            if (versionPageIssueType != null)
            {
                versionPageIssueType.Issues = issueCount;
                versionPageIssueType.LastIssue = lastIssue;
                versionPageIssueType.Enviroments = enviroments;
            }
            else
            {
                var newVersionPageIssueType = new VersionPageIssueType
                {
                    Id = issueType.Id,
                    ProjectId = project.Id,
                    ApplicationId = application.Id,
                    VersionId = version.Id,
                    Enviroments = enviroments,
                    Issues = issueCount,
                    LastIssue = lastIssue,
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
            var issueTypePageIssueType = context.IssueTypePageIssueTypes.SingleOrDefault(x => x.Id == issueType.Id);

            if (issueTypePageIssueType == null)
            {
                var newIssueTypePageIssueType = new IssueTypePageIssueType
                {
                    Id = issueType.Id,
                    ProjectId = project.Id,
                    ApplicationId = application.Id,
                    VersionId = version.Id,
                    ProjectName = project.Name,
                    ApplicationName = application.Name,
                    Version = version.Version1,
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
                Id = issue.IssueKey,
                ProjectId = project.Id, 
                ApplicationId = application.Id,
                VersionId = version.Id,
                IssueTypeId = issueType.Id,
                Data = dataDictionary,
                Enviroment = session.Enviroment,
                IssueUser = session.UserData.UserName,
                Time = issue.ClientTime
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
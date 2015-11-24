using System;
using System.Configuration;
using System.Linq;
using Newtonsoft.Json;
using Quilt4.Service.Interface.Repository;

namespace Quilt4.Service.Repository.SqlRepository
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

        public void UpdateProjectPageApplication(Guid projectId, Guid applicaitonId)
        {
            using (var context = GetDataContext())
            {
                var application = context.Applications.Single(x => x.Id == applicaitonId && x.ProjectId == projectId);

                var versions = application.Versions.Count;

                var projectPageApplication =
                    context.ProjectPageApplications.SingleOrDefault(
                        x => x.ProjectId == projectId && x.Id == application.Id);

                if (projectPageApplication != null)
                {
                    projectPageApplication.Name = application.Name;
                    projectPageApplication.Versions = versions;
                }
                else
                {
                    var newProjectPageApplication = new ProjectPageApplication
                    {
                        Id = application.Id,
                        ProjectId = application.ProjectId,
                        Name = application.Name,
                        Versions = versions
                    };

                    context.ProjectPageApplications.InsertOnSubmit(newProjectPageApplication);
                }

                context.SubmitChanges();
            }
        }

        public void UpdateProjectPageVersion(Guid projectId, Guid applicaitonId, Guid versionId)
        {
            using (var context = GetDataContext())
            {
                var version =
                    context.Versions.Single(x => x.Id == versionId);

                var sessions = version.Sessions.Count;
                var issueTypes = version.IssueTypes.Count;
                var issues = version.IssueTypes.SelectMany(x => x.Issues).Count();
                var enviroment = version.Sessions.Select(x => x.Enviroment).Distinct();
                var allIssues = version.IssueTypes.SelectMany(x => x.Issues);

                DateTime? lastIssue;
                if (allIssues.Any())
                    lastIssue = allIssues.Max(y => y.CreationDate);
                else
                {
                    lastIssue = null;
                }


                var projectPageVersion =
                    context.ProjectPageVersions.SingleOrDefault(
                        x =>
                            x.ProjectId == projectId && x.ApplicationId == version.ApplicationId &&
                            x.Id == version.Id);

                if (projectPageVersion != null)
                {
                    projectPageVersion.Version = version.Version1;
                    projectPageVersion.Sessions = sessions;
                    projectPageVersion.IssueTypes = issueTypes;
                    projectPageVersion.Issues = issues;
                    projectPageVersion.Last = lastIssue;
                    projectPageVersion.Enviroments = string.Join(";", enviroment);
                }
                else
                {
                    var newProjectPageVersion = new ProjectPageVersion
                    {
                        ProjectId = projectId,
                        ApplicationId = version.ApplicationId,
                        Id = version.Id,
                        Version = version.Version1,
                        Sessions = sessions,
                        IssueTypes = issueTypes,
                        Issues = issues,
                        Last = lastIssue,
                        Enviroments = string.Join(";", enviroment)
                    };

                    context.ProjectPageVersions.InsertOnSubmit(newProjectPageVersion);
                }

                context.SubmitChanges();
            }
        }

        public void UpdateVersionPageVersion(Guid projectId, Guid applicaitonId, Guid versionId)
        {
            using (var context = GetDataContext())
            {
                var project = context.Projects.Single(x => x.Id == projectId);

                var version =
                    context.Versions.Single(x => x.Id == versionId);

                var versionPageVersion =
                    context.VersionPageVersions.SingleOrDefault(
                        x => x.Id == versionId && x.ProjectId == projectId && x.ApplicaitonId == applicaitonId);

                if (versionPageVersion != null)
                {
                    versionPageVersion.ProjectName = project.Name;
                    versionPageVersion.ApplicationName = version.Application.Name;
                    versionPageVersion.Version = version.Version1;
                }
                else
                {
                    var newVersionPageVersion = new VersionPageVersion
                    {
                        Id = versionId,
                        ProjectId = projectId,
                        ApplicaitonId = applicaitonId,
                        ProjectName = project.Name,
                        ApplicationName = version.Application.Name,
                        Version = version.Version1
                    };

                    context.VersionPageVersions.InsertOnSubmit(newVersionPageVersion);
                }

                context.SubmitChanges();
            }
        }

        public void UpdateVersionPageIssueType(Guid projectId, Guid applicationId, Guid versionId, Guid issueTypeId)
        {
            using (var context = GetDataContext())
            {
                var issueType = context.IssueTypes.Single(x => x.Id == issueTypeId);
                var issues = issueType.Issues.Count;
                var lastIssue = issueType.Issues.Max(x => x.CreationDate);
                var allEnviroments = issueType.Issues.Select(x => x.Session).Select(y => y.Enviroment).Distinct();
                var enviroments = string.Join(";", allEnviroments);

                var versionPageIssueType =
                    context.VersionPageIssueTypes.SingleOrDefault(
                        x =>
                            x.ProjectId == projectId && x.ApplicationId == applicationId && x.VersionId == versionId &&
                            x.Id == issueTypeId);

                if (versionPageIssueType != null)
                {
                    versionPageIssueType.Ticket = issueType.Ticket;
                    versionPageIssueType.Type = issueType.Type;
                    versionPageIssueType.Level = issueType.Level;
                    versionPageIssueType.Issues = issues;
                    versionPageIssueType.LastIssue = lastIssue;
                    versionPageIssueType.Enviroments = enviroments;
                    versionPageIssueType.Message = issueType.Message;
                }
                else
                {
                    var newVersionPageIssueType = new VersionPageIssueType
                    {
                        Id = issueTypeId,
                        ProjectId = projectId,
                        ApplicationId = applicationId,
                        VersionId = versionId,
                        Ticket = issueType.Ticket,
                        Type = issueType.Type,
                        Level = issueType.Level,
                        Issues = issues,
                        LastIssue = lastIssue,
                        Enviroments = enviroments,
                        Message = issueType.Message
                    };

                    context.VersionPageIssueTypes.InsertOnSubmit(newVersionPageIssueType);
                }

                context.SubmitChanges();
            }
        }

        public void UpdateIssueTypePageIssueType(Guid projectId, Guid applicationId, Guid versionId, Guid issueTypeId)
        {
            using (var context = GetDataContext())
            {
                var project = context.Projects.Single(x => x.Id == projectId);
                var application = context.Applications.Single(x => x.Id == applicationId);
                var version = context.Versions.Single(x => x.Id == versionId);
                var issueType = context.IssueTypes.Single(x => x.Id == issueTypeId);

                var issueTypePageIssueType =
                    context.IssueTypePageIssueTypes.SingleOrDefault(
                        x =>
                            x.Id == issueTypeId && x.ProjectId == projectId && x.ApplicationId == applicationId &&
                            x.VersionId == versionId);

                if (issueTypePageIssueType != null)
                {
                    issueTypePageIssueType.ProjectName = project.Name;
                    issueTypePageIssueType.ApplicationName = application.Name;
                    issueTypePageIssueType.Version = version.Version1;
                    issueTypePageIssueType.Ticket = issueType.Ticket;
                    issueTypePageIssueType.Type = issueType.Type;
                    issueTypePageIssueType.Message = issueType.Message;
                    issueTypePageIssueType.Level = issueType.Level;
                    issueTypePageIssueType.StackTrace = issueType.StackTrace;
                }
                else
                {
                    var newIssueTypePageIssueType = new IssueTypePageIssueType
                    {
                        Id = issueTypeId,
                        ProjectId = projectId,
                        ApplicationId = applicationId,
                        VersionId = versionId,
                        ProjectName = project.Name,
                        ApplicationName = application.Name,
                        Version = version.Version1,
                        Ticket = issueType.Ticket,
                        Type = issueType.Type,
                        Message = issueType.Message,
                        Level = issueType.Level,
                        StackTrace = issueType.StackTrace,
                    };

                    context.IssueTypePageIssueTypes.InsertOnSubmit(newIssueTypePageIssueType);
                }

                context.SubmitChanges();
            }
        }

        public void UpdateIssueTypePageIssue(Guid projectId, Guid applicationId, Guid versionId, Guid issueTypeId, Guid issueId)
        {
            using (var context = GetDataContext())
            {
                var issue = context.Issues.Single(x => x.Id == issueId);

                var issueTypePageIssue =
                    context.IssueTypePageIssues.SingleOrDefault(
                        x =>
                            x.Id == issueId && x.ProjectId == projectId && x.ApplicationId == applicationId &&
                            x.VersionId == versionId && x.IssueTypeId == issueTypeId);

               
                var dataDictionary = JsonConvert.SerializeObject(issue.IssueDatas.ToDictionary(data => data.Name, data => data.Value));

                if (issueTypePageIssue != null)
                {
                    issueTypePageIssue.Time = issue.CreationDate;
                    issueTypePageIssue.IssueUser = issue.Session.UserData.UserName;
                    issueTypePageIssue.Enviroment = issue.Session.Enviroment;
                    issueTypePageIssue.Data = dataDictionary;

                }
                else
                {
                    var newIssueTypePageIssue = new IssueTypePageIssue
                    {
                        Id = issueId,
                        ProjectId = projectId,
                        ApplicationId = applicationId,
                        VersionId = versionId,
                        IssueTypeId = issueTypeId,
                        Time = issue.CreationDate,
                        IssueUser = issue.Session.UserData.UserName,
                        Enviroment = issue.Session.Enviroment,
                        Data = dataDictionary,
                    };
                    context.IssueTypePageIssues.InsertOnSubmit(newIssueTypePageIssue);
                }

                context.SubmitChanges();
            }
        }

        private Quilt4DataContext GetDataContext()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            return new Quilt4DataContext(connectionString);
        }
    }
}
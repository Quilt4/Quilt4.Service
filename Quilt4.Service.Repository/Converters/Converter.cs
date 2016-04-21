using System;
using System.Collections.Generic;
using System.Linq;
using Quilt4.Service.Entity;

namespace Quilt4.Service.SqlRepository.Converters
{
    internal static class Converter
    {
        public static IssueTypePageIssueType ToIssueTypePageIssueType(this IssueType x, IssueTypeDetail d)
        {
            return new IssueTypePageIssueType
            {
                IssueTypeKey = x.IssueTypeKey,
                Message = d.Message,
                Ticket = x.Ticket,
                Type = d.Type,
                Version = x.Version.VersionNumber,
                ApplicationName = x.Version.Application.Name,
                Level = x.Level,
                ProjectName = x.Version.Application.Project.Name,
                StackTrace = d.StackTrace,
                VersionId = x.Version.VersionKey,
                ApplicationId = x.Version.Application.ApplicationKey,
                ProjectId = x.Version.Application.Project.ProjectKey,
                Issues = x.Issues.Count() != 0 ? x.Issues.Select(y => y.ToIssueTypePageIssue()).ToArray() : null,                
            };
        }

        public static IssueTypePageIssue ToIssueTypePageIssue(this Issue y)
        {
            var response = new IssueTypePageIssue
            {
                IssueKey = y.IssueKey,
                UserName = y.Session.ApplicationUser != null ? y.Session.ApplicationUser.UserName : "",
                Data = y.IssueDatas != null ? y.IssueDatas.ToDictionary(yy => yy.Name, yy => yy.Value) : new Dictionary<string, string>(),
                Enviroment = y.Session.Enviroment,
                CreationServerTime = y.CreationServerTime,
                IssueThreadKeys = y.IssueThreadKey != null ? new Dictionary<string, string> { { "Other IssueKey", "Other issue description" } } : new Dictionary<string, string>()
            };
            return response;
        }

        public static ProjectPageProject ToProjectPageProject(this Project x)
        {
            return new ProjectPageProject(x.ProjectKey, x.Name, x.DashboardColor, x.ProjectApiKey, x.Applications.Select(y => y.ToProjectPageApplication()).ToArray());
        }

        public static ProjectPageApplication ToProjectPageApplication(this Application x)
        {
            return new ProjectPageApplication
            {
                ApplicationKey = x.ApplicationKey,
                Name = x.Name,
                VersionCount = x.Versions.Count,
            };
        }

        public static Entity.Application ToApplication(this Application x)
        {
            return new Entity.Application(x.ApplicationKey, x.Name);
        }

        public static Entity.Version ToVersion(this Version x)
        {
            return new Entity.Version(x.VersionKey, x.Application.Project.ProjectKey, x.VersionNumber);
        }
    }
}
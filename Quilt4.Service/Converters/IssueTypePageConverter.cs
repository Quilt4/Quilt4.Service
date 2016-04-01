using System.Collections.Generic;
using System.Linq;
using Quilt4.Service.Controllers.WebAPI.Web.DataTransfer;
using Quilt4.Service.Entity;

namespace Quilt4.Service.Converters
{
    public static class IssueTypePageConverter
    {
        public static IssueTypePageIssueTypeResponse ToIssueTypePageIssueTypeResponse(this IssueTypePageIssueType item)
        {
            if (item == null)
                return null;

            return new IssueTypePageIssueTypeResponse
            {
                Id = item.Id.ToString(),
                ProjectId = item.ProjectId.ToString(),
                ApplicationId = item.ApplicationId.ToString(),
                VersionId = item.VersionId.ToString(),
                ProjectName = item.ProjectName,
                ApplicationName = item.ApplicationName,
                Version = item.Version,
                Ticket = item.Ticket,
                Type = item.Type,
                Level = item.Level,
                Message = item.Message,
                StackTrace = item.StackTrace,
                Issues = item.Issues.ToIssueTypePageIssueResponses().ToArray()
            };
        }

        public static IEnumerable<IssueTypePageIssueResponse> ToIssueTypePageIssueResponses(
            this IEnumerable<IssueTypePageIssue> items)
        {
            return items?.Select(x => x.ToIssueTypePageIssueResponse());
        }

        public static IssueTypePageIssueResponse ToIssueTypePageIssueResponse(this IssueTypePageIssue item)
        {
            if (item == null)
                return null;

            return new IssueTypePageIssueResponse
            {
                Id = item.Id.ToString(),
                Time = item.Time,
                User = item.User,
                Enviroment = item.Enviroment,
                Data = item.Data
            };
        }
    }
}
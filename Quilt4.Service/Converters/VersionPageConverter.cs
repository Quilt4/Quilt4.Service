using System.Collections.Generic;
using System.Linq;
using Quilt4.Service.Controllers.WebAPI.Web.DataTransfer;
using Quilt4.Service.Entity;

namespace Quilt4.Service.Converters
{
    public static class VersionPageConverter
    {
        public static VersionPageVersionResponse ToVersionPageVersionResponse(this VersionPageVersion item)
        {
            if (item == null)
                return null;

            return new VersionPageVersionResponse
            {
                Id = item.Id.ToString(),
                ProjectId = item.ProjectId.ToString(),
                ApplicationId = item.ApplicationId.ToString(),
                Version = item.Version,
                ProjectName = item.ProjectName,
                ApplicationName = item.ApplicationName,
                IssueTypes = item.IssueTypes.ToVersionPageIssueTypeResponses()
            };
        }

        public static IEnumerable<VersionPageIssueTypeResponse> ToVersionPageIssueTypeResponses(
            this IEnumerable<VersionPageIssueType> items)
        {
            return items?.Select(x => x.ToVersionPageIssueTypeResponse());
        }

        public static VersionPageIssueTypeResponse ToVersionPageIssueTypeResponse(this VersionPageIssueType item)
        {
            if (item == null)
                return null;

            return new VersionPageIssueTypeResponse
            {
                Id = item.Id.ToString(),
                Ticket = item.Ticket,
                Type = item.Type,
                Issues = item.Issues,
                Level = item.Level,
                LastIssue = item.LastIssue,
                Enviroments = item.Enviroments,
                Message = item.Message
            };
        }
    }
}
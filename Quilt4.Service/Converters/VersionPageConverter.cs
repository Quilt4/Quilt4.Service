using System.Collections.Generic;
using System.Linq;
using Quilt4.Service.Controllers.WebAPI.Web.DataTransfer;
using Quilt4.Service.Entity;

namespace Quilt4.Service.Converters
{
    public static class VersionPageConverter
    {
        public static VersionPageVersionResponse ToVersionPageVersionResponse(this VersionDetail item)
        {
            if (item == null)
                return null;

            return new VersionPageVersionResponse
            {
                Id = item.VersionKey.ToString(),
                ProjectId = item.ProjectKey.ToString(),
                ApplicationId = item.ApplicationKey.ToString(),
                Version = item.VersionNumber,
                ProjectName = item.ProjectName,
                ApplicationName = item.ApplicationName,
                IssueTypes = item.IssueTypes.ToVersionPageIssueTypeResponses()
            };
        }

        public static IEnumerable<VersionPageIssueTypeResponse> ToVersionPageIssueTypeResponses(
            this IEnumerable<IssueTypeDetail> items)
        {
            return items?.Select(x => x.ToVersionPageIssueTypeResponse());
        }

        public static VersionPageIssueTypeResponse ToVersionPageIssueTypeResponse(this IssueTypeDetail item)
        {
            if (item == null)
                return null;

            return new VersionPageIssueTypeResponse
            {
                Id = item.IssueTypeKey.ToString(),
                Ticket = item.Ticket,
                Type = item.Type,
                Issues = item.IssueCount,
                Level = item.Level,
                LastIssue = item.LastIssue,
                Enviroments = item.Enviroments,
                Message = item.Message
            };
        }
    }
}
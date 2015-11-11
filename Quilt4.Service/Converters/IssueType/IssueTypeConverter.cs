using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quilt4.Service.DataTransfer;

namespace Quilt4.Service.Converters.IssueType
{
    public static class IssueTypeConverter
    {
        public static IEnumerable<IssueTypeResponse> ToIssueTypeResponses(this IEnumerable<Entity.IssueType> items)
        {
            return items?.Select(x => x.ToIssueTypeResponse());
        }

        public static IssueTypeResponse ToIssueTypeResponse(this Entity.IssueType item)
        {
            if (item == null)
                return null;

            return new IssueTypeResponse
            {
                Id = item.Id.ToString(),
                ProjectId = item.ProjectId.ToString(),
                ApplicationId = item.ApplicationId.ToString(),
                VersionId = item.VersionId.ToString(),
                Type = item.Type,
                Level = item.Level,
                Issues = item.Issues,
                Enviroments = item.Enviroments,
                Last = item.Last,
                Message = item.Message,
                StackTrace = item.StackTrace
            };
        }
    }
}
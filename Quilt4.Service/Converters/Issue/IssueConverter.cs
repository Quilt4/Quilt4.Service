using System.Collections.Generic;
using System.Linq;
using Quilt4.Service.DataTransfer;

namespace Quilt4.Service.Converters.Issue
{
    public static class IssueConverter
    {
        public static IEnumerable<IssueResponse> ToIssueResponses(this IEnumerable<Entity.Issue> items)
        {
            return items?.Select(x => x.ToIssueResponse());
        }

        public static IssueResponse ToIssueResponse(this Entity.Issue item)
        {
            if (item == null)
                return null;

            return new IssueResponse
            {
                Id = item.Id.ToString(),
                ProjectId = item.ProjectId.ToString(),
                ApplicationId = item.ApplicationId.ToString(),
                VersionId = item.VersionId.ToString(),
                IssueTypeId = item.IssueTypeId.ToString(),
                Enviroment = item.Enviroment,
                User = item.User,
                Time = item.Time,
                Data = item.Data
            };
        }
    }
}
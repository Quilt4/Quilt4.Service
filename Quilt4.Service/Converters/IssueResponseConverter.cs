using Quilt4.Service.Entity;
using Quilt4Net.Core.DataTransfer;

namespace Quilt4.Service.Converters
{
    internal static class IssueResponseConverter
    {
        public static IssueResponse ToIssueResponse(this RegisterIssueResponseEntity response)
        {
            return new IssueResponse
            {
                Ticket = response.Ticket.ToString(),
                IssueKey = response.IssueKey,
                ServerTime = response.ServerTime,
                //TODO: Append correct paths here
                IssueTypeUrl = "p1/SomePathToIssueType",
                IssueUrl = "p2/SomePathToIssue",
            };
        }
    }
}
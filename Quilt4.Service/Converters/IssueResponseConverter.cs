using Quilt4.Service.Entity;
using Quilt4Net.Core.DataTransfer;

namespace Quilt4.Service.Converters
{
    internal static class IssueResponseConverter
    {
        public static IssueResponse ToIssueResponse(this RegisterIssueResponseEntity response, string webUrl)
        {
            return new IssueResponse
            {
                Ticket = response.Ticket.ToString(),
                IssueKey = response.IssueKey,
                ServerTime = response.ServerTime,
                IssueTypeUrl = webUrl + "IssueType/" + response.Ticket,
                IssueUrl = webUrl + "Issue/" + response.IssueKey,
            };
        }
    }
}
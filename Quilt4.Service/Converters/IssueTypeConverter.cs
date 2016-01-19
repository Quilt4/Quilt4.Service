using Quilt4.Service.Entity;
using Quilt4Net.Core.DataTransfer;

namespace Quilt4.Service.Converters
{
    public static class IssueTypeConverter
    {
        public static IssueTypeResponse ToIssueTypeResponse(this IssueType x)
        {
            return new IssueTypeResponse { Ticket = x.Ticket, CreationServerTime = x.CreationServerTime, IssueTypeKey = x.IssueTypeKey, VersionKey = x.VersionKey, Message = x.Message, Type = x.Type, Level = x.Level, StackTrace = x.StackTrace };
        }
    }
}
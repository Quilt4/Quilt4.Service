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
                VersionKey = item.VersionKey.ToString(),
                ProjectKey = item.ProjectKey.ToString(),
                ApplicationKey = item.ApplicationKey.ToString(),
                VersionNumber = item.VersionNumber,
                ProjectName = item.ProjectName,
                ApplicationName = item.ApplicationName,
                IssueTypes = item.IssueTypes.ToVersionPageIssueTypeResponses(),
                Sessions = item.Sessions.ToVersionPageSessionResponses(),
            };
        }

        public static IEnumerable<VersionPageIssueTypeResponse> ToVersionPageIssueTypeResponses(this IEnumerable<IssueTypeDetail> items)
        {
            return items?.Select(x => x.ToVersionPageIssueTypeResponse());
        }

        public static VersionPageIssueTypeResponse ToVersionPageIssueTypeResponse(this IssueTypeDetail item)
        {
            if (item == null)
                return null;

            return new VersionPageIssueTypeResponse
            {
                IssueTypeKey = item.IssueTypeKey.ToString(),
                Ticket = item.Ticket,
                Type = item.Type,
                IssueCount = item.IssueCount,
                Level = item.Level,
                LastIssue = item.LastIssue,
                Enviroments = item.Enviroments,
                Message = item.Message
            };
        }

        public static IEnumerable<VersionPageSessionResponse> ToVersionPageSessionResponses(this IEnumerable<SessionDetail> items)
        {
            return items?.Select(x => x.ToVersionPageSessionResponse());
        }

        public static VersionPageSessionResponse ToVersionPageSessionResponse(this SessionDetail item)
        {
            if (item == null)
                return null;

            return new VersionPageSessionResponse
            {
                SessionKey = item.SessionKey,
                UserName = item.UserName,
                ServerStartTime = item.StartServerTime,
                LastUsedServerTime = item.LastUsedServerTime,
                EndServerTime = item.EndServerTime,
                CallerIp = item.CallerIp,
                Environment = item.Environment,
                MachineName = item.MachineName
            };
        }
    }
}
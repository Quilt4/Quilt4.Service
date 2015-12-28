using System.Collections.Generic;
using System.Linq;

namespace Quilt4.Service.SqlRepository.Extensions
{
    public static class VersionPageExtensions
    {
        public static Entity.VersionPageVersion ToVersionPageVersion(this VersionPageVersion item,
            IQueryable<VersionPageIssueType> versionPageIssueTypes)
        {
            if (item == null)
                return null;

            return new Entity.VersionPageVersion
            {
                Id = item.VersionKey,
                ProjectId = item.ProjectKey,
                ApplicationId = item.ApplicationKey,
                Version = item.VersionName,
                ProjectName = item.ProjectName,
                ApplicationName = item.ApplicationName,
                IssueTypes = versionPageIssueTypes.ToVersionPageIssueTypes().ToArray()
            };
        }

        public static IEnumerable<Entity.VersionPageIssueType> ToVersionPageIssueTypes(
            this IQueryable<VersionPageIssueType> items)
        {
            return items?.Select(x => x.ToVersionPageIssueType());
        }

        public static Entity.VersionPageIssueType ToVersionPageIssueType(this VersionPageIssueType item)
        {
            if (item == null)
                return null;

            return new Entity.VersionPageIssueType
            {
                Id = item.IssueTypeKey,
                Ticket = item.Ticket,
                Type = item.Type,
                Issues = item.IssueCount,
                Level = item.Level,
                LastIssue = item.LastIssueServerTime,
                Enviroments = item.Enviroments.Split(';'),
                Message = item.Message
            };
        }
    }
}
using System.Collections.Generic;
using System.Linq;

namespace Quilt4.Service.Repository.SqlRepository.Extensions
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
                Id = item.Id,
                ProjectId = item.ProjectId,
                ApplicationId = item.ApplicaitonId,
                Version = item.Version,
                ProjectName = item.ProjectName,
                ApplicationName = item.ApplicationName,
                IssueTypes = versionPageIssueTypes.ToVersionPageIssueTypes().ToArray()
            };
        }

        public static IEnumerable<Entity.VersionPageIssueType> ToVersionPageIssueTypes(
            this IEnumerable<VersionPageIssueType> items)
        {
            return items?.Select(x => x.ToVersionPageIssueType());
        }

        public static Entity.VersionPageIssueType ToVersionPageIssueType(this VersionPageIssueType item)
        {
            if (item == null)
                return null;

            return new Entity.VersionPageIssueType
            {
                Id = item.Id,
                Ticket = item.Ticket,
                Type = item.Type,
                Issues = item.Issues,
                Level = item.Level,
                LastIssue = item.LastIssue,
                Enviroments = item.Enviroments.Split(';')
            };
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using Quilt4.Service.DataTransfer;

namespace Quilt4.Service.Converters.Version
{
    public static class VersionConverter
    {
        public static IEnumerable<VersionResponse> ToVersionResponses(this IEnumerable<Entity.Version> items)
        {
            return items?.Select(x => x.ToVersionResponse());
        }

        public static VersionResponse ToVersionResponse(this Entity.Version item)
        {
            if (item == null)
                return null;

            return new VersionResponse
            {
                Id = item.Id.ToString(),
                ProjectId = item.ProjectId.ToString(),
                ApplicationId = item.ApplicationId.ToString(),
                Name = item.Name,
                Sessions = item.Sessions,
                IssueTypes = item.IssueTypes,
                Issues = item.Issues,
                Enviroments = item.Enviroments,
                Last = item.Last
            };
        }
    }
}
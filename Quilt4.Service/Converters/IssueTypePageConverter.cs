//using System.Collections.Generic;
//using System.Linq;
//using Quilt4.Service.Controllers.WebAPI.Web.DataTransfer;
//using Quilt4.Service.Entity;

//namespace Quilt4.Service.Converters
//{
//    public static class IssueTypePageConverter
//    {
//        public static IssueTypePageIssueTypeResponse ToIssueTypePageIssueTypeResponse(this IssueTypePageIssueType item)
//        {
//            if (item == null)
//                return null;

//            return new IssueTypePageIssueTypeResponse
//            {
//                IssueTypeKey = item.IssueTypeKey.ToString(),
//                ProjectKey = item.ProjectId.ToString(),
//                ApplicationKey = item.ApplicationId.ToString(),
//                VersionKey = item.VersionId.ToString(),
//                ProjectName = item.ProjectName,
//                ApplicationName = item.ApplicationName,
//                VersionNumber = item.Version,
//                Ticket = item.Ticket,
//                Type = item.Type,
//                Level = item.Level,
//                Message = item.Message,
//                StackTrace = item.StackTrace,
//                Issues = item.Issues.ToIssueTypePageIssueResponses().ToArray(),
//                InnerIssueType = new List<InnerIssueTypeResponse> { new InnerIssueTypeResponse { Message = "A" }, new InnerIssueTypeResponse { Message = "B" } }                
//            };
//        }

//        public static IEnumerable<IssueTypePageIssueResponse> ToIssueTypePageIssueResponses(this IEnumerable<IssueTypePageIssue> items)
//        {
//            return items?.Select(x => x.ToIssueTypePageIssueResponse());
//        }

//        public static IssueTypePageIssueResponse ToIssueTypePageIssueResponse(this IssueTypePageIssue item)
//        {
//            if (item == null)
//                return null;

//            return new IssueTypePageIssueResponse
//            {
//                IssueKey = item.IssueKey.ToString(),
//                CreationServerTime = item.CreationServerTime,
//                UserName = item.UserName,
//                Enviroment = item.Enviroment,
//                Data = item.Data,
//                IssueThreadKeys = item.IssueThreadKeys
//            };
//        }
//    }
//}
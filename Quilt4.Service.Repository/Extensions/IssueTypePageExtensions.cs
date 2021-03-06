﻿//using System.Collections.Generic;
//using System.Linq;
//using Newtonsoft.Json;

//namespace Quilt4.Service.SqlRepository.Extensions
//{
//    public static class IssueTypePageExtensions
//    {
//        public static Entity.IssueTypePageIssueType ToIssueTypePageIssueType(this IssueTypePageIssueType item,
//            IQueryable<IssueTypePageIssue> issueTypePageIssues)
//        {
//            if (item == null)
//                return null;

//            return new Entity.IssueTypePageIssueType
//            {
//                Id = item.IssueTypeKey,
//                ProjectId = item.ProjectKey,
//                ApplicationId = item.ApplicationKey,
//                VersionId = item.VersionKey,
//                ProjectName = item.ProjectName,
//                ApplicationName = item.ApplicationName,
//                Version = item.VersionNumber,
//                Ticket = item.Ticket,
//                Type = item.Type,
//                Level = item.Level,
//                Message = item.Message,
//                StackTrace = item.StackTrace,
//                Issues = issueTypePageIssues.ToIssueTypePageIssues().ToArray()
//            };
//        }

//        public static IEnumerable<Entity.IssueTypePageIssue> ToIssueTypePageIssues(
//            this IQueryable<IssueTypePageIssue> items)
//        {
//            return items?.Select(x => x.ToIssueTypePageIssue());
//        }

//        public static Entity.IssueTypePageIssue ToIssueTypePageIssue(this IssueTypePageIssue item)
//        {
//            if (item == null)
//                return null;

//            return new Entity.IssueTypePageIssue
//            {
//                Id = item.IssueTypeKey,
//                Time = item.LastUpdateServerTime,
//                User = item.ApplicationUserName,
//                Enviroment = item.Enviroment,
//                Data = JsonConvert.DeserializeObject<IDictionary<string, string>>(item.Data)
//            };
//        }
//    }
//}
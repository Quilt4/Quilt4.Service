//using System;
//using System.Collections.Generic;
//using System.Configuration;
//using System.Linq;
//using Quilt4.Service.Entity;
//using Quilt4.Service.Interface.Repository;
//using Quilt4.Service.SqlRepository.Converters;

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Quilt4.Service.Entity;
using Quilt4.Service.Interface.Business;
using Quilt4.Service.Interface.Repository;

namespace Quilt4.Service.SqlRepository
{
    public class SqlReadRepository : IReadRepository
    {
        //        public ProjectPageProject GetProject(Guid projectKey)
        //        {
        //            using (var context = GetDataContext())
        //            {
        //                var response = context.Projects.SingleOrDefault(x => x.ProjectKey == projectKey).ToProjectPageProject();
        //                return response;
        //            }
        //        }

        //        private Quilt4DataContext GetDataContext()
        //        {
        //            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        //            return new Quilt4DataContext(connectionString);
        //        }

        //        public IEnumerable<ProjectPageVersion> GetVersions(Guid applicationKey)
        //        {
        //            using (var context = GetDataContext())
        //            {
        //                return context.Versions
        //                    .Where(x => x.Application.ApplicationKey == applicationKey)
        //                    .Select(x => new ProjectPageVersion
        //                    {
        //                        VersionKey = x.VersionKey,
        //                        VersionNumber = x.VersionNumber,
        //                        ApplicationKey = x.Application.ApplicationKey,
        //                        Enviroments = x.Application.Versions.SelectMany(y => y.Sessions.Where(z => z.Version.VersionKey == x.VersionKey)).Select(y => y.Enviroment).Distinct().ToArray(),
        //                        IssueCount = x.Application.Versions.SelectMany(y => y.Sessions.Where(z => z.Version.VersionKey == x.VersionKey)).SelectMany(y => y.Issues).Count(),
        //                        IssueTypeCount = x.IssueTypes.Count,
        //                        SessionCount = x.Application.Versions.SelectMany(y => y.Sessions.Where(z => z.Version.VersionKey == x.VersionKey)).Count(),
        //                        ProjectKey = x.Application.Project.ProjectKey,
        //                        LastIssue = x.IssueTypes.SelectMany(z => z.Issues).Select(z => z.CreationServerTime).Max(),
        //                        LastSession = x.Sessions.Where(z => z.Version.VersionKey == x.VersionKey).Max(y => y.LastUsedServerTime),
        //                        FirstSession = x.Sessions.Where(z => z.Version.VersionKey == x.VersionKey).Min(y => y.LastUsedServerTime),
        //                    }).ToArray();
        //            }
        //        }

        //        public VersionDetail GetVersion(Guid versionKey)
        //        {
        //            using (var context = GetDataContext())
        //            {
        //                var versionPageIssueTypes = context.Versions.Where(x => x.VersionKey == versionKey)
        //                    .Select(x => new VersionDetail
        //                    {
        //                        ApplicationKey = x.Application.ApplicationKey,
        //                        VersionNumber = x.VersionNumber,
        //                        VersionKey = x.VersionKey,
        //                        ProjectKey = x.Application.Project.ProjectKey,
        //                        ApplicationName = x.Application.Name,
        //                        ProjectName = x.Application.Project.Name,
        //                        IssueTypes = x.IssueTypes.Select(y => new Entity.IssueTypeDetail
        //                        {
        //                            IssueTypeKey = y.IssueTypeKey,
        //                            Level = y.Level,
        //                            Message = y.IssueTypeDetail.Message,
        //                            IssueCount = y.Issues.Count,
        //                            Ticket = y.Ticket,
        //                            LastIssue = y.Issues.Select(z => z.CreationServerTime).Max(),
        //                            Type = y.IssueTypeDetail.Type,
        //                            Enviroments = context.Sessions.Where(z => z.Issues.Any(z1 => z1.IssueType.IssueTypeKey == y.IssueTypeKey)).Select(y2 => y2.Enviroment).Distinct()
        //                        }).ToArray(),
        //                        Sessions = x.Sessions.Select(z => new SessionDetail
        //                        {
        //                            SessionKey = z.SessionKey,
        //                            StartClientTime = z.StartClientTime,
        //                            StartServerTime = z.StartServerTime,
        //                            LastUsedServerTime = z.LastUsedServerTime,
        //                            EndServerTime = z.EndServerTime,
        //                            MachineName = z.Machine.Name,
        //                            UserName = z.ApplicationUser.UserName,
        //                            Environment = z.Enviroment,
        //                            CallerIp = z.CallerIp,
        //                        }).ToArray()
        //                    }).FirstOrDefault();
        //                return versionPageIssueTypes;
        //            }
        //        }

        //        public IssueTypePageIssueType GetIssueType(Guid issueTypeKey)
        //        {
        //            using (var context = GetDataContext())
        //            {
        //                var issueType = context.IssueTypes.Single(x => x.IssueTypeKey == issueTypeKey);
        //                return issueType.ToIssueTypePageIssueType(issueType.IssueTypeDetail);
        //            }
        //        }

        //        public IEnumerable<Entity.DashboardPageProject> GetDashboardProjects(string userName)
        //        {
        //            using (var context = GetDataContext())
        //            {
        //                var response = context.Projects.Where(x => x.ProjectUsers.Any(y => y.User.UserName.ToLower() == userName.ToLower())).Select(x => new DashboardPageProject
        //                {
        //                    ProjectKey = x.ProjectKey,
        //                    Name = x.Name,
        //                    DashboardColor = x.DashboardColor,
        //                    Applications = x.Applications.Count(),
        //                    Versions = x.Applications.SelectMany(y => y.Versions).Count(),
        //                    IssueTypes = x.Applications.SelectMany(y => y.Versions).SelectMany(y => y.IssueTypes).Count(),
        //                    Issues = x.Applications.SelectMany(y => y.Versions).SelectMany(y => y.IssueTypes).SelectMany(y => y.Issues).Count(),
        //                    Sessions = x.Applications.SelectMany(y => y.Versions).SelectMany(y => y.Sessions).Count(),
        //                }).ToArray();

        //                return response;
        //            }
        //        }

        //        public IEnumerable<string> GetProjectUsers(Guid projectKey)
        //        {
        //            using (var context = GetDataContext())
        //            {
        //                return context.ProjectUsers.Where(x => x.Project.ProjectKey == projectKey).Select(x => x.User.UserName).ToArray();
        //            }
        //        }

        private static ReadDataContext GetDataContext()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            return new ReadDataContext(connectionString);
        }

        public void AddIssue(IIssueRead issueRead)
        {
            using (var context = GetDataContext())
            {
                context.IssueReads.InsertOnSubmit(new IssueRead
                {
                    Ticket = issueRead.Ticket,
                    StackTrace = issueRead.StackTrace,
                    IssueKey = issueRead.IssueKey,
                    Type = issueRead.Type,
                    VersionNumber = issueRead.VersionNumber,
                    Message = issueRead.Message,
                    ApplicationName = issueRead.ApplicationName,
                    Level = issueRead.Level,
                    ProjectName = issueRead.ProjectName,
                });
                context.SubmitChanges();
            }
        }

        public IIssueRead GetIssue(Guid issueKey)
        {
            using (var context = GetDataContext())
            {
                var result = context.IssueReads.Single(x => x.IssueKey == issueKey);
                var issueRead = new Quilt4.Service.Entity.IssueRead
                {
                    Type = result.Type,
                    IssueKey = result.IssueKey,
                    Message = result.Message,
                    VersionNumber = result.VersionNumber,
                    Ticket = result.Ticket,
                    StackTrace = result.StackTrace,
                    Level = result.Level,
                    ProjectName = result.ProjectName,
                    ApplicationName = result.ApplicationName,
                    //MachineName = result.MachineName,
                };
                return issueRead;
            }
        }
    }
}